
namespace ISTL.RAB.View.New.Enrollment.NotEntry
{
    partial class NotEntryUploadPendingUserControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblUIName = new System.Windows.Forms.Label();
            this.exitButton = new FontAwesome.Sharp.IconButton();
            this.btnFirst = new FontAwesome.Sharp.IconButton();
            this.btnPrev = new FontAwesome.Sharp.IconButton();
            this.btnLast = new FontAwesome.Sharp.IconButton();
            this.btnNext = new FontAwesome.Sharp.IconButton();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.dgvSerial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvReferenceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFullname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCaseType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvMobileNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bcEnroll = new System.Windows.Forms.DataGridViewButtonColumn();
            this.CreatedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbWarrantNo = new MaterialSkin.Controls.MaterialTextBox();
            this.tbMobileNo = new MaterialSkin.Controls.MaterialTextBox();
            this.tbFatherName = new MaterialSkin.Controls.MaterialTextBox();
            this.cmbCaseType = new MaterialSkin.Controls.MaterialComboBox();
            this.cmbNotEntryReason = new MaterialSkin.Controls.MaterialComboBox();
            this.btnSearch = new FontAwesome.Sharp.IconButton();
            this.tbRefNo = new MaterialSkin.Controls.MaterialTextBox();
            this.tbFullName = new MaterialSkin.Controls.MaterialTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelTotalRecords = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUIName
            // 
            this.lblUIName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblUIName.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUIName.ForeColor = System.Drawing.Color.White;
            this.lblUIName.Location = new System.Drawing.Point(358, 0);
            this.lblUIName.Name = "lblUIName";
            this.lblUIName.Size = new System.Drawing.Size(892, 45);
            this.lblUIName.TabIndex = 309;
            this.lblUIName.Text = "Upload Pending No Entry Profile List";
            this.lblUIName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.exitButton.TabIndex = 314;
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.BackColor = System.Drawing.Color.Black;
            this.btnFirst.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFirst.FlatAppearance.BorderSize = 0;
            this.btnFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFirst.IconChar = FontAwesome.Sharp.IconChar.AngleDoubleLeft;
            this.btnFirst.IconColor = System.Drawing.Color.White;
            this.btnFirst.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFirst.IconSize = 20;
            this.btnFirst.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFirst.Location = new System.Drawing.Point(1049, 615);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(45, 32);
            this.btnFirst.TabIndex = 313;
            this.btnFirst.UseVisualStyleBackColor = false;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
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
            this.btnPrev.Location = new System.Drawing.Point(1100, 615);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(45, 32);
            this.btnPrev.TabIndex = 312;
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
            this.btnLast.Location = new System.Drawing.Point(1202, 615);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(45, 32);
            this.btnLast.TabIndex = 311;
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
            this.btnNext.Location = new System.Drawing.Point(1151, 615);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(45, 32);
            this.btnNext.TabIndex = 310;
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvSerial,
            this.dgvReferenceNo,
            this.dgvFullname,
            this.dgvReason,
            this.dgvCaseType,
            this.dgvMobileNo,
            this.bcEnroll,
            this.CreatedBy});
            this.dgvList.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvList.Location = new System.Drawing.Point(359, 48);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvList.RowTemplate.DividerHeight = 2;
            this.dgvList.RowTemplate.Height = 35;
            this.dgvList.RowTemplate.ReadOnly = true;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvList.Size = new System.Drawing.Size(888, 561);
            this.dgvList.TabIndex = 316;
            // 
            // dgvSerial
            // 
            this.dgvSerial.HeaderText = "Serial";
            this.dgvSerial.Name = "dgvSerial";
            this.dgvSerial.ReadOnly = true;
            this.dgvSerial.Width = 75;
            // 
            // dgvReferenceNo
            // 
            this.dgvReferenceNo.HeaderText = "Reference No";
            this.dgvReferenceNo.Name = "dgvReferenceNo";
            this.dgvReferenceNo.ReadOnly = true;
            this.dgvReferenceNo.Width = 135;
            // 
            // dgvFullname
            // 
            this.dgvFullname.HeaderText = "Accused Name";
            this.dgvFullname.Name = "dgvFullname";
            this.dgvFullname.ReadOnly = true;
            this.dgvFullname.Width = 175;
            // 
            // dgvReason
            // 
            this.dgvReason.HeaderText = "Reason";
            this.dgvReason.Name = "dgvReason";
            this.dgvReason.ReadOnly = true;
            this.dgvReason.Width = 165;
            // 
            // dgvCaseType
            // 
            this.dgvCaseType.HeaderText = "Case Type";
            this.dgvCaseType.Name = "dgvCaseType";
            this.dgvCaseType.ReadOnly = true;
            this.dgvCaseType.Width = 150;
            // 
            // dgvMobileNo
            // 
            this.dgvMobileNo.HeaderText = "Mobile No";
            this.dgvMobileNo.Name = "dgvMobileNo";
            this.dgvMobileNo.ReadOnly = true;
            this.dgvMobileNo.Width = 125;
            // 
            // bcEnroll
            // 
            this.bcEnroll.HeaderText = "Edit";
            this.bcEnroll.Name = "bcEnroll";
            this.bcEnroll.ReadOnly = true;
            this.bcEnroll.Text = "Hello";
            this.bcEnroll.Visible = false;
            this.bcEnroll.Width = 75;
            // 
            // CreatedBy
            // 
            this.CreatedBy.HeaderText = "Created By";
            this.CreatedBy.Name = "CreatedBy";
            this.CreatedBy.ReadOnly = true;
            this.CreatedBy.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tbWarrantNo);
            this.panel1.Controls.Add(this.tbMobileNo);
            this.panel1.Controls.Add(this.tbFatherName);
            this.panel1.Controls.Add(this.cmbCaseType);
            this.panel1.Controls.Add(this.cmbNotEntryReason);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.tbRefNo);
            this.panel1.Controls.Add(this.tbFullName);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 650);
            this.panel1.TabIndex = 317;
            // 
            // tbWarrantNo
            // 
            this.tbWarrantNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbWarrantNo.Depth = 0;
            this.tbWarrantNo.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbWarrantNo.Hint = "Warrant No";
            this.tbWarrantNo.LeadingIcon = null;
            this.tbWarrantNo.Location = new System.Drawing.Point(9, 361);
            this.tbWarrantNo.MaxLength = 50;
            this.tbWarrantNo.MouseState = MaterialSkin.MouseState.OUT;
            this.tbWarrantNo.Multiline = false;
            this.tbWarrantNo.Name = "tbWarrantNo";
            this.tbWarrantNo.Size = new System.Drawing.Size(334, 50);
            this.tbWarrantNo.TabIndex = 650;
            this.tbWarrantNo.Text = "";
            this.tbWarrantNo.TrailingIcon = null;
            this.tbWarrantNo.UseAccent = false;
            // 
            // tbMobileNo
            // 
            this.tbMobileNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMobileNo.Depth = 0;
            this.tbMobileNo.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbMobileNo.Hint = "Mobile No";
            this.tbMobileNo.LeadingIcon = null;
            this.tbMobileNo.Location = new System.Drawing.Point(9, 305);
            this.tbMobileNo.MaxLength = 50;
            this.tbMobileNo.MouseState = MaterialSkin.MouseState.OUT;
            this.tbMobileNo.Multiline = false;
            this.tbMobileNo.Name = "tbMobileNo";
            this.tbMobileNo.Size = new System.Drawing.Size(334, 50);
            this.tbMobileNo.TabIndex = 649;
            this.tbMobileNo.Text = "";
            this.tbMobileNo.TrailingIcon = null;
            this.tbMobileNo.UseAccent = false;
            // 
            // tbFatherName
            // 
            this.tbFatherName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbFatherName.Depth = 0;
            this.tbFatherName.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbFatherName.Hint = "Father Name";
            this.tbFatherName.LeadingIcon = null;
            this.tbFatherName.Location = new System.Drawing.Point(8, 249);
            this.tbFatherName.MaxLength = 50;
            this.tbFatherName.MouseState = MaterialSkin.MouseState.OUT;
            this.tbFatherName.Multiline = false;
            this.tbFatherName.Name = "tbFatherName";
            this.tbFatherName.Size = new System.Drawing.Size(335, 50);
            this.tbFatherName.TabIndex = 648;
            this.tbFatherName.Text = "";
            this.tbFatherName.TrailingIcon = null;
            this.tbFatherName.UseAccent = false;
            // 
            // cmbCaseType
            // 
            this.cmbCaseType.AutoResize = false;
            this.cmbCaseType.BackColor = System.Drawing.Color.White;
            this.cmbCaseType.Depth = 0;
            this.cmbCaseType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbCaseType.DropDownHeight = 174;
            this.cmbCaseType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCaseType.DropDownWidth = 200;
            this.cmbCaseType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCaseType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbCaseType.FormattingEnabled = true;
            this.cmbCaseType.Hint = "Case Type";
            this.cmbCaseType.IntegralHeight = false;
            this.cmbCaseType.ItemHeight = 43;
            this.cmbCaseType.Location = new System.Drawing.Point(9, 136);
            this.cmbCaseType.MaxDropDownItems = 4;
            this.cmbCaseType.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbCaseType.Name = "cmbCaseType";
            this.cmbCaseType.Size = new System.Drawing.Size(334, 49);
            this.cmbCaseType.StartIndex = 0;
            this.cmbCaseType.TabIndex = 164;
            this.cmbCaseType.UseAccent = false;
            // 
            // cmbNotEntryReason
            // 
            this.cmbNotEntryReason.AutoResize = false;
            this.cmbNotEntryReason.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbNotEntryReason.Depth = 0;
            this.cmbNotEntryReason.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbNotEntryReason.DropDownHeight = 174;
            this.cmbNotEntryReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNotEntryReason.DropDownWidth = 250;
            this.cmbNotEntryReason.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbNotEntryReason.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbNotEntryReason.FormattingEnabled = true;
            this.cmbNotEntryReason.Hint = "No Entry Reason";
            this.cmbNotEntryReason.IntegralHeight = false;
            this.cmbNotEntryReason.ItemHeight = 43;
            this.cmbNotEntryReason.Location = new System.Drawing.Point(8, 191);
            this.cmbNotEntryReason.MaxDropDownItems = 4;
            this.cmbNotEntryReason.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbNotEntryReason.Name = "cmbNotEntryReason";
            this.cmbNotEntryReason.Size = new System.Drawing.Size(334, 49);
            this.cmbNotEntryReason.StartIndex = 0;
            this.cmbNotEntryReason.TabIndex = 105;
            this.cmbNotEntryReason.UseAccent = false;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.IconChar = FontAwesome.Sharp.IconChar.Search;
            this.btnSearch.IconColor = System.Drawing.Color.White;
            this.btnSearch.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSearch.IconSize = 25;
            this.btnSearch.Location = new System.Drawing.Point(9, 438);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(338, 52);
            this.btnSearch.TabIndex = 104;
            this.btnSearch.Text = "Search";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tbRefNo
            // 
            this.tbRefNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbRefNo.Depth = 0;
            this.tbRefNo.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbRefNo.Hint = "Reference Number";
            this.tbRefNo.LeadingIcon = null;
            this.tbRefNo.Location = new System.Drawing.Point(9, 24);
            this.tbRefNo.MaxLength = 50;
            this.tbRefNo.MouseState = MaterialSkin.MouseState.OUT;
            this.tbRefNo.Multiline = false;
            this.tbRefNo.Name = "tbRefNo";
            this.tbRefNo.Size = new System.Drawing.Size(334, 50);
            this.tbRefNo.TabIndex = 81;
            this.tbRefNo.Text = "";
            this.tbRefNo.TrailingIcon = null;
            this.tbRefNo.UseAccent = false;
            // 
            // tbFullName
            // 
            this.tbFullName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbFullName.Depth = 0;
            this.tbFullName.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbFullName.Hint = "Accused Name";
            this.tbFullName.LeadingIcon = null;
            this.tbFullName.Location = new System.Drawing.Point(9, 80);
            this.tbFullName.MaxLength = 50;
            this.tbFullName.MouseState = MaterialSkin.MouseState.OUT;
            this.tbFullName.Multiline = false;
            this.tbFullName.Name = "tbFullName";
            this.tbFullName.Size = new System.Drawing.Size(334, 50);
            this.tbFullName.TabIndex = 85;
            this.tbFullName.Text = "";
            this.tbFullName.TrailingIcon = null;
            this.tbFullName.UseAccent = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.labelTotalRecords);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(29, 505);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 78);
            this.groupBox1.TabIndex = 99;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Summary";
            // 
            // labelTotalRecords
            // 
            this.labelTotalRecords.AutoSize = true;
            this.labelTotalRecords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelTotalRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalRecords.Location = new System.Drawing.Point(125, 48);
            this.labelTotalRecords.Name = "labelTotalRecords";
            this.labelTotalRecords.Size = new System.Drawing.Size(18, 18);
            this.labelTotalRecords.TabIndex = 105;
            this.labelTotalRecords.Text = "0";
            this.labelTotalRecords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(91, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 15);
            this.label3.TabIndex = 101;
            this.label3.Text = "Total Record(s)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NotEntryUploadPendingUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lblUIName);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "NotEntryUploadPendingUserControl";
            this.Size = new System.Drawing.Size(1250, 650);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialRadioButton hello;
        private System.Windows.Forms.Label label7;
        private MaterialSkin.Controls.MaterialComboBox materialComboBox1;
        private System.Windows.Forms.Label lblUIName;
        private FontAwesome.Sharp.IconButton btnFirst;
        private FontAwesome.Sharp.IconButton btnPrev;
        private FontAwesome.Sharp.IconButton btnLast;
        private FontAwesome.Sharp.IconButton btnNext;
        private FontAwesome.Sharp.IconButton exitButton;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Panel panel1;
        private MaterialSkin.Controls.MaterialTextBox tbWarrantNo;
        private MaterialSkin.Controls.MaterialTextBox tbMobileNo;
        private MaterialSkin.Controls.MaterialTextBox tbFatherName;
        private MaterialSkin.Controls.MaterialComboBox cmbCaseType;
        private MaterialSkin.Controls.MaterialComboBox cmbNotEntryReason;
        private FontAwesome.Sharp.IconButton btnSearch;
        private MaterialSkin.Controls.MaterialTextBox tbRefNo;
        private MaterialSkin.Controls.MaterialTextBox tbFullName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelTotalRecords;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSerial;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvReferenceNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFullname;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvReason;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCaseType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvMobileNo;
        private System.Windows.Forms.DataGridViewButtonColumn bcEnroll;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedBy;
    }
}
