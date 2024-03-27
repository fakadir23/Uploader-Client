namespace ISTL.RAB.View.New.CriminalProfile
{
    partial class AddNickNameDialogForm
    {
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
            this.tbNickName = new MaterialSkin.Controls.MaterialTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iconBtnSave = new FontAwesome.Sharp.IconButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.iconBtnMinimize = new FontAwesome.Sharp.IconButton();
            this.exitButton = new FontAwesome.Sharp.IconButton();
            this.iconBtnEdit = new FontAwesome.Sharp.IconButton();
            this.iconBtnRemove = new FontAwesome.Sharp.IconButton();
            this.iconBtnAdd = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbNickName
            // 
            this.tbNickName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbNickName.Depth = 0;
            this.tbNickName.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.tbNickName.Hint = "Name";
            this.tbNickName.LeadingIcon = null;
            this.tbNickName.Location = new System.Drawing.Point(12, 55);
            this.tbNickName.MaxLength = 50;
            this.tbNickName.MouseState = MaterialSkin.MouseState.OUT;
            this.tbNickName.Multiline = false;
            this.tbNickName.Name = "tbNickName";
            this.tbNickName.Size = new System.Drawing.Size(287, 50);
            this.tbNickName.TabIndex = 83;
            this.tbNickName.Text = "";
            this.tbNickName.TrailingIcon = null;
            this.tbNickName.UseAccent = false;
            this.tbNickName.TextChanged += new System.EventHandler(this.tbBankName_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn12});
            this.dataGridView1.Location = new System.Drawing.Point(12, 111);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.DividerHeight = 2;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(536, 232);
            this.dataGridView1.TabIndex = 147;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "Name";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Width = 530;
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
            this.iconBtnSave.Location = new System.Drawing.Point(448, 349);
            this.iconBtnSave.Name = "iconBtnSave";
            this.iconBtnSave.Size = new System.Drawing.Size(100, 30);
            this.iconBtnSave.TabIndex = 149;
            this.iconBtnSave.Text = "Save";
            this.iconBtnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconBtnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.iconBtnSave.UseVisualStyleBackColor = false;
            this.iconBtnSave.Click += new System.EventHandler(this.iconBtnSave_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.iconBtnMinimize);
            this.panel1.Controls.Add(this.exitButton);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 32);
            this.panel1.TabIndex = 140;
            // 
            // iconBtnMinimize
            // 
            this.iconBtnMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.iconBtnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconBtnMinimize.FlatAppearance.BorderSize = 0;
            this.iconBtnMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.iconBtnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.iconBtnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtnMinimize.IconChar = FontAwesome.Sharp.IconChar.Minus;
            this.iconBtnMinimize.IconColor = System.Drawing.Color.White;
            this.iconBtnMinimize.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtnMinimize.IconSize = 22;
            this.iconBtnMinimize.Location = new System.Drawing.Point(495, 0);
            this.iconBtnMinimize.Name = "iconBtnMinimize";
            this.iconBtnMinimize.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.iconBtnMinimize.Size = new System.Drawing.Size(32, 32);
            this.iconBtnMinimize.TabIndex = 151;
            this.iconBtnMinimize.UseVisualStyleBackColor = false;
            this.iconBtnMinimize.Visible = false;
            this.iconBtnMinimize.Click += new System.EventHandler(this.iconBtnMinimize_Click);
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
            this.exitButton.Location = new System.Drawing.Point(528, 0);
            this.exitButton.Name = "exitButton";
            this.exitButton.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.exitButton.Size = new System.Drawing.Size(32, 32);
            this.exitButton.TabIndex = 153;
            this.exitButton.TabStop = false;
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // iconBtnEdit
            // 
            this.iconBtnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.iconBtnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconBtnEdit.FlatAppearance.BorderSize = 0;
            this.iconBtnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconBtnEdit.ForeColor = System.Drawing.Color.White;
            this.iconBtnEdit.IconChar = FontAwesome.Sharp.IconChar.Edit;
            this.iconBtnEdit.IconColor = System.Drawing.Color.White;
            this.iconBtnEdit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtnEdit.IconSize = 22;
            this.iconBtnEdit.Location = new System.Drawing.Point(386, 55);
            this.iconBtnEdit.Name = "iconBtnEdit";
            this.iconBtnEdit.Size = new System.Drawing.Size(75, 50);
            this.iconBtnEdit.TabIndex = 143;
            this.iconBtnEdit.Text = "Edit";
            this.iconBtnEdit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconBtnEdit.UseVisualStyleBackColor = false;
            this.iconBtnEdit.Click += new System.EventHandler(this.iconBtnEdit_Click);
            // 
            // iconBtnRemove
            // 
            this.iconBtnRemove.BackColor = System.Drawing.Color.IndianRed;
            this.iconBtnRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconBtnRemove.FlatAppearance.BorderSize = 0;
            this.iconBtnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconBtnRemove.ForeColor = System.Drawing.Color.White;
            this.iconBtnRemove.IconChar = FontAwesome.Sharp.IconChar.TimesCircle;
            this.iconBtnRemove.IconColor = System.Drawing.Color.White;
            this.iconBtnRemove.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtnRemove.IconSize = 22;
            this.iconBtnRemove.Location = new System.Drawing.Point(467, 55);
            this.iconBtnRemove.Name = "iconBtnRemove";
            this.iconBtnRemove.Size = new System.Drawing.Size(81, 50);
            this.iconBtnRemove.TabIndex = 145;
            this.iconBtnRemove.Text = "Remove";
            this.iconBtnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconBtnRemove.UseVisualStyleBackColor = false;
            this.iconBtnRemove.Click += new System.EventHandler(this.iconBtnRemove_Click);
            // 
            // iconBtnAdd
            // 
            this.iconBtnAdd.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.iconBtnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconBtnAdd.FlatAppearance.BorderSize = 0;
            this.iconBtnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconBtnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconBtnAdd.ForeColor = System.Drawing.Color.White;
            this.iconBtnAdd.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.iconBtnAdd.IconColor = System.Drawing.Color.White;
            this.iconBtnAdd.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconBtnAdd.IconSize = 22;
            this.iconBtnAdd.Location = new System.Drawing.Point(305, 55);
            this.iconBtnAdd.Name = "iconBtnAdd";
            this.iconBtnAdd.Size = new System.Drawing.Size(75, 50);
            this.iconBtnAdd.TabIndex = 141;
            this.iconBtnAdd.Text = "Add";
            this.iconBtnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.iconBtnAdd.UseVisualStyleBackColor = false;
            this.iconBtnAdd.Click += new System.EventHandler(this.iconBtnAdd_Click);
            // 
            // AddNickNameDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MintCream;
            this.ClientSize = new System.Drawing.Size(560, 391);
            this.Controls.Add(this.iconBtnEdit);
            this.Controls.Add(this.iconBtnRemove);
            this.Controls.Add(this.iconBtnAdd);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.iconBtnSave);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.tbNickName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "AddNickNameDialogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Nick Name";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MaterialSkin.Controls.MaterialTextBox tbNickName;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private FontAwesome.Sharp.IconButton iconBtnSave;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton iconBtnMinimize;
        private FontAwesome.Sharp.IconButton exitButton;
        private FontAwesome.Sharp.IconButton iconBtnEdit;
        private FontAwesome.Sharp.IconButton iconBtnRemove;
        private FontAwesome.Sharp.IconButton iconBtnAdd;
    }
}