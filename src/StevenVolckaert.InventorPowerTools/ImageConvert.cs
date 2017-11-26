namespace StevenVolckaert.InventorPowerTools
{
    using System.Drawing;
    using System.Security;
    using System.Security.Permissions;
    using System.Windows.Forms;
    using Inventor;

    /// <summary>
    /// Converts various image formats to <see cref="IPictureDisp"/> instances and vice-versa.
    /// <para>This class cannot be inherited.</para>
    /// </summary>
    [PermissionSet(SecurityAction.InheritanceDemand)]
    internal sealed class ImageConvert : AxHost
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageConvert"/> class.
        /// </summary>
        private ImageConvert()
            : base(string.Empty)
        {
        }

        /// <summary>
        /// Converts a <see cref="Icon"/> instance to an equivalent <see cref="IPictureDisp"/> instance.
        /// </summary>
        /// <param name="icon">The <see cref="Icon"/> instance to convert.</param>
        /// <returns>A <see cref="IPictureDisp"/> instance equivalent to <paramref name="icon"/>.</returns>
        public static IPictureDisp FromIconToIPictureDisp(Icon icon)
        {
            return FromImageToIPictureDisp(icon.ToBitmap());
        }

        /// <summary>
        /// Converts a <see cref="Image"/> instance to an equivalent <see cref="IPictureDisp"/> instance.
        /// </summary>
        /// <param name="image">The <see cref="Image"/> instance to convert.</param>
        /// <returns>A <see cref="IPictureDisp"/> instance equivalent to <paramref name="image"/>.</returns>
        public static IPictureDisp FromImageToIPictureDisp(Image image)
        {
            return (IPictureDisp)GetIPictureDispFromPicture(image);
        }

        /// <summary>
        /// Converts a <see cref="IPictureDisp"/> instance to an equivalent <see cref="Icon"/>  instance.
        /// </summary>
        /// <param name="pictureDisp">The <see cref="IPictureDisp"/> instance to convert.</param>
        /// <returns>A <see cref="Icon"/> instance equivalent to <paramref name="pictureDisp"/>.</returns>
        public static Icon IPictureDispToIcon(IPictureDisp pictureDisp)
        {
            using (var bitmap = new Bitmap(IPictureDispToImage(pictureDisp)))
            {
                return Icon.FromHandle(bitmap.GetHicon());
            }
        }

        /// <summary>
        /// Converts a <see cref="IPictureDisp"/> instance to an equivalent <see cref="Image"/> instance.
        /// </summary>
        /// <param name="pictureDisp">The <see cref="IPictureDisp"/> instance.</param>
        /// <returns>A <see cref="Image"/> instance equivalent to <paramref name="pictureDisp"/>.</returns>
        public static Image IPictureDispToImage(IPictureDisp pictureDisp)
        {
            return GetPictureFromIPicture(pictureDisp);
        }
    }
}
