using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ISTL.FP
{
    public class CrossmatchEngine : IFpEngine
    {
        IFpControl fpcontrol;
        public Object obj;
        int state = 0;

        public CrossmatchEngine(IFpControl fpcontrol)
        {
            try
            {
                this.fpcontrol = fpcontrol;
                int deviceCount = -1;
                int output = -1;
                deviceCount = CrossmatchDll.getDeviceCount(0);
                output = CrossmatchDll.initializeDevice(deviceCount);
                if (output != 0 && output != 603 && output != 601)
                {
                    this.fpcontrol.SetMessage("Device not integrated.");
                    state = -1;
                }
                else if (output == 603 || output == 601)
                {
                    this.fpcontrol.SetMessage("Put finger into device, please ...");
                    state = 0;
                }
                else
                {
                    this.fpcontrol.SetMessage("Put finger into device, please ...");
                    state = 0;
                }


            }
            catch (Exception e)
            {
                throw e;

            }
        }

        public delegate void Result_Callback_Delegate(int status, int width, int height, double resolutionX, double resolutionY, int pitch, byte bitsperpixel, PixelFormat format, IntPtr Buffer, bool isFinal);

        public delegate void LScan_Callback_Delegate(int devicehandle, object pcontext);

        LScan_Callback_Delegate dl2;
        Result_Callback_Delegate dl1;

        /*protected virtual void Dispose(bool disposing)
        {
        }*/
        /*public void Disposes()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }*/
        public static unsafe Bitmap ToGrayscale(Bitmap colorBitmap)
        {
            int Width = colorBitmap.Width;
            int Height = colorBitmap.Height;

            Bitmap grayscaleBitmap = new Bitmap(Width, Height, PixelFormat.Format8bppIndexed);

            grayscaleBitmap.SetResolution(colorBitmap.HorizontalResolution,
                                 colorBitmap.VerticalResolution);

            ColorPalette colorPalette = grayscaleBitmap.Palette;
            for (int i = 0; i < colorPalette.Entries.Length; i++)
            {
                colorPalette.Entries[i] = Color.FromArgb(i, i, i);
            }
            grayscaleBitmap.Palette = colorPalette;
            BitmapData bitmapData = grayscaleBitmap.LockBits(
                new Rectangle(Point.Empty, grayscaleBitmap.Size),
                ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

            Byte* pPixel = (Byte*)bitmapData.Scan0;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Color clr = colorBitmap.GetPixel(x, y);

                    Byte byPixel = (byte)((30 * clr.R + 59 * clr.G + 11 * clr.B) / 100);

                    pPixel[x] = byPixel;
                }

                pPixel += bitmapData.Stride;
            }

            grayscaleBitmap.UnlockBits(bitmapData);

            return grayscaleBitmap;
        }


        /*public delegate void Result_ResultCB_Delegate(int Status);*/

        public void LScan_Callback(int devicehandle, object pcontext) { }
        /*public void Result_Resultcb(int Status){}*/
        public void Result_Callback(int status, int width, int height, double resolutionX, double resolutionY, int pitch, byte bitsperpixel, PixelFormat format, IntPtr Buffer, bool isFinal)
        {
            if (status == 0)
            {
                /*Disposes();*/
                Bitmap newBitmap = new Bitmap(width, height, 800, PixelFormat.Format8bppIndexed, Buffer);
                Bitmap image1 = new Bitmap(newBitmap);
                newBitmap.Dispose();
                newBitmap = null;
                try
                {

                    Bitmap imagegray = ToGrayscale(image1);
                    imagegray.RotateFlip(RotateFlipType.Rotate180FlipX);
                    this.fpcontrol.OnGetImage(imagegray);
                    completefp();

                }
                catch (ArgumentException)
                {
                    MessageBox.Show("There was an error." + "Image Grayscale");
                }
            }
            else
            {

                this.fpcontrol.SetMessage("Device not integrated.");
                CloseDevice();

            }

        }
        public void startCapture()
        {
            try
            {
                /*Disposes();*/
                int output = 0;
                dl2 = new LScan_Callback_Delegate(LScan_Callback);
                CrossmatchDll.communicationBreakCallback(output, Marshal.GetFunctionPointerForDelegate(dl2));

                dl1 = new Result_Callback_Delegate(Result_Callback);
                CrossmatchDll.registerCallbackResultImage(0, Marshal.GetFunctionPointerForDelegate(dl1));

                CrossmatchDll.setCaptureMode(output);
                if (state == 0)
                {
                    this.fpcontrol.SetMessage("Put finger into device, please...");
                }
                else
                {
                    this.fpcontrol.SetMessage("Device not integrated.");
                }
                /*Disposes();*/
                CrossmatchDll.startCapture(0, ref obj, ref obj);

            }
            catch
            {
                CrossmatchDll.releaseDevice(0);
            }
            finally
            {

            }
        }
        private void completefp()
        {
            this.fpcontrol.SetMessage("Enrollment process finished successfully.");
            this.fpcontrol.OnComplete(true);

        }
        override public bool OpenDevice()
        {
            try
            {
                startCapture();
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        override public void CloseDevice()
        {
            CrossmatchDll.releaseDevice(0);
        }
    }
}
