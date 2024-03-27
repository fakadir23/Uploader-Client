namespace ISTL.RAB.View.New
{
    partial class UserManagementUserControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnRemove = new FontAwesome.Sharp.IconButton();
            this.btnSave = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbConfirmPassword = new MaterialSkin.Controls.MaterialTextBox();
            this.tbPassword = new MaterialSkin.Controls.MaterialTextBox();
            this.tbUserId = new MaterialSkin.Controls.MaterialTextBox();
            this.tbName = new MaterialSkin.Controls.MaterialTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkNotEntry = new MaterialSkin.Controls.MaterialCheckbox();
            this.chkProfileEnrollment = new MaterialSkin.Controls.MaterialCheckbox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkUpdate = new MaterialSkin.Controls.MaterialCheckbox();
            this.chkUserManagement = new MaterialSkin.Controls.MaterialCheckbox();
            this.chkSettings = new MaterialSkin.Controls.MaterialCheckbox();
            this.chkReport = new MaterialSkin.Controls.MaterialCheckbox();
            this.chkBiometricSearch = new MaterialSkin.Controls.MaterialCheckbox();
            this.chkSpecialEntry = new MaterialSkin.Controls.MaterialCheckbox();
            this.chkFailedUpload = new MaterialSkin.Controls.MaterialCheckbox();
            this.chkDraftRecords = new MaterialSkin.Controls.MaterialCheckbox();
            this.chkSearchProfile = new MaterialSkin.Controls.MaterialCheckbox();
            this.chkNewEntry = new MaterialSkin.Controls.MaterialCheckbox();
            this.chkSelectAll = new MaterialSkin.Controls.MaterialCheckbox();
            this.cmbRole = new MaterialSkin.Controls.MaterialComboBox();
            this.btnReset = new FontAwesome.Sharp.IconButton();
            this.cmbUnit = new MaterialSkin.Controls.MaterialComboBox();
            this.cmbSubUnit = new MaterialSkin.Controls.MaterialComboBox();
            this.btnNew = new FontAwesome.Sharp.IconButton();
            this.dgUserList = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStart = new FontAwesome.Sharp.IconButton();
            this.btnPrev = new FontAwesome.Sharp.IconButton();
            this.btnLast = new FontAwesome.Sharp.IconButton();
            this.btnNext = new FontAwesome.Sharp.IconButton();
            this.labelTotalRecords = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.exitButton = new FontAwesome.Sharp.IconButton();
            this.dgvSerial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubUnitId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubUnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NewEntry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpecialEntry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DraftRecords = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FailedUploads = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SearchProfile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BiometricSearch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserManagement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Report = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Settings = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Update = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfileEnrollment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NotEntry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUserList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.Crimson;
            this.btnRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Crimson;
            this.btnRemove.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Crimson;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.btnRemove.IconColor = System.Drawing.Color.White;
            this.btnRemove.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRemove.IconSize = 20;
            this.btnRemove.Location = new System.Drawing.Point(936, 609);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(97, 35);
            this.btnRemove.TabIndex = 113;
            this.btnRemove.Text = "Remove";
            this.btnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SeaGreen;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.IconChar = FontAwesome.Sharp.IconChar.Save;
            this.btnSave.IconColor = System.Drawing.Color.White;
            this.btnSave.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSave.IconSize = 20;
            this.btnSave.Location = new System.Drawing.Point(848, 609);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 35);
            this.btnSave.TabIndex = 111;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbConfirmPassword);
            this.panel1.Controls.Add(this.tbPassword);
            this.panel1.Controls.Add(this.tbUserId);
            this.panel1.Controls.Add(this.tbName);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.cmbUnit);
            this.panel1.Controls.Add(this.cmbSubUnit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 650);
            this.panel1.TabIndex = 114;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(162, 234);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(10, 14);
            this.label7.TabIndex = 505;
            this.label7.Text = "*";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(337, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 14);
            this.label6.TabIndex = 504;
            this.label6.Text = "*";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(339, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 14);
            this.label5.TabIndex = 504;
            this.label5.Text = "*";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(339, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 14);
            this.label4.TabIndex = 504;
            this.label4.Text = "*";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(339, 234);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 14);
            this.label2.TabIndex = 504;
            this.label2.Text = "*";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tbConfirmPassword
            // 
            this.tbConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbConfirmPassword.Depth = 0;
            this.tbConfirmPassword.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbConfirmPassword.Hint = "Confirm Password";
            this.tbConfirmPassword.LeadingIcon = null;
            this.tbConfirmPassword.Location = new System.Drawing.Point(178, 234);
            this.tbConfirmPassword.MaxLength = 50;
            this.tbConfirmPassword.MouseState = MaterialSkin.MouseState.OUT;
            this.tbConfirmPassword.Multiline = false;
            this.tbConfirmPassword.Name = "tbConfirmPassword";
            this.tbConfirmPassword.Password = true;
            this.tbConfirmPassword.Size = new System.Drawing.Size(158, 50);
            this.tbConfirmPassword.TabIndex = 101;
            this.tbConfirmPassword.Text = "";
            this.tbConfirmPassword.TrailingIcon = null;
            this.tbConfirmPassword.UseAccent = false;
            // 
            // tbPassword
            // 
            this.tbPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPassword.Depth = 0;
            this.tbPassword.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbPassword.Hint = "Password";
            this.tbPassword.LeadingIcon = null;
            this.tbPassword.Location = new System.Drawing.Point(9, 234);
            this.tbPassword.MaxLength = 50;
            this.tbPassword.MouseState = MaterialSkin.MouseState.OUT;
            this.tbPassword.Multiline = false;
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Password = true;
            this.tbPassword.Size = new System.Drawing.Size(149, 50);
            this.tbPassword.TabIndex = 100;
            this.tbPassword.Text = "";
            this.tbPassword.TrailingIcon = null;
            this.tbPassword.UseAccent = false;
            // 
            // tbUserId
            // 
            this.tbUserId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbUserId.Depth = 0;
            this.tbUserId.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbUserId.Hint = "User ID";
            this.tbUserId.LeadingIcon = null;
            this.tbUserId.Location = new System.Drawing.Point(9, 122);
            this.tbUserId.MaxLength = 50;
            this.tbUserId.MouseState = MaterialSkin.MouseState.OUT;
            this.tbUserId.Multiline = false;
            this.tbUserId.Name = "tbUserId";
            this.tbUserId.Size = new System.Drawing.Size(327, 50);
            this.tbUserId.TabIndex = 81;
            this.tbUserId.Text = "";
            this.tbUserId.TrailingIcon = null;
            this.tbUserId.UseAccent = false;
            // 
            // tbName
            // 
            this.tbName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbName.Depth = 0;
            this.tbName.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbName.Hint = "Name";
            this.tbName.LeadingIcon = null;
            this.tbName.Location = new System.Drawing.Point(9, 178);
            this.tbName.MaxLength = 50;
            this.tbName.MouseState = MaterialSkin.MouseState.OUT;
            this.tbName.Multiline = false;
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(327, 50);
            this.tbName.TabIndex = 85;
            this.tbName.Text = "";
            this.tbName.TrailingIcon = null;
            this.tbName.UseAccent = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox1.Controls.Add(this.chkNotEntry);
            this.groupBox1.Controls.Add(this.chkProfileEnrollment);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.chkUpdate);
            this.groupBox1.Controls.Add(this.chkUserManagement);
            this.groupBox1.Controls.Add(this.chkSettings);
            this.groupBox1.Controls.Add(this.chkReport);
            this.groupBox1.Controls.Add(this.chkBiometricSearch);
            this.groupBox1.Controls.Add(this.chkSpecialEntry);
            this.groupBox1.Controls.Add(this.chkFailedUpload);
            this.groupBox1.Controls.Add(this.chkDraftRecords);
            this.groupBox1.Controls.Add(this.chkSearchProfile);
            this.groupBox1.Controls.Add(this.chkNewEntry);
            this.groupBox1.Controls.Add(this.chkSelectAll);
            this.groupBox1.Controls.Add(this.cmbRole);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(9, 286);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(327, 322);
            this.groupBox1.TabIndex = 99;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "User Privilege";
            // 
            // chkNotEntry
            // 
            this.chkNotEntry.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkNotEntry.Depth = 0;
            this.chkNotEntry.Location = new System.Drawing.Point(13, 134);
            this.chkNotEntry.Margin = new System.Windows.Forms.Padding(0);
            this.chkNotEntry.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkNotEntry.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkNotEntry.Name = "chkNotEntry";
            this.chkNotEntry.Ripple = true;
            this.chkNotEntry.Size = new System.Drawing.Size(139, 39);
            this.chkNotEntry.TabIndex = 505;
            this.chkNotEntry.Text = "No Entry";
            this.chkNotEntry.UseVisualStyleBackColor = true;
            // 
            // chkProfileEnrollment
            // 
            this.chkProfileEnrollment.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkProfileEnrollment.Depth = 0;
            this.chkProfileEnrollment.Location = new System.Drawing.Point(11, 282);
            this.chkProfileEnrollment.Margin = new System.Windows.Forms.Padding(0);
            this.chkProfileEnrollment.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkProfileEnrollment.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkProfileEnrollment.Name = "chkProfileEnrollment";
            this.chkProfileEnrollment.Ripple = true;
            this.chkProfileEnrollment.Size = new System.Drawing.Size(186, 32);
            this.chkProfileEnrollment.TabIndex = 504;
            this.chkProfileEnrollment.Text = "Profile Management";
            this.chkProfileEnrollment.UseVisualStyleBackColor = true;
            this.chkProfileEnrollment.Visible = false;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(220, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(10, 14);
            this.label9.TabIndex = 503;
            this.label9.Text = "*";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chkUpdate
            // 
            this.chkUpdate.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkUpdate.Depth = 0;
            this.chkUpdate.Location = new System.Drawing.Point(176, 210);
            this.chkUpdate.Margin = new System.Windows.Forms.Padding(0);
            this.chkUpdate.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkUpdate.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkUpdate.Name = "chkUpdate";
            this.chkUpdate.Ripple = true;
            this.chkUpdate.Size = new System.Drawing.Size(147, 39);
            this.chkUpdate.TabIndex = 99;
            this.chkUpdate.Text = "Update";
            this.chkUpdate.UseVisualStyleBackColor = true;
            // 
            // chkUserManagement
            // 
            this.chkUserManagement.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkUserManagement.Depth = 0;
            this.chkUserManagement.Location = new System.Drawing.Point(11, 244);
            this.chkUserManagement.Margin = new System.Windows.Forms.Padding(0);
            this.chkUserManagement.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkUserManagement.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkUserManagement.Name = "chkUserManagement";
            this.chkUserManagement.Ripple = true;
            this.chkUserManagement.Size = new System.Drawing.Size(165, 39);
            this.chkUserManagement.TabIndex = 98;
            this.chkUserManagement.Text = "User Management";
            this.chkUserManagement.UseVisualStyleBackColor = true;
            // 
            // chkSettings
            // 
            this.chkSettings.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSettings.Depth = 0;
            this.chkSettings.Location = new System.Drawing.Point(212, 279);
            this.chkSettings.Margin = new System.Windows.Forms.Padding(0);
            this.chkSettings.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkSettings.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkSettings.Name = "chkSettings";
            this.chkSettings.Ripple = true;
            this.chkSettings.Size = new System.Drawing.Size(103, 39);
            this.chkSettings.TabIndex = 97;
            this.chkSettings.Text = "Settings";
            this.chkSettings.UseVisualStyleBackColor = true;
            this.chkSettings.Visible = false;
            // 
            // chkReport
            // 
            this.chkReport.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkReport.Depth = 0;
            this.chkReport.Location = new System.Drawing.Point(176, 171);
            this.chkReport.Margin = new System.Windows.Forms.Padding(0);
            this.chkReport.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkReport.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkReport.Name = "chkReport";
            this.chkReport.Ripple = true;
            this.chkReport.Size = new System.Drawing.Size(147, 39);
            this.chkReport.TabIndex = 96;
            this.chkReport.Text = "Report";
            this.chkReport.UseVisualStyleBackColor = true;
            // 
            // chkBiometricSearch
            // 
            this.chkBiometricSearch.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkBiometricSearch.Depth = 0;
            this.chkBiometricSearch.Location = new System.Drawing.Point(11, 207);
            this.chkBiometricSearch.Margin = new System.Windows.Forms.Padding(0);
            this.chkBiometricSearch.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkBiometricSearch.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkBiometricSearch.Name = "chkBiometricSearch";
            this.chkBiometricSearch.Ripple = true;
            this.chkBiometricSearch.Size = new System.Drawing.Size(154, 39);
            this.chkBiometricSearch.TabIndex = 95;
            this.chkBiometricSearch.Text = "Biometric Search";
            this.chkBiometricSearch.UseVisualStyleBackColor = true;
            // 
            // chkSpecialEntry
            // 
            this.chkSpecialEntry.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSpecialEntry.Depth = 0;
            this.chkSpecialEntry.Location = new System.Drawing.Point(188, 244);
            this.chkSpecialEntry.Margin = new System.Windows.Forms.Padding(0);
            this.chkSpecialEntry.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkSpecialEntry.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkSpecialEntry.Name = "chkSpecialEntry";
            this.chkSpecialEntry.Ripple = true;
            this.chkSpecialEntry.Size = new System.Drawing.Size(139, 39);
            this.chkSpecialEntry.TabIndex = 94;
            this.chkSpecialEntry.Text = "Special Entry";
            this.chkSpecialEntry.UseVisualStyleBackColor = true;
            this.chkSpecialEntry.Visible = false;
            // 
            // chkFailedUpload
            // 
            this.chkFailedUpload.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkFailedUpload.Depth = 0;
            this.chkFailedUpload.Location = new System.Drawing.Point(176, 95);
            this.chkFailedUpload.Margin = new System.Windows.Forms.Padding(0);
            this.chkFailedUpload.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkFailedUpload.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkFailedUpload.Name = "chkFailedUpload";
            this.chkFailedUpload.Ripple = true;
            this.chkFailedUpload.Size = new System.Drawing.Size(146, 39);
            this.chkFailedUpload.TabIndex = 93;
            this.chkFailedUpload.Text = "Failed Uploads";
            this.chkFailedUpload.UseVisualStyleBackColor = true;
            // 
            // chkDraftRecords
            // 
            this.chkDraftRecords.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDraftRecords.Depth = 0;
            this.chkDraftRecords.Location = new System.Drawing.Point(176, 134);
            this.chkDraftRecords.Margin = new System.Windows.Forms.Padding(0);
            this.chkDraftRecords.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkDraftRecords.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkDraftRecords.Name = "chkDraftRecords";
            this.chkDraftRecords.Ripple = true;
            this.chkDraftRecords.Size = new System.Drawing.Size(146, 39);
            this.chkDraftRecords.TabIndex = 92;
            this.chkDraftRecords.Text = "Draft Records";
            this.chkDraftRecords.UseVisualStyleBackColor = true;
            // 
            // chkSearchProfile
            // 
            this.chkSearchProfile.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSearchProfile.Depth = 0;
            this.chkSearchProfile.Location = new System.Drawing.Point(11, 171);
            this.chkSearchProfile.Margin = new System.Windows.Forms.Padding(0);
            this.chkSearchProfile.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkSearchProfile.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkSearchProfile.Name = "chkSearchProfile";
            this.chkSearchProfile.Ripple = true;
            this.chkSearchProfile.Size = new System.Drawing.Size(146, 39);
            this.chkSearchProfile.TabIndex = 91;
            this.chkSearchProfile.Text = "Search Profile";
            this.chkSearchProfile.UseVisualStyleBackColor = true;
            // 
            // chkNewEntry
            // 
            this.chkNewEntry.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkNewEntry.Depth = 0;
            this.chkNewEntry.Location = new System.Drawing.Point(11, 95);
            this.chkNewEntry.Margin = new System.Windows.Forms.Padding(0);
            this.chkNewEntry.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkNewEntry.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkNewEntry.Name = "chkNewEntry";
            this.chkNewEntry.Ripple = true;
            this.chkNewEntry.Size = new System.Drawing.Size(160, 39);
            this.chkNewEntry.TabIndex = 90;
            this.chkNewEntry.Text = "New Entry";
            this.chkNewEntry.UseVisualStyleBackColor = true;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSelectAll.Depth = 0;
            this.chkSelectAll.Location = new System.Drawing.Point(218, 39);
            this.chkSelectAll.Margin = new System.Windows.Forms.Padding(0);
            this.chkSelectAll.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkSelectAll.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Ripple = true;
            this.chkSelectAll.Size = new System.Drawing.Size(104, 39);
            this.chkSelectAll.TabIndex = 89;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.Click += new System.EventHandler(this.chkSelectAll_Click);
            // 
            // cmbRole
            // 
            this.cmbRole.AutoResize = false;
            this.cmbRole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbRole.Depth = 0;
            this.cmbRole.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbRole.DropDownHeight = 174;
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.DropDownWidth = 121;
            this.cmbRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Hint = "Role";
            this.cmbRole.IntegralHeight = false;
            this.cmbRole.ItemHeight = 43;
            this.cmbRole.Location = new System.Drawing.Point(13, 32);
            this.cmbRole.MaxDropDownItems = 4;
            this.cmbRole.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(201, 49);
            this.cmbRole.StartIndex = 0;
            this.cmbRole.TabIndex = 88;
            this.cmbRole.UseAccent = false;
            this.cmbRole.SelectionChangeCommitted += new System.EventHandler(this.cmbRole_SelectionChangeCommitted);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.IndianRed;
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.IconChar = FontAwesome.Sharp.IconChar.TimesCircle;
            this.btnReset.IconColor = System.Drawing.Color.White;
            this.btnReset.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnReset.IconSize = 20;
            this.btnReset.Location = new System.Drawing.Point(261, 609);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(82, 35);
            this.btnReset.TabIndex = 108;
            this.btnReset.Text = "Reset";
            this.btnReset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Visible = false;
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
            this.cmbUnit.Location = new System.Drawing.Point(9, 12);
            this.cmbUnit.MaxDropDownItems = 4;
            this.cmbUnit.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(327, 49);
            this.cmbUnit.StartIndex = 0;
            this.cmbUnit.TabIndex = 86;
            this.cmbUnit.UseAccent = false;
            this.cmbUnit.SelectionChangeCommitted += new System.EventHandler(this.cmbUnit_SelectionChangeCommitted);
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
            this.cmbSubUnit.Location = new System.Drawing.Point(9, 67);
            this.cmbSubUnit.MaxDropDownItems = 4;
            this.cmbSubUnit.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbSubUnit.Name = "cmbSubUnit";
            this.cmbSubUnit.Size = new System.Drawing.Size(327, 49);
            this.cmbSubUnit.StartIndex = 0;
            this.cmbSubUnit.TabIndex = 87;
            this.cmbSubUnit.UseAccent = false;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.btnNew.IconColor = System.Drawing.Color.White;
            this.btnNew.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNew.IconSize = 20;
            this.btnNew.Location = new System.Drawing.Point(358, 609);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(90, 35);
            this.btnNew.TabIndex = 109;
            this.btnNew.Text = "New";
            this.btnNew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // dgUserList
            // 
            this.dgUserList.AllowUserToAddRows = false;
            this.dgUserList.AllowUserToDeleteRows = false;
            this.dgUserList.AllowUserToOrderColumns = true;
            this.dgUserList.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgUserList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUserList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvSerial,
            this.Id,
            this.UnitId,
            this.SubUnitId,
            this.RoleId,
            this.UnitName,
            this.SubUnitName,
            this.UserId,
            this.UserName,
            this.RoleName,
            this.AccountStatus,
            this.NewEntry,
            this.SpecialEntry,
            this.DraftRecords,
            this.FailedUploads,
            this.SearchProfile,
            this.BiometricSearch,
            this.UserManagement,
            this.Report,
            this.Settings,
            this.Update,
            this.ProfileEnrollment,
            this.NotEntry,
            this.dgvEmail,
            this.dgvPhone});
            this.dgUserList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgUserList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgUserList.Location = new System.Drawing.Point(356, 43);
            this.dgUserList.MultiSelect = false;
            this.dgUserList.Name = "dgUserList";
            this.dgUserList.ReadOnly = true;
            this.dgUserList.RowHeadersVisible = false;
            this.dgUserList.RowTemplate.DividerHeight = 2;
            this.dgUserList.RowTemplate.Height = 40;
            this.dgUserList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgUserList.Size = new System.Drawing.Size(889, 558);
            this.dgUserList.TabIndex = 112;
            this.dgUserList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgUserList_CellClick);
            this.dgUserList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgUserList_CellContentClick);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(350, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(900, 40);
            this.label3.TabIndex = 115;
            this.label3.Text = "User List";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Black;
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.IconChar = FontAwesome.Sharp.IconChar.AngleDoubleLeft;
            this.btnStart.IconColor = System.Drawing.Color.White;
            this.btnStart.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnStart.IconSize = 20;
            this.btnStart.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnStart.Location = new System.Drawing.Point(1046, 612);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(45, 32);
            this.btnStart.TabIndex = 317;
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.BackColor = System.Drawing.Color.Black;
            this.btnPrev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrev.IconChar = FontAwesome.Sharp.IconChar.ChevronLeft;
            this.btnPrev.IconColor = System.Drawing.Color.White;
            this.btnPrev.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPrev.IconSize = 20;
            this.btnPrev.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnPrev.Location = new System.Drawing.Point(1097, 612);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(45, 32);
            this.btnPrev.TabIndex = 316;
            this.btnPrev.UseVisualStyleBackColor = false;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnLast
            // 
            this.btnLast.BackColor = System.Drawing.Color.Black;
            this.btnLast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLast.FlatAppearance.BorderSize = 0;
            this.btnLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast.IconChar = FontAwesome.Sharp.IconChar.AngleDoubleRight;
            this.btnLast.IconColor = System.Drawing.Color.White;
            this.btnLast.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLast.IconSize = 20;
            this.btnLast.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLast.Location = new System.Drawing.Point(1199, 612);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(45, 32);
            this.btnLast.TabIndex = 315;
            this.btnLast.UseVisualStyleBackColor = false;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.Black;
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.IconChar = FontAwesome.Sharp.IconChar.ChevronRight;
            this.btnNext.IconColor = System.Drawing.Color.White;
            this.btnNext.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNext.IconSize = 20;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNext.Location = new System.Drawing.Point(1148, 612);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(45, 32);
            this.btnNext.TabIndex = 314;
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // labelTotalRecords
            // 
            this.labelTotalRecords.AutoSize = true;
            this.labelTotalRecords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelTotalRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalRecords.Location = new System.Drawing.Point(629, 617);
            this.labelTotalRecords.Name = "labelTotalRecords";
            this.labelTotalRecords.Size = new System.Drawing.Size(21, 22);
            this.labelTotalRecords.TabIndex = 110;
            this.labelTotalRecords.Text = "0";
            this.labelTotalRecords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(481, 617);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 20);
            this.label1.TabIndex = 109;
            this.label1.Text = "Total Record(s)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.exitButton.Size = new System.Drawing.Size(48, 40);
            this.exitButton.TabIndex = 318;
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // dgvSerial
            // 
            this.dgvSerial.HeaderText = "Serial";
            this.dgvSerial.Name = "dgvSerial";
            this.dgvSerial.ReadOnly = true;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // UnitId
            // 
            this.UnitId.HeaderText = "UnitId";
            this.UnitId.Name = "UnitId";
            this.UnitId.ReadOnly = true;
            this.UnitId.Visible = false;
            // 
            // SubUnitId
            // 
            this.SubUnitId.HeaderText = "SubUnitId";
            this.SubUnitId.Name = "SubUnitId";
            this.SubUnitId.ReadOnly = true;
            this.SubUnitId.Visible = false;
            // 
            // RoleId
            // 
            this.RoleId.HeaderText = "RoleId";
            this.RoleId.Name = "RoleId";
            this.RoleId.ReadOnly = true;
            this.RoleId.Visible = false;
            // 
            // UnitName
            // 
            this.UnitName.HeaderText = "RAB Unit";
            this.UnitName.Name = "UnitName";
            this.UnitName.ReadOnly = true;
            this.UnitName.Width = 150;
            // 
            // SubUnitName
            // 
            this.SubUnitName.HeaderText = "Sub Unit";
            this.SubUnitName.Name = "SubUnitName";
            this.SubUnitName.ReadOnly = true;
            this.SubUnitName.Width = 170;
            // 
            // UserId
            // 
            this.UserId.HeaderText = "User ID";
            this.UserId.Name = "UserId";
            this.UserId.ReadOnly = true;
            // 
            // UserName
            // 
            this.UserName.HeaderText = "Name";
            this.UserName.Name = "UserName";
            this.UserName.ReadOnly = true;
            // 
            // RoleName
            // 
            this.RoleName.HeaderText = "Role";
            this.RoleName.Name = "RoleName";
            this.RoleName.ReadOnly = true;
            // 
            // AccountStatus
            // 
            this.AccountStatus.HeaderText = "Status";
            this.AccountStatus.Name = "AccountStatus";
            this.AccountStatus.ReadOnly = true;
            this.AccountStatus.Width = 130;
            // 
            // NewEntry
            // 
            this.NewEntry.HeaderText = "New Entry";
            this.NewEntry.Name = "NewEntry";
            this.NewEntry.ReadOnly = true;
            this.NewEntry.Width = 130;
            // 
            // SpecialEntry
            // 
            this.SpecialEntry.HeaderText = "Special Entry";
            this.SpecialEntry.Name = "SpecialEntry";
            this.SpecialEntry.ReadOnly = true;
            this.SpecialEntry.Visible = false;
            // 
            // DraftRecords
            // 
            this.DraftRecords.HeaderText = "Draft Records";
            this.DraftRecords.Name = "DraftRecords";
            this.DraftRecords.ReadOnly = true;
            // 
            // FailedUploads
            // 
            this.FailedUploads.HeaderText = "Failed Uploads";
            this.FailedUploads.Name = "FailedUploads";
            this.FailedUploads.ReadOnly = true;
            // 
            // SearchProfile
            // 
            this.SearchProfile.HeaderText = "Search Profile";
            this.SearchProfile.Name = "SearchProfile";
            this.SearchProfile.ReadOnly = true;
            // 
            // BiometricSearch
            // 
            this.BiometricSearch.HeaderText = "Biometric Search";
            this.BiometricSearch.Name = "BiometricSearch";
            this.BiometricSearch.ReadOnly = true;
            // 
            // UserManagement
            // 
            this.UserManagement.HeaderText = "User Management";
            this.UserManagement.Name = "UserManagement";
            this.UserManagement.ReadOnly = true;
            this.UserManagement.Width = 130;
            // 
            // Report
            // 
            this.Report.HeaderText = "Report";
            this.Report.Name = "Report";
            this.Report.ReadOnly = true;
            this.Report.Width = 130;
            // 
            // Settings
            // 
            this.Settings.HeaderText = "Settings";
            this.Settings.Name = "Settings";
            this.Settings.ReadOnly = true;
            this.Settings.Visible = false;
            this.Settings.Width = 130;
            // 
            // Update
            // 
            this.Update.HeaderText = "Update";
            this.Update.Name = "Update";
            this.Update.ReadOnly = true;
            this.Update.Width = 130;
            // 
            // ProfileEnrollment
            // 
            this.ProfileEnrollment.HeaderText = "Profile Management";
            this.ProfileEnrollment.Name = "ProfileEnrollment";
            this.ProfileEnrollment.ReadOnly = true;
            this.ProfileEnrollment.Visible = false;
            this.ProfileEnrollment.Width = 130;
            // 
            // NotEntry
            // 
            this.NotEntry.HeaderText = "No Entry";
            this.NotEntry.Name = "NotEntry";
            this.NotEntry.ReadOnly = true;
            // 
            // dgvEmail
            // 
            this.dgvEmail.HeaderText = "dgvEmail";
            this.dgvEmail.Name = "dgvEmail";
            this.dgvEmail.ReadOnly = true;
            this.dgvEmail.Visible = false;
            // 
            // dgvPhone
            // 
            this.dgvPhone.HeaderText = "dgvPhone";
            this.dgvPhone.Name = "dgvPhone";
            this.dgvPhone.ReadOnly = true;
            this.dgvPhone.Visible = false;
            // 
            // UserManagementUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.labelTotalRecords);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgUserList);
            this.Name = "UserManagementUserControl";
            this.Size = new System.Drawing.Size(1250, 650);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgUserList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private FontAwesome.Sharp.IconButton btnRemove;
        private FontAwesome.Sharp.IconButton btnSave;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton btnNew;
        private FontAwesome.Sharp.IconButton btnReset;
        private MaterialSkin.Controls.MaterialTextBox tbConfirmPassword;
        private MaterialSkin.Controls.MaterialTextBox tbPassword;
        private MaterialSkin.Controls.MaterialTextBox tbUserId;
        private MaterialSkin.Controls.MaterialTextBox tbName;
        private System.Windows.Forms.GroupBox groupBox1;
        private MaterialSkin.Controls.MaterialCheckbox chkBiometricSearch;
        private MaterialSkin.Controls.MaterialCheckbox chkSpecialEntry;
        private MaterialSkin.Controls.MaterialCheckbox chkFailedUpload;
        private MaterialSkin.Controls.MaterialCheckbox chkDraftRecords;
        private MaterialSkin.Controls.MaterialCheckbox chkSearchProfile;
        private MaterialSkin.Controls.MaterialCheckbox chkNewEntry;
        private MaterialSkin.Controls.MaterialCheckbox chkSelectAll;
        private MaterialSkin.Controls.MaterialComboBox cmbRole;
        private MaterialSkin.Controls.MaterialComboBox cmbUnit;
        private MaterialSkin.Controls.MaterialComboBox cmbSubUnit;
        private System.Windows.Forms.DataGridView dgUserList;
        private System.Windows.Forms.Label label3;
        private MaterialSkin.Controls.MaterialCheckbox chkUpdate;
        private MaterialSkin.Controls.MaterialCheckbox chkUserManagement;
        private MaterialSkin.Controls.MaterialCheckbox chkSettings;
        private MaterialSkin.Controls.MaterialCheckbox chkReport;
        private FontAwesome.Sharp.IconButton btnStart;
        private FontAwesome.Sharp.IconButton btnPrev;
        private FontAwesome.Sharp.IconButton btnLast;
        private FontAwesome.Sharp.IconButton btnNext;
        private System.Windows.Forms.Label labelTotalRecords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private FontAwesome.Sharp.IconButton exitButton;
        private MaterialSkin.Controls.MaterialCheckbox chkProfileEnrollment;
        private MaterialSkin.Controls.MaterialCheckbox chkNotEntry;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSerial;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubUnitId;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoleId;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubUnitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserId;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn NewEntry;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpecialEntry;
        private System.Windows.Forms.DataGridViewTextBoxColumn DraftRecords;
        private System.Windows.Forms.DataGridViewTextBoxColumn FailedUploads;
        private System.Windows.Forms.DataGridViewTextBoxColumn SearchProfile;
        private System.Windows.Forms.DataGridViewTextBoxColumn BiometricSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserManagement;
        private System.Windows.Forms.DataGridViewTextBoxColumn Report;
        private System.Windows.Forms.DataGridViewTextBoxColumn Settings;
        private System.Windows.Forms.DataGridViewTextBoxColumn Update;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfileEnrollment;
        private System.Windows.Forms.DataGridViewTextBoxColumn NotEntry;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvPhone;
    }
}
