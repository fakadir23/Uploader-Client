namespace ISTL.RAB.View.New.Enrollment.CriminalProfile
{
    partial class AddRecoveryDialogForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.exitButton = new FontAwesome.Sharp.IconButton();
            this.iconBtnSave = new FontAwesome.Sharp.IconButton();
            this.tbRecoveryItemAmount = new MaterialSkin.Controls.MaterialTextBox();
            this.cmbRecoveryType = new ISTL.COMMON.CustomControl.SuggestComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbRecoveryName = new ISTL.COMMON.CustomControl.SuggestComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.iconButton1);
            this.panel1.Controls.Add(this.exitButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 32);
            this.panel1.TabIndex = 138;
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
            this.iconButton1.TabIndex = 153;
            this.iconButton1.UseVisualStyleBackColor = false;
            this.iconButton1.Visible = false;
            this.iconButton1.Click += new System.EventHandler(this.btnMinimize_Click);
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
            this.exitButton.TabIndex = 154;
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
            this.iconBtnSave.Location = new System.Drawing.Point(288, 247);
            this.iconBtnSave.Name = "iconBtnSave";
            this.iconBtnSave.Size = new System.Drawing.Size(100, 30);
            this.iconBtnSave.TabIndex = 151;
            this.iconBtnSave.Text = "Save";
            this.iconBtnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconBtnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconBtnSave.UseVisualStyleBackColor = false;
            this.iconBtnSave.Click += new System.EventHandler(this.iconBtnSave_Click);
            // 
            // tbRecoveryItemAmount
            // 
            this.tbRecoveryItemAmount.BackColor = System.Drawing.SystemColors.Window;
            this.tbRecoveryItemAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbRecoveryItemAmount.Depth = 0;
            this.tbRecoveryItemAmount.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbRecoveryItemAmount.Hint = "Amount/Quantity";
            this.tbRecoveryItemAmount.LeadingIcon = null;
            this.tbRecoveryItemAmount.Location = new System.Drawing.Point(12, 175);
            this.tbRecoveryItemAmount.MaxLength = 250;
            this.tbRecoveryItemAmount.MouseState = MaterialSkin.MouseState.OUT;
            this.tbRecoveryItemAmount.Multiline = false;
            this.tbRecoveryItemAmount.Name = "tbRecoveryItemAmount";
            this.tbRecoveryItemAmount.Size = new System.Drawing.Size(376, 50);
            this.tbRecoveryItemAmount.TabIndex = 150;
            this.tbRecoveryItemAmount.Text = "";
            this.tbRecoveryItemAmount.TrailingIcon = null;
            this.tbRecoveryItemAmount.UseAccent = false;
            this.tbRecoveryItemAmount.Enter += new System.EventHandler(this.tbRecoveryItemAmount_Enter);
            // 
            // cmbRecoveryType
            // 
            this.cmbRecoveryType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmbRecoveryType.FilterRule = null;
            this.cmbRecoveryType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRecoveryType.FormattingEnabled = true;
            this.cmbRecoveryType.Location = new System.Drawing.Point(12, 76);
            this.cmbRecoveryType.Name = "cmbRecoveryType";
            this.cmbRecoveryType.PropertySelector = null;
            this.cmbRecoveryType.Size = new System.Drawing.Size(376, 24);
            this.cmbRecoveryType.SuggestBoxHeight = 96;
            this.cmbRecoveryType.SuggestListOrderRule = null;
            this.cmbRecoveryType.TabIndex = 144;
            this.cmbRecoveryType.SelectedIndexChanged += new System.EventHandler(this.cmbRecoveryType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 24);
            this.label1.TabIndex = 200;
            this.label1.Text = "Recovery Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 24);
            this.label2.TabIndex = 201;
            this.label2.Text = "Recovery Name";
            // 
            // cmbRecoveryName
            // 
            this.cmbRecoveryName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmbRecoveryName.FilterRule = null;
            this.cmbRecoveryName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRecoveryName.FormattingEnabled = true;
            this.cmbRecoveryName.Location = new System.Drawing.Point(12, 136);
            this.cmbRecoveryName.Name = "cmbRecoveryName";
            this.cmbRecoveryName.PropertySelector = null;
            this.cmbRecoveryName.Size = new System.Drawing.Size(376, 24);
            this.cmbRecoveryName.SuggestBoxHeight = 96;
            this.cmbRecoveryName.SuggestListOrderRule = null;
            this.cmbRecoveryName.TabIndex = 148;
            this.cmbRecoveryName.Enter += new System.EventHandler(this.cmbRecoveryName_Enter);
            // 
            // AddRecoveryDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MintCream;
            this.ClientSize = new System.Drawing.Size(400, 289);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbRecoveryName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbRecoveryType);
            this.Controls.Add(this.tbRecoveryItemAmount);
            this.Controls.Add(this.iconBtnSave);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddRecoveryDialogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddRecoveryDialogForm";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton iconButton1;
        private FontAwesome.Sharp.IconButton exitButton;
        private FontAwesome.Sharp.IconButton iconBtnSave;
        private MaterialSkin.Controls.MaterialTextBox tbRecoveryItemAmount;
        private COMMON.CustomControl.SuggestComboBox cmbRecoveryType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private COMMON.CustomControl.SuggestComboBox cmbRecoveryName;
    }
}