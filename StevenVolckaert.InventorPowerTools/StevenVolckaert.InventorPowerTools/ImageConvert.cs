using System;
using System.Drawing;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms;
using Inventor;

namespace StevenVolckaert.InventorPowerTools
{
    /// <summary>
    /// Converts various image formats to Inventor.IPictureDisp objects and vice-versa.
    /// <para>This class cannot be inherited.</para>
    /// </summary>
    [PermissionSet(SecurityAction.InheritanceDemand)]
    internal sealed class ImageConvert : AxHost
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageConvert"/> class.
        /// </summary>
        private ImageConvert()
            : base(String.Empty)
        {
        }

        /// <summary>
        /// Converts a System.Drawing.Icon instance to the corresponding OLE IPictureDisp object.
        /// </summary>
        /// <param name="icon">The System.Drawing.Icon instance to convert.</param>
        /// <returns>A Inventor.IPictureDisp representing the OLE IPictureDisp object.</returns>
        public static IPictureDisp FromIconToIPictureDisp(Icon icon)
        {
            return FromImageToIPictureDisp(icon.ToBitmap());
        }

        /// <summary>
        /// Converts a System.Drawing.Image instance to the corresponding OLE IPictureDisp object.
        /// </summary>
        /// <param name="image">The System.Drawing.Image instance to convert.</param>
        /// <returns>A Inventor.IPictureDisp representing the OLE IPictureDisp object.</returns>
        public static IPictureDisp FromImageToIPictureDisp(Image image)
        {
            return (IPictureDisp)GetIPictureDispFromPicture(image);
        }

        ///// <summary>
        ///// Converts a OLE IPictureDisp instance to the corresponding System.Drawing.Icon object.
        ///// </summary>
        ///// <param name="picture">The OLE IPictureDisp instance.</param>
        ///// <returns>The corresponding System.Drawing.Icon instance.</returns>
        //public static Icon IPictureDispToIcon(IPictureDisp picture)
        //{
        //    using (var bitmap = new Bitmap(IPictureDispToImage(picture)))
        //    {
        //        return Icon.FromHandle(bitmap.GetHicon());
        //    }
        //}

        ///// <summary>
        ///// Converts a OLE IPictureDisp instance to the corresponding System.Drawing.Image object.
        ///// </summary>
        ///// <param name="picture">The OLE IPictureDisp instance.</param>
        ///// <returns>The corresponding System.Drawing.Image instance.</returns>
        //public static Image IPictureDispToImage(IPictureDisp picture)
        //{
        //    return GetPictureFromIPicture(picture);
        //}
    }
}
