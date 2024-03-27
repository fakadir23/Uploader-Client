
namespace ISTL.RAB.View.New.Enrollment.Special
{
    partial class SearchSpecialProfileUserControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbGender = new MaterialSkin.Controls.MaterialComboBox();
            this.cmbCrimeType = new MaterialSkin.Controls.MaterialComboBox();
            this.cmbSubUnit = new MaterialSkin.Controls.MaterialComboBox();
            this.cmbUnit = new MaterialSkin.Controls.MaterialComboBox();
            this.chkEnableDateFilter = new System.Windows.Forms.CheckBox();
            this.cmbArrestType = new MaterialSkin.Controls.MaterialComboBox();
            this.btnSearch = new FontAwesome.Sharp.IconButton();
            this.tbRefNo = new MaterialSkin.Controls.MaterialTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbDistrict = new MaterialSkin.Controls.MaterialComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbUpazilla = new MaterialSkin.Controls.MaterialComboBox();
            this.cmbUnion = new MaterialSkin.Controls.MaterialComboBox();
            this.tbFullName = new MaterialSkin.Controls.MaterialTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelTotalRecords = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.lblUIName = new System.Windows.Forms.Label();
            this.btnFirst = new FontAwesome.Sharp.IconButton();
            this.btnPrev = new FontAwesome.Sharp.IconButton();
            this.btnLast = new FontAwesome.Sharp.IconButton();
            this.btnNext = new FontAwesome.Sharp.IconButton();
            this.exitButton = new FontAwesome.Sharp.IconButton();
            this.dgvSerial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvReferenceNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvFullname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvGender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCrimeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvArrestType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPreview = new System.Windows.Forms.DataGridViewButtonColumn();
            this.bcEnroll = new System.Windows.Forms.DataGridViewButtonColumn();
            this.CreatedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cmbGender);
            this.panel1.Controls.Add(this.cmbCrimeType);
            this.panel1.Controls.Add(this.cmbSubUnit);
            this.panel1.Controls.Add(this.cmbUnit);
            this.panel1.Controls.Add(this.chkEnableDateFilter);
            this.panel1.Controls.Add(this.cmbArrestType);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.tbRefNo);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.cmbDistrict);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cmbUpazilla);
            this.panel1.Controls.Add(this.cmbUnion);
            this.panel1.Controls.Add(this.tbFullName);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpFrom);
            this.panel1.Controls.Add(this.dtpTo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 650);
            this.panel1.TabIndex = 111;
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
            this.cmbGender.Location = new System.Drawing.Point(176, 290);
            this.cmbGender.MaxDropDownItems = 4;
            this.cmbGender.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.Size = new System.Drawing.Size(171, 49);
            this.cmbGender.StartIndex = 0;
            this.cmbGender.TabIndex = 165;
            this.cmbGender.UseAccent = false;
            // 
            // cmbCrimeType
            // 
            this.cmbCrimeType.AutoResize = false;
            this.cmbCrimeType.BackColor = System.Drawing.Color.White;
            this.cmbCrimeType.Depth = 0;
            this.cmbCrimeType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbCrimeType.DropDownHeight = 174;
            this.cmbCrimeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCrimeType.DropDownWidth = 450;
            this.cmbCrimeType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCrimeType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbCrimeType.FormattingEnabled = true;
            this.cmbCrimeType.Hint = "Crime Type";
            this.cmbCrimeType.IntegralHeight = false;
            this.cmbCrimeType.ItemHeight = 43;
            this.cmbCrimeType.Location = new System.Drawing.Point(9, 124);
            this.cmbCrimeType.MaxDropDownItems = 4;
            this.cmbCrimeType.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbCrimeType.Name = "cmbCrimeType";
            this.cmbCrimeType.Size = new System.Drawing.Size(338, 49);
            this.cmbCrimeType.StartIndex = 0;
            this.cmbCrimeType.TabIndex = 164;
            this.cmbCrimeType.UseAccent = false;
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
            this.cmbSubUnit.Location = new System.Drawing.Point(9, 69);
            this.cmbSubUnit.MaxDropDownItems = 4;
            this.cmbSubUnit.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbSubUnit.Name = "cmbSubUnit";
            this.cmbSubUnit.Size = new System.Drawing.Size(162, 49);
            this.cmbSubUnit.StartIndex = 0;
            this.cmbSubUnit.TabIndex = 163;
            this.cmbSubUnit.UseAccent = false;
            this.cmbSubUnit.Enter += new System.EventHandler(this.cmbSubUnit_Enter);
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
            this.cmbUnit.Location = new System.Drawing.Point(9, 10);
            this.cmbUnit.MaxDropDownItems = 4;
            this.cmbUnit.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(162, 49);
            this.cmbUnit.StartIndex = 0;
            this.cmbUnit.TabIndex = 162;
            this.cmbUnit.UseAccent = false;
            this.cmbUnit.SelectedIndexChanged += new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
            // 
            // chkEnableDateFilter
            // 
            this.chkEnableDateFilter.AutoSize = true;
            this.chkEnableDateFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEnableDateFilter.Location = new System.Drawing.Point(136, 355);
            this.chkEnableDateFilter.Name = "chkEnableDateFilter";
            this.chkEnableDateFilter.Size = new System.Drawing.Size(148, 20);
            this.chkEnableDateFilter.TabIndex = 161;
            this.chkEnableDateFilter.Text = "Enable Date && Time";
            this.chkEnableDateFilter.UseVisualStyleBackColor = true;
            this.chkEnableDateFilter.CheckedChanged += new System.EventHandler(this.chkEnableDateFilter_CheckedChanged);
            // 
            // cmbArrestType
            // 
            this.cmbArrestType.AutoResize = false;
            this.cmbArrestType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbArrestType.Depth = 0;
            this.cmbArrestType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbArrestType.DropDownHeight = 174;
            this.cmbArrestType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbArrestType.DropDownWidth = 121;
            this.cmbArrestType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbArrestType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbArrestType.FormattingEnabled = true;
            this.cmbArrestType.Hint = "Arrest Type";
            this.cmbArrestType.IntegralHeight = false;
            this.cmbArrestType.ItemHeight = 43;
            this.cmbArrestType.Location = new System.Drawing.Point(9, 179);
            this.cmbArrestType.MaxDropDownItems = 4;
            this.cmbArrestType.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbArrestType.Name = "cmbArrestType";
            this.cmbArrestType.Size = new System.Drawing.Size(338, 49);
            this.cmbArrestType.StartIndex = 0;
            this.cmbArrestType.TabIndex = 105;
            this.cmbArrestType.UseAccent = false;
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
            this.btnSearch.Location = new System.Drawing.Point(9, 473);
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
            this.tbRefNo.Location = new System.Drawing.Point(177, 11);
            this.tbRefNo.MaxLength = 50;
            this.tbRefNo.MouseState = MaterialSkin.MouseState.OUT;
            this.tbRefNo.Multiline = false;
            this.tbRefNo.Name = "tbRefNo";
            this.tbRefNo.Size = new System.Drawing.Size(170, 50);
            this.tbRefNo.TabIndex = 81;
            this.tbRefNo.Text = "";
            this.tbRefNo.TrailingIcon = null;
            this.tbRefNo.UseAccent = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(10, 390);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 16);
            this.label8.TabIndex = 103;
            this.label8.Text = "From";
            // 
            // cmbDistrict
            // 
            this.cmbDistrict.AutoResize = false;
            this.cmbDistrict.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbDistrict.Depth = 0;
            this.cmbDistrict.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbDistrict.DropDownHeight = 174;
            this.cmbDistrict.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDistrict.DropDownWidth = 150;
            this.cmbDistrict.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDistrict.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbDistrict.FormattingEnabled = true;
            this.cmbDistrict.Hint = "District";
            this.cmbDistrict.IntegralHeight = false;
            this.cmbDistrict.ItemHeight = 43;
            this.cmbDistrict.Location = new System.Drawing.Point(3, 234);
            this.cmbDistrict.MaxDropDownItems = 4;
            this.cmbDistrict.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbDistrict.Name = "cmbDistrict";
            this.cmbDistrict.Size = new System.Drawing.Size(170, 49);
            this.cmbDistrict.StartIndex = 0;
            this.cmbDistrict.TabIndex = 82;
            this.cmbDistrict.UseAccent = false;
            this.cmbDistrict.SelectedIndexChanged += new System.EventHandler(this.cmbDistrict_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(26, 426);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 16);
            this.label2.TabIndex = 102;
            this.label2.Text = "To";
            // 
            // cmbUpazilla
            // 
            this.cmbUpazilla.AutoResize = false;
            this.cmbUpazilla.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbUpazilla.Depth = 0;
            this.cmbUpazilla.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbUpazilla.DropDownHeight = 174;
            this.cmbUpazilla.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUpazilla.DropDownWidth = 150;
            this.cmbUpazilla.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUpazilla.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbUpazilla.FormattingEnabled = true;
            this.cmbUpazilla.Hint = "Upazila/Thana";
            this.cmbUpazilla.IntegralHeight = false;
            this.cmbUpazilla.ItemHeight = 43;
            this.cmbUpazilla.Location = new System.Drawing.Point(177, 234);
            this.cmbUpazilla.MaxDropDownItems = 4;
            this.cmbUpazilla.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbUpazilla.Name = "cmbUpazilla";
            this.cmbUpazilla.Size = new System.Drawing.Size(170, 49);
            this.cmbUpazilla.StartIndex = 0;
            this.cmbUpazilla.TabIndex = 83;
            this.cmbUpazilla.UseAccent = false;
            this.cmbUpazilla.SelectedIndexChanged += new System.EventHandler(this.cmbUpazilla_SelectedIndexChanged);
            this.cmbUpazilla.Enter += new System.EventHandler(this.cmbUpazilla_Enter);
            // 
            // cmbUnion
            // 
            this.cmbUnion.AutoResize = false;
            this.cmbUnion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbUnion.Depth = 0;
            this.cmbUnion.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbUnion.DropDownHeight = 174;
            this.cmbUnion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnion.DropDownWidth = 150;
            this.cmbUnion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUnion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbUnion.FormattingEnabled = true;
            this.cmbUnion.Hint = "Union/Ward";
            this.cmbUnion.IntegralHeight = false;
            this.cmbUnion.ItemHeight = 43;
            this.cmbUnion.Location = new System.Drawing.Point(3, 290);
            this.cmbUnion.MaxDropDownItems = 4;
            this.cmbUnion.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbUnion.Name = "cmbUnion";
            this.cmbUnion.Size = new System.Drawing.Size(170, 49);
            this.cmbUnion.StartIndex = 0;
            this.cmbUnion.TabIndex = 84;
            this.cmbUnion.UseAccent = false;
            this.cmbUnion.Enter += new System.EventHandler(this.cmbUnion_Enter);
            // 
            // tbFullName
            // 
            this.tbFullName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbFullName.Depth = 0;
            this.tbFullName.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbFullName.Hint = "Accused Name";
            this.tbFullName.LeadingIcon = null;
            this.tbFullName.Location = new System.Drawing.Point(177, 68);
            this.tbFullName.MaxLength = 50;
            this.tbFullName.MouseState = MaterialSkin.MouseState.OUT;
            this.tbFullName.Multiline = false;
            this.tbFullName.Name = "tbFullName";
            this.tbFullName.Size = new System.Drawing.Size(170, 50);
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
            this.groupBox1.Location = new System.Drawing.Point(29, 540);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 356);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 94;
            this.label1.Text = "Date of Creation";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.dtpFrom.Enabled = false;
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(58, 384);
            this.dtpFrom.MinimumSize = new System.Drawing.Size(4, 30);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(155, 30);
            this.dtpFrom.TabIndex = 93;
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd/MM/yyyy hh:mm tt";
            this.dtpTo.Enabled = false;
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(58, 420);
            this.dtpTo.MinimumSize = new System.Drawing.Size(4, 30);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(155, 30);
            this.dtpTo.TabIndex = 92;
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
            this.dgvGender,
            this.dgvCrimeType,
            this.dgvArrestType,
            this.dgvPreview,
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
            this.dgvList.Location = new System.Drawing.Point(358, 48);
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
            this.dgvList.TabIndex = 110;
            this.dgvList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellContentClick);
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
            this.lblUIName.Text = "Special Profile List";
            this.lblUIName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.dgvReferenceNo.Width = 150;
            // 
            // dgvFullname
            // 
            this.dgvFullname.HeaderText = "Accused Name";
            this.dgvFullname.Name = "dgvFullname";
            this.dgvFullname.ReadOnly = true;
            this.dgvFullname.Width = 200;
            // 
            // dgvGender
            // 
            this.dgvGender.HeaderText = "Gender";
            this.dgvGender.Name = "dgvGender";
            this.dgvGender.ReadOnly = true;
            // 
            // dgvCrimeType
            // 
            this.dgvCrimeType.HeaderText = "Crime Type";
            this.dgvCrimeType.Name = "dgvCrimeType";
            this.dgvCrimeType.ReadOnly = true;
            this.dgvCrimeType.Visible = false;
            this.dgvCrimeType.Width = 175;
            // 
            // dgvArrestType
            // 
            this.dgvArrestType.HeaderText = "Arrestee Type";
            this.dgvArrestType.Name = "dgvArrestType";
            this.dgvArrestType.ReadOnly = true;
            this.dgvArrestType.Width = 175;
            // 
            // dgvPreview
            // 
            this.dgvPreview.HeaderText = "Preview";
            this.dgvPreview.Name = "dgvPreview";
            this.dgvPreview.ReadOnly = true;
            // 
            // bcEnroll
            // 
            this.bcEnroll.HeaderText = "Edit";
            this.bcEnroll.Name = "bcEnroll";
            this.bcEnroll.ReadOnly = true;
            this.bcEnroll.Text = "Hello";
            this.bcEnroll.Width = 75;
            // 
            // CreatedBy
            // 
            this.CreatedBy.HeaderText = "Created By";
            this.CreatedBy.Name = "CreatedBy";
            this.CreatedBy.ReadOnly = true;
            this.CreatedBy.Visible = false;
            // 
            // SearchSpecialProfileUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.btnFirst);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnLast);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lblUIName);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvList);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SearchSpecialProfileUserControl";
            this.Size = new System.Drawing.Size(1250, 650);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelTotalRecords;

        private MaterialSkin.Controls.MaterialRadioButton hello;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton btnSearch;
        private MaterialSkin.Controls.MaterialTextBox tbRefNo;
        private System.Windows.Forms.Label label8;
        private MaterialSkin.Controls.MaterialComboBox cmbDistrict;
        private System.Windows.Forms.Label label2;
        private MaterialSkin.Controls.MaterialComboBox cmbUpazilla;
        private MaterialSkin.Controls.MaterialComboBox cmbUnion;
        private MaterialSkin.Controls.MaterialTextBox tbFullName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private MaterialSkin.Controls.MaterialComboBox materialComboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DataGridView dgvList;
        private MaterialSkin.Controls.MaterialComboBox cmbArrestType;
        private System.Windows.Forms.Label lblUIName;
        private FontAwesome.Sharp.IconButton btnFirst;
        private FontAwesome.Sharp.IconButton btnPrev;
        private FontAwesome.Sharp.IconButton btnLast;
        private FontAwesome.Sharp.IconButton btnNext;
        private FontAwesome.Sharp.IconButton exitButton;
        private System.Windows.Forms.CheckBox chkEnableDateFilter;
        private MaterialSkin.Controls.MaterialComboBox cmbUnit;
        private MaterialSkin.Controls.MaterialComboBox cmbSubUnit;
        private MaterialSkin.Controls.MaterialComboBox cmbCrimeType;
        private MaterialSkin.Controls.MaterialComboBox cmbGender;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvSerial;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvReferenceNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvFullname;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvGender;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvCrimeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvArrestType;
        private System.Windows.Forms.DataGridViewButtonColumn dgvPreview;
        private System.Windows.Forms.DataGridViewButtonColumn bcEnroll;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedBy;
    }
}
