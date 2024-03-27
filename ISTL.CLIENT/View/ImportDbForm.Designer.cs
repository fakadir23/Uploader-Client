namespace ISTL.RAB.View
{
    partial class ImportDbForm
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
            this.btnImportCancel = new System.Windows.Forms.Button();
            this.btnImportBrowse = new System.Windows.Forms.Button();
            this.tbSelectedPath = new System.Windows.Forms.TextBox();
            this.btnStartImportDb = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lblProgress = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblSelectLocation = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMinimize = new FontAwesome.Sharp.IconButton();
            this.btnExit = new FontAwesome.Sharp.IconButton();
            this.iconBtnMinimize = new FontAwesome.Sharp.IconButton();
            this.exitButton = new FontAwesome.Sharp.IconButton();
            this.picProcessing = new System.Windows.Forms.PictureBox();
            this.picImport = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProcessing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImport)).BeginInit();
            this.SuspendLayout();
            // 
            // btnImportCancel
            // 
            this.btnImportCancel.BackColor = System.Drawing.Color.IndianRed;
            this.btnImportCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportCancel.FlatAppearance.BorderSize = 0;
            this.btnImportCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportCancel.ForeColor = System.Drawing.Color.White;
            this.btnImportCancel.Location = new System.Drawing.Point(233, 183);
            this.btnImportCancel.Name = "btnImportCancel";
            this.btnImportCancel.Size = new System.Drawing.Size(110, 30);
            this.btnImportCancel.TabIndex = 2;
            this.btnImportCancel.Text = "Cancel";
            this.btnImportCancel.UseVisualStyleBackColor = false;
            this.btnImportCancel.Click += new System.EventHandler(this.btnImportCancel_Click);
            // 
            // btnImportBrowse
            // 
            this.btnImportBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportBrowse.Location = new System.Drawing.Point(287, 83);
            this.btnImportBrowse.Name = "btnImportBrowse";
            this.btnImportBrowse.Size = new System.Drawing.Size(99, 23);
            this.btnImportBrowse.TabIndex = 0;
            this.btnImportBrowse.Text = "Browse";
            this.btnImportBrowse.UseVisualStyleBackColor = true;
            this.btnImportBrowse.Click += new System.EventHandler(this.btnImportBrowse_Click);
            // 
            // tbSelectedPath
            // 
            this.tbSelectedPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSelectedPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSelectedPath.Location = new System.Drawing.Point(87, 83);
            this.tbSelectedPath.MinimumSize = new System.Drawing.Size(2, 23);
            this.tbSelectedPath.Name = "tbSelectedPath";
            this.tbSelectedPath.ReadOnly = true;
            this.tbSelectedPath.Size = new System.Drawing.Size(194, 21);
            this.tbSelectedPath.TabIndex = 3;
            // 
            // btnStartImportDb
            // 
            this.btnStartImportDb.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnStartImportDb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStartImportDb.FlatAppearance.BorderSize = 0;
            this.btnStartImportDb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartImportDb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartImportDb.ForeColor = System.Drawing.Color.White;
            this.btnStartImportDb.Location = new System.Drawing.Point(117, 183);
            this.btnStartImportDb.Name = "btnStartImportDb";
            this.btnStartImportDb.Size = new System.Drawing.Size(110, 30);
            this.btnStartImportDb.TabIndex = 1;
            this.btnStartImportDb.Text = "Start Import";
            this.btnStartImportDb.UseVisualStyleBackColor = false;
            this.btnStartImportDb.Click += new System.EventHandler(this.btnStartImportDb_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblProgress.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.Location = new System.Drawing.Point(392, 135);
            this.lblProgress.MinimumSize = new System.Drawing.Size(50, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(50, 15);
            this.lblProgress.TabIndex = 12;
            this.lblProgress.Text = "0%";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(76, 135);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(310, 15);
            this.progressBar.TabIndex = 13;
            // 
            // lblSelectLocation
            // 
            this.lblSelectLocation.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectLocation.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectLocation.Location = new System.Drawing.Point(33, 44);
            this.lblSelectLocation.Name = "lblSelectLocation";
            this.lblSelectLocation.Size = new System.Drawing.Size(353, 20);
            this.lblSelectLocation.TabIndex = 14;
            this.lblSelectLocation.Text = "Please select a location to start import from";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(6, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(97, 19);
            this.lblTitle.TabIndex = 15;
            this.lblTitle.Text = "Import Data";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnMinimize);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.iconBtnMinimize);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Controls.Add(this.exitButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 32);
            this.panel1.TabIndex = 300;
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
            this.btnMinimize.Location = new System.Drawing.Point(380, -1);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnMinimize.Size = new System.Drawing.Size(32, 32);
            this.btnMinimize.TabIndex = 287;
            this.btnMinimize.UseVisualStyleBackColor = false;
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
            this.btnExit.Location = new System.Drawing.Point(414, -1);
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
            // picProcessing
            // 
            this.picProcessing.BackColor = System.Drawing.Color.Transparent;
            this.picProcessing.Image = global::ISTL.RAB.Properties.Resources.loader_2_;
            this.picProcessing.Location = new System.Drawing.Point(33, 126);
            this.picProcessing.Name = "picProcessing";
            this.picProcessing.Size = new System.Drawing.Size(37, 36);
            this.picProcessing.TabIndex = 18;
            this.picProcessing.TabStop = false;
            this.picProcessing.Visible = false;
            // 
            // picImport
            // 
            this.picImport.BackColor = System.Drawing.Color.Transparent;
            this.picImport.Image = global::ISTL.RAB.Properties.Resources.import_screen_icon;
            this.picImport.Location = new System.Drawing.Point(33, 75);
            this.picImport.Name = "picImport";
            this.picImport.Size = new System.Drawing.Size(48, 38);
            this.picImport.TabIndex = 16;
            this.picImport.TabStop = false;
            // 
            // ImportDbForm
            // 
            this.AcceptButton = this.btnStartImportDb;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(450, 225);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.picProcessing);
            this.Controls.Add(this.picImport);
            this.Controls.Add(this.lblSelectLocation);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnImportCancel);
            this.Controls.Add(this.btnImportBrowse);
            this.Controls.Add(this.tbSelectedPath);
            this.Controls.Add(this.btnStartImportDb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportDbForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import Data";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProcessing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnImportCancel;
        private System.Windows.Forms.Button btnImportBrowse;
        private System.Windows.Forms.TextBox tbSelectedPath;
        private System.Windows.Forms.Button btnStartImportDb;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblSelectLocation;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picImport;
        private System.Windows.Forms.PictureBox picProcessing;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton btnMinimize;
        private FontAwesome.Sharp.IconButton btnExit;
        private FontAwesome.Sharp.IconButton iconBtnMinimize;
        private FontAwesome.Sharp.IconButton exitButton;
    }
}