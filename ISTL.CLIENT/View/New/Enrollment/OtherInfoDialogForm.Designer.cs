
namespace ISTL.RAB.View.New.Enrollment
{
    partial class OtherInfoDialogForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMinimize = new FontAwesome.Sharp.IconButton();
            this.btnExit = new FontAwesome.Sharp.IconButton();
            this.iconBtnMinimize = new FontAwesome.Sharp.IconButton();
            this.exitButton = new FontAwesome.Sharp.IconButton();
            this.lblMatchFoundFlag = new System.Windows.Forms.Label();
            this.dgvOtherInfo = new System.Windows.Forms.DataGridView();
            this.Union = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VillageRoadHouseNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOtherInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnMinimize);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.iconBtnMinimize);
            this.panel1.Controls.Add(this.exitButton);
            this.panel1.Controls.Add(this.lblMatchFoundFlag);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 32);
            this.panel1.TabIndex = 299;
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.IconChar = FontAwesome.Sharp.IconChar.Minus;
            this.btnMinimize.IconColor = System.Drawing.Color.White;
            this.btnMinimize.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMinimize.IconSize = 22;
            this.btnMinimize.Location = new System.Drawing.Point(729, -1);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnMinimize.Size = new System.Drawing.Size(32, 32);
            this.btnMinimize.TabIndex = 287;
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Visible = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Crimson;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.IconChar = FontAwesome.Sharp.IconChar.Times;
            this.btnExit.IconColor = System.Drawing.Color.White;
            this.btnExit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnExit.IconSize = 22;
            this.btnExit.Location = new System.Drawing.Point(763, -1);
            this.btnExit.Name = "btnExit";
            this.btnExit.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnExit.Size = new System.Drawing.Size(32, 32);
            this.btnExit.TabIndex = 286;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
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
            this.iconBtnMinimize.Location = new System.Drawing.Point(1120, -1);
            this.iconBtnMinimize.Name = "iconBtnMinimize";
            this.iconBtnMinimize.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.iconBtnMinimize.Size = new System.Drawing.Size(32, 32);
            this.iconBtnMinimize.TabIndex = 91;
            this.iconBtnMinimize.UseVisualStyleBackColor = false;
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
            this.exitButton.Location = new System.Drawing.Point(1154, -1);
            this.exitButton.Name = "exitButton";
            this.exitButton.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.exitButton.Size = new System.Drawing.Size(32, 32);
            this.exitButton.TabIndex = 90;
            this.exitButton.UseVisualStyleBackColor = false;
            // 
            // lblMatchFoundFlag
            // 
            this.lblMatchFoundFlag.AutoSize = true;
            this.lblMatchFoundFlag.BackColor = System.Drawing.Color.Transparent;
            this.lblMatchFoundFlag.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatchFoundFlag.ForeColor = System.Drawing.Color.White;
            this.lblMatchFoundFlag.Location = new System.Drawing.Point(8, 5);
            this.lblMatchFoundFlag.Name = "lblMatchFoundFlag";
            this.lblMatchFoundFlag.Size = new System.Drawing.Size(83, 18);
            this.lblMatchFoundFlag.TabIndex = 285;
            this.lblMatchFoundFlag.Text = "Other Info";
            this.lblMatchFoundFlag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvOtherInfo
            // 
            this.dgvOtherInfo.AllowUserToAddRows = false;
            this.dgvOtherInfo.AllowUserToDeleteRows = false;
            this.dgvOtherInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
            this.dgvOtherInfo.Location = new System.Drawing.Point(32, 65);
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
            this.dgvOtherInfo.Size = new System.Drawing.Size(730, 356);
            this.dgvOtherInfo.TabIndex = 300;
            // 
            // Union
            // 
            this.Union.HeaderText = "Title Name";
            this.Union.Name = "Union";
            this.Union.ReadOnly = true;
            // 
            // VillageRoadHouseNo
            // 
            this.VillageRoadHouseNo.HeaderText = "Title Value";
            this.VillageRoadHouseNo.Name = "VillageRoadHouseNo";
            this.VillageRoadHouseNo.ReadOnly = true;
            // 
            // OtherInfoDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aquamarine;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvOtherInfo);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "OtherInfoDialogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OtherInfoDialogForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOtherInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton iconBtnMinimize;
        private FontAwesome.Sharp.IconButton exitButton;
        private System.Windows.Forms.Label lblMatchFoundFlag;
        private FontAwesome.Sharp.IconButton btnMinimize;
        private FontAwesome.Sharp.IconButton btnExit;
        private System.Windows.Forms.DataGridView dgvOtherInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Union;
        private System.Windows.Forms.DataGridViewTextBoxColumn VillageRoadHouseNo;
    }
}