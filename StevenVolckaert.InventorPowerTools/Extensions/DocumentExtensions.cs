namespace StevenVolckaert.InventorPowerTools
{
    using System;
    using Inventor;

    /// <summary>
    ///     Provides extension methods for <see cref="Document"/> instances.
    /// </summary>
    internal static class DocumentExtensions
    {
        /// <summary>
        ///     Returns the path of the document's directory.
        /// </summary>
        /// <param name="document">The <see cref="Document"/> instance that this extension method affects.</param>
        /// <returns>
        ///     The path of the document's directory, or <c>null</c> if the document isn't saved to disk.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="document"/> is <c>null</c>.</exception>
        public static string DirectoryPath(this Document document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            return document.FullFileName.IsNullOrWhiteSpace()
                ? null
                : System.IO.Path.GetDirectoryName(document.FullFileName);
        }

        /// <summary>
        ///     Saves the drawing document as a PDF file.
        /// </summary>
        /// <param name="document">The <see cref="Document"/> instance that this extension method affects.</param>
        /// <param name="filePath">The path of the PDF file to be created.</param>
        /// <returns>A value that indicates whether the operation was successful.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="document"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="filePath"/> is <c>null</c>, empty, or white space.
        /// </exception>
        public static bool SaveAsPDF(this Document document, string filePath)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            if (filePath.IsNullOrWhiteSpace())
                throw new ArgumentException(Resources.ValueNullEmptyOrWhiteSpace, nameof(filePath));

            try
            {
                var inventor = (Inventor.Application)document.Parent;
                var PDFAddIn = (TranslatorAddIn)inventor.ApplicationAddIns
                    .ItemById["{0AC6FD96-2F4D-42CE-8BE0-8AEA580399E4}"];

                var translationContext = inventor.TransientObjects.CreateTranslationContext();
                translationContext.Type = IOMechanismEnum.kFileBrowseIOMechanism;

                var options = inventor.TransientObjects.CreateNameValueMap();
                var targetData = inventor.TransientObjects.CreateDataMedium();

                if (PDFAddIn.HasSaveCopyAsOptions[document, translationContext, options])
                {
                    options.Value["All_Color_AS_Black"] = 0;
                    options.Value["Sheet_Range"] = PrintRangeEnum.kPrintAllSheets;
                    //options.Value["Remove_Line_Weights"] = 0;
                    //options.Value["Vector_Resolution"] = 400;
                    //options.Value["Custom_Begin_Sheet"] = 2;
                    //options.Value["Custom_End_Sheet"] = 4;
                }

                targetData.FileName = filePath;
                PDFAddIn.SaveCopyAs(document, translationContext, options, targetData);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
