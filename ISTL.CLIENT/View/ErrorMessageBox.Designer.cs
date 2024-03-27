namespace ISTL.RAB.View
{
    partial class ErrorMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorMessageBox));
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnOk = new FontAwesome.Sharp.IconButton();
            this.btnSendReport = new FontAwesome.Sharp.IconButton();
            this.btnIgnore = new FontAwesome.Sharp.IconButton();
            this.picTriangle = new FontAwesome.Sharp.IconPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picTriangle)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.lblMessage.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(70, 40);
            this.lblMessage.MinimumSize = new System.Drawing.Size(280, 100);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(361, 127);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "Message goes here";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.lblTitle.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 11);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(426, 29);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "Critical Error";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.IconChar = FontAwesome.Sharp.IconChar.CheckCircle;
            this.btnOk.IconColor = System.Drawing.Color.White;
            this.btnOk.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnOk.IconSize = 22;
            this.btnOk.Location = new System.Drawing.Point(179, 177);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(109, 30);
            this.btnOk.TabIndex = 141;
            this.btnOk.Text = "OK";
            this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnSendReport
            // 
            this.btnSendReport.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnSendReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSendReport.FlatAppearance.BorderSize = 0;
            this.btnSendReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendReport.ForeColor = System.Drawing.Color.White;
            this.btnSendReport.IconChar = FontAwesome.Sharp.IconChar.ListAlt;
            this.btnSendReport.IconColor = System.Drawing.Color.White;
            this.btnSendReport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSendReport.IconSize = 22;
            this.btnSendReport.Location = new System.Drawing.Point(44, 177);
            this.btnSendReport.Name = "btnSendReport";
            this.btnSendReport.Size = new System.Drawing.Size(129, 30);
            this.btnSendReport.TabIndex = 140;
            this.btnSendReport.Text = "Send Report";
            this.btnSendReport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSendReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSendReport.UseVisualStyleBackColor = false;
            this.btnSendReport.Visible = false;
            this.btnSendReport.Click += new System.EventHandler(this.btnSendReport_Click);
            // 
            // btnIgnore
            // 
            this.btnIgnore.BackColor = System.Drawing.Color.IndianRed;
            this.btnIgnore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIgnore.FlatAppearance.BorderSize = 0;
            this.btnIgnore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIgnore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIgnore.ForeColor = System.Drawing.Color.White;
            this.btnIgnore.IconChar = FontAwesome.Sharp.IconChar.TimesCircle;
            this.btnIgnore.IconColor = System.Drawing.Color.White;
            this.btnIgnore.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnIgnore.IconSize = 22;
            this.btnIgnore.Location = new System.Drawing.Point(294, 177);
            this.btnIgnore.Name = "btnIgnore";
            this.btnIgnore.Size = new System.Drawing.Size(106, 30);
            this.btnIgnore.TabIndex = 139;
            this.btnIgnore.Text = "Ignore";
            this.btnIgnore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnIgnore.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnIgnore.UseVisualStyleBackColor = false;
            this.btnIgnore.Visible = false;
            this.btnIgnore.Click += new System.EventHandler(this.btnIgnore_Click);
            // 
            // picTriangle
            // 
            this.picTriangle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.picTriangle.ForeColor = System.Drawing.Color.IndianRed;
            this.picTriangle.IconChar = FontAwesome.Sharp.IconChar.ExclamationTriangle;
            this.picTriangle.IconColor = System.Drawing.Color.IndianRed;
            this.picTriangle.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.picTriangle.IconSize = 44;
            this.picTriangle.Location = new System.Drawing.Point(20, 82);
            this.picTriangle.Name = "picTriangle";
            this.picTriangle.Size = new System.Drawing.Size(44, 45);
            this.picTriangle.TabIndex = 142;
            this.picTriangle.TabStop = false;
            // 
            // ErrorMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(450, 225);
            this.Controls.Add(this.picTriangle);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnSendReport);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnIgnore);
            this.Controls.Add(this.lblMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ErrorMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Critical Error";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.picTriangle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblTitle;
        private FontAwesome.Sharp.IconButton btnOk;
        private FontAwesome.Sharp.IconButton btnSendReport;
        private FontAwesome.Sharp.IconButton btnIgnore;
        private FontAwesome.Sharp.IconPictureBox picTriangle;
    }
}