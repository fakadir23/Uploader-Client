namespace ISTL.RAB.View.New.FamilyAlliesFoes
{
    partial class OtherInformationUserControl
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
            this.dgvOtherInfo = new System.Windows.Forms.DataGridView();
            this.Union = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VillageRoadHouseNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelButton = new System.Windows.Forms.Panel();
            this.btnPreviewSubmit = new System.Windows.Forms.Button();
            this.btnBiometric = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnCriminalProfile = new System.Windows.Forms.Button();
            this.btnOtherInfo = new System.Windows.Forms.Button();
            this.btnEdit = new FontAwesome.Sharp.IconButton();
            this.btnRemove = new FontAwesome.Sharp.IconButton();
            this.btnAdd = new FontAwesome.Sharp.IconButton();
            this.tbTitleValue = new MaterialSkin.Controls.MaterialTextBox();
            this.tbTitleName = new MaterialSkin.Controls.MaterialTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOtherInfo)).BeginInit();
            this.panelButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOtherInfo
            // 
            this.dgvOtherInfo.AllowUserToAddRows = false;
            this.dgvOtherInfo.AllowUserToDeleteRows = false;
            this.dgvOtherInfo.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOtherInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvOtherInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOtherInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Union,
            this.VillageRoadHouseNo});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOtherInfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvOtherInfo.Location = new System.Drawing.Point(141, 61);
            this.dgvOtherInfo.Name = "dgvOtherInfo";
            this.dgvOtherInfo.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOtherInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvOtherInfo.RowHeadersVisible = false;
            this.dgvOtherInfo.RowTemplate.DividerHeight = 2;
            this.dgvOtherInfo.RowTemplate.Height = 35;
            this.dgvOtherInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOtherInfo.Size = new System.Drawing.Size(1106, 586);
            this.dgvOtherInfo.TabIndex = 115;
            // 
            // Union
            // 
            this.Union.HeaderText = "Title Name";
            this.Union.Name = "Union";
            this.Union.ReadOnly = true;
            this.Union.Width = 400;
            // 
            // VillageRoadHouseNo
            // 
            this.VillageRoadHouseNo.HeaderText = "Title Value";
            this.VillageRoadHouseNo.Name = "VillageRoadHouseNo";
            this.VillageRoadHouseNo.ReadOnly = true;
            this.VillageRoadHouseNo.Width = 700;
            // 
            // panelButton
            // 
            this.panelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.panelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelButton.Controls.Add(this.btnPreviewSubmit);
            this.panelButton.Controls.Add(this.btnBiometric);
            this.panelButton.Controls.Add(this.lblTitle);
            this.panelButton.Controls.Add(this.pictureBox2);
            this.panelButton.Controls.Add(this.btnCriminalProfile);
            this.panelButton.Controls.Add(this.btnOtherInfo);
            this.panelButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelButton.Location = new System.Drawing.Point(0, 0);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(135, 650);
            this.panelButton.TabIndex = 9;
            // 
            // btnPreviewSubmit
            // 
            this.btnPreviewSubmit.BackColor = System.Drawing.Color.White;
            this.btnPreviewSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPreviewSubmit.FlatAppearance.BorderSize = 0;
            this.btnPreviewSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviewSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPreviewSubmit.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.btnPreviewSubmit.Location = new System.Drawing.Point(5, 590);
            this.btnPreviewSubmit.Name = "btnPreviewSubmit";
            this.btnPreviewSubmit.Size = new System.Drawing.Size(125, 55);
            this.btnPreviewSubmit.TabIndex = 22;
            this.btnPreviewSubmit.Text = "Preview\r\nand Submit";
            this.btnPreviewSubmit.UseVisualStyleBackColor = false;
            this.btnPreviewSubmit.Click += new System.EventHandler(this.btnPreviewSubmit_Click);
            // 
            // btnBiometric
            // 
            this.btnBiometric.BackColor = System.Drawing.Color.White;
            this.btnBiometric.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBiometric.FlatAppearance.BorderSize = 0;
            this.btnBiometric.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBiometric.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBiometric.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.btnBiometric.Location = new System.Drawing.Point(5, 169);
            this.btnBiometric.Name = "btnBiometric";
            this.btnBiometric.Size = new System.Drawing.Size(125, 55);
            this.btnBiometric.TabIndex = 21;
            this.btnBiometric.Text = "Biometric\r\nInformation";
            this.btnBiometric.UseVisualStyleBackColor = false;
            this.btnBiometric.Click += new System.EventHandler(this.btnBiometric_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTitle.Location = new System.Drawing.Point(5, 115);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(125, 40);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "Dynamic\r\nData";
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
            // btnCriminalProfile
            // 
            this.btnCriminalProfile.BackColor = System.Drawing.Color.White;
            this.btnCriminalProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCriminalProfile.FlatAppearance.BorderSize = 0;
            this.btnCriminalProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCriminalProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCriminalProfile.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.btnCriminalProfile.Location = new System.Drawing.Point(5, 230);
            this.btnCriminalProfile.Name = "btnCriminalProfile";
            this.btnCriminalProfile.Size = new System.Drawing.Size(125, 55);
            this.btnCriminalProfile.TabIndex = 18;
            this.btnCriminalProfile.Text = "Criminal\r\nProfile";
            this.btnCriminalProfile.UseVisualStyleBackColor = false;
            this.btnCriminalProfile.Click += new System.EventHandler(this.btnCriminalProfile_Click);
            // 
            // btnOtherInfo
            // 
            this.btnOtherInfo.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnOtherInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOtherInfo.FlatAppearance.BorderSize = 0;
            this.btnOtherInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOtherInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOtherInfo.ForeColor = System.Drawing.Color.White;
            this.btnOtherInfo.Location = new System.Drawing.Point(5, 291);
            this.btnOtherInfo.Name = "btnOtherInfo";
            this.btnOtherInfo.Size = new System.Drawing.Size(125, 55);
            this.btnOtherInfo.TabIndex = 17;
            this.btnOtherInfo.Text = "Other\r\nInformation";
            this.btnOtherInfo.UseVisualStyleBackColor = false;
            this.btnOtherInfo.Click += new System.EventHandler(this.btnFamily_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.IconChar = FontAwesome.Sharp.IconChar.Edit;
            this.btnEdit.IconColor = System.Drawing.Color.White;
            this.btnEdit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEdit.IconSize = 22;
            this.btnEdit.Location = new System.Drawing.Point(1080, 5);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 50);
            this.btnEdit.TabIndex = 113;
            this.btnEdit.Text = "Edit";
            this.btnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEditFamily_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.IndianRed;
            this.btnRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.ForeColor = System.Drawing.Color.White;
            this.btnRemove.IconChar = FontAwesome.Sharp.IconChar.TimesCircle;
            this.btnRemove.IconColor = System.Drawing.Color.White;
            this.btnRemove.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRemove.IconSize = 22;
            this.btnRemove.Location = new System.Drawing.Point(1161, 5);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(86, 50);
            this.btnRemove.TabIndex = 114;
            this.btnRemove.Text = "Remove";
            this.btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemoveFamily_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.btnAdd.IconColor = System.Drawing.Color.White;
            this.btnAdd.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAdd.IconSize = 22;
            this.btnAdd.Location = new System.Drawing.Point(999, 5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 50);
            this.btnAdd.TabIndex = 112;
            this.btnAdd.Text = "Add";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAddFamily_Click);
            // 
            // tbTitleValue
            // 
            this.tbTitleValue.BackColor = System.Drawing.SystemColors.Window;
            this.tbTitleValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbTitleValue.Depth = 0;
            this.tbTitleValue.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbTitleValue.Hint = "Title Value";
            this.tbTitleValue.LeadingIcon = null;
            this.tbTitleValue.Location = new System.Drawing.Point(550, 5);
            this.tbTitleValue.MaxLength = 50;
            this.tbTitleValue.MouseState = MaterialSkin.MouseState.OUT;
            this.tbTitleValue.Multiline = false;
            this.tbTitleValue.Name = "tbTitleValue";
            this.tbTitleValue.Size = new System.Drawing.Size(443, 50);
            this.tbTitleValue.TabIndex = 111;
            this.tbTitleValue.Text = "";
            this.tbTitleValue.TrailingIcon = null;
            this.tbTitleValue.UseAccent = false;
            this.tbTitleValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTitleValue_KeyDown);
            // 
            // tbTitleName
            // 
            this.tbTitleName.BackColor = System.Drawing.SystemColors.Window;
            this.tbTitleName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbTitleName.Depth = 0;
            this.tbTitleName.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbTitleName.Hint = "Title Name";
            this.tbTitleName.LeadingIcon = null;
            this.tbTitleName.Location = new System.Drawing.Point(141, 5);
            this.tbTitleName.MaxLength = 50;
            this.tbTitleName.MouseState = MaterialSkin.MouseState.OUT;
            this.tbTitleName.Multiline = false;
            this.tbTitleName.Name = "tbTitleName";
            this.tbTitleName.Size = new System.Drawing.Size(403, 50);
            this.tbTitleName.TabIndex = 110;
            this.tbTitleName.Text = "";
            this.tbTitleName.TrailingIcon = null;
            this.tbTitleName.UseAccent = false;
            this.tbTitleName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTitleName_KeyDown);
            // 
            // OtherInformationUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tbTitleName);
            this.Controls.Add(this.tbTitleValue);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.panelButton);
            this.Controls.Add(this.dgvOtherInfo);
            this.Name = "OtherInformationUserControl";
            this.Size = new System.Drawing.Size(1250, 650);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOtherInfo)).EndInit();
            this.panelButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvOtherInfo;
        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.Button btnPreviewSubmit;
        private System.Windows.Forms.Button btnBiometric;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnCriminalProfile;
        private System.Windows.Forms.Button btnOtherInfo;
        private FontAwesome.Sharp.IconButton btnEdit;
        private FontAwesome.Sharp.IconButton btnRemove;
        private FontAwesome.Sharp.IconButton btnAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Union;
        private System.Windows.Forms.DataGridViewTextBoxColumn VillageRoadHouseNo;
        private MaterialSkin.Controls.MaterialTextBox tbTitleValue;
        private MaterialSkin.Controls.MaterialTextBox tbTitleName;
    }
}
