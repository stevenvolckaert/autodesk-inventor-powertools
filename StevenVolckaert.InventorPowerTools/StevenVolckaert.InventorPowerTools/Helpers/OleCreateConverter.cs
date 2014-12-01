using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace StevenVolckaert.InventorPowerTools.Helpers
{
    internal class OleCreateConverter
    {
        [DllImport("oleaut32.dll", EntryPoint = "OleCreatePictureIndirect", CharSet = CharSet.Ansi, ExactSpelling = true, PreserveSig = true)]
        private static extern int OleCreatePictureIndirect([In] PictDescBitmap pictdesc, ref Guid iid, bool fOwn, [MarshalAs(UnmanagedType.Interface)] out object ppVoid);

        const short _PictureTypeBitmap = 1;
        [StructLayout(LayoutKind.Sequential)]
        internal class PictDescBitmap
        {
            internal int cbSizeOfStruct = Marshal.SizeOf(typeof(PictDescBitmap));
            internal int pictureType = _PictureTypeBitmap;
            internal IntPtr hBitmap = IntPtr.Zero;
            internal IntPtr hPalette = IntPtr.Zero;
            internal int unused = 0;

            internal PictDescBitmap(Bitmap bitmap)
            {
                this.hBitmap = bitmap.GetHbitmap();
            }
        }

        public static stdole.IPictureDisp ImageToPictureDisp(Image image)
        {
            if (image == null || !(image is Bitmap))
                return null;

            PictDescBitmap pictDescBitmap = new PictDescBitmap((Bitmap)image);
            object ppVoid = null;
            Guid iPictureDispGuid = typeof(stdole.IPictureDisp).GUID;
            OleCreatePictureIndirect(pictDescBitmap, ref iPictureDispGuid, true, out ppVoid);
            stdole.IPictureDisp picture = (stdole.IPictureDisp)ppVoid;
            return picture;
        }

        public static Image PictureDispToImage(stdole.IPictureDisp pictureDisp)
        {
            if (pictureDisp == null || pictureDisp.Type != _PictureTypeBitmap)
                return null;

            var paletteHandle = new IntPtr(pictureDisp.hPal);
            var bitmapHandle = new IntPtr(pictureDisp.Handle);
            return Image.FromHbitmap(bitmapHandle, paletteHandle);
        }
    }
}
