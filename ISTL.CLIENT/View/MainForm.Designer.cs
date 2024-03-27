namespace ISTL.RAB.View
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lblOnlineOffline = new System.Windows.Forms.Label();
            this.btnUpdate = new System.Windows.Forms.PictureBox();
            this.btnNoUpdate = new System.Windows.Forms.PictureBox();
            this.lblPending = new System.Windows.Forms.Label();
            this.lblPendingCount = new System.Windows.Forms.Label();
            this.picErrorStatus = new FontAwesome.Sharp.IconPictureBox();
            this.lblUploaded = new System.Windows.Forms.Label();
            this.lblUploadedCount = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblErrorStatus = new System.Windows.Forms.Label();
            this.picIrisSuccessFailed = new FontAwesome.Sharp.IconPictureBox();
            this.picFPSuccessFailed = new FontAwesome.Sharp.IconPictureBox();
            this.picPhotoSuccessFailed = new FontAwesome.Sharp.IconPictureBox();
            this.lblFailed = new System.Windows.Forms.Label();
            this.lblEnrolledErrorCount = new System.Windows.Forms.Label();
            this.lblTodayName = new System.Windows.Forms.Label();
            this.lblEnrolledToday = new System.Windows.Forms.Label();
            this.lblPrinting = new System.Windows.Forms.Label();
            this.lblUploadStatus = new System.Windows.Forms.Label();
            this.picUploadFailedStatus = new System.Windows.Forms.PictureBox();
            this.picUploadStatus = new System.Windows.Forms.PictureBox();
            this.picOfflineStatus = new System.Windows.Forms.PictureBox();
            this.picOnlineStatus = new System.Windows.Forms.PictureBox();
            this.Mainmenu = new System.Windows.Forms.MenuStrip();
            this.HomeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.NewEntryMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DemographicSearchMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BiometricSearchMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userManagementMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.localRecords = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDraftRecords = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUploadPending = new System.Windows.Forms.ToolStripMenuItem();
            this.btnFailedUploads = new System.Windows.Forms.ToolStripMenuItem();
            this.searchByFingerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loggerInUser = new System.Windows.Forms.Label();
            this.iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            this.iconButton2 = new FontAwesome.Sharp.IconButton();
            this.btnMinize = new FontAwesome.Sharp.IconButton();
            this.exitButton = new FontAwesome.Sharp.IconButton();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNoUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picErrorStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIrisSuccessFailed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFPSuccessFailed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPhotoSuccessFailed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUploadFailedStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUploadStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOfflineStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOnlineStatus)).BeginInit();
            this.Mainmenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelStatus
            // 
            this.panelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelStatus.Controls.Add(this.lblOnlineOffline);
            this.panelStatus.Controls.Add(this.btnUpdate);
            this.panelStatus.Controls.Add(this.btnNoUpdate);
            this.panelStatus.Controls.Add(this.lblPending);
            this.panelStatus.Controls.Add(this.lblPendingCount);
            this.panelStatus.Controls.Add(this.picErrorStatus);
            this.panelStatus.Controls.Add(this.lblUploaded);
            this.panelStatus.Controls.Add(this.lblUploadedCount);
            this.panelStatus.Controls.Add(this.lblVersion);
            this.panelStatus.Controls.Add(this.lblErrorStatus);
            this.panelStatus.Controls.Add(this.picIrisSuccessFailed);
            this.panelStatus.Controls.Add(this.picFPSuccessFailed);
            this.panelStatus.Controls.Add(this.picPhotoSuccessFailed);
            this.panelStatus.Controls.Add(this.lblFailed);
            this.panelStatus.Controls.Add(this.lblEnrolledErrorCount);
            this.panelStatus.Controls.Add(this.lblTodayName);
            this.panelStatus.Controls.Add(this.lblEnrolledToday);
            this.panelStatus.Controls.Add(this.lblPrinting);
            this.panelStatus.Controls.Add(this.lblUploadStatus);
            this.panelStatus.Controls.Add(this.picUploadFailedStatus);
            this.panelStatus.Controls.Add(this.picUploadStatus);
            this.panelStatus.Controls.Add(this.picOfflineStatus);
            this.panelStatus.Controls.Add(this.picOnlineStatus);
            this.panelStatus.Location = new System.Drawing.Point(0, 684);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(1252, 36);
            this.panelStatus.TabIndex = 2;
            // 
            // lblOnlineOffline
            // 
            this.lblOnlineOffline.AutoSize = true;
            this.lblOnlineOffline.BackColor = System.Drawing.Color.Transparent;
            this.lblOnlineOffline.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblOnlineOffline.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOnlineOffline.ForeColor = System.Drawing.Color.IndianRed;
            this.lblOnlineOffline.Location = new System.Drawing.Point(36, 12);
            this.lblOnlineOffline.Name = "lblOnlineOffline";
            this.lblOnlineOffline.Size = new System.Drawing.Size(43, 14);
            this.lblOnlineOffline.TabIndex = 35;
            this.lblOnlineOffline.Text = "Offline";
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUpdate.Image = global::ISTL.RAB.Properties.Resources.download;
            this.btnUpdate.Location = new System.Drawing.Point(1149, 5);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(26, 28);
            this.btnUpdate.TabIndex = 34;
            this.btnUpdate.TabStop = false;
            this.btnUpdate.Visible = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnNoUpdate
            // 
            this.btnNoUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNoUpdate.Image = global::ISTL.RAB.Properties.Resources.download_disable;
            this.btnNoUpdate.Location = new System.Drawing.Point(1149, 5);
            this.btnNoUpdate.Name = "btnNoUpdate";
            this.btnNoUpdate.Size = new System.Drawing.Size(26, 26);
            this.btnNoUpdate.TabIndex = 33;
            this.btnNoUpdate.TabStop = false;
            // 
            // lblPending
            // 
            this.lblPending.AutoSize = true;
            this.lblPending.BackColor = System.Drawing.Color.Transparent;
            this.lblPending.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPending.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPending.ForeColor = System.Drawing.Color.Gray;
            this.lblPending.Location = new System.Drawing.Point(841, 10);
            this.lblPending.Name = "lblPending";
            this.lblPending.Size = new System.Drawing.Size(52, 14);
            this.lblPending.TabIndex = 32;
            this.lblPending.Text = "Pending";
            this.lblPending.Click += new System.EventHandler(this.lblPending_Click);
            // 
            // lblPendingCount
            // 
            this.lblPendingCount.AutoSize = true;
            this.lblPendingCount.BackColor = System.Drawing.Color.Transparent;
            this.lblPendingCount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPendingCount.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPendingCount.ForeColor = System.Drawing.Color.Gray;
            this.lblPendingCount.Location = new System.Drawing.Point(906, 8);
            this.lblPendingCount.Name = "lblPendingCount";
            this.lblPendingCount.Size = new System.Drawing.Size(17, 18);
            this.lblPendingCount.TabIndex = 31;
            this.lblPendingCount.Text = "0";
            this.lblPendingCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPendingCount.Click += new System.EventHandler(this.lblPendingCount_Click);
            // 
            // picErrorStatus
            // 
            this.picErrorStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.picErrorStatus.ForeColor = System.Drawing.Color.IndianRed;
            this.picErrorStatus.IconChar = FontAwesome.Sharp.IconChar.ExclamationTriangle;
            this.picErrorStatus.IconColor = System.Drawing.Color.IndianRed;
            this.picErrorStatus.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.picErrorStatus.IconSize = 28;
            this.picErrorStatus.Location = new System.Drawing.Point(359, 5);
            this.picErrorStatus.Name = "picErrorStatus";
            this.picErrorStatus.Size = new System.Drawing.Size(28, 28);
            this.picErrorStatus.TabIndex = 30;
            this.picErrorStatus.TabStop = false;
            this.picErrorStatus.Visible = false;
            // 
            // lblUploaded
            // 
            this.lblUploaded.AutoSize = true;
            this.lblUploaded.BackColor = System.Drawing.Color.Transparent;
            this.lblUploaded.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblUploaded.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUploaded.ForeColor = System.Drawing.Color.Gold;
            this.lblUploaded.Location = new System.Drawing.Point(1012, 10);
            this.lblUploaded.Name = "lblUploaded";
            this.lblUploaded.Size = new System.Drawing.Size(58, 14);
            this.lblUploaded.TabIndex = 29;
            this.lblUploaded.Text = "Uploaded";
            this.lblUploaded.Click += new System.EventHandler(this.lblDrafted_Click);
            // 
            // lblUploadedCount
            // 
            this.lblUploadedCount.AutoSize = true;
            this.lblUploadedCount.BackColor = System.Drawing.Color.Transparent;
            this.lblUploadedCount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblUploadedCount.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUploadedCount.ForeColor = System.Drawing.Color.Gold;
            this.lblUploadedCount.Location = new System.Drawing.Point(1086, 8);
            this.lblUploadedCount.Name = "lblUploadedCount";
            this.lblUploadedCount.Size = new System.Drawing.Size(17, 18);
            this.lblUploadedCount.TabIndex = 28;
            this.lblUploadedCount.Text = "0";
            this.lblUploadedCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblUploadedCount.Click += new System.EventHandler(this.lblEnrolledDraftCount_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.Silver;
            this.lblVersion.Location = new System.Drawing.Point(1181, 5);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(67, 26);
            this.lblVersion.TabIndex = 27;
            this.lblVersion.Text = "v";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblErrorStatus
            // 
            this.lblErrorStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblErrorStatus.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorStatus.ForeColor = System.Drawing.Color.IndianRed;
            this.lblErrorStatus.Location = new System.Drawing.Point(392, 4);
            this.lblErrorStatus.Name = "lblErrorStatus";
            this.lblErrorStatus.Size = new System.Drawing.Size(248, 28);
            this.lblErrorStatus.TabIndex = 26;
            this.lblErrorStatus.Text = "Network Failed, Upload Failed, Device Failed";
            this.lblErrorStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblErrorStatus.Visible = false;
            // 
            // picIrisSuccessFailed
            // 
            this.picIrisSuccessFailed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.picIrisSuccessFailed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(75)))));
            this.picIrisSuccessFailed.IconChar = FontAwesome.Sharp.IconChar.Eye;
            this.picIrisSuccessFailed.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(75)))));
            this.picIrisSuccessFailed.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.picIrisSuccessFailed.IconSize = 26;
            this.picIrisSuccessFailed.Location = new System.Drawing.Point(711, 8);
            this.picIrisSuccessFailed.Name = "picIrisSuccessFailed";
            this.picIrisSuccessFailed.Size = new System.Drawing.Size(26, 26);
            this.picIrisSuccessFailed.TabIndex = 25;
            this.picIrisSuccessFailed.TabStop = false;
            this.picIrisSuccessFailed.Visible = false;
            // 
            // picFPSuccessFailed
            // 
            this.picFPSuccessFailed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.picFPSuccessFailed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(75)))));
            this.picFPSuccessFailed.IconChar = FontAwesome.Sharp.IconChar.Fingerprint;
            this.picFPSuccessFailed.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(75)))));
            this.picFPSuccessFailed.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.picFPSuccessFailed.IconSize = 28;
            this.picFPSuccessFailed.Location = new System.Drawing.Point(678, 8);
            this.picFPSuccessFailed.Name = "picFPSuccessFailed";
            this.picFPSuccessFailed.Size = new System.Drawing.Size(28, 28);
            this.picFPSuccessFailed.TabIndex = 24;
            this.picFPSuccessFailed.TabStop = false;
            this.picFPSuccessFailed.Visible = false;
            // 
            // picPhotoSuccessFailed
            // 
            this.picPhotoSuccessFailed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.picPhotoSuccessFailed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(75)))));
            this.picPhotoSuccessFailed.IconChar = FontAwesome.Sharp.IconChar.Camera;
            this.picPhotoSuccessFailed.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(75)))));
            this.picPhotoSuccessFailed.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.picPhotoSuccessFailed.IconSize = 28;
            this.picPhotoSuccessFailed.Location = new System.Drawing.Point(645, 7);
            this.picPhotoSuccessFailed.Name = "picPhotoSuccessFailed";
            this.picPhotoSuccessFailed.Size = new System.Drawing.Size(28, 28);
            this.picPhotoSuccessFailed.TabIndex = 23;
            this.picPhotoSuccessFailed.TabStop = false;
            this.picPhotoSuccessFailed.Visible = false;
            // 
            // lblFailed
            // 
            this.lblFailed.AutoSize = true;
            this.lblFailed.BackColor = System.Drawing.Color.Transparent;
            this.lblFailed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFailed.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFailed.ForeColor = System.Drawing.Color.IndianRed;
            this.lblFailed.Location = new System.Drawing.Point(935, 10);
            this.lblFailed.Name = "lblFailed";
            this.lblFailed.Size = new System.Drawing.Size(39, 14);
            this.lblFailed.TabIndex = 21;
            this.lblFailed.Text = "Failed";
            this.lblFailed.Click += new System.EventHandler(this.lblFailed_Click);
            // 
            // lblEnrolledErrorCount
            // 
            this.lblEnrolledErrorCount.AutoSize = true;
            this.lblEnrolledErrorCount.BackColor = System.Drawing.Color.Transparent;
            this.lblEnrolledErrorCount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblEnrolledErrorCount.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnrolledErrorCount.ForeColor = System.Drawing.Color.IndianRed;
            this.lblEnrolledErrorCount.Location = new System.Drawing.Point(980, 8);
            this.lblEnrolledErrorCount.Name = "lblEnrolledErrorCount";
            this.lblEnrolledErrorCount.Size = new System.Drawing.Size(17, 18);
            this.lblEnrolledErrorCount.TabIndex = 20;
            this.lblEnrolledErrorCount.Text = "0";
            this.lblEnrolledErrorCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEnrolledErrorCount.Click += new System.EventHandler(this.lblEnrolledErrorCount_Click);
            // 
            // lblTodayName
            // 
            this.lblTodayName.AutoSize = true;
            this.lblTodayName.BackColor = System.Drawing.Color.Transparent;
            this.lblTodayName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblTodayName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTodayName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(75)))));
            this.lblTodayName.Location = new System.Drawing.Point(769, 8);
            this.lblTodayName.Name = "lblTodayName";
            this.lblTodayName.Size = new System.Drawing.Size(33, 14);
            this.lblTodayName.TabIndex = 15;
            this.lblTodayName.Text = "Total";
            this.lblTodayName.Click += new System.EventHandler(this.lblTodayName_Click);
            // 
            // lblEnrolledToday
            // 
            this.lblEnrolledToday.AutoSize = true;
            this.lblEnrolledToday.BackColor = System.Drawing.Color.Transparent;
            this.lblEnrolledToday.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblEnrolledToday.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnrolledToday.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(144)))), ((int)(((byte)(75)))));
            this.lblEnrolledToday.Location = new System.Drawing.Point(816, 8);
            this.lblEnrolledToday.Name = "lblEnrolledToday";
            this.lblEnrolledToday.Size = new System.Drawing.Size(17, 18);
            this.lblEnrolledToday.TabIndex = 14;
            this.lblEnrolledToday.Text = "0";
            this.lblEnrolledToday.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblEnrolledToday.Click += new System.EventHandler(this.lblEnrolledToday_Click);
            // 
            // lblPrinting
            // 
            this.lblPrinting.AutoSize = true;
            this.lblPrinting.BackColor = System.Drawing.Color.Transparent;
            this.lblPrinting.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrinting.ForeColor = System.Drawing.Color.Gray;
            this.lblPrinting.Location = new System.Drawing.Point(227, 10);
            this.lblPrinting.Name = "lblPrinting";
            this.lblPrinting.Size = new System.Drawing.Size(0, 14);
            this.lblPrinting.TabIndex = 3;
            // 
            // lblUploadStatus
            // 
            this.lblUploadStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblUploadStatus.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUploadStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblUploadStatus.Location = new System.Drawing.Point(111, 12);
            this.lblUploadStatus.Name = "lblUploadStatus";
            this.lblUploadStatus.Size = new System.Drawing.Size(243, 13);
            this.lblUploadStatus.TabIndex = 3;
            // 
            // picUploadFailedStatus
            // 
            this.picUploadFailedStatus.BackColor = System.Drawing.Color.Transparent;
            this.picUploadFailedStatus.Image = global::ISTL.RAB.Properties.Resources.st_uploading_stopped;
            this.picUploadFailedStatus.Location = new System.Drawing.Point(88, 10);
            this.picUploadFailedStatus.Name = "picUploadFailedStatus";
            this.picUploadFailedStatus.Size = new System.Drawing.Size(17, 17);
            this.picUploadFailedStatus.TabIndex = 3;
            this.picUploadFailedStatus.TabStop = false;
            this.picUploadFailedStatus.Visible = false;
            // 
            // picUploadStatus
            // 
            this.picUploadStatus.BackColor = System.Drawing.Color.Transparent;
            this.picUploadStatus.Image = global::ISTL.RAB.Properties.Resources.st_uploading;
            this.picUploadStatus.Location = new System.Drawing.Point(88, 10);
            this.picUploadStatus.Name = "picUploadStatus";
            this.picUploadStatus.Size = new System.Drawing.Size(17, 17);
            this.picUploadStatus.TabIndex = 2;
            this.picUploadStatus.TabStop = false;
            this.picUploadStatus.Visible = false;
            // 
            // picOfflineStatus
            // 
            this.picOfflineStatus.BackColor = System.Drawing.Color.Transparent;
            this.picOfflineStatus.BackgroundImage = global::ISTL.RAB.Properties.Resources.st_network_offline;
            this.picOfflineStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picOfflineStatus.Location = new System.Drawing.Point(3, 3);
            this.picOfflineStatus.Name = "picOfflineStatus";
            this.picOfflineStatus.Size = new System.Drawing.Size(30, 30);
            this.picOfflineStatus.TabIndex = 1;
            this.picOfflineStatus.TabStop = false;
            this.picOfflineStatus.Visible = false;
            // 
            // picOnlineStatus
            // 
            this.picOnlineStatus.BackColor = System.Drawing.Color.Transparent;
            this.picOnlineStatus.BackgroundImage = global::ISTL.RAB.Properties.Resources.st_network_online;
            this.picOnlineStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.picOnlineStatus.Location = new System.Drawing.Point(3, 3);
            this.picOnlineStatus.Name = "picOnlineStatus";
            this.picOnlineStatus.Size = new System.Drawing.Size(30, 30);
            this.picOnlineStatus.TabIndex = 0;
            this.picOnlineStatus.TabStop = false;
            this.picOnlineStatus.Visible = false;
            // 
            // Mainmenu
            // 
            this.Mainmenu.AutoSize = false;
            this.Mainmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.Mainmenu.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.Mainmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HomeMenu,
            this.NewEntryMenu,
            this.SearchMenu,
            this.userManagementMenu,
            this.localRecords});
            this.Mainmenu.Location = new System.Drawing.Point(0, 0);
            this.Mainmenu.Name = "Mainmenu";
            this.Mainmenu.ShowItemToolTips = true;
            this.Mainmenu.Size = new System.Drawing.Size(1252, 34);
            this.Mainmenu.TabIndex = 3;
            this.Mainmenu.Text = "mainMenu";
            // 
            // HomeMenu
            // 
            this.HomeMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.HomeMenu.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.HomeMenu.Name = "HomeMenu";
            this.HomeMenu.Size = new System.Drawing.Size(52, 30);
            this.HomeMenu.Text = "Home";
            this.HomeMenu.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // NewEntryMenu
            // 
            this.NewEntryMenu.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.NewEntryMenu.Name = "NewEntryMenu";
            this.NewEntryMenu.Size = new System.Drawing.Size(73, 30);
            this.NewEntryMenu.Text = "New Entry";
            this.NewEntryMenu.Visible = false;
            this.NewEntryMenu.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // SearchMenu
            // 
            this.SearchMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DemographicSearchMenuItem,
            this.BiometricSearchMenuItem});
            this.SearchMenu.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.SearchMenu.Name = "SearchMenu";
            this.SearchMenu.Size = new System.Drawing.Size(54, 30);
            this.SearchMenu.Text = "Search";
            this.SearchMenu.Visible = false;
            this.SearchMenu.Click += new System.EventHandler(this.SearchMenu_Click);
            // 
            // DemographicSearchMenuItem
            // 
            this.DemographicSearchMenuItem.Name = "DemographicSearchMenuItem";
            this.DemographicSearchMenuItem.Size = new System.Drawing.Size(184, 22);
            this.DemographicSearchMenuItem.Text = "Demographic Search";
            this.DemographicSearchMenuItem.Click += new System.EventHandler(this.DemographicSearchMenuItem_Click);
            // 
            // BiometricSearchMenuItem
            // 
            this.BiometricSearchMenuItem.Name = "BiometricSearchMenuItem";
            this.BiometricSearchMenuItem.Size = new System.Drawing.Size(184, 22);
            this.BiometricSearchMenuItem.Text = "Biometric Search";
            this.BiometricSearchMenuItem.Click += new System.EventHandler(this.BiometricSearchMenuItem_Click);
            // 
            // userManagementMenu
            // 
            this.userManagementMenu.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.userManagementMenu.Name = "userManagementMenu";
            this.userManagementMenu.Size = new System.Drawing.Size(116, 30);
            this.userManagementMenu.Text = "User Management";
            this.userManagementMenu.Visible = false;
            this.userManagementMenu.Click += new System.EventHandler(this.userManagementMenu_Click);
            // 
            // localRecords
            // 
            this.localRecords.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDraftRecords,
            this.btnUploadPending,
            this.btnFailedUploads,
            this.searchByFingerToolStripMenuItem,
            this.exportDatabaseToolStripMenuItem,
            this.importDatabaseToolStripMenuItem});
            this.localRecords.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.localRecords.Name = "localRecords";
            this.localRecords.Size = new System.Drawing.Size(92, 30);
            this.localRecords.Text = "Local Records";
            this.localRecords.Visible = false;
            // 
            // btnDraftRecords
            // 
            this.btnDraftRecords.Name = "btnDraftRecords";
            this.btnDraftRecords.Size = new System.Drawing.Size(257, 22);
            this.btnDraftRecords.Text = "Draft Records";
            this.btnDraftRecords.Click += new System.EventHandler(this.btnDraftRecords_Click);
            // 
            // btnUploadPending
            // 
            this.btnUploadPending.Name = "btnUploadPending";
            this.btnUploadPending.Size = new System.Drawing.Size(257, 22);
            this.btnUploadPending.Text = "Upload Pending";
            this.btnUploadPending.Click += new System.EventHandler(this.btnUploadPending_Click);
            // 
            // btnFailedUploads
            // 
            this.btnFailedUploads.Name = "btnFailedUploads";
            this.btnFailedUploads.Size = new System.Drawing.Size(257, 22);
            this.btnFailedUploads.Text = "Failed Uploads";
            this.btnFailedUploads.Click += new System.EventHandler(this.btnFailedUploads_Click);
            // 
            // searchByFingerToolStripMenuItem
            // 
            this.searchByFingerToolStripMenuItem.Name = "searchByFingerToolStripMenuItem";
            this.searchByFingerToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.searchByFingerToolStripMenuItem.Text = "Search by Fingerprint in NID Server";
            this.searchByFingerToolStripMenuItem.Click += new System.EventHandler(this.searchByFingerToolStripMenuItem_Click);
            // 
            // exportDatabaseToolStripMenuItem
            // 
            this.exportDatabaseToolStripMenuItem.Name = "exportDatabaseToolStripMenuItem";
            this.exportDatabaseToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.exportDatabaseToolStripMenuItem.Text = "Export Database";
            this.exportDatabaseToolStripMenuItem.Click += new System.EventHandler(this.exportDatabaseToolStripMenuItem_Click);
            // 
            // importDatabaseToolStripMenuItem
            // 
            this.importDatabaseToolStripMenuItem.Name = "importDatabaseToolStripMenuItem";
            this.importDatabaseToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.importDatabaseToolStripMenuItem.Text = "Import Database";
            this.importDatabaseToolStripMenuItem.Click += new System.EventHandler(this.importDatabaseToolStripMenuItem_Click);
            // 
            // loggerInUser
            // 
            this.loggerInUser.AutoSize = true;
            this.loggerInUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.loggerInUser.ForeColor = System.Drawing.Color.White;
            this.loggerInUser.Location = new System.Drawing.Point(978, 11);
            this.loggerInUser.Name = "loggerInUser";
            this.loggerInUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.loggerInUser.Size = new System.Drawing.Size(36, 13);
            this.loggerInUser.TabIndex = 5;
            this.loggerInUser.Text = "Admin";
            this.loggerInUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // iconPictureBox1
            // 
            this.iconPictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.UserCircle;
            this.iconPictureBox1.IconColor = System.Drawing.Color.White;
            this.iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconPictureBox1.IconSize = 26;
            this.iconPictureBox1.Location = new System.Drawing.Point(952, 6);
            this.iconPictureBox1.Name = "iconPictureBox1";
            this.iconPictureBox1.Size = new System.Drawing.Size(26, 26);
            this.iconPictureBox1.TabIndex = 10;
            this.iconPictureBox1.TabStop = false;
            // 
            // iconButton2
            // 
            this.iconButton2.BackColor = System.Drawing.Color.IndianRed;
            this.iconButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconButton2.FlatAppearance.BorderSize = 0;
            this.iconButton2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.IndianRed;
            this.iconButton2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Crimson;
            this.iconButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconButton2.ForeColor = System.Drawing.Color.White;
            this.iconButton2.IconChar = FontAwesome.Sharp.IconChar.SignOutAlt;
            this.iconButton2.IconColor = System.Drawing.Color.White;
            this.iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton2.IconSize = 22;
            this.iconButton2.Location = new System.Drawing.Point(1080, 2);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Size = new System.Drawing.Size(90, 30);
            this.iconButton2.TabIndex = 9;
            this.iconButton2.Text = "Logout";
            this.iconButton2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.iconButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.iconButton2.UseVisualStyleBackColor = false;
            this.iconButton2.Click += new System.EventHandler(this.logout_Click);
            // 
            // btnMinize
            // 
            this.btnMinize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.btnMinize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinize.FlatAppearance.BorderSize = 0;
            this.btnMinize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.btnMinize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btnMinize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinize.IconChar = FontAwesome.Sharp.IconChar.Minus;
            this.btnMinize.IconColor = System.Drawing.Color.White;
            this.btnMinize.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMinize.IconSize = 22;
            this.btnMinize.Location = new System.Drawing.Point(1184, 1);
            this.btnMinize.Name = "btnMinize";
            this.btnMinize.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnMinize.Size = new System.Drawing.Size(32, 32);
            this.btnMinize.TabIndex = 8;
            this.btnMinize.UseVisualStyleBackColor = false;
            this.btnMinize.Click += new System.EventHandler(this.btnMinize_Click);
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.exitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitButton.FlatAppearance.BorderSize = 0;
            this.exitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Crimson;
            this.exitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Crimson;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.IconChar = FontAwesome.Sharp.IconChar.PowerOff;
            this.exitButton.IconColor = System.Drawing.Color.White;
            this.exitButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.exitButton.IconSize = 22;
            this.exitButton.Location = new System.Drawing.Point(1219, 1);
            this.exitButton.Name = "exitButton";
            this.exitButton.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.exitButton.Size = new System.Drawing.Size(32, 32);
            this.exitButton.TabIndex = 7;
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panelMain
            // 
            this.panelMain.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMain.Location = new System.Drawing.Point(0, 35);
            this.panelMain.Name = "panelMain";
            this.panelMain.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panelMain.Size = new System.Drawing.Size(1252, 650);
            this.panelMain.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(1252, 720);
            this.Controls.Add(this.iconPictureBox1);
            this.Controls.Add(this.iconButton2);
            this.Controls.Add(this.btnMinize);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.loggerInUser);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.Mainmenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SNSOP TOOLS";
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnNoUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picErrorStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picIrisSuccessFailed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFPSuccessFailed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPhotoSuccessFailed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUploadFailedStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUploadStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOfflineStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOnlineStatus)).EndInit();
            this.Mainmenu.ResumeLayout(false);
            this.Mainmenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lblPrinting;
        private System.Windows.Forms.Label lblUploadStatus;
        private System.Windows.Forms.PictureBox picUploadFailedStatus;
        private System.Windows.Forms.PictureBox picUploadStatus;
        private System.Windows.Forms.PictureBox picOfflineStatus;
        private System.Windows.Forms.PictureBox picOnlineStatus;
        private System.Windows.Forms.MenuStrip Mainmenu;
        private System.Windows.Forms.Label loggerInUser;
        private System.Windows.Forms.ToolStripMenuItem NewEntryMenu;
        private System.Windows.Forms.ToolStripMenuItem SearchMenu;
        private System.Windows.Forms.ToolStripMenuItem HomeMenu;
        private FontAwesome.Sharp.IconButton exitButton;
        private FontAwesome.Sharp.IconButton btnMinize;
        private System.Windows.Forms.ToolStripMenuItem userManagementMenu;
        private FontAwesome.Sharp.IconButton iconButton2;
        private System.Windows.Forms.Label lblTodayName;
        private System.Windows.Forms.Label lblEnrolledToday;
        private System.Windows.Forms.Label lblFailed;
        private System.Windows.Forms.Label lblEnrolledErrorCount;
        private FontAwesome.Sharp.IconPictureBox picIrisSuccessFailed;
        private FontAwesome.Sharp.IconPictureBox picFPSuccessFailed;
        private FontAwesome.Sharp.IconPictureBox picPhotoSuccessFailed;
        private System.Windows.Forms.Label lblErrorStatus;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblUploaded;
        private System.Windows.Forms.Label lblUploadedCount;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private FontAwesome.Sharp.IconPictureBox picErrorStatus;
        private System.Windows.Forms.ToolStripMenuItem localRecords;
        private System.Windows.Forms.ToolStripMenuItem btnDraftRecords;
        private System.Windows.Forms.ToolStripMenuItem btnUploadPending;
        private System.Windows.Forms.ToolStripMenuItem btnFailedUploads;
        private System.Windows.Forms.Label lblPending;
        private System.Windows.Forms.Label lblPendingCount;
        private System.Windows.Forms.PictureBox btnNoUpdate;
        private System.Windows.Forms.PictureBox btnUpdate;
        private System.Windows.Forms.Label lblOnlineOffline;
        private System.Windows.Forms.ToolStripMenuItem searchByFingerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DemographicSearchMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BiometricSearchMenuItem;
    }
}