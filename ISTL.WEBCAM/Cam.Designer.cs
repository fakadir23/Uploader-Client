namespace ISTL.WEBCAM
{
    partial class Cam
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
            this.label40 = new System.Windows.Forms.Label();
            this.btnCapture = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.cmbCamera = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label40.Location = new System.Drawing.Point(16, 24);
            this.label40.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(285, 44);
            this.label40.TabIndex = 27;
            this.label40.Text = "Select Camera";
            // 
            // btnCapture
            // 
            this.btnCapture.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnCapture.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCapture.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapture.ForeColor = System.Drawing.Color.White;
            this.btnCapture.Location = new System.Drawing.Point(13, 985);
            this.btnCapture.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(285, 74);
            this.btnCapture.TabIndex = 26;
            this.btnCapture.Text = "&Save";
            this.btnCapture.UseVisualStyleBackColor = false;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.IndianRed;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(315, 985);
            this.btnExit.Margin = new System.Windows.Forms.Padding(8, 7, 8, 7);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(285, 74);
            this.btnExit.TabIndex = 25;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(11, 112);
            this.picBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(1163, 847);
            this.picBox.TabIndex = 24;
            this.picBox.TabStop = false;
            // 
            // cmbCamera
            // 
            this.cmbCamera.FormattingEnabled = true;
            this.cmbCamera.Location = new System.Drawing.Point(368, 24);
            this.cmbCamera.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbCamera.Name = "cmbCamera";
            this.cmbCamera.Size = new System.Drawing.Size(799, 39);
            this.cmbCamera.TabIndex = 23;
            // 
            // Cam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 1080);
            this.Controls.Add(this.label40);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.cmbCamera);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Cam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cam";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Cam_FormClosing);
            this.Load += new System.EventHandler(this.Cam_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.ComboBox cmbCamera;
    }
}