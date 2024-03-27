using ISTL.COMMON;
using ISTL.FP;
using ISTL.MODELS.DTO.Fingerprint;
using ISTL.RAB.DbManager;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ISTL.RAB.View.New.Enrollment.BiometricInformation
{
    public partial class FingerprintCaptureDialogForm : Form, IFpControl
    {
        private Bitmap[] fpArray = new Bitmap[10];
        private object _locker = new object();

        private int currentIndex = 0;

        private bool m_bExit;

        private IFpEngine fpEngine = null;

        public FingerprintData FingerData { get; set; }

        String deviceName = "";

        public FingerprintCaptureDialogForm()
        {
            Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
            InitializeComponent();
            reset();
        }

        // Ref: http://msdn.microsoft.com/en-us/library/windows/desktop/aa363480(v=vs.85).aspx
        private const int WM_DEVICECHANGE = 0x0219; // device change event       

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_DEVICECHANGE)
            {
                OpenDevice();
            }
        }

        private void OpenDevice()
        {
            if (fpEngine != null)
            {
                fpEngine.OpenDevice();
                //this.SetMessage("");
            }
        }


        public FingerprintCaptureDialogForm(String deviceName) : this()
        {
            this.deviceName = deviceName;
        }

        delegate void UpdateFingerCallback(PictureBox p, bool isVisible);

        private void updateFinger()
        {
            for (int i = 0; i < 10; i++)
            {
                Control[] controls = this.Controls.Find("pictureBox" + i, true);
                if (controls != null && controls.Length > 0)
                {
                    PictureBox p = (PictureBox)controls[0];
                    if (p.InvokeRequired)
                    {
                        UpdateFingerCallback cb = new UpdateFingerCallback(updateFingerImageDeligate);
                        this.Invoke(cb, new object[] { p, fpArray[i] == null });
                    }
                    else
                    {
                        if (fpArray[i] == null)
                        {
                            //p.Visible = false;
                        }
                        else
                        {
                            //p.Visible = true;
                            p.BackgroundImage = ISTL.RAB.Properties.Resources.r_thumb_orange;
                        }
                    }
                }
            }
        }

        delegate void UpdatePointerCallback(Panel p, bool isVisible);

        private void updatePointer(int currentIndex)
        {
            for (int i = 0; i < 10; i++)
            {
                Control[] controls = this.Controls.Find("panel" + i, true);
                if (controls != null && controls.Length > 0)
                {
                    Panel p = (Panel)controls[0];
                    if (p.InvokeRequired)
                    {
                        UpdatePointerCallback cb = new UpdatePointerCallback(updateFingerPointerDeligate);
                        this.Invoke(cb, new object[] { p, i == currentIndex });
                    }
                    else
                    {
                        if (i == currentIndex)
                        {
                            p.Visible = true;
                        }
                        else
                        {
                            p.Visible = false;
                        }
                    }
                }
            }
        }

        private void updateFingerPointerDeligate(Panel p, bool isVisible)
        {
            p.Visible = isVisible;
        }

        private void updateFingerImageDeligate(PictureBox p, bool isVisible)
        {
            //p.Visible = !isVisible;
            if (!isVisible)
            {
                Bitmap takenImage = ISTL.RAB.Properties.Resources.r_thumb_orange;
                switch (p.Name)
                {
                    case "pictureBox0":
                        takenImage = ISTL.RAB.Properties.Resources.r_thumb_orange;
                        break;
                    case "pictureBox1":
                        takenImage = ISTL.RAB.Properties.Resources.r_index_orange;
                        break;
                    case "pictureBox2":
                        takenImage = ISTL.RAB.Properties.Resources.r_middle_orange;
                        break;
                    case "pictureBox3":
                        takenImage = ISTL.RAB.Properties.Resources.r_ring_orange;
                        break;
                    case "pictureBox4":
                        takenImage = ISTL.RAB.Properties.Resources.r_small_orange;
                        break;

                    case "pictureBox5":
                        takenImage = ISTL.RAB.Properties.Resources.l_thumb_orange;
                        break;
                    case "pictureBox6":
                        takenImage = ISTL.RAB.Properties.Resources.l_index_orange;
                        break;
                    case "pictureBox7":
                        takenImage = ISTL.RAB.Properties.Resources.l_middle_orange;
                        break;
                    case "pictureBox8":
                        takenImage = ISTL.RAB.Properties.Resources.l_ring_orange;
                        break;
                    case "pictureBox9":
                        takenImage = ISTL.RAB.Properties.Resources.l_small_orange;
                        break;
                    default:
                        break;

                }
                p.BackgroundImage = takenImage;
            }
        }

        private void FingerprintCaptureDialogForm_Load(object sender, EventArgs e)
        {
            if (deviceName == null || deviceName.Length == 0)
            {
                DbDeviceManager dManager = new DbDeviceManager();
                try
                {
                    this.deviceName = dManager.GetDevice("FP").Name;
                }
                catch (Exception ex)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "No Fingerprint Capture Device Set");
                    this.Close();
                    return;
                }
            }
            if (deviceName.Contains("Futronic"))
            {
                try
                {
                    fpEngine = new FutronicEngine(this);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Please setup fingerprint driver first and try again.", "RAB CDMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please setup fingerprint driver first and try again.");
                }
            }
            if (deviceName.Contains("Cross Match"))
            {
                try
                {
                    fpEngine = new CrossmatchEngine(this);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Please setup fingerprint driver (CrossMatch) first and try again.", "RAB CDMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please setup fingerprint driver (CrossMatch) first and try again.");
                }
            }
            else
            {
                this.txtMessage.Text = "Device not integrated.";
            }
            if (fpEngine != null)
            {
                fpEngine.OpenDevice();
            }

        }


        delegate void SetTextCallback(string text);

        public void SetMessage(string text)
        {
            // Do not change the state control during application closing.
            if (m_bExit)
                return;

            if (this.txtMessage.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(this.SetMessage);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtMessage.Text = text;
                this.Update();
            }
        }
        delegate void SetImageCallback(Bitmap hBitmap);


        public void OnComplete(bool resp)
        {
            try
            {
                resolveFingerprint(currentIndex);
                //this.currentIndex++;

                /*currentIndex = GetCurrentIndex();*/
                currentIndex = FingerClick(currentIndex);

                if (currentIndex < 10)
                {

                    Invoke((MethodInvoker)delegate
                    {
                        txtMessage.Select(0, 0);
                        btnOk.Enabled = false;
                        btnReset.Enabled = false;
                        iconButton2.Enabled = false;
                        exitButton.Enabled = false;
                    });

                    System.Threading.Thread.Sleep(2000);

                    Invoke((MethodInvoker)delegate
                    {
                        txtMessage.Select(0, 0);
                        btnOk.Enabled = true;
                        btnReset.Enabled = true;
                        iconButton2.Enabled = true;
                        exitButton.Enabled = true;
                    });

                    fpEngine.OpenDevice();
                }

                if (currentIndex > 9)
                {
                    currentIndex = 0;
                }

                bool isCompleted = false;
                /*for (int i = 0; i < fpArray.Length; i++)
                {
                    if (fpArray[i] == null)
                    {
                        isCompleted = false;
                        break;
                    }
                }*/

                if (isCompleted)
                {
                    fpEngine.CloseDevice();
                    this.SetMessage("Fingerprint capture completed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an unexpected error when complete fp capture." + ex.ToString());
            }
        }

        private void FingerprintCaptureDialogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                m_bExit = true;
                if (fpEngine != null)
                {
                    fpEngine.CloseDevice();
                }
            }
            catch { }
        }

        private void resolveFingerprint(int currentIndex)
        {
            Control[] controls = this.Controls.Find("fpPB" + currentIndex, true);
            if (controls != null && controls.Length > 0)
            {
                PictureBox p = (PictureBox)controls[0];
                p.Image = fpPB.Image;
            }
            updateFinger();
            //if (currentIndex != 9)
            //{
            //    currentIndex++;
            //}

            /*currentIndex = GetCurrentIndex();*/

            currentIndex = FingerClick(currentIndex);

            updatePointer(currentIndex);
        }

        private Bitmap invertImage(Bitmap pic)
        {
            Bitmap newBmp = new Bitmap(pic, pic.Width, pic.Height);
            for (int y = 0; (y <= (newBmp.Height - 1)); y++)
            {
                for (int x = 0; (x <= (newBmp.Width - 1)); x++)
                {
                    Color inv = newBmp.GetPixel(x, y);
                    inv = Color.FromArgb(255, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                    newBmp.SetPixel(x, y, inv);
                }
            }
            return newBmp;
        }

        private void btnArrestInfo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void populateWsqData(Bitmap bitmap, int currentIndex)
        {
            if (currentIndex == 0)
            {
                FingerData.WsqRt = ImageToWsq(bitmap, currentIndex);
                FingerData.FpRt = Utils.ImageToByte(bitmap);
            }

            if (currentIndex == 1)
            {
                FingerData.WsqRi = ImageToWsq(bitmap, currentIndex);
                FingerData.FpRi = Utils.ImageToByte(bitmap);
            }

            if (currentIndex == 2)
            {
                FingerData.WsqRm = ImageToWsq(bitmap, currentIndex);
                FingerData.FpRm = Utils.ImageToByte(bitmap);
            }

            if (currentIndex == 3)
            {
                FingerData.WsqRr = ImageToWsq(bitmap, currentIndex);
                FingerData.FpRr = Utils.ImageToByte(bitmap);
            }

            if (currentIndex == 4)
            {
                FingerData.WsqRl = ImageToWsq(bitmap, currentIndex);
                FingerData.FpRl = Utils.ImageToByte(bitmap);
            }

            if (currentIndex == 5)
            {
                FingerData.WsqLt = ImageToWsq(bitmap, currentIndex);
                FingerData.FpLt = Utils.ImageToByte(bitmap);
            }

            if (currentIndex == 6)
            {
                FingerData.WsqLi = ImageToWsq(bitmap, currentIndex);
                FingerData.FpLi = Utils.ImageToByte(bitmap);
            }

            if (currentIndex == 7)
            {
                FingerData.WsqLm = ImageToWsq(bitmap, currentIndex);
                FingerData.FpLm = Utils.ImageToByte(bitmap);
            }

            if (currentIndex == 8)
            {
                FingerData.WsqLr = ImageToWsq(bitmap, currentIndex);
                FingerData.FpLr = Utils.ImageToByte(bitmap);
            }

            if (currentIndex == 9)
            {
                FingerData.WsqLl = ImageToWsq(bitmap, currentIndex);
                FingerData.FpLl = Utils.ImageToByte(bitmap);
            }
        }

        public void OnGetImage(Bitmap hBitmap)
        {
            if (m_bExit)
                return;
            if (fpPB.InvokeRequired)
            {
                try
                {
                    SetImageCallback d = new SetImageCallback(this.OnGetImage);
                    this.Invoke(d, new object[] { hBitmap });
                }
                catch
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Please select a finger first to update. ");

                }

            }
            else
            {
                IntPtr hbmp = hBitmap.GetHbitmap();

                //if (this.deviceName.Contains("Futronic"))   hBitmap = invertImage(hBitmap);
                fpPB.Image = hBitmap;
                fpArray[currentIndex] = hBitmap;
                populateWsqData(hBitmap, currentIndex);
            }
        }

        private byte[] ImageToWsqV1(Image img)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                img.Save(ms, img.RawFormat);
                byte[] rawData = ms.ToArray();
                byte[] wsqdata = new byte[img.Width * img.Height];
                byte[] finalImage;
                int wsqsize = 0;
                ISTL.RAB.CustomFpEngine.BmpToWsq(rawData, img.Width, img.Height, 8, 500, 2.833755f, wsqdata, ref wsqsize);
                finalImage = new byte[wsqsize];
                Buffer.BlockCopy(wsqdata, 0, finalImage, 0, wsqsize);
                return finalImage;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //private byte[] ImageToWsq(Bitmap img)
        //{
        //    try
        //    {
        //        lock (_locker)
        //        {
        //            Utils.ByteToFile("finger.bmp", Utils.ImageToByte(img));

        //            img = (Bitmap)Bitmap.FromFile("finger.bmp");
        //            Wsqm.WSQ wsqEncoder = new Wsqm.WSQ();
        //            wsqEncoder.EnconderFile("finger.bmp", "finger.wsq", new String[] { "anwar" }, 0.75f);
        //            byte[] wsqData = Utils.FileToByteArray("finger.wsq");
        //            return wsqData;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        private byte[] ImageToWsq(Bitmap img, int index)
        {
            try
            {
                lock (_locker)
                {
                    Utils.ByteToFile(string.Format("finger_{0}.bmp", index), Utils.ImageToByte(img));

                    img = (Bitmap)Bitmap.FromFile(string.Format("finger_{0}.bmp", index));
                    Wsqm.WSQ wsqEncoder = new Wsqm.WSQ();
                    wsqEncoder.EnconderFile(string.Format("finger_{0}.bmp", index), string.Format("finger_{0}.wsq", index), new String[] { "anwar" }, 0.75f);
                    byte[] wsqData = Utils.FileToByteArray(string.Format("finger_{0}.wsq", index));
                    return wsqData;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            btnOk.Enabled = false;
            btnReset.Enabled = false;
            txtMessage.Text = "Please wait for initialization...";
            try
            {
                reset();
            }
            catch { }
            finally
            {
                btnOk.Enabled = true;
                btnReset.Enabled = true;
            }
        }

        public int FingerClick(int nIndex)
        {
            int lIndex = -1;
            try
            {
                if (nIndex >= 0 && nIndex <= 4)
                {
                    for (int i = 0; i <= 9; i++)
                    {

                        if (fpArray[i] == null)
                        {
                            lIndex = i;
                            break;
                        }
                    }

                }
                else if (nIndex >= 5 && nIndex <= 9)
                {

                    for (int i = 5; i < 10; i++)
                    {

                        if (fpArray[i] == null)
                        {
                            lIndex = i;
                            break;
                        }
                    }
                    if (lIndex == -1)
                    {
                        for (int i = 0; i < 5; i++)
                        {

                            if (fpArray[i] == null)
                            {
                                lIndex = i;
                                break;
                            }
                        }
                    }

                }
            }
            catch
            {

            }
            return lIndex;
        }
        private int GetCurrentIndex()
        {
            int cIndex = 0;
            try
            {
                for (int i = 0; i < fpArray.Length; i++)
                {

                    if (fpArray[i] == null)
                    {
                        cIndex = i;
                        break;
                    }
                }
            }
            catch
            {

            }
            return cIndex;
        }

        private void reset()
        {
            this.currentIndex = 0;
            updateFinger();
            updatePointer(currentIndex);
            FingerData = new FingerprintData();
            fpArray = new Bitmap[10];
            for (int i = 0; i < 10; i++)
            {
                Control[] controls = this.Controls.Find("fpPB" + i, true);
                if (controls != null && controls.Length > 0)
                {
                    PictureBox p = (PictureBox)controls[0];
                    p.Image = null;
                }
            }

            fpPB.Image = null;

            pictureBox0.BackgroundImage = ISTL.RAB.Properties.Resources.r_thumb_blue;
            pictureBox1.BackgroundImage = ISTL.RAB.Properties.Resources.r_index_blue;
            pictureBox2.BackgroundImage = ISTL.RAB.Properties.Resources.r_middle_blue;
            pictureBox3.BackgroundImage = ISTL.RAB.Properties.Resources.r_ring_blue;
            pictureBox4.BackgroundImage = ISTL.RAB.Properties.Resources.r_small_blue;
            pictureBox5.BackgroundImage = ISTL.RAB.Properties.Resources.l_thumb_blue;
            pictureBox6.BackgroundImage = ISTL.RAB.Properties.Resources.l_index_blue;
            pictureBox7.BackgroundImage = ISTL.RAB.Properties.Resources.l_middle_blue;
            pictureBox8.BackgroundImage = ISTL.RAB.Properties.Resources.l_ring_blue;
            pictureBox9.BackgroundImage = ISTL.RAB.Properties.Resources.l_small_blue;
            /*
                        if (fpEngine != null)
                        {
                            fpEngine = new FutronicEngine(this);
                            fpEngine.OpenDevice();
                        }*/
            if (deviceName.Contains("Futronic"))
            {
                fpEngine = new FutronicEngine(this);
                fpEngine.OpenDevice();
            }
            if (deviceName.Contains("Cross Match"))
            {
                fpEngine = new CrossmatchEngine(this);
                fpEngine.OpenDevice();
            }
            /*  else
              {
                  this.txtMessage.Text = "Device not integrated.";
              }*/
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /*private void pictureBoxRightHand_Click(object sender, EventArgs e)
        {
            *//*this.currentIndex = 0;
            updatePointer(this.currentIndex);*//*
        }*/

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.currentIndex = 1;
            updatePointer(this.currentIndex);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.currentIndex = 2;
            updatePointer(this.currentIndex);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.currentIndex = 3;
            updatePointer(this.currentIndex);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.currentIndex = 4;
            updatePointer(this.currentIndex);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.currentIndex = 5;
            updatePointer(this.currentIndex);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.currentIndex = 6;
            updatePointer(this.currentIndex);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.currentIndex = 7;
            updatePointer(this.currentIndex);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            this.currentIndex = 8;
            updatePointer(this.currentIndex);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.currentIndex = 9;
            updatePointer(this.currentIndex);
        }

        private void pictureBox0_Click(object sender, EventArgs e)
        {
            this.currentIndex = 0;
            updatePointer(this.currentIndex);
        }
    }
}
