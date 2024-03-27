namespace ISTL.RAB.View
{
    partial class ManageUserControl
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
            this.gridViewEnrollment = new System.Windows.Forms.DataGridView();
            this.lblEnrollmentList = new System.Windows.Forms.Label();
            this.btnStartingPosition = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnLast = new System.Windows.Forms.Button();
            this.Sl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstNameEn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MiddleNameEn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastNameEn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MobileNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PermanentAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransactionType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AFISstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReferenceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.View = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.NID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Occupation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEnrollment)).BeginInit();
            this.SuspendLayout();
            // 
            // gridViewEnrollment
            // 
            this.gridViewEnrollment.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.gridViewEnrollment.AllowUserToAddRows = false;
            this.gridViewEnrollment.AllowUserToDeleteRows = false;
            this.gridViewEnrollment.AllowUserToResizeColumns = false;
            this.gridViewEnrollment.AllowUserToResizeRows = false;
            this.gridViewEnrollment.BackgroundColor = System.Drawing.Color.White;
            this.gridViewEnrollment.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.gridViewEnrollment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridViewEnrollment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sl,
            this.Id,
            this.FirstNameEn,
            this.MiddleNameEn,
            this.LastNameEn,
            this.MobileNumber,
            this.PermanentAddress,
            this.TransactionType,
            this.AFISstatus,
            this.ReferenceNo,
            this.View,
            this.btnEdit,
            this.NID,
            this.Occupation,
            this.Gender});
            this.gridViewEnrollment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gridViewEnrollment.ImeMode = System.Windows.Forms.ImeMode.On;
            this.gridViewEnrollment.Location = new System.Drawing.Point(80, 54);
            this.gridViewEnrollment.MultiSelect = false;
            this.gridViewEnrollment.Name = "gridViewEnrollment";
            this.gridViewEnrollment.ReadOnly = true;
            this.gridViewEnrollment.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gridViewEnrollment.RowHeadersVisible = false;
            this.gridViewEnrollment.RowTemplate.DividerHeight = 2;
            this.gridViewEnrollment.RowTemplate.ReadOnly = true;
            this.gridViewEnrollment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridViewEnrollment.ShowCellErrors = false;
            this.gridViewEnrollment.ShowEditingIcon = false;
            this.gridViewEnrollment.ShowRowErrors = false;
            this.gridViewEnrollment.Size = new System.Drawing.Size(891, 270);
            this.gridViewEnrollment.TabIndex = 577;
            this.gridViewEnrollment.TabStop = false;
            this.gridViewEnrollment.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridViewEnrollment_CellContentClick);
            // 
            // lblEnrollmentList
            // 
            this.lblEnrollmentList.AutoSize = true;
            this.lblEnrollmentList.BackColor = System.Drawing.Color.Transparent;
            this.lblEnrollmentList.Font = new System.Drawing.Font("Arial", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnrollmentList.Location = new System.Drawing.Point(74, 18);
            this.lblEnrollmentList.Name = "lblEnrollmentList";
            this.lblEnrollmentList.Size = new System.Drawing.Size(209, 33);
            this.lblEnrollmentList.TabIndex = 578;
            this.lblEnrollmentList.Text = "Enrollment List";
            // 
            // btnStartingPosition
            // 
            this.btnStartingPosition.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnStartingPosition.Location = new System.Drawing.Point(683, 351);
            this.btnStartingPosition.Name = "btnStartingPosition";
            this.btnStartingPosition.Size = new System.Drawing.Size(55, 34);
            this.btnStartingPosition.TabIndex = 579;
            this.btnStartingPosition.Text = "<<";
            this.btnStartingPosition.UseVisualStyleBackColor = false;
            this.btnStartingPosition.Click += new System.EventHandler(this.btnStartingPosition_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnPrevious.Location = new System.Drawing.Point(760, 351);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(55, 34);
            this.btnPrevious.TabIndex = 580;
            this.btnPrevious.Text = "<";
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnNext.Location = new System.Drawing.Point(839, 351);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(55, 34);
            this.btnNext.TabIndex = 581;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnLast.Location = new System.Drawing.Point(918, 351);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(55, 34);
            this.btnLast.TabIndex = 582;
            this.btnLast.Text = ">>";
            this.btnLast.UseVisualStyleBackColor = false;
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // Sl
            // 
            this.Sl.HeaderText = "#";
            this.Sl.Name = "Sl";
            this.Sl.ReadOnly = true;
            this.Sl.Width = 55;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // FirstNameEn
            // 
            this.FirstNameEn.HeaderText = "Full Name";
            this.FirstNameEn.Name = "FirstNameEn";
            this.FirstNameEn.ReadOnly = true;
            this.FirstNameEn.Width = 140;
            // 
            // MiddleNameEn
            // 
            this.MiddleNameEn.HeaderText = "Nick Name";
            this.MiddleNameEn.Name = "MiddleNameEn";
            this.MiddleNameEn.ReadOnly = true;
            this.MiddleNameEn.Width = 120;
            // 
            // LastNameEn
            // 
            this.LastNameEn.HeaderText = "Criminal Name";
            this.LastNameEn.Name = "LastNameEn";
            this.LastNameEn.ReadOnly = true;
            this.LastNameEn.Width = 120;
            // 
            // MobileNumber
            // 
            this.MobileNumber.HeaderText = "Phone Number";
            this.MobileNumber.Name = "MobileNumber";
            this.MobileNumber.ReadOnly = true;
            this.MobileNumber.Visible = false;
            // 
            // PermanentAddress
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PermanentAddress.DefaultCellStyle = dataGridViewCellStyle1;
            this.PermanentAddress.HeaderText = "Date of Birth";
            this.PermanentAddress.Name = "PermanentAddress";
            this.PermanentAddress.ReadOnly = true;
            this.PermanentAddress.Width = 80;
            // 
            // TransactionType
            // 
            this.TransactionType.HeaderText = "Transaction Type";
            this.TransactionType.Name = "TransactionType";
            this.TransactionType.ReadOnly = true;
            this.TransactionType.Width = 80;
            // 
            // AFISstatus
            // 
            this.AFISstatus.HeaderText = "AFIS Status";
            this.AFISstatus.Name = "AFISstatus";
            this.AFISstatus.ReadOnly = true;
            // 
            // ReferenceNo
            // 
            this.ReferenceNo.HeaderText = "Reference No";
            this.ReferenceNo.Name = "ReferenceNo";
            this.ReferenceNo.ReadOnly = true;
            this.ReferenceNo.Width = 140;
            // 
            // View
            // 
            this.View.HeaderText = "View";
            this.View.Name = "View";
            this.View.ReadOnly = true;
            this.View.Text = "View";
            this.View.UseColumnTextForButtonValue = true;
            this.View.Width = 50;
            // 
            // btnEdit
            // 
            this.btnEdit.HeaderText = "Edit";
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.ReadOnly = true;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseColumnTextForButtonValue = true;
            this.btnEdit.Visible = false;
            this.btnEdit.Width = 75;
            // 
            // NID
            // 
            this.NID.HeaderText = "National ID";
            this.NID.Name = "NID";
            this.NID.ReadOnly = true;
            this.NID.Visible = false;
            // 
            // Occupation
            // 
            this.Occupation.HeaderText = "Occupation";
            this.Occupation.Name = "Occupation";
            this.Occupation.ReadOnly = true;
            this.Occupation.Visible = false;
            // 
            // Gender
            // 
            this.Gender.HeaderText = "Gender";
            this.Gender.Name = "Gender";
            this.Gender.ReadOnly = true;
            this.Gender.Visible = false;
            // 
            // ManageUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ISTL.RAB.Properties.Resources.white_bg;
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.btnStartingPosition);
            this.Controls.Add(this.lblEnrollmentList);
            this.Controls.Add(this.gridViewEnrollment);
            this.Name = "ManageUserControl";
            this.Size = new System.Drawing.Size(1025, 485);
            ((System.ComponentModel.ISupportInitialize)(this.gridViewEnrollment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridViewEnrollment;
        private System.Windows.Forms.Label lblEnrollmentList;
        private System.Windows.Forms.Button btnStartingPosition;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnLast;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sl;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstNameEn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MiddleNameEn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastNameEn;
        private System.Windows.Forms.DataGridViewTextBoxColumn MobileNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn PermanentAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransactionType;
        private System.Windows.Forms.DataGridViewTextBoxColumn AFISstatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReferenceNo;
        private System.Windows.Forms.DataGridViewButtonColumn View;
        private System.Windows.Forms.DataGridViewButtonColumn btnEdit;
        private System.Windows.Forms.DataGridViewTextBoxColumn NID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Occupation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gender;
    }
}
