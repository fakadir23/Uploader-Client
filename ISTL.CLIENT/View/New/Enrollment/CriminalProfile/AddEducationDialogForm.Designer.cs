namespace ISTL.RAB.View.New.CriminalProfile
{
    partial class AddEducationDialogForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbInstituteName = new MaterialSkin.Controls.MaterialTextBox();
            this.cmbEducationStatus = new MaterialSkin.Controls.MaterialComboBox();
            this.tbRemarks = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.rdBtnNo = new MaterialSkin.Controls.MaterialRadioButton();
            this.rdBtnYes = new MaterialSkin.Controls.MaterialRadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.exitButton = new FontAwesome.Sharp.IconButton();
            this.iconBtnSave = new FontAwesome.Sharp.IconButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbInstituteName
            // 
            this.tbInstituteName.BackColor = System.Drawing.SystemColors.Window;
            this.tbInstituteName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbInstituteName.Depth = 0;
            this.tbInstituteName.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbInstituteName.Hint = "Name of Institute";
            this.tbInstituteName.LeadingIcon = null;
            this.tbInstituteName.Location = new System.Drawing.Point(12, 113);
            this.tbInstituteName.MaxLength = 50;
            this.tbInstituteName.MouseState = MaterialSkin.MouseState.OUT;
            this.tbInstituteName.Multiline = false;
            this.tbInstituteName.Name = "tbInstituteName";
            this.tbInstituteName.Size = new System.Drawing.Size(376, 50);
            this.tbInstituteName.TabIndex = 79;
            this.tbInstituteName.Text = "";
            this.tbInstituteName.TrailingIcon = null;
            this.tbInstituteName.UseAccent = false;
            // 
            // cmbEducationStatus
            // 
            this.cmbEducationStatus.AutoResize = false;
            this.cmbEducationStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbEducationStatus.Depth = 0;
            this.cmbEducationStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbEducationStatus.DropDownHeight = 174;
            this.cmbEducationStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEducationStatus.DropDownWidth = 121;
            this.cmbEducationStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEducationStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cmbEducationStatus.FormattingEnabled = true;
            this.cmbEducationStatus.Hint = "Education Status";
            this.cmbEducationStatus.IntegralHeight = false;
            this.cmbEducationStatus.ItemHeight = 43;
            this.cmbEducationStatus.Location = new System.Drawing.Point(12, 49);
            this.cmbEducationStatus.MaxDropDownItems = 4;
            this.cmbEducationStatus.MouseState = MaterialSkin.MouseState.OUT;
            this.cmbEducationStatus.Name = "cmbEducationStatus";
            this.cmbEducationStatus.Size = new System.Drawing.Size(376, 49);
            this.cmbEducationStatus.StartIndex = 0;
            this.cmbEducationStatus.TabIndex = 76;
            this.cmbEducationStatus.UseAccent = false;
            // 
            // tbRemarks
            // 
            this.tbRemarks.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbRemarks.Location = new System.Drawing.Point(12, 233);
            this.tbRemarks.Multiline = true;
            this.tbRemarks.Name = "tbRemarks";
            this.tbRemarks.Size = new System.Drawing.Size(376, 115);
            this.tbRemarks.TabIndex = 140;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(12, 210);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(70, 16);
            this.label17.TabIndex = 74;
            this.label17.Text = "Remarks";
            // 
            // rdBtnNo
            // 
            this.rdBtnNo.AutoSize = true;
            this.rdBtnNo.Depth = 0;
            this.rdBtnNo.Location = new System.Drawing.Point(327, 171);
            this.rdBtnNo.Margin = new System.Windows.Forms.Padding(0);
            this.rdBtnNo.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rdBtnNo.MouseState = MaterialSkin.MouseState.HOVER;
            this.rdBtnNo.Name = "rdBtnNo";
            this.rdBtnNo.Ripple = true;
            this.rdBtnNo.Size = new System.Drawing.Size(55, 37);
            this.rdBtnNo.TabIndex = 135;
            this.rdBtnNo.TabStop = true;
            this.rdBtnNo.Text = "No";
            this.rdBtnNo.UseVisualStyleBackColor = true;
            // 
            // rdBtnYes
            // 
            this.rdBtnYes.AutoSize = true;
            this.rdBtnYes.Depth = 0;
            this.rdBtnYes.Location = new System.Drawing.Point(259, 171);
            this.rdBtnYes.Margin = new System.Windows.Forms.Padding(0);
            this.rdBtnYes.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rdBtnYes.MouseState = MaterialSkin.MouseState.HOVER;
            this.rdBtnYes.Name = "rdBtnYes";
            this.rdBtnYes.Ripple = true;
            this.rdBtnYes.Size = new System.Drawing.Size(61, 37);
            this.rdBtnYes.TabIndex = 134;
            this.rdBtnYes.TabStop = true;
            this.rdBtnYes.Text = "Yes";
            this.rdBtnYes.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 16);
            this.label1.TabIndex = 136;
            this.label1.Text = "Political Involvement in Institution";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.iconButton1);
            this.panel1.Controls.Add(this.exitButton);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 32);
            this.panel1.TabIndex = 137;
            // 
            // iconButton1
            // 
            this.iconButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.iconButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconButton1.FlatAppearance.BorderSize = 0;
            this.iconButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.iconButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.iconButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.Minus;
            this.iconButton1.IconColor = System.Drawing.Color.White;
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton1.IconSize = 22;
            this.iconButton1.Location = new System.Drawing.Point(335, 0);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.iconButton1.Size = new System.Drawing.Size(32, 32);
            this.iconButton1.TabIndex = 158;
            this.iconButton1.UseVisualStyleBackColor = false;
            this.iconButton1.Visible = false;
            this.iconButton1.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.exitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitButton.FlatAppearance.BorderSize = 0;
            this.exitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.exitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Crimson;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.exitButton.IconColor = System.Drawing.Color.White;
            this.exitButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.exitButton.IconSize = 22;
            this.exitButton.Location = new System.Drawing.Point(368, 0);
            this.exitButton.Name = "exitButton";
            this.exitButton.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.exitButton.Size = new System.Drawing.Size(32, 32);
            this.exitButton.TabIndex = 160;
            this.exitButton.TabStop = false;
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // iconBtnSave
            // 
            this.iconBtnSave.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.iconBtnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconBtnSave.FlatAppearance.BorderSize = 0;
            this.iconBtnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.MediumSeaGreen;
            this.iconBtnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SeaGreen;
            this.iconBtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconBtnSave.ForeColor = System.Drawing.Color.White;
            this.iconBtnSave.IconChar = FontAwesome.Sharp.IconChar.Save;
            this.iconBtnSave.IconColor = System.Drawing.Color.White;
            this.iconBtnSave.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtnSave.IconSize = 22;
            this.iconBtnSave.Location = new System.Drawing.Point(288, 354);
            this.iconBtnSave.Name = "iconBtnSave";
            this.iconBtnSave.Size = new System.Drawing.Size(100, 30);
            this.iconBtnSave.TabIndex = 141;
            this.iconBtnSave.Text = "Save";
            this.iconBtnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconBtnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconBtnSave.UseVisualStyleBackColor = false;
            this.iconBtnSave.Click += new System.EventHandler(this.iconBtnSave_Click);
            // 
            // AddEducationDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MintCream;
            this.ClientSize = new System.Drawing.Size(400, 396);
            this.Controls.Add(this.iconBtnSave);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rdBtnNo);
            this.Controls.Add(this.rdBtnYes);
            this.Controls.Add(this.tbInstituteName);
            this.Controls.Add(this.cmbEducationStatus);
            this.Controls.Add(this.tbRemarks);
            this.Controls.Add(this.label17);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "AddEducationDialogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Education Information";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialTextBox tbInstituteName;
        private MaterialSkin.Controls.MaterialComboBox cmbEducationStatus;
        private System.Windows.Forms.TextBox tbRemarks;
        private System.Windows.Forms.Label label17;
        private MaterialSkin.Controls.MaterialRadioButton rdBtnNo;
        private MaterialSkin.Controls.MaterialRadioButton rdBtnYes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton iconButton1;
        private FontAwesome.Sharp.IconButton exitButton;
        private FontAwesome.Sharp.IconButton iconBtnSave;
    }
}