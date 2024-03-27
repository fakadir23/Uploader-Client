namespace ISTL.RAB.View.New.Report
{
    partial class DailyEnrollmentReportUserControl
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
            this.btnDailyEnrollReport = new System.Windows.Forms.Button();
            this.btnSummaryReport = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.materialRadioButton1 = new MaterialSkin.Controls.MaterialRadioButton();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.rrBtnFemale = new MaterialSkin.Controls.MaterialRadioButton();
            this.rdBtnMale = new MaterialSkin.Controls.MaterialRadioButton();
            this.cmbUpazilla = new MaterialSkin.Controls.MaterialComboBox();
            this.materialRadioButton2 = new MaterialSkin.Controls.MaterialRadioButton();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.label3 = new System.Windows.Forms.Label();
            this.panelButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelButton
            // 
            this.panelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.panelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelButton.Controls.Add(this.lblTitle);
            this.panelButton.Controls.Add(this.pictureBox2);
            this.panelButton.Controls.Add(this.btnDailyEnrollReport);
            this.panelButton.Controls.Add(this.btnSummaryReport);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelButton.Location = new System.Drawing.Point(0, 0);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(135, 650);
            this.panelButton.TabIndex = 7;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTitle.Location = new System.Drawing.Point(5, 115);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(125, 55);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Criminal\r\nReport";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ISTL.RAB.Properties.Resources.logo75;
            this.pictureBox2.Location = new System.Drawing.Point(5, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(125, 89);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // btnDailyEnrollReport
            // 
            this.btnDailyEnrollReport.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnDailyEnrollReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDailyEnrollReport.FlatAppearance.BorderSize = 0;
            this.btnDailyEnrollReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDailyEnrollReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDailyEnrollReport.ForeColor = System.Drawing.Color.White;
            this.btnDailyEnrollReport.Location = new System.Drawing.Point(5, 230);
            this.btnDailyEnrollReport.Name = "btnDailyEnrollReport";
            this.btnDailyEnrollReport.Size = new System.Drawing.Size(125, 79);
            this.btnDailyEnrollReport.TabIndex = 19;
            this.btnDailyEnrollReport.Text = "Daily\r\nEnrollment\r\nReport";
            this.btnDailyEnrollReport.UseVisualStyleBackColor = false;
            this.btnDailyEnrollReport.Click += new System.EventHandler(this.btnDailyEnrollReport_Click);
            // 
            // btnSummaryReport
            // 
            this.btnSummaryReport.BackColor = System.Drawing.Color.White;
            this.btnSummaryReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSummaryReport.FlatAppearance.BorderSize = 0;
            this.btnSummaryReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSummaryReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSummaryReport.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.btnSummaryReport.Location = new System.Drawing.Point(5, 315);
            this.btnSummaryReport.Name = "btnSummaryReport";
            this.btnSummaryReport.Size = new System.Drawing.Size(125, 55);
            this.btnSummaryReport.TabIndex = 17;
            this.btnSummaryReport.Text = "Summary\r\nReport";
            this.btnSummaryReport.UseVisualStyleBackColor = false;
            this.btnSummaryReport.Click += new System.EventHandler(this.btnSummaryReport_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(421, 249);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 16);
            this.label8.TabIndex = 108;
            this.label8.Text = "From";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(613, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 16);
            this.label2.TabIndex = 107;
            this.label2.Text = "To";
            // 
            // materialRadioButton1
            // 
            this.materialRadioButton1.AutoSize = true;
            this.materialRadioButton1.Depth = 0;
            this.materialRadioButton1.Location = new System.Drawing.Point(774, 241);
            this.materialRadioButton1.Margin = new System.Windows.Forms.Padding(0);
            this.materialRadioButton1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialRadioButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRadioButton1.Name = "materialRadioButton1";
            this.materialRadioButton1.Ripple = true;
            this.materialRadioButton1.Size = new System.Drawing.Size(92, 37);
            this.materialRadioButton1.TabIndex = 106;
            this.materialRadioButton1.TabStop = true;
            this.materialRadioButton1.Text = "No Date";
            this.materialRadioButton1.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(469, 242);
            this.dateTimePicker2.MinimumSize = new System.Drawing.Size(4, 30);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(125, 30);
            this.dateTimePicker2.TabIndex = 105;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(644, 242);
            this.dateTimePicker1.MinimumSize = new System.Drawing.Size(4, 30);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(127, 30);
            this.dateTimePicker1.TabIndex = 104;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(563, 292);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 18);
            this.label1.TabIndex = 123;
            this.label1.Text = "Gender";
            // 
            // rrBtnFemale
            // 
            this.rrBtnFemale.AutoSize = true;
            this.rrBtnFemale.Depth = 0;
            this.rrBtnFemale.Location = new System.Drawing.Point(707, 284);
            this.rrBtnFemale.Margin = new System.Windows.Forms.Padding(0);
            this.rrBtnFemale.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rrBtnFemale.MouseState = MaterialSkin.MouseState.HOVER;
            this.rrBtnFemale.Name = "rrBtnFemale";
            this.rrBtnFemale.Ripple = true;
            this.rrBtnFemale.Size = new System.Drawing.Size(87, 37);
            this.rrBtnFemale.TabIndex = 125;
            this.rrBtnFemale.TabStop = true;
            this.rrBtnFemale.Text = "Female";
            this.rrBtnFemale.UseVisualStyleBackColor = true;
            // 
            // rdBtnMale
            // 
            this.rdBtnMale.AutoSize = true;
            this.rdBtnMale.Depth = 0;
            this.rdBtnMale.Location = new System.Drawing.Point(629, 284);
            this.rdBtnMale.Margin = new System.Windows.Forms.Padding(0);
            this.rdBtnMale.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rdBtnMale.MouseState = MaterialSkin.MouseState.HOVER;
            this.rdBtnMale.Name = "rdBtnMale";
            this.rdBtnMale.Ripple = true;
            this.rdBtnMale.Size = new System.Drawing.Size(70, 37);
            this.rdBtnMale.TabIndex = 124;
            this.rdBtnMale.TabStop = true;
            this.rdBtnMale.Text = "Male";
            this.rdBtnMale.UseVisualStyleBackColor = true;
            // 
            // cmbUpazilla
            // 
            this.cmbUpazilla.AutoResize = false;
            this.cmbUpazilla.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbUpazilla.Depth = 0;
            this.cmbUpazilla.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbUpazilla.DropDownHeight = 174;
            this.cmbUpazilla.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUpazilla.DropDownWidth = 121;
            this.cmbUpazilla.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUpazilla.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbUpazilla.FormattingEnabled = true;
            this.cmbUpazilla.Hint = "Status";
            this.cmbUpazilla.IntegralHeight = false;
            this.cmbUpazilla.ItemHeight = 43;
            this.cmbUpazilla.Location = new System.Drawing.Point(582, 332);
            this.cmbUpazilla.MaxDropDownItems = 4;
            this.cmbUpazilla.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbUpazilla.Name = "cmbUpazilla";
            this.cmbUpazilla.Size = new System.Drawing.Size(150, 49);
            this.cmbUpazilla.StartIndex = 0;
            this.cmbUpazilla.TabIndex = 126;
            this.cmbUpazilla.UseAccent = false;
            // 
            // materialRadioButton2
            // 
            this.materialRadioButton2.AutoSize = true;
            this.materialRadioButton2.Depth = 0;
            this.materialRadioButton2.Location = new System.Drawing.Point(802, 284);
            this.materialRadioButton2.Margin = new System.Windows.Forms.Padding(0);
            this.materialRadioButton2.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialRadioButton2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRadioButton2.Name = "materialRadioButton2";
            this.materialRadioButton2.Ripple = true;
            this.materialRadioButton2.Size = new System.Drawing.Size(68, 37);
            this.materialRadioButton2.TabIndex = 128;
            this.materialRadioButton2.TabStop = true;
            this.materialRadioButton2.Text = "Both";
            this.materialRadioButton2.UseVisualStyleBackColor = true;
            // 
            // iconButton1
            // 
            this.iconButton1.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MediumSeaGreen;
            this.iconButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SeaGreen;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton1.ForeColor = System.Drawing.Color.White;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.Download;
            this.iconButton1.IconColor = System.Drawing.Color.White;
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton1.IconSize = 25;
            this.iconButton1.Location = new System.Drawing.Point(738, 332);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Size = new System.Drawing.Size(132, 49);
            this.iconButton1.TabIndex = 129;
            this.iconButton1.Text = "Generate";
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconButton1.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(421, 212);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 16);
            this.label3.TabIndex = 144;
            this.label3.Text = "Date of Entry";
            // 
            // DailyEnrollmentReportUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.iconButton1);
            this.Controls.Add(this.materialRadioButton2);
            this.Controls.Add(this.cmbUpazilla);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rrBtnFemale);
            this.Controls.Add(this.rdBtnMale);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.materialRadioButton1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.panelButton);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "DailyEnrollmentReportUserControl";
            this.Size = new System.Drawing.Size(1250, 650);
            this.panelButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnDailyEnrollReport;
        private System.Windows.Forms.Button btnSummaryReport;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private MaterialSkin.Controls.MaterialRadioButton materialRadioButton1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private MaterialSkin.Controls.MaterialRadioButton rrBtnFemale;
        private MaterialSkin.Controls.MaterialRadioButton rdBtnMale;
        private MaterialSkin.Controls.MaterialComboBox cmbUpazilla;
        private MaterialSkin.Controls.MaterialRadioButton materialRadioButton2;
        private FontAwesome.Sharp.IconButton iconButton1;
        private System.Windows.Forms.Label label3;
    }
}
