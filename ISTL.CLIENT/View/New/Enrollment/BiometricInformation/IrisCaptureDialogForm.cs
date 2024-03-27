using ISTL.IRIS;
using ISTL.MODELS.DTO.Iris;
using ISTL.RAB.Controllers;
using ISTL.RAB.DbManager;
using ISTL.RAB.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTL.RAB.View.New.Enrollment.BiometricInformation
{
    public partial class IrisCaptureDialogForm : Form, IIrisControl
    {
        public IrisData irisData;
        IIrisEngine irisEngine = null;
        string deviceName = "";
        ImageCtrl leftCtrl;
        ImageCtrl rightCtrl;
        public IrisCaptureDialogForm()
        {
            Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);
            InitializeComponent();
            leftCtrl = new ImageCtrl();
            rightCtrl = new ImageCtrl();
            panel3.Controls.Add(rightCtrl);
            panel4.Controls.Add(leftCtrl);
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            LoadBiometricPosition();
        }

        private void InitBgColor()
        {
            /*
            this.pictureBox1.BackColor = Color.DarkGreen;
            this.pictureBox2.BackColor = Color.DarkGreen;

            panel3.BackColor = Color.DarkGreen;
            panel4.BackColor = Color.DarkGreen;
            */
        }
        
        private void LoadBiometricPosition()
        {
            cmbPosition.DataSource = new BindingSource(ComboBoxItems.biometricPositions, null);
            cmbPosition.DisplayMember = "Value";
            cmbPosition.ValueMember = "Key";
            cmbPosition.SelectedIndex = 0;



        }

        public IrisCaptureDialogForm(string deviceName) : this()
        {
            this.deviceName = deviceName;
        }



        private void IrisCaptureDialogForm_Load(object sender, EventArgs e)
        {
            this.cmbPosition.Visible = true;

            if (deviceName == null || deviceName.Length == 0)
            {
                DbDeviceManager dManager = new DbDeviceManager();
                try
                {
                    this.deviceName = dManager.GetDevice("IRIS").Name;
                }
                catch (Exception ex)
                {
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "No Iris Capture Device Set");
                    this.Close();
                    return;
                }
            }
            if (deviceName.Contains("Cross Match"))
            {
                irisEngine = new CmIris(this, leftCtrl, rightCtrl);
            }
            else if (deviceName.Contains("IriMagic"))
            {
                this.cmbPosition.Visible = false;
                irisEngine = new IriMagic(this, leftCtrl, rightCtrl);
            }
            else if (deviceName.Contains("IriShield"))
            {
                this.cmbPosition.Visible = false;
                irisEngine = new IriShield(this, leftCtrl, rightCtrl);
            }
            else if (deviceName.Contains("CROSSMATCH USB2.0 Camera (5.0M Monochrome)"))
            {
                this.cmbPosition.Visible = true;
                irisEngine = new CrossMatchIScan(this, leftCtrl, rightCtrl);
            }
            else
            {
                CustomMessageBox.ShowMessage("SNSOP TOOLS", "Device is not integrated.");
            }

            string position = cmbPosition.SelectedValue.ToString();
            if (irisEngine != null)
            {
                bool status = false;

                //ProcessingDialog.Run(delegate ()
                //{
                //    Invoke((MethodInvoker)delegate
                //    {
                //        status = irisEngine.OpenDevice(position);
                //    });
                //});

                OnComplete(false);
                status = irisEngine.OpenDevice(position);

                if (!status)
                {
                    OnComplete(true);
                    CustomMessageBox.ShowMessage("SNSOP TOOLS", "Unable to open the device. Please make sure you have connected the device or setup the device or installed the driver.");
                }             
            }

            irisData = new IrisData();             
        }

        public void SetMessage(string message)
        {

        }

        public void OnComplete(bool resopnse)
        {
            try
            {
                OnCompleteDelegate setOnComplete = new OnCompleteDelegate(SetOnComplete);
                this.Invoke(setOnComplete, resopnse);
            }
            catch { }
        }

        delegate void OnCompleteDelegate(bool response);

        private void SetOnComplete(bool response)
        {
            this.btnOk.Enabled = response;
        }

        public void OnGetLeftIris(Bitmap image)
        {
            OnGetLeftIrisDelegate setImage = new OnGetLeftIrisDelegate(SetLeftImage);
            this.Invoke(setImage, image);
        }

        delegate void OnGetLeftIrisDelegate(Bitmap image);

        private void SetLeftImage(Bitmap image)
        {
            this.pictureBox2.Visible = true;
            this.pictureBox2.Image = image;
            this.pictureBox2.Update();
        }


        delegate void OnGetRightIrisDelegate(Bitmap image);

        public void OnGetRightIris(Bitmap image)
        {
            OnGetRightIrisDelegate setImage = new OnGetRightIrisDelegate(SetRightImage);
            this.Invoke(setImage, image);
        }

        private void SetRightImage(Bitmap image)
        {
            this.pictureBox1.Visible = true;
            this.pictureBox1.Image = image;
            this.pictureBox1.Update();
        }

        private void btnArrestInfo_Click(object sender, EventArgs e)
        {
            irisData.LeftIris = ImageToByte(pictureBox2.Image);
            irisData.RightIris = ImageToByte(pictureBox1.Image);
            this.DialogResult = DialogResult.OK;
        }

        private byte[] ImageToByte(Image img)
        {
            try
            {
                if (img == null) return null;

                ImageConverter converter = new ImageConverter();
                return (byte[])converter.ConvertTo(img, typeof(byte[]));
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

        private void IrisCaptureDialogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(irisEngine != null)
            {
                irisEngine.CloseDevice();
            }
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            //irisData = new IrisData();
            //pictureBox1.Image = null;
            //pictureBox2.Image = null;
            //pictureBox1.Visible = false;
            //pictureBox2.Visible = false;
            //cmbPosition.SelectedIndex = 0;

            //if (irisEngine != null)
            //{
            //    irisEngine.StartCapture(cmbPosition.SelectedValue.ToString());
            //}

            this.btnOk.Enabled = false;
            this.btnReset.Enabled = false;
            try
            {

                cmbPosition_SelectionChangeCommitted(null, null);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }         
            finally
            {
                //this.btnOk.Enabled = true;
                this.btnReset.Enabled = true;
            }
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void cmbPosition_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(irisEngine != null)
            {
                pictureBox1.Image = null;
                pictureBox2.Image = null;
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
                panel3.Controls.Remove(rightCtrl);
                panel4.Controls.Remove(leftCtrl);

                //ProcessingDialog.Run(delegate ()
                //{
                //    irisEngine.CloseDevice();
                //});

                irisEngine.CloseDevice();

                InitBgColor();

                leftCtrl = new ImageCtrl();
                rightCtrl = new ImageCtrl();
                panel3.Controls.Add(rightCtrl);
                panel4.Controls.Add(leftCtrl);
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;

                IrisCaptureDialogForm_Load(null, null);
                //irisEngine.StartCapture(cmbPosition.SelectedValue.ToString());                
            }
        }
    }
}
