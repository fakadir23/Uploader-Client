using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace ISTL.IRIS
{
    public partial class ImageCtrl : UserControl
    {

        private Bitmap _bitmap;
        private float _imageDpi = 500.0f;
        private float _panX;
        private float _panY;
        private float _imageScale = 1.0f;        
        private float _scale = 1.0f;
        private string _captionUL;
        public ImageCtrl()
        {
            InitializeComponent();
        }

        public void ClearImage()
        {
            if (_bitmap != null)
            {
                _bitmap.Dispose();
                _bitmap = null;
            }
        }

        public Bitmap Bitmap
        {
            get { return _bitmap; }
            set { _bitmap = value; Invalidate(); }
        }

        public Bitmap LoadImage(int iHll, int iVll, int iDpi, ref byte[] data)
        {
            try
            {
                Bitmap bitmap = new Bitmap(iHll, iVll, PixelFormat.Format8bppIndexed);
                Rectangle rect = new Rectangle(0, 0, iHll, iVll);
                BitmapData bmData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
                //if (bmData.Stride == iHll)
                //    Marshal.Copy( data, 0, bmData.Scan0, data.Length );
                //else
                {   // Stride and image width not same, do hard way!
                    int x, y;
                    byte[] row = new byte[bmData.Stride];
                    for (x = 0; x < row.Length; x++)
                        row[x] = 0;

                    for (y = 0; y < iVll; y++)
                    {
                        int from = (y) * iHll;
                        for (x = 0; x < iHll; x++)
                            row[x] = data[from++];

                        IntPtr toPtr = new IntPtr((int)bmData.Scan0 + y * bmData.Stride);
                        Marshal.Copy(row, 0, toPtr, bmData.Stride);
                    }
                }

                bitmap.UnlockBits(bmData);

                bitmap.SetResolution(iDpi, iDpi);
                bitmap.Palette = GrayscalePalette();

                ClearImage();
                this.Bitmap = bitmap;
                this.ImageDpi = iDpi;

                CenterAndFit(true);

                return bitmap;

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ImageCtrl.LoadImage");
            }
            return null;
        }

        public void CenterAndFit(bool another)
        {
            CenterAndFitDelegate cfd = new CenterAndFitDelegate(CenterAndFit);
            this.Invoke(cfd);
        }

        delegate void CenterAndFitDelegate();

        public void CenterAndFit()
        {	// Note, the image DPI must be correct for this to work

            if (_bitmap == null)
                return;

            Graphics gr = Graphics.FromHwnd(this.Handle);
            float dpi = gr.DpiX;
            _scale = (dpi / _imageDpi) * _imageScale;

            float scaleH = (float)Width / _bitmap.Width;
            float scaleV = (float)Height / _bitmap.Height;
            _imageScale = Math.Min(scaleH, scaleV);

            _panX = 0.0f;
            _panY = 0.0f;

            if (_imageScale == scaleH)
            {
                float heightPx = _bitmap.Height * _imageScale;
                _panY = (((float)Height) - heightPx) / 2.0f;
            }
            else
            {
                float widthPx = _bitmap.Width * _imageScale;
                _panX = (((float)Width) - widthPx) / 2.0f;
            }

            Invalidate();

        }

        private static ColorPalette GrayscalePalette()
        {	// A palette cannot be directly created. Make a bitmap and "steal" its palette.
            try
            {
                Bitmap bitmap = new Bitmap(1, 1, PixelFormat.Format8bppIndexed);
                ColorPalette palette = bitmap.Palette;
                bitmap.Dispose();
                for (int i = 0; i < 256; i++)
                    palette.Entries[i] = Color.FromArgb(255, i, i, i);
                return palette;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "PluginBase.ColorPalette");
                return null;
            }
        }

        public float ImageDpi
        {
            get { return _imageDpi; }
            set { _imageDpi = value; Invalidate(); }
        }

        public string CaptionUL
        {
            get { return _captionUL; }
            set { _captionUL = value; Invalidate(); }
        }

    }
}
