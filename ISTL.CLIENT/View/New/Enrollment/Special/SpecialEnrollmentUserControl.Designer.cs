
namespace ISTL.RAB.View.New.Enrollment.Special
{
    partial class SpecialEnrollmentUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelButton = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tbFullName = new MaterialSkin.Controls.MaterialTextBox();
            this.tbFatherName = new MaterialSkin.Controls.MaterialTextBox();
            this.tbFineAmount = new MaterialSkin.Controls.MaterialTextBox();
            this.tbMagistrateName = new MaterialSkin.Controls.MaterialTextBox();
            this.tbVillageRoadHouse = new MaterialSkin.Controls.MaterialTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbArresteeType = new MaterialSkin.Controls.MaterialComboBox();
            this.tbPlaceOfFine = new MaterialSkin.Controls.MaterialTextBox();
            this.btnSave = new FontAwesome.Sharp.IconButton();
            this.tbRefNo = new MaterialSkin.Controls.MaterialTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbSubUnit = new MaterialSkin.Controls.MaterialComboBox();
            this.cmbUnit = new MaterialSkin.Controls.MaterialComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbRABOfficeName = new MaterialSkin.Controls.MaterialTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbGender = new MaterialSkin.Controls.MaterialComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbNID = new MaterialSkin.Controls.MaterialTextBox();
            this.tbLaw = new MaterialSkin.Controls.MaterialTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.pbPhoto = new System.Windows.Forms.PictureBox();
            this.btnCaptureImage = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDistrict = new ISTL.COMMON.CustomControl.SuggestComboBox();
            this.cmbUpazilla = new ISTL.COMMON.CustomControl.SuggestComboBox();
            this.cmbUnion = new ISTL.COMMON.CustomControl.SuggestComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbCrimeType = new ISTL.COMMON.CustomControl.SuggestComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbCrimeDistrict = new ISTL.COMMON.CustomControl.SuggestComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbCrimeThana = new ISTL.COMMON.CustomControl.SuggestComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.exitButton = new FontAwesome.Sharp.IconButton();
            this.panelButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // panelButton
            // 
            this.panelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.panelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelButton.Controls.Add(this.lblTitle);
            this.panelButton.Controls.Add(this.pictureBox2);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelButton.Location = new System.Drawing.Point(0, 0);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(135, 650);
            this.panelButton.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTitle.Location = new System.Drawing.Point(5, 282);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(125, 59);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Special Enrollment";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ISTL.RAB.Properties.Resources.logo75;
            this.pictureBox2.Location = new System.Drawing.Point(5, 180);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(125, 89);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // tbFullName
            // 
            this.tbFullName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbFullName.Depth = 0;
            this.tbFullName.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbFullName.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tbFullName.Hint = "Accused Name";
            this.tbFullName.LeadingIcon = null;
            this.tbFullName.Location = new System.Drawing.Point(177, 106);
            this.tbFullName.MaxLength = 100;
            this.tbFullName.MouseState = MaterialSkin.MouseState.OUT;
            this.tbFullName.Multiline = false;
            this.tbFullName.Name = "tbFullName";
            this.tbFullName.Size = new System.Drawing.Size(250, 50);
            this.tbFullName.TabIndex = 201;
            this.tbFullName.Text = "";
            this.tbFullName.TrailingIcon = null;
            this.tbFullName.UseAccent = false;
            this.tbFullName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbFullName_KeyDown);
            this.tbFullName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFullName_KeyPress);
            this.tbFullName.Leave += new System.EventHandler(this.tbFullName_Leave);
            // 
            // tbFatherName
            // 
            this.tbFatherName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbFatherName.Depth = 0;
            this.tbFatherName.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbFatherName.Hint = "Father Name";
            this.tbFatherName.LeadingIcon = null;
            this.tbFatherName.Location = new System.Drawing.Point(177, 179);
            this.tbFatherName.MaxLength = 100;
            this.tbFatherName.MouseState = MaterialSkin.MouseState.OUT;
            this.tbFatherName.Multiline = false;
            this.tbFatherName.Name = "tbFatherName";
            this.tbFatherName.Size = new System.Drawing.Size(250, 50);
            this.tbFatherName.TabIndex = 202;
            this.tbFatherName.Text = "";
            this.tbFatherName.TrailingIcon = null;
            this.tbFatherName.UseAccent = false;
            this.tbFatherName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbFatherName_KeyDown);
            this.tbFatherName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFatherName_KeyPress);
            this.tbFatherName.Leave += new System.EventHandler(this.tbFatherName_Leave);
            // 
            // tbFineAmount
            // 
            this.tbFineAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbFineAmount.Depth = 0;
            this.tbFineAmount.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbFineAmount.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tbFineAmount.Hint = "Fine Amount";
            this.tbFineAmount.LeadingIcon = null;
            this.tbFineAmount.Location = new System.Drawing.Point(177, 473);
            this.tbFineAmount.MaxLength = 100;
            this.tbFineAmount.MouseState = MaterialSkin.MouseState.OUT;
            this.tbFineAmount.Multiline = false;
            this.tbFineAmount.Name = "tbFineAmount";
            this.tbFineAmount.Size = new System.Drawing.Size(250, 50);
            this.tbFineAmount.TabIndex = 206;
            this.tbFineAmount.Text = "";
            this.tbFineAmount.TrailingIcon = null;
            this.tbFineAmount.UseAccent = false;
            this.tbFineAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbFineAmount_KeyDown);
            this.tbFineAmount.Leave += new System.EventHandler(this.tbFineAmount_Leave);
            // 
            // tbMagistrateName
            // 
            this.tbMagistrateName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMagistrateName.Depth = 0;
            this.tbMagistrateName.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbMagistrateName.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tbMagistrateName.Hint = "Magistrate Name";
            this.tbMagistrateName.LeadingIcon = null;
            this.tbMagistrateName.Location = new System.Drawing.Point(446, 37);
            this.tbMagistrateName.MaxLength = 100;
            this.tbMagistrateName.MouseState = MaterialSkin.MouseState.OUT;
            this.tbMagistrateName.Multiline = false;
            this.tbMagistrateName.Name = "tbMagistrateName";
            this.tbMagistrateName.Size = new System.Drawing.Size(250, 50);
            this.tbMagistrateName.TabIndex = 207;
            this.tbMagistrateName.Text = "";
            this.tbMagistrateName.TrailingIcon = null;
            this.tbMagistrateName.UseAccent = false;
            this.tbMagistrateName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbMagistrateName_KeyDown);
            this.tbMagistrateName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMagistrateName_KeyPress);
            this.tbMagistrateName.Leave += new System.EventHandler(this.tbMagistrateName_Leave);
            // 
            // tbVillageRoadHouse
            // 
            this.tbVillageRoadHouse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbVillageRoadHouse.Depth = 0;
            this.tbVillageRoadHouse.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbVillageRoadHouse.Hint = "Village/Road No/House No";
            this.tbVillageRoadHouse.LeadingIcon = null;
            this.tbVillageRoadHouse.Location = new System.Drawing.Point(446, 473);
            this.tbVillageRoadHouse.MaxLength = 50;
            this.tbVillageRoadHouse.MouseState = MaterialSkin.MouseState.OUT;
            this.tbVillageRoadHouse.Multiline = false;
            this.tbVillageRoadHouse.Name = "tbVillageRoadHouse";
            this.tbVillageRoadHouse.Size = new System.Drawing.Size(250, 50);
            this.tbVillageRoadHouse.TabIndex = 212;
            this.tbVillageRoadHouse.Text = "";
            this.tbVillageRoadHouse.TrailingIcon = null;
            this.tbVillageRoadHouse.UseAccent = false;
            this.tbVillageRoadHouse.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbVillageRoadHouse_KeyDown);
            this.tbVillageRoadHouse.Leave += new System.EventHandler(this.tbVillageRoadHouse_Leave);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(446, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 63);
            this.label1.TabIndex = 23;
            this.label1.Text = "Address";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbArresteeType
            // 
            this.cmbArresteeType.AutoResize = false;
            this.cmbArresteeType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbArresteeType.Depth = 0;
            this.cmbArresteeType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbArresteeType.DropDownHeight = 174;
            this.cmbArresteeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbArresteeType.DropDownWidth = 121;
            this.cmbArresteeType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbArresteeType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbArresteeType.FormattingEnabled = true;
            this.cmbArresteeType.Hint = "Arrest Type";
            this.cmbArresteeType.IntegralHeight = false;
            this.cmbArresteeType.ItemHeight = 43;
            this.cmbArresteeType.Location = new System.Drawing.Point(728, 105);
            this.cmbArresteeType.MaxDropDownItems = 4;
            this.cmbArresteeType.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbArresteeType.Name = "cmbArresteeType";
            this.cmbArresteeType.Size = new System.Drawing.Size(250, 49);
            this.cmbArresteeType.StartIndex = 0;
            this.cmbArresteeType.TabIndex = 214;
            this.cmbArresteeType.UseAccent = false;
            this.cmbArresteeType.SelectedIndexChanged += new System.EventHandler(this.cmbArresteeType_SelectedIndexChanged);
            this.cmbArresteeType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbArresteeType_KeyDown);
            this.cmbArresteeType.Leave += new System.EventHandler(this.cmbArresteeType_Leave);
            // 
            // tbPlaceOfFine
            // 
            this.tbPlaceOfFine.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPlaceOfFine.Depth = 0;
            this.tbPlaceOfFine.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbPlaceOfFine.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tbPlaceOfFine.Hint = "Place of Fine";
            this.tbPlaceOfFine.LeadingIcon = null;
            this.tbPlaceOfFine.Location = new System.Drawing.Point(177, 399);
            this.tbPlaceOfFine.MaxLength = 100;
            this.tbPlaceOfFine.MouseState = MaterialSkin.MouseState.OUT;
            this.tbPlaceOfFine.Multiline = false;
            this.tbPlaceOfFine.Name = "tbPlaceOfFine";
            this.tbPlaceOfFine.Size = new System.Drawing.Size(250, 50);
            this.tbPlaceOfFine.TabIndex = 205;
            this.tbPlaceOfFine.Text = "";
            this.tbPlaceOfFine.TrailingIcon = null;
            this.tbPlaceOfFine.UseAccent = false;
            this.tbPlaceOfFine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPlaceOfFine_KeyDown);
            this.tbPlaceOfFine.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbPlaceOfFine_KeyPress);
            this.tbPlaceOfFine.Leave += new System.EventHandler(this.tbPlaceOfFine_Leave);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.IconChar = FontAwesome.Sharp.IconChar.Save;
            this.btnSave.IconColor = System.Drawing.Color.White;
            this.btnSave.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSave.IconSize = 22;
            this.btnSave.Location = new System.Drawing.Point(1036, 581);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(175, 40);
            this.btnSave.TabIndex = 199;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbRefNo
            // 
            this.tbRefNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbRefNo.Depth = 0;
            this.tbRefNo.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbRefNo.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tbRefNo.Hint = "Reference No";
            this.tbRefNo.LeadingIcon = null;
            this.tbRefNo.Location = new System.Drawing.Point(177, 37);
            this.tbRefNo.MaxLength = 100;
            this.tbRefNo.MouseState = MaterialSkin.MouseState.OUT;
            this.tbRefNo.Multiline = false;
            this.tbRefNo.Name = "tbRefNo";
            this.tbRefNo.ReadOnly = true;
            this.tbRefNo.Size = new System.Drawing.Size(250, 50);
            this.tbRefNo.TabIndex = 200;
            this.tbRefNo.Text = "";
            this.tbRefNo.TrailingIcon = null;
            this.tbRefNo.UseAccent = false;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(161, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(10, 49);
            this.label9.TabIndex = 503;
            this.label9.Text = "*";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbSubUnit
            // 
            this.cmbSubUnit.AutoResize = false;
            this.cmbSubUnit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbSubUnit.Depth = 0;
            this.cmbSubUnit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbSubUnit.DropDownHeight = 174;
            this.cmbSubUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubUnit.DropDownWidth = 121;
            this.cmbSubUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSubUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbSubUnit.FormattingEnabled = true;
            this.cmbSubUnit.Hint = "Sub Unit";
            this.cmbSubUnit.IntegralHeight = false;
            this.cmbSubUnit.ItemHeight = 43;
            this.cmbSubUnit.Location = new System.Drawing.Point(728, 253);
            this.cmbSubUnit.MaxDropDownItems = 4;
            this.cmbSubUnit.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbSubUnit.Name = "cmbSubUnit";
            this.cmbSubUnit.Size = new System.Drawing.Size(250, 49);
            this.cmbSubUnit.StartIndex = 0;
            this.cmbSubUnit.TabIndex = 216;
            this.cmbSubUnit.UseAccent = false;
            this.cmbSubUnit.SelectedIndexChanged += new System.EventHandler(this.cmbSubUnit_SelectedIndexChanged);
            this.cmbSubUnit.Enter += new System.EventHandler(this.cmbSubUnit_Enter);
            this.cmbSubUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSubUnit_KeyDown);
            this.cmbSubUnit.Leave += new System.EventHandler(this.cmbSubUnit_Leave);
            // 
            // cmbUnit
            // 
            this.cmbUnit.AutoResize = false;
            this.cmbUnit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbUnit.Depth = 0;
            this.cmbUnit.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbUnit.DropDownHeight = 174;
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.DropDownWidth = 121;
            this.cmbUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Hint = "Unit";
            this.cmbUnit.IntegralHeight = false;
            this.cmbUnit.ItemHeight = 43;
            this.cmbUnit.Location = new System.Drawing.Point(728, 178);
            this.cmbUnit.MaxDropDownItems = 4;
            this.cmbUnit.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(250, 49);
            this.cmbUnit.StartIndex = 0;
            this.cmbUnit.TabIndex = 215;
            this.cmbUnit.UseAccent = false;
            this.cmbUnit.SelectedIndexChanged += new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.cmbUnit.Leave += new System.EventHandler(this.cmbUnit_Leave);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(712, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 49);
            this.label7.TabIndex = 511;
            this.label7.Text = "*";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tbRABOfficeName
            // 
            this.tbRABOfficeName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbRABOfficeName.Depth = 0;
            this.tbRABOfficeName.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbRABOfficeName.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tbRABOfficeName.Hint = "RAB Officer Name";
            this.tbRABOfficeName.LeadingIcon = null;
            this.tbRABOfficeName.Location = new System.Drawing.Point(446, 106);
            this.tbRABOfficeName.MaxLength = 100;
            this.tbRABOfficeName.MouseState = MaterialSkin.MouseState.OUT;
            this.tbRABOfficeName.Multiline = false;
            this.tbRABOfficeName.Name = "tbRABOfficeName";
            this.tbRABOfficeName.Size = new System.Drawing.Size(250, 50);
            this.tbRABOfficeName.TabIndex = 208;
            this.tbRABOfficeName.Text = "";
            this.tbRABOfficeName.TrailingIcon = null;
            this.tbRABOfficeName.UseAccent = false;
            this.tbRABOfficeName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbRABOfficeName_KeyDown);
            this.tbRABOfficeName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbRABOfficeName_KeyPress);
            this.tbRABOfficeName.Leave += new System.EventHandler(this.tbRABOfficeName_Leave);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(161, 253);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 49);
            this.label8.TabIndex = 513;
            this.label8.Text = "*";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbGender
            // 
            this.cmbGender.AutoResize = false;
            this.cmbGender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbGender.Depth = 0;
            this.cmbGender.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbGender.DropDownHeight = 174;
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.DropDownWidth = 121;
            this.cmbGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbGender.FormattingEnabled = true;
            this.cmbGender.Hint = "Gender";
            this.cmbGender.IntegralHeight = false;
            this.cmbGender.ItemHeight = 43;
            this.cmbGender.Location = new System.Drawing.Point(177, 253);
            this.cmbGender.MaxDropDownItems = 4;
            this.cmbGender.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(250, 49);
            this.cmbGender.StartIndex = 0;
            this.cmbGender.TabIndex = 203;
            this.cmbGender.UseAccent = false;
            this.cmbGender.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbGender_KeyDown);
            this.cmbGender.Leave += new System.EventHandler(this.cmbGender_Leave);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(712, 107);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(10, 49);
            this.label10.TabIndex = 514;
            this.label10.Text = "*";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tbNID
            // 
            this.tbNID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbNID.Depth = 0;
            this.tbNID.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbNID.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tbNID.Hint = "NID";
            this.tbNID.LeadingIcon = null;
            this.tbNID.Location = new System.Drawing.Point(177, 324);
            this.tbNID.MaxLength = 17;
            this.tbNID.MouseState = MaterialSkin.MouseState.OUT;
            this.tbNID.Multiline = false;
            this.tbNID.Name = "tbNID";
            this.tbNID.Size = new System.Drawing.Size(250, 50);
            this.tbNID.TabIndex = 204;
            this.tbNID.Text = "";
            this.tbNID.TrailingIcon = null;
            this.tbNID.UseAccent = false;
            this.tbNID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbNID_KeyDown);
            this.tbNID.Leave += new System.EventHandler(this.tbNID_Leave);
            // 
            // tbLaw
            // 
            this.tbLaw.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbLaw.Depth = 0;
            this.tbLaw.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbLaw.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tbLaw.Hint = "Law/Act";
            this.tbLaw.LeadingIcon = null;
            this.tbLaw.Location = new System.Drawing.Point(728, 473);
            this.tbLaw.MaxLength = 100;
            this.tbLaw.MouseState = MaterialSkin.MouseState.OUT;
            this.tbLaw.Multiline = false;
            this.tbLaw.Name = "tbLaw";
            this.tbLaw.Size = new System.Drawing.Size(250, 50);
            this.tbLaw.TabIndex = 219;
            this.tbLaw.Text = "";
            this.tbLaw.TrailingIcon = null;
            this.tbLaw.UseAccent = false;
            this.tbLaw.Leave += new System.EventHandler(this.tbLaw_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1091, 399);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 18);
            this.label3.TabIndex = 521;
            this.label3.Text = "Image";
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.AntiqueWhite;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label31.Location = new System.Drawing.Point(999, 98);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(231, 28);
            this.label31.TabIndex = 522;
            this.label31.Text = "Note: Click image box to upload";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbPhoto
            // 
            this.pbPhoto.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pbPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPhoto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPhoto.Location = new System.Drawing.Point(1002, 136);
            this.pbPhoto.Name = "pbPhoto";
            this.pbPhoto.Size = new System.Drawing.Size(228, 260);
            this.pbPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPhoto.TabIndex = 520;
            this.pbPhoto.TabStop = false;
            this.pbPhoto.Click += new System.EventHandler(this.pbPhoto_Click);
            // 
            // btnCaptureImage
            // 
            this.btnCaptureImage.BackColor = System.Drawing.Color.IndianRed;
            this.btnCaptureImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCaptureImage.FlatAppearance.BorderSize = 0;
            this.btnCaptureImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaptureImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaptureImage.ForeColor = System.Drawing.Color.White;
            this.btnCaptureImage.Location = new System.Drawing.Point(1002, 444);
            this.btnCaptureImage.Name = "btnCaptureImage";
            this.btnCaptureImage.Size = new System.Drawing.Size(228, 40);
            this.btnCaptureImage.TabIndex = 220;
            this.btnCaptureImage.Text = "Capture Image";
            this.btnCaptureImage.UseVisualStyleBackColor = false;
            this.btnCaptureImage.Click += new System.EventHandler(this.btnCaptureImage_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(712, 253);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 49);
            this.label2.TabIndex = 523;
            this.label2.Text = "*";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(712, 323);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 49);
            this.label4.TabIndex = 524;
            this.label4.Text = "*";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(712, 398);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 49);
            this.label5.TabIndex = 525;
            this.label5.Text = "*";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmbDistrict
            // 
            this.cmbDistrict.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmbDistrict.DropDownWidth = 150;
            this.cmbDistrict.FilterRule = null;
            this.cmbDistrict.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDistrict.FormattingEnabled = true;
            this.cmbDistrict.ItemHeight = 16;
            this.cmbDistrict.Location = new System.Drawing.Point(446, 276);
            this.cmbDistrict.MaxDropDownItems = 4;
            this.cmbDistrict.Name = "cmbDistrict";
            this.cmbDistrict.PropertySelector = null;
            this.cmbDistrict.Size = new System.Drawing.Size(250, 24);
            this.cmbDistrict.SuggestBoxHeight = 96;
            this.cmbDistrict.SuggestListOrderRule = null;
            this.cmbDistrict.TabIndex = 209;
            this.cmbDistrict.SelectedIndexChanged += new System.EventHandler(this.cmbDistrict_SelectedIndexChanged);
            this.cmbDistrict.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDistrict_KeyDown);
            this.cmbDistrict.Leave += new System.EventHandler(this.cmbDistrict_Leave);
            // 
            // cmbUpazilla
            // 
            this.cmbUpazilla.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmbUpazilla.FilterRule = null;
            this.cmbUpazilla.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUpazilla.FormattingEnabled = true;
            this.cmbUpazilla.Location = new System.Drawing.Point(446, 348);
            this.cmbUpazilla.Name = "cmbUpazilla";
            this.cmbUpazilla.PropertySelector = null;
            this.cmbUpazilla.Size = new System.Drawing.Size(250, 24);
            this.cmbUpazilla.SuggestBoxHeight = 96;
            this.cmbUpazilla.SuggestListOrderRule = null;
            this.cmbUpazilla.TabIndex = 210;
            this.cmbUpazilla.SelectedIndexChanged += new System.EventHandler(this.cmbUpazilla_SelectedIndexChanged);
            this.cmbUpazilla.Enter += new System.EventHandler(this.cmbUpazilla_Enter);
            this.cmbUpazilla.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUpazilla_KeyDown);
            this.cmbUpazilla.Leave += new System.EventHandler(this.cmbUpazilla_Leave);
            // 
            // cmbUnion
            // 
            this.cmbUnion.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmbUnion.FilterRule = null;
            this.cmbUnion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUnion.FormattingEnabled = true;
            this.cmbUnion.Location = new System.Drawing.Point(446, 423);
            this.cmbUnion.Name = "cmbUnion";
            this.cmbUnion.PropertySelector = null;
            this.cmbUnion.Size = new System.Drawing.Size(250, 24);
            this.cmbUnion.SuggestBoxHeight = 96;
            this.cmbUnion.SuggestListOrderRule = null;
            this.cmbUnion.TabIndex = 211;
            this.cmbUnion.Enter += new System.EventHandler(this.cmbUnion_Enter);
            this.cmbUnion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnion_KeyDown);
            this.cmbUnion.Leave += new System.EventHandler(this.cmbUnion_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(448, 251);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 16);
            this.label12.TabIndex = 532;
            this.label12.Text = "District";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(448, 321);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 16);
            this.label6.TabIndex = 533;
            this.label6.Text = "Upazila";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(448, 396);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 16);
            this.label11.TabIndex = 534;
            this.label11.Text = "Union/Ward";
            // 
            // cmbCrimeType
            // 
            this.cmbCrimeType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmbCrimeType.DropDownWidth = 450;
            this.cmbCrimeType.FilterRule = null;
            this.cmbCrimeType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCrimeType.FormattingEnabled = true;
            this.cmbCrimeType.Location = new System.Drawing.Point(728, 63);
            this.cmbCrimeType.Name = "cmbCrimeType";
            this.cmbCrimeType.PropertySelector = null;
            this.cmbCrimeType.Size = new System.Drawing.Size(250, 28);
            this.cmbCrimeType.SuggestBoxHeight = 96;
            this.cmbCrimeType.SuggestListOrderRule = null;
            this.cmbCrimeType.TabIndex = 213;
            this.cmbCrimeType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCrimeType_KeyDown);
            this.cmbCrimeType.Leave += new System.EventHandler(this.cmbCrimeType_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(725, 37);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 16);
            this.label13.TabIndex = 537;
            this.label13.Text = "Crime Type";
            // 
            // cmbCrimeDistrict
            // 
            this.cmbCrimeDistrict.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmbCrimeDistrict.FilterRule = null;
            this.cmbCrimeDistrict.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCrimeDistrict.FormattingEnabled = true;
            this.cmbCrimeDistrict.Location = new System.Drawing.Point(728, 353);
            this.cmbCrimeDistrict.Name = "cmbCrimeDistrict";
            this.cmbCrimeDistrict.PropertySelector = null;
            this.cmbCrimeDistrict.Size = new System.Drawing.Size(250, 24);
            this.cmbCrimeDistrict.SuggestBoxHeight = 96;
            this.cmbCrimeDistrict.SuggestListOrderRule = null;
            this.cmbCrimeDistrict.TabIndex = 217;
            this.cmbCrimeDistrict.SelectedIndexChanged += new System.EventHandler(this.cmbCrimeDistrict_SelectedIndexChanged);
            this.cmbCrimeDistrict.Enter += new System.EventHandler(this.cmbCrimeDistrict_Enter);
            this.cmbCrimeDistrict.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCrimeDistrict_KeyDown);
            this.cmbCrimeDistrict.Leave += new System.EventHandler(this.cmbCrimeDistrict_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(728, 326);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(100, 16);
            this.label14.TabIndex = 540;
            this.label14.Text = "Crime District";
            // 
            // cmbCrimeThana
            // 
            this.cmbCrimeThana.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmbCrimeThana.FilterRule = null;
            this.cmbCrimeThana.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCrimeThana.FormattingEnabled = true;
            this.cmbCrimeThana.Location = new System.Drawing.Point(731, 428);
            this.cmbCrimeThana.Name = "cmbCrimeThana";
            this.cmbCrimeThana.PropertySelector = null;
            this.cmbCrimeThana.Size = new System.Drawing.Size(250, 24);
            this.cmbCrimeThana.SuggestBoxHeight = 96;
            this.cmbCrimeThana.SuggestListOrderRule = null;
            this.cmbCrimeThana.TabIndex = 218;
            this.cmbCrimeThana.Enter += new System.EventHandler(this.cmbCrimeThana_Enter);
            this.cmbCrimeThana.Leave += new System.EventHandler(this.cmbCrimeThana_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(728, 401);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(105, 16);
            this.label15.TabIndex = 543;
            this.label15.Text = "Crime Upazila";
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.IndianRed;
            this.exitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitButton.FlatAppearance.BorderSize = 0;
            this.exitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.exitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Crimson;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.exitButton.IconColor = System.Drawing.Color.White;
            this.exitButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.exitButton.IconSize = 22;
            this.exitButton.Location = new System.Drawing.Point(1202, 0);
            this.exitButton.Name = "exitButton";
            this.exitButton.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.exitButton.Size = new System.Drawing.Size(48, 45);
            this.exitButton.TabIndex = 550;
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // SpecialEnrollmentUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cmbCrimeThana);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.cmbCrimeDistrict);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cmbCrimeType);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmbUnion);
            this.Controls.Add(this.cmbUpazilla);
            this.Controls.Add(this.cmbDistrict);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.pbPhoto);
            this.Controls.Add(this.btnCaptureImage);
            this.Controls.Add(this.tbLaw);
            this.Controls.Add(this.tbNID);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbGender);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbSubUnit);
            this.Controls.Add(this.cmbUnit);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tbRefNo);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbPlaceOfFine);
            this.Controls.Add(this.cmbArresteeType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbVillageRoadHouse);
            this.Controls.Add(this.tbMagistrateName);
            this.Controls.Add(this.tbRABOfficeName);
            this.Controls.Add(this.tbFineAmount);
            this.Controls.Add(this.tbFatherName);
            this.Controls.Add(this.tbFullName);
            this.Controls.Add(this.panelButton);
            this.Name = "SpecialEnrollmentUserControl";
            this.Size = new System.Drawing.Size(1250, 650);
            this.panelButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPhoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pictureBox2;
        private MaterialSkin.Controls.MaterialTextBox tbFullName;
        private MaterialSkin.Controls.MaterialTextBox tbFatherName;
        private MaterialSkin.Controls.MaterialTextBox tbFineAmount;
        private MaterialSkin.Controls.MaterialTextBox tbMagistrateName;
        private MaterialSkin.Controls.MaterialTextBox tbVillageRoadHouse;
        private System.Windows.Forms.Label label1;
        private MaterialSkin.Controls.MaterialComboBox cmbArresteeType;
        private MaterialSkin.Controls.MaterialTextBox tbPlaceOfFine;
        private FontAwesome.Sharp.IconButton btnSave;
        private MaterialSkin.Controls.MaterialTextBox tbRefNo;
        private System.Windows.Forms.Label label9;
        private MaterialSkin.Controls.MaterialComboBox cmbSubUnit;
        private MaterialSkin.Controls.MaterialComboBox cmbUnit;
        private System.Windows.Forms.Label label7;
        private MaterialSkin.Controls.MaterialTextBox tbRABOfficeName;
        private System.Windows.Forms.Label label8;
        private MaterialSkin.Controls.MaterialComboBox cmbGender;
        private System.Windows.Forms.Label label10;
        private MaterialSkin.Controls.MaterialTextBox tbNID;
        private MaterialSkin.Controls.MaterialTextBox tbLaw;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.PictureBox pbPhoto;
        private System.Windows.Forms.Button btnCaptureImage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private COMMON.CustomControl.SuggestComboBox cmbDistrict;
        private COMMON.CustomControl.SuggestComboBox cmbUpazilla;
        private COMMON.CustomControl.SuggestComboBox cmbUnion;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private COMMON.CustomControl.SuggestComboBox cmbCrimeType;
        private System.Windows.Forms.Label label13;
        private COMMON.CustomControl.SuggestComboBox cmbCrimeDistrict;
        private System.Windows.Forms.Label label14;
        private COMMON.CustomControl.SuggestComboBox cmbCrimeThana;
        private System.Windows.Forms.Label label15;
        private FontAwesome.Sharp.IconButton exitButton;
    }
}
