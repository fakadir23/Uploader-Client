namespace ISTL.RAB.View
{
    partial class DefaultUserControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdateCheck = new FontAwesome.Sharp.IconButton();
            this.lblFIRUploadPendingCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblEnrolledWithBiometricCount = new System.Windows.Forms.Label();
            this.btnEnrollBioList = new FontAwesome.Sharp.IconButton();
            this.btnFIRpendingList = new FontAwesome.Sharp.IconButton();
            this.btnSyncDashboard = new FontAwesome.Sharp.IconButton();
            this.btnImport = new FontAwesome.Sharp.IconButton();
            this.btnExport = new FontAwesome.Sharp.IconButton();
            this.label4 = new System.Windows.Forms.Label();
            this.lblUserUnitSubUnit = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(75)))));
            this.label1.Location = new System.Drawing.Point(91, 50);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1481, 58);
            this.label1.TabIndex = 577;
            this.label1.Text = "SNSOP TOOLS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUpdateCheck
            // 
            this.btnUpdateCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnUpdateCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdateCheck.FlatAppearance.BorderSize = 0;
            this.btnUpdateCheck.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnUpdateCheck.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(177)))), ((int)(((byte)(231)))));
            this.btnUpdateCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateCheck.ForeColor = System.Drawing.Color.White;
            this.btnUpdateCheck.IconChar = FontAwesome.Sharp.IconChar.SyncAlt;
            this.btnUpdateCheck.IconColor = System.Drawing.Color.White;
            this.btnUpdateCheck.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnUpdateCheck.IconSize = 25;
            this.btnUpdateCheck.Location = new System.Drawing.Point(91, 362);
            this.btnUpdateCheck.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUpdateCheck.Name = "btnUpdateCheck";
            this.btnUpdateCheck.Size = new System.Drawing.Size(333, 62);
            this.btnUpdateCheck.TabIndex = 590;
            this.btnUpdateCheck.Text = "Update";
            this.btnUpdateCheck.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdateCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUpdateCheck.UseVisualStyleBackColor = false;
            this.btnUpdateCheck.Visible = false;
            this.btnUpdateCheck.Click += new System.EventHandler(this.btnUpdateCheck_Click);
            // 
            // lblFIRUploadPendingCount
            // 
            this.lblFIRUploadPendingCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFIRUploadPendingCount.ForeColor = System.Drawing.Color.Red;
            this.lblFIRUploadPendingCount.Location = new System.Drawing.Point(176, 644);
            this.lblFIRUploadPendingCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFIRUploadPendingCount.Name = "lblFIRUploadPendingCount";
            this.lblFIRUploadPendingCount.Size = new System.Drawing.Size(167, 37);
            this.lblFIRUploadPendingCount.TabIndex = 594;
            this.lblFIRUploadPendingCount.Text = "0";
            this.lblFIRUploadPendingCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFIRUploadPendingCount.Visible = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label2.Location = new System.Drawing.Point(92, 591);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(333, 28);
            this.label2.TabIndex = 595;
            this.label2.Text = "FIR Upload Pending Count";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label3.Location = new System.Drawing.Point(1196, 591);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(419, 28);
            this.label3.TabIndex = 597;
            this.label3.Text = "Enrolled Without Full Biometric Count";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Visible = false;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // lblEnrolledWithBiometricCount
            // 
            this.lblEnrolledWithBiometricCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnrolledWithBiometricCount.ForeColor = System.Drawing.Color.Red;
            this.lblEnrolledWithBiometricCount.Location = new System.Drawing.Point(1324, 644);
            this.lblEnrolledWithBiometricCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnrolledWithBiometricCount.Name = "lblEnrolledWithBiometricCount";
            this.lblEnrolledWithBiometricCount.Size = new System.Drawing.Size(167, 37);
            this.lblEnrolledWithBiometricCount.TabIndex = 596;
            this.lblEnrolledWithBiometricCount.Text = "0";
            this.lblEnrolledWithBiometricCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEnrolledWithBiometricCount.Visible = false;
            // 
            // btnEnrollBioList
            // 
            this.btnEnrollBioList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.btnEnrollBioList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnrollBioList.FlatAppearance.BorderSize = 0;
            this.btnEnrollBioList.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnEnrollBioList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(177)))), ((int)(((byte)(231)))));
            this.btnEnrollBioList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnrollBioList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnrollBioList.ForeColor = System.Drawing.Color.White;
            this.btnEnrollBioList.IconChar = FontAwesome.Sharp.IconChar.ExternalLinkAlt;
            this.btnEnrollBioList.IconColor = System.Drawing.Color.White;
            this.btnEnrollBioList.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEnrollBioList.IconSize = 20;
            this.btnEnrollBioList.Location = new System.Drawing.Point(1284, 693);
            this.btnEnrollBioList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEnrollBioList.Name = "btnEnrollBioList";
            this.btnEnrollBioList.Size = new System.Drawing.Size(233, 49);
            this.btnEnrollBioList.TabIndex = 599;
            this.btnEnrollBioList.Text = "View All";
            this.btnEnrollBioList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEnrollBioList.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnEnrollBioList.UseVisualStyleBackColor = false;
            this.btnEnrollBioList.Visible = false;
            this.btnEnrollBioList.Click += new System.EventHandler(this.btnEnrollBioList_Click);
            // 
            // btnFIRpendingList
            // 
            this.btnFIRpendingList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.btnFIRpendingList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFIRpendingList.FlatAppearance.BorderSize = 0;
            this.btnFIRpendingList.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnFIRpendingList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(177)))), ((int)(((byte)(231)))));
            this.btnFIRpendingList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFIRpendingList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFIRpendingList.ForeColor = System.Drawing.Color.White;
            this.btnFIRpendingList.IconChar = FontAwesome.Sharp.IconChar.ExternalLinkAlt;
            this.btnFIRpendingList.IconColor = System.Drawing.Color.White;
            this.btnFIRpendingList.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFIRpendingList.IconSize = 20;
            this.btnFIRpendingList.Location = new System.Drawing.Point(144, 693);
            this.btnFIRpendingList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFIRpendingList.Name = "btnFIRpendingList";
            this.btnFIRpendingList.Size = new System.Drawing.Size(233, 49);
            this.btnFIRpendingList.TabIndex = 600;
            this.btnFIRpendingList.Text = "View All";
            this.btnFIRpendingList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFIRpendingList.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnFIRpendingList.UseVisualStyleBackColor = false;
            this.btnFIRpendingList.Visible = false;
            this.btnFIRpendingList.Click += new System.EventHandler(this.btnFIRpendingList_Click);
            // 
            // btnSyncDashboard
            // 
            this.btnSyncDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(75)))));
            this.btnSyncDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSyncDashboard.FlatAppearance.BorderSize = 0;
            this.btnSyncDashboard.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(75)))));
            this.btnSyncDashboard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnSyncDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSyncDashboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSyncDashboard.ForeColor = System.Drawing.Color.White;
            this.btnSyncDashboard.IconChar = FontAwesome.Sharp.IconChar.Sync;
            this.btnSyncDashboard.IconColor = System.Drawing.Color.White;
            this.btnSyncDashboard.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSyncDashboard.IconSize = 25;
            this.btnSyncDashboard.Location = new System.Drawing.Point(713, 692);
            this.btnSyncDashboard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSyncDashboard.Name = "btnSyncDashboard";
            this.btnSyncDashboard.Size = new System.Drawing.Size(233, 49);
            this.btnSyncDashboard.TabIndex = 601;
            this.btnSyncDashboard.Text = "Sync";
            this.btnSyncDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSyncDashboard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSyncDashboard.UseVisualStyleBackColor = false;
            this.btnSyncDashboard.Visible = false;
            this.btnSyncDashboard.Click += new System.EventHandler(this.btnSyncDashboard_Click);
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(75)))));
            this.btnImport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImport.FlatAppearance.BorderSize = 0;
            this.btnImport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnImport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(177)))), ((int)(((byte)(231)))));
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.ForeColor = System.Drawing.Color.White;
            this.btnImport.IconChar = FontAwesome.Sharp.IconChar.Upload;
            this.btnImport.IconColor = System.Drawing.Color.White;
            this.btnImport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnImport.IconSize = 30;
            this.btnImport.Location = new System.Drawing.Point(91, 277);
            this.btnImport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(333, 63);
            this.btnImport.TabIndex = 602;
            this.btnImport.Text = "Import DB";
            this.btnImport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(177)))), ((int)(((byte)(231)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.IconChar = FontAwesome.Sharp.IconChar.Download;
            this.btnExport.IconColor = System.Drawing.Color.White;
            this.btnExport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExport.IconSize = 30;
            this.btnExport.Location = new System.Drawing.Point(1235, 80);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(156, 62);
            this.btnExport.TabIndex = 603;
            this.btnExport.Text = "Export DB";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Visible = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label4.Location = new System.Drawing.Point(92, 619);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(333, 25);
            this.label4.TabIndex = 604;
            this.label4.Text = "(Last 30 Days)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = false;
            // 
            // lblUserUnitSubUnit
            // 
            this.lblUserUnitSubUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserUnitSubUnit.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.lblUserUnitSubUnit.Location = new System.Drawing.Point(561, 108);
            this.lblUserUnitSubUnit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUserUnitSubUnit.Name = "lblUserUnitSubUnit";
            this.lblUserUnitSubUnit.Size = new System.Drawing.Size(531, 33);
            this.lblUserUnitSubUnit.TabIndex = 605;
            this.lblUserUnitSubUnit.Text = "Loggedin Users Unit && Subunit Name";
            this.lblUserUnitSubUnit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUserUnitSubUnit.Visible = false;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.label6.Location = new System.Drawing.Point(1239, 619);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(333, 25);
            this.label6.TabIndex = 606;
            this.label6.Text = "(Last 24 Hours)";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label6.Visible = false;
            // 
            // iconButton1
            // 
            this.iconButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(75)))));
            this.iconButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(75)))));
            this.iconButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton1.ForeColor = System.Drawing.Color.White;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.UserPlus;
            this.iconButton1.IconColor = System.Drawing.Color.White;
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton1.IconSize = 30;
            this.iconButton1.Location = new System.Drawing.Point(91, 190);
            this.iconButton1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Size = new System.Drawing.Size(333, 62);
            this.iconButton1.TabIndex = 586;
            this.iconButton1.Text = "Add New Profile";
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton1.UseVisualStyleBackColor = false;
            this.iconButton1.Visible = false;
            this.iconButton1.Click += new System.EventHandler(this.btnNewEntry_Click);
            // 
            // DefaultUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblUserUnitSubUnit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnSyncDashboard);
            this.Controls.Add(this.btnFIRpendingList);
            this.Controls.Add(this.btnEnrollBioList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblEnrolledWithBiometricCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblFIRUploadPendingCount);
            this.Controls.Add(this.btnUpdateCheck);
            this.Controls.Add(this.iconButton1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "DefaultUserControl";
            this.Size = new System.Drawing.Size(1667, 800);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton btnUpdateCheck;
        private System.Windows.Forms.Label lblFIRUploadPendingCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblEnrolledWithBiometricCount;
        private FontAwesome.Sharp.IconButton btnEnrollBioList;
        private FontAwesome.Sharp.IconButton btnFIRpendingList;
        private FontAwesome.Sharp.IconButton btnSyncDashboard;
        private FontAwesome.Sharp.IconButton btnImport;
        private FontAwesome.Sharp.IconButton btnExport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblUserUnitSubUnit;
        private System.Windows.Forms.Label label6;
        private FontAwesome.Sharp.IconButton iconButton1;
    }
}
