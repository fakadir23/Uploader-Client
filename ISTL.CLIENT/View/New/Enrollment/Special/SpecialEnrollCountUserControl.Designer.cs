
namespace ISTL.RAB.View.New.Enrollment.Special
{
    partial class SpecialEnrollCountUserControl
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
            this.tbMobileCourtCount = new MaterialSkin.Controls.MaterialTextBox();
            this.tbDirectSubmitPSCount = new MaterialSkin.Controls.MaterialTextBox();
            this.tbContagiousCount = new MaterialSkin.Controls.MaterialTextBox();
            this.btnSave = new FontAwesome.Sharp.IconButton();
            this.dtpCountDate = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.tableResult = new System.Windows.Forms.TableLayoutPanel();
            this.tbCPCount = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tbDirectSubmitCount = new System.Windows.Forms.TextBox();
            this.tbMBCount = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.exitButton = new FontAwesome.Sharp.IconButton();
            this.panelButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tableResult.SuspendLayout();
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
            this.panelButton.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTitle.Location = new System.Drawing.Point(5, 282);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(125, 86);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Special Enrollment Count";
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
            // tbMobileCourtCount
            // 
            this.tbMobileCourtCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMobileCourtCount.Depth = 0;
            this.tbMobileCourtCount.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbMobileCourtCount.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tbMobileCourtCount.Hint = "Mobile Court Count";
            this.tbMobileCourtCount.LeadingIcon = null;
            this.tbMobileCourtCount.Location = new System.Drawing.Point(305, 214);
            this.tbMobileCourtCount.MaxLength = 100;
            this.tbMobileCourtCount.MouseState = MaterialSkin.MouseState.OUT;
            this.tbMobileCourtCount.Multiline = false;
            this.tbMobileCourtCount.Name = "tbMobileCourtCount";
            this.tbMobileCourtCount.Size = new System.Drawing.Size(250, 50);
            this.tbMobileCourtCount.TabIndex = 118;
            this.tbMobileCourtCount.Text = "";
            this.tbMobileCourtCount.TrailingIcon = null;
            this.tbMobileCourtCount.UseAccent = false;
            this.tbMobileCourtCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbMobileCourtCount_KeyDown);
            this.tbMobileCourtCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMobileCourtCount_KeyPress);
            // 
            // tbDirectSubmitPSCount
            // 
            this.tbDirectSubmitPSCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbDirectSubmitPSCount.Depth = 0;
            this.tbDirectSubmitPSCount.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbDirectSubmitPSCount.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tbDirectSubmitPSCount.Hint = "Direct Submit in PS Count";
            this.tbDirectSubmitPSCount.LeadingIcon = null;
            this.tbDirectSubmitPSCount.Location = new System.Drawing.Point(305, 332);
            this.tbDirectSubmitPSCount.MaxLength = 100;
            this.tbDirectSubmitPSCount.MouseState = MaterialSkin.MouseState.OUT;
            this.tbDirectSubmitPSCount.Multiline = false;
            this.tbDirectSubmitPSCount.Name = "tbDirectSubmitPSCount";
            this.tbDirectSubmitPSCount.Size = new System.Drawing.Size(250, 50);
            this.tbDirectSubmitPSCount.TabIndex = 122;
            this.tbDirectSubmitPSCount.Text = "";
            this.tbDirectSubmitPSCount.TrailingIcon = null;
            this.tbDirectSubmitPSCount.UseAccent = false;
            this.tbDirectSubmitPSCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbGamblerCount_KeyDown);
            this.tbDirectSubmitPSCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbGamblerCount_KeyPress);
            // 
            // tbContagiousCount
            // 
            this.tbContagiousCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbContagiousCount.Depth = 0;
            this.tbContagiousCount.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbContagiousCount.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.tbContagiousCount.Hint = "Contagious Patient Count";
            this.tbContagiousCount.LeadingIcon = null;
            this.tbContagiousCount.Location = new System.Drawing.Point(305, 270);
            this.tbContagiousCount.MaxLength = 100;
            this.tbContagiousCount.MouseState = MaterialSkin.MouseState.OUT;
            this.tbContagiousCount.Multiline = false;
            this.tbContagiousCount.Name = "tbContagiousCount";
            this.tbContagiousCount.Size = new System.Drawing.Size(250, 50);
            this.tbContagiousCount.TabIndex = 121;
            this.tbContagiousCount.Text = "";
            this.tbContagiousCount.TrailingIcon = null;
            this.tbContagiousCount.UseAccent = false;
            this.tbContagiousCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbContagiousCount_KeyDown);
            this.tbContagiousCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbContagiousCount_KeyPress);
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
            this.btnSave.Location = new System.Drawing.Point(305, 465);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(250, 45);
            this.btnSave.TabIndex = 123;
            this.btnSave.Text = "Submit";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dtpCountDate
            // 
            this.dtpCountDate.CustomFormat = "dd/MM/yyyy";
            this.dtpCountDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCountDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCountDate.Location = new System.Drawing.Point(305, 156);
            this.dtpCountDate.MinimumSize = new System.Drawing.Size(4, 49);
            this.dtpCountDate.Name = "dtpCountDate";
            this.dtpCountDate.Size = new System.Drawing.Size(250, 49);
            this.dtpCountDate.TabIndex = 202;
            this.dtpCountDate.ValueChanged += new System.EventHandler(this.dtpCountDate_ValueChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(301, 118);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 24);
            this.label15.TabIndex = 201;
            this.label15.Text = "Date";
            // 
            // tableResult
            // 
            this.tableResult.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableResult.ColumnCount = 2;
            this.tableResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableResult.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableResult.Controls.Add(this.tbCPCount, 1, 1);
            this.tableResult.Controls.Add(this.label20, 0, 1);
            this.tableResult.Controls.Add(this.tbDirectSubmitCount, 1, 2);
            this.tableResult.Controls.Add(this.tbMBCount, 1, 0);
            this.tableResult.Controls.Add(this.label38, 0, 0);
            this.tableResult.Controls.Add(this.label23, 0, 2);
            this.tableResult.Location = new System.Drawing.Point(606, 214);
            this.tableResult.Name = "tableResult";
            this.tableResult.RowCount = 3;
            this.tableResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableResult.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableResult.Size = new System.Drawing.Size(476, 169);
            this.tableResult.TabIndex = 203;
            // 
            // tbCPCount
            // 
            this.tbCPCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbCPCount.BackColor = System.Drawing.Color.White;
            this.tbCPCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbCPCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCPCount.Location = new System.Drawing.Point(264, 72);
            this.tbCPCount.Name = "tbCPCount";
            this.tbCPCount.ReadOnly = true;
            this.tbCPCount.Size = new System.Drawing.Size(184, 24);
            this.tbCPCount.TabIndex = 172;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(4, 57);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(230, 55);
            this.label20.TabIndex = 41;
            this.label20.Text = "Contagious Patient Count";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDirectSubmitCount
            // 
            this.tbDirectSubmitCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbDirectSubmitCount.BackColor = System.Drawing.Color.White;
            this.tbDirectSubmitCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbDirectSubmitCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDirectSubmitCount.Location = new System.Drawing.Point(263, 128);
            this.tbDirectSubmitCount.Margin = new System.Windows.Forms.Padding(2);
            this.tbDirectSubmitCount.Name = "tbDirectSubmitCount";
            this.tbDirectSubmitCount.ReadOnly = true;
            this.tbDirectSubmitCount.Size = new System.Drawing.Size(186, 24);
            this.tbDirectSubmitCount.TabIndex = 153;
            // 
            // tbMBCount
            // 
            this.tbMBCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbMBCount.BackColor = System.Drawing.Color.White;
            this.tbMBCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbMBCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMBCount.Location = new System.Drawing.Point(264, 16);
            this.tbMBCount.Name = "tbMBCount";
            this.tbMBCount.Size = new System.Drawing.Size(184, 24);
            this.tbMBCount.TabIndex = 171;
            // 
            // label38
            // 
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(4, 1);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(230, 55);
            this.label38.TabIndex = 42;
            this.label38.Text = "Mobile Court Count";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(4, 113);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(230, 55);
            this.label23.TabIndex = 7;
            this.label23.Text = "Direct Submit in PS Count";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.exitButton.TabIndex = 311;
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // SpecialEnrollCountUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.tableResult);
            this.Controls.Add(this.dtpCountDate);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbContagiousCount);
            this.Controls.Add(this.tbDirectSubmitPSCount);
            this.Controls.Add(this.tbMobileCourtCount);
            this.Controls.Add(this.panelButton);
            this.Name = "SpecialEnrollCountUserControl";
            this.Size = new System.Drawing.Size(1250, 650);
            this.panelButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tableResult.ResumeLayout(false);
            this.tableResult.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pictureBox2;
        private MaterialSkin.Controls.MaterialTextBox tbMobileCourtCount;
        private MaterialSkin.Controls.MaterialTextBox tbDirectSubmitPSCount;
        private MaterialSkin.Controls.MaterialTextBox tbContagiousCount;
        private FontAwesome.Sharp.IconButton btnSave;
        private System.Windows.Forms.DateTimePicker dtpCountDate;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TableLayoutPanel tableResult;
        private System.Windows.Forms.TextBox tbCPCount;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tbDirectSubmitCount;
        private System.Windows.Forms.TextBox tbMBCount;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label23;
        private FontAwesome.Sharp.IconButton exitButton;
    }
}
