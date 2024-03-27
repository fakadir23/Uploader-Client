
namespace ISTL.RAB.View.New.Home
{
    partial class JailDbMatchForm
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
            this.iconBtnMinimize = new FontAwesome.Sharp.IconButton();
            this.exitButton = new FontAwesome.Sharp.IconButton();
            this.lblMatchFoundFlag = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbPermanentAddress = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbPresentAddress = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbReligion = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbMobileNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblJailMatchScore = new System.Windows.Forms.Label();
            this.btnJailMatchNext = new FontAwesome.Sharp.IconButton();
            this.btnJailMatchPrev = new FontAwesome.Sharp.IconButton();
            this.lblJailMatchNo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblJailMatchCount = new System.Windows.Forms.Label();
            this.tbFullName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDob = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbConvictNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbNID = new System.Windows.Forms.TextBox();
            this.pbJailPhoto = new System.Windows.Forms.PictureBox();
            this.btnCancel = new FontAwesome.Sharp.IconButton();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbJailPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.iconBtnMinimize);
            this.panel1.Controls.Add(this.exitButton);
            this.panel1.Controls.Add(this.lblMatchFoundFlag);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1250, 32);
            this.panel1.TabIndex = 299;
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
            this.iconBtnMinimize.Location = new System.Drawing.Point(1162, -1);
            this.iconBtnMinimize.Name = "iconBtnMinimize";
            this.iconBtnMinimize.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.iconBtnMinimize.Size = new System.Drawing.Size(32, 32);
            this.iconBtnMinimize.TabIndex = 91;
            this.iconBtnMinimize.UseVisualStyleBackColor = false;
            this.iconBtnMinimize.Visible = false;
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
            this.exitButton.Location = new System.Drawing.Point(1196, -1);
            this.exitButton.Name = "exitButton";
            this.exitButton.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.exitButton.Size = new System.Drawing.Size(32, 32);
            this.exitButton.TabIndex = 90;
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // lblMatchFoundFlag
            // 
            this.lblMatchFoundFlag.AutoSize = true;
            this.lblMatchFoundFlag.BackColor = System.Drawing.Color.Transparent;
            this.lblMatchFoundFlag.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatchFoundFlag.ForeColor = System.Drawing.Color.White;
            this.lblMatchFoundFlag.Location = new System.Drawing.Point(8, 5);
            this.lblMatchFoundFlag.Name = "lblMatchFoundFlag";
            this.lblMatchFoundFlag.Size = new System.Drawing.Size(107, 18);
            this.lblMatchFoundFlag.TabIndex = 285;
            this.lblMatchFoundFlag.Text = "Match Result";
            this.lblMatchFoundFlag.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.tbPermanentAddress);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.tbPresentAddress);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tbReligion);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.tbMobileNo);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lblJailMatchScore);
            this.groupBox2.Controls.Add(this.btnJailMatchNext);
            this.groupBox2.Controls.Add(this.btnJailMatchPrev);
            this.groupBox2.Controls.Add(this.lblJailMatchNo);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblJailMatchCount);
            this.groupBox2.Controls.Add(this.tbFullName);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tbDob);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tbConvictNo);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tbNID);
            this.groupBox2.Controls.Add(this.pbJailPhoto);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(323, 49);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(585, 613);
            this.groupBox2.TabIndex = 300;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Jail Data";
            // 
            // tbPermanentAddress
            // 
            this.tbPermanentAddress.BackColor = System.Drawing.Color.White;
            this.tbPermanentAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPermanentAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPermanentAddress.Location = new System.Drawing.Point(141, 428);
            this.tbPermanentAddress.MaxLength = 150;
            this.tbPermanentAddress.Multiline = true;
            this.tbPermanentAddress.Name = "tbPermanentAddress";
            this.tbPermanentAddress.ReadOnly = true;
            this.tbPermanentAddress.Size = new System.Drawing.Size(397, 60);
            this.tbPermanentAddress.TabIndex = 324;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(48, 438);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 41);
            this.label10.TabIndex = 323;
            this.label10.Text = "Permanent Address";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbPresentAddress
            // 
            this.tbPresentAddress.BackColor = System.Drawing.Color.White;
            this.tbPresentAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPresentAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPresentAddress.Location = new System.Drawing.Point(141, 362);
            this.tbPresentAddress.MaxLength = 150;
            this.tbPresentAddress.Multiline = true;
            this.tbPresentAddress.Name = "tbPresentAddress";
            this.tbPresentAddress.ReadOnly = true;
            this.tbPresentAddress.Size = new System.Drawing.Size(397, 60);
            this.tbPresentAddress.TabIndex = 322;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(48, 371);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 41);
            this.label9.TabIndex = 321;
            this.label9.Text = "Present Address";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbReligion
            // 
            this.tbReligion.BackColor = System.Drawing.Color.White;
            this.tbReligion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbReligion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbReligion.Location = new System.Drawing.Point(342, 316);
            this.tbReligion.MaxLength = 150;
            this.tbReligion.Name = "tbReligion";
            this.tbReligion.ReadOnly = true;
            this.tbReligion.Size = new System.Drawing.Size(196, 24);
            this.tbReligion.TabIndex = 320;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(233, 316);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 21);
            this.label8.TabIndex = 319;
            this.label8.Text = "Religion";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbMobileNo
            // 
            this.tbMobileNo.BackColor = System.Drawing.Color.White;
            this.tbMobileNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMobileNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMobileNo.Location = new System.Drawing.Point(342, 286);
            this.tbMobileNo.MaxLength = 150;
            this.tbMobileNo.Name = "tbMobileNo";
            this.tbMobileNo.ReadOnly = true;
            this.tbMobileNo.Size = new System.Drawing.Size(196, 24);
            this.tbMobileNo.TabIndex = 318;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(222, 286);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 21);
            this.label6.TabIndex = 317;
            this.label6.Text = "Mobile Number";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblJailMatchScore
            // 
            this.lblJailMatchScore.AutoSize = true;
            this.lblJailMatchScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJailMatchScore.ForeColor = System.Drawing.Color.Black;
            this.lblJailMatchScore.Location = new System.Drawing.Point(233, 129);
            this.lblJailMatchScore.Name = "lblJailMatchScore";
            this.lblJailMatchScore.Size = new System.Drawing.Size(109, 18);
            this.lblJailMatchScore.TabIndex = 314;
            this.lblJailMatchScore.Text = "Match Score:";
            // 
            // btnJailMatchNext
            // 
            this.btnJailMatchNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnJailMatchNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnJailMatchNext.FlatAppearance.BorderSize = 0;
            this.btnJailMatchNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJailMatchNext.IconChar = FontAwesome.Sharp.IconChar.ChevronRight;
            this.btnJailMatchNext.IconColor = System.Drawing.Color.White;
            this.btnJailMatchNext.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnJailMatchNext.IconSize = 22;
            this.btnJailMatchNext.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnJailMatchNext.Location = new System.Drawing.Point(286, 514);
            this.btnJailMatchNext.Name = "btnJailMatchNext";
            this.btnJailMatchNext.Size = new System.Drawing.Size(40, 35);
            this.btnJailMatchNext.TabIndex = 315;
            this.btnJailMatchNext.UseVisualStyleBackColor = false;
            this.btnJailMatchNext.Click += new System.EventHandler(this.btnJailMatchNext_Click);
            // 
            // btnJailMatchPrev
            // 
            this.btnJailMatchPrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnJailMatchPrev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnJailMatchPrev.FlatAppearance.BorderSize = 0;
            this.btnJailMatchPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJailMatchPrev.IconChar = FontAwesome.Sharp.IconChar.ChevronLeft;
            this.btnJailMatchPrev.IconColor = System.Drawing.Color.White;
            this.btnJailMatchPrev.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnJailMatchPrev.IconSize = 22;
            this.btnJailMatchPrev.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnJailMatchPrev.Location = new System.Drawing.Point(235, 514);
            this.btnJailMatchPrev.Name = "btnJailMatchPrev";
            this.btnJailMatchPrev.Size = new System.Drawing.Size(40, 35);
            this.btnJailMatchPrev.TabIndex = 314;
            this.btnJailMatchPrev.UseVisualStyleBackColor = false;
            this.btnJailMatchPrev.Click += new System.EventHandler(this.btnJailMatchPrev_Click);
            // 
            // lblJailMatchNo
            // 
            this.lblJailMatchNo.AutoSize = true;
            this.lblJailMatchNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJailMatchNo.ForeColor = System.Drawing.Color.Black;
            this.lblJailMatchNo.Location = new System.Drawing.Point(48, 129);
            this.lblJailMatchNo.Name = "lblJailMatchNo";
            this.lblJailMatchNo.Size = new System.Drawing.Size(69, 18);
            this.lblJailMatchNo.TabIndex = 314;
            this.lblJailMatchNo.Text = "Match-1";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(283, 163);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 21);
            this.label1.TabIndex = 261;
            this.label1.Text = "Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblJailMatchCount
            // 
            this.lblJailMatchCount.AutoSize = true;
            this.lblJailMatchCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJailMatchCount.ForeColor = System.Drawing.Color.Black;
            this.lblJailMatchCount.Location = new System.Drawing.Point(48, 78);
            this.lblJailMatchCount.Name = "lblJailMatchCount";
            this.lblJailMatchCount.Size = new System.Drawing.Size(227, 18);
            this.lblJailMatchCount.TabIndex = 300;
            this.lblJailMatchCount.Text = "Number of Matches Found: 1";
            // 
            // tbFullName
            // 
            this.tbFullName.BackColor = System.Drawing.Color.White;
            this.tbFullName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFullName.Location = new System.Drawing.Point(342, 163);
            this.tbFullName.MaxLength = 150;
            this.tbFullName.Name = "tbFullName";
            this.tbFullName.ReadOnly = true;
            this.tbFullName.Size = new System.Drawing.Size(196, 24);
            this.tbFullName.TabIndex = 262;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(243, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 21);
            this.label2.TabIndex = 263;
            this.label2.Text = "Date of Birth";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDob
            // 
            this.tbDob.BackColor = System.Drawing.Color.White;
            this.tbDob.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDob.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDob.Location = new System.Drawing.Point(342, 193);
            this.tbDob.MaxLength = 150;
            this.tbDob.Name = "tbDob";
            this.tbDob.ReadOnly = true;
            this.tbDob.Size = new System.Drawing.Size(196, 24);
            this.tbDob.TabIndex = 264;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(233, 255);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 21);
            this.label4.TabIndex = 265;
            this.label4.Text = "Convict No";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbConvictNo
            // 
            this.tbConvictNo.BackColor = System.Drawing.Color.White;
            this.tbConvictNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbConvictNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbConvictNo.Location = new System.Drawing.Point(342, 253);
            this.tbConvictNo.MaxLength = 150;
            this.tbConvictNo.Name = "tbConvictNo";
            this.tbConvictNo.ReadOnly = true;
            this.tbConvictNo.Size = new System.Drawing.Size(196, 24);
            this.tbConvictNo.TabIndex = 266;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(243, 226);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 21);
            this.label5.TabIndex = 267;
            this.label5.Text = "National ID";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbNID
            // 
            this.tbNID.BackColor = System.Drawing.Color.White;
            this.tbNID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbNID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNID.Location = new System.Drawing.Point(342, 223);
            this.tbNID.MaxLength = 150;
            this.tbNID.Name = "tbNID";
            this.tbNID.ReadOnly = true;
            this.tbNID.Size = new System.Drawing.Size(196, 24);
            this.tbNID.TabIndex = 268;
            // 
            // pbJailPhoto
            // 
            this.pbJailPhoto.BackColor = System.Drawing.Color.White;
            this.pbJailPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbJailPhoto.Location = new System.Drawing.Point(51, 163);
            this.pbJailPhoto.Name = "pbJailPhoto";
            this.pbJailPhoto.Size = new System.Drawing.Size(165, 183);
            this.pbJailPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbJailPhoto.TabIndex = 280;
            this.pbJailPhoto.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.IndianRed;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.IconChar = FontAwesome.Sharp.IconChar.StepBackward;
            this.btnCancel.IconColor = System.Drawing.Color.White;
            this.btnCancel.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCancel.IconSize = 22;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancel.Location = new System.Drawing.Point(992, 627);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(203, 35);
            this.btnCancel.TabIndex = 317;
            this.btnCancel.Text = "Back";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // JailDbMatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MintCream;
            this.ClientSize = new System.Drawing.Size(1250, 725);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "JailDbMatchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbJailPhoto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton iconBtnMinimize;
        private FontAwesome.Sharp.IconButton exitButton;
        private System.Windows.Forms.Label lblMatchFoundFlag;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbPermanentAddress;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbPresentAddress;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbReligion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbMobileNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblJailMatchScore;
        private FontAwesome.Sharp.IconButton btnJailMatchNext;
        private FontAwesome.Sharp.IconButton btnJailMatchPrev;
        private System.Windows.Forms.Label lblJailMatchNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblJailMatchCount;
        private System.Windows.Forms.TextBox tbFullName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDob;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbConvictNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbNID;
        private System.Windows.Forms.PictureBox pbJailPhoto;
        private FontAwesome.Sharp.IconButton btnCancel;
    }
}