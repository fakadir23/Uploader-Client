
namespace ISTL.RAB.View
{
    partial class YesNoMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YesNoMessageBox));
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnYes = new FontAwesome.Sharp.IconButton();
            this.btnNo = new FontAwesome.Sharp.IconButton();
            this.picTriangle = new FontAwesome.Sharp.IconPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picTriangle)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(94, 54);
            this.lblMessage.MinimumSize = new System.Drawing.Size(280, 80);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(306, 100);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "Message goes here";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.lblTitle.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(12, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(426, 29);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "Message Header";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnYes
            // 
            this.btnYes.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnYes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYes.FlatAppearance.BorderSize = 0;
            this.btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYes.ForeColor = System.Drawing.Color.White;
            this.btnYes.IconChar = FontAwesome.Sharp.IconChar.CheckCircle;
            this.btnYes.IconColor = System.Drawing.Color.White;
            this.btnYes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnYes.IconSize = 22;
            this.btnYes.Location = new System.Drawing.Point(79, 177);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(135, 30);
            this.btnYes.TabIndex = 144;
            this.btnYes.Text = "Yes";
            this.btnYes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnYes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnYes.UseVisualStyleBackColor = false;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.BackColor = System.Drawing.Color.IndianRed;
            this.btnNo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNo.FlatAppearance.BorderSize = 0;
            this.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNo.ForeColor = System.Drawing.Color.White;
            this.btnNo.IconChar = FontAwesome.Sharp.IconChar.TimesCircle;
            this.btnNo.IconColor = System.Drawing.Color.White;
            this.btnNo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNo.IconSize = 22;
            this.btnNo.Location = new System.Drawing.Point(220, 177);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(135, 30);
            this.btnNo.TabIndex = 142;
            this.btnNo.Text = "No";
            this.btnNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNo.UseVisualStyleBackColor = false;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // picTriangle
            // 
            this.picTriangle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.picTriangle.ForeColor = System.Drawing.Color.IndianRed;
            this.picTriangle.IconChar = FontAwesome.Sharp.IconChar.ExclamationTriangle;
            this.picTriangle.IconColor = System.Drawing.Color.IndianRed;
            this.picTriangle.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.picTriangle.IconSize = 44;
            this.picTriangle.Location = new System.Drawing.Point(44, 82);
            this.picTriangle.Name = "picTriangle";
            this.picTriangle.Size = new System.Drawing.Size(44, 45);
            this.picTriangle.TabIndex = 148;
            this.picTriangle.TabStop = false;
            // 
            // YesNoMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(450, 225);
            this.Controls.Add(this.picTriangle);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.lblMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "YesNoMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BarcodeMessageBox";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.picTriangle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblTitle;
        private FontAwesome.Sharp.IconButton btnYes;
        private FontAwesome.Sharp.IconButton btnNo;
        private FontAwesome.Sharp.IconPictureBox picTriangle;
    }
}