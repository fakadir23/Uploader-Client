
namespace ISTL.RAB.View
{
    partial class PersonMatchDialogForm
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
            this.lblMatchFoundFlag = new System.Windows.Forms.Label();
            this.tbRefNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnNotMatch = new System.Windows.Forms.Button();
            this.btnMatch = new System.Windows.Forms.Button();
            this.tabControlLayout = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panelMatch1 = new System.Windows.Forms.Panel();
            this.lblNumberOfMatches = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.tbOccupationMatch1 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tbPhoneMatch1 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tbCriminalNameMatch1 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbNickNameMatch1 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tbGenderMatch1 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbDOBMatch1 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbFullNameMatch1 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbNIDmatch1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBoxMatch1 = new System.Windows.Forms.PictureBox();
            this.label16 = new System.Windows.Forms.Label();
            this.lblMatchPercentage = new System.Windows.Forms.Label();
            this.pictureBoxPhoto = new System.Windows.Forms.PictureBox();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.iconButton2 = new FontAwesome.Sharp.IconButton();
            this.exitButton = new FontAwesome.Sharp.IconButton();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControlLayout.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panelMatch1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMatch1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMatchFoundFlag
            // 
            this.lblMatchFoundFlag.AutoSize = true;
            this.lblMatchFoundFlag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatchFoundFlag.Location = new System.Drawing.Point(904, 95);
            this.lblMatchFoundFlag.Name = "lblMatchFoundFlag";
            this.lblMatchFoundFlag.Size = new System.Drawing.Size(96, 16);
            this.lblMatchFoundFlag.TabIndex = 292;
            this.lblMatchFoundFlag.Text = "Match Found";
            // 
            // tbRefNo
            // 
            this.tbRefNo.Location = new System.Drawing.Point(30, 91);
            this.tbRefNo.Name = "tbRefNo";
            this.tbRefNo.Size = new System.Drawing.Size(217, 20);
            this.tbRefNo.TabIndex = 291;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 290;
            this.label6.Text = "Reference No:";
            // 
            // btnNotMatch
            // 
            this.btnNotMatch.BackColor = System.Drawing.Color.Crimson;
            this.btnNotMatch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNotMatch.FlatAppearance.BorderSize = 0;
            this.btnNotMatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNotMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNotMatch.ForeColor = System.Drawing.Color.White;
            this.btnNotMatch.Location = new System.Drawing.Point(1120, 86);
            this.btnNotMatch.Name = "btnNotMatch";
            this.btnNotMatch.Size = new System.Drawing.Size(100, 35);
            this.btnNotMatch.TabIndex = 289;
            this.btnNotMatch.Text = "Not Match";
            this.btnNotMatch.UseVisualStyleBackColor = false;
            // 
            // btnMatch
            // 
            this.btnMatch.BackColor = System.Drawing.Color.SeaGreen;
            this.btnMatch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMatch.FlatAppearance.BorderSize = 0;
            this.btnMatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMatch.ForeColor = System.Drawing.Color.White;
            this.btnMatch.Location = new System.Drawing.Point(1014, 86);
            this.btnMatch.Name = "btnMatch";
            this.btnMatch.Size = new System.Drawing.Size(100, 35);
            this.btnMatch.TabIndex = 288;
            this.btnMatch.Text = "Match";
            this.btnMatch.UseVisualStyleBackColor = false;
            // 
            // tabControlLayout
            // 
            this.tabControlLayout.Controls.Add(this.tabPage1);
            this.tabControlLayout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlLayout.Location = new System.Drawing.Point(669, 129);
            this.tabControlLayout.Name = "tabControlLayout";
            this.tabControlLayout.SelectedIndex = 0;
            this.tabControlLayout.Size = new System.Drawing.Size(551, 440);
            this.tabControlLayout.TabIndex = 287;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelMatch1);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(543, 411);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Match";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelMatch1
            // 
            this.panelMatch1.Controls.Add(this.lblNumberOfMatches);
            this.panelMatch1.Controls.Add(this.btnNext);
            this.panelMatch1.Controls.Add(this.btnPrevious);
            this.panelMatch1.Controls.Add(this.tbOccupationMatch1);
            this.panelMatch1.Controls.Add(this.label17);
            this.panelMatch1.Controls.Add(this.tbPhoneMatch1);
            this.panelMatch1.Controls.Add(this.label18);
            this.panelMatch1.Controls.Add(this.tbCriminalNameMatch1);
            this.panelMatch1.Controls.Add(this.label14);
            this.panelMatch1.Controls.Add(this.tbNickNameMatch1);
            this.panelMatch1.Controls.Add(this.label15);
            this.panelMatch1.Controls.Add(this.tbGenderMatch1);
            this.panelMatch1.Controls.Add(this.label13);
            this.panelMatch1.Controls.Add(this.tbDOBMatch1);
            this.panelMatch1.Controls.Add(this.label12);
            this.panelMatch1.Controls.Add(this.tbFullNameMatch1);
            this.panelMatch1.Controls.Add(this.label10);
            this.panelMatch1.Controls.Add(this.tbNIDmatch1);
            this.panelMatch1.Controls.Add(this.label9);
            this.panelMatch1.Controls.Add(this.pictureBoxMatch1);
            this.panelMatch1.Location = new System.Drawing.Point(17, 15);
            this.panelMatch1.Name = "panelMatch1";
            this.panelMatch1.Size = new System.Drawing.Size(491, 333);
            this.panelMatch1.TabIndex = 300;
            // 
            // lblNumberOfMatches
            // 
            this.lblNumberOfMatches.AutoSize = true;
            this.lblNumberOfMatches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfMatches.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblNumberOfMatches.Location = new System.Drawing.Point(12, 301);
            this.lblNumberOfMatches.Name = "lblNumberOfMatches";
            this.lblNumberOfMatches.Size = new System.Drawing.Size(178, 16);
            this.lblNumberOfMatches.TabIndex = 300;
            this.lblNumberOfMatches.Text = "Number of Matches Found: 0";
            // 
            // btnNext
            // 
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.Location = new System.Drawing.Point(283, 297);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(35, 23);
            this.btnNext.TabIndex = 299;
            this.btnNext.Text = ">";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevious.Location = new System.Drawing.Point(233, 297);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(35, 23);
            this.btnPrevious.TabIndex = 298;
            this.btnPrevious.Text = "<";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // tbOccupationMatch1
            // 
            this.tbOccupationMatch1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.tbOccupationMatch1.Location = new System.Drawing.Point(162, 254);
            this.tbOccupationMatch1.MaxLength = 150;
            this.tbOccupationMatch1.Name = "tbOccupationMatch1";
            this.tbOccupationMatch1.Size = new System.Drawing.Size(217, 21);
            this.tbOccupationMatch1.TabIndex = 297;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label17.Location = new System.Drawing.Point(74, 257);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(70, 27);
            this.label17.TabIndex = 296;
            this.label17.Text = "Occupation:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbPhoneMatch1
            // 
            this.tbPhoneMatch1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.tbPhoneMatch1.Location = new System.Drawing.Point(162, 227);
            this.tbPhoneMatch1.MaxLength = 150;
            this.tbPhoneMatch1.Name = "tbPhoneMatch1";
            this.tbPhoneMatch1.Size = new System.Drawing.Size(217, 21);
            this.tbPhoneMatch1.TabIndex = 295;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label18.Location = new System.Drawing.Point(71, 230);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(70, 27);
            this.label18.TabIndex = 294;
            this.label18.Text = "Phone:";
            this.label18.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbCriminalNameMatch1
            // 
            this.tbCriminalNameMatch1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.tbCriminalNameMatch1.Location = new System.Drawing.Point(162, 200);
            this.tbCriminalNameMatch1.MaxLength = 150;
            this.tbCriminalNameMatch1.Name = "tbCriminalNameMatch1";
            this.tbCriminalNameMatch1.Size = new System.Drawing.Size(217, 21);
            this.tbCriminalNameMatch1.TabIndex = 293;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label14.Location = new System.Drawing.Point(52, 197);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(101, 27);
            this.label14.TabIndex = 292;
            this.label14.Text = "Criminal Name:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbNickNameMatch1
            // 
            this.tbNickNameMatch1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.tbNickNameMatch1.Location = new System.Drawing.Point(162, 173);
            this.tbNickNameMatch1.MaxLength = 150;
            this.tbNickNameMatch1.Name = "tbNickNameMatch1";
            this.tbNickNameMatch1.Size = new System.Drawing.Size(217, 21);
            this.tbNickNameMatch1.TabIndex = 291;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label15.Location = new System.Drawing.Point(74, 176);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(70, 27);
            this.label15.TabIndex = 290;
            this.label15.Text = "Nick Name";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbGenderMatch1
            // 
            this.tbGenderMatch1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.tbGenderMatch1.Location = new System.Drawing.Point(267, 106);
            this.tbGenderMatch1.MaxLength = 150;
            this.tbGenderMatch1.Name = "tbGenderMatch1";
            this.tbGenderMatch1.Size = new System.Drawing.Size(217, 21);
            this.tbGenderMatch1.TabIndex = 289;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label13.Location = new System.Drawing.Point(203, 109);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 27);
            this.label13.TabIndex = 288;
            this.label13.Text = "Gender:";
            // 
            // tbDOBMatch1
            // 
            this.tbDOBMatch1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.tbDOBMatch1.Location = new System.Drawing.Point(267, 79);
            this.tbDOBMatch1.MaxLength = 150;
            this.tbDOBMatch1.Name = "tbDOBMatch1";
            this.tbDOBMatch1.Size = new System.Drawing.Size(217, 21);
            this.tbDOBMatch1.TabIndex = 287;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label12.Location = new System.Drawing.Point(178, 82);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 27);
            this.label12.TabIndex = 286;
            this.label12.Text = "Date of Birth:";
            // 
            // tbFullNameMatch1
            // 
            this.tbFullNameMatch1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.tbFullNameMatch1.Location = new System.Drawing.Point(267, 52);
            this.tbFullNameMatch1.MaxLength = 150;
            this.tbFullNameMatch1.Name = "tbFullNameMatch1";
            this.tbFullNameMatch1.Size = new System.Drawing.Size(217, 21);
            this.tbFullNameMatch1.TabIndex = 283;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label10.Location = new System.Drawing.Point(187, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 27);
            this.label10.TabIndex = 282;
            this.label10.Text = "Full Name:";
            // 
            // tbNIDmatch1
            // 
            this.tbNIDmatch1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.tbNIDmatch1.Location = new System.Drawing.Point(267, 25);
            this.tbNIDmatch1.MaxLength = 150;
            this.tbNIDmatch1.Name = "tbNIDmatch1";
            this.tbNIDmatch1.Size = new System.Drawing.Size(217, 21);
            this.tbNIDmatch1.TabIndex = 281;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label9.Location = new System.Drawing.Point(178, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 27);
            this.label9.TabIndex = 280;
            this.label9.Text = "National ID:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pictureBoxMatch1
            // 
            this.pictureBoxMatch1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBoxMatch1.Location = new System.Drawing.Point(27, 16);
            this.pictureBoxMatch1.Name = "pictureBoxMatch1";
            this.pictureBoxMatch1.Size = new System.Drawing.Size(135, 135);
            this.pictureBoxMatch1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxMatch1.TabIndex = 280;
            this.pictureBoxMatch1.TabStop = false;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label16.Location = new System.Drawing.Point(91, 332);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 27);
            this.label16.TabIndex = 298;
            this.label16.Text = "Nationality:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblMatchPercentage
            // 
            this.lblMatchPercentage.AutoSize = true;
            this.lblMatchPercentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatchPercentage.Location = new System.Drawing.Point(27, 279);
            this.lblMatchPercentage.Name = "lblMatchPercentage";
            this.lblMatchPercentage.Size = new System.Drawing.Size(94, 16);
            this.lblMatchPercentage.TabIndex = 293;
            this.lblMatchPercentage.Text = "Match Score";
            // 
            // pictureBoxPhoto
            // 
            this.pictureBoxPhoto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pictureBoxPhoto.Location = new System.Drawing.Point(30, 129);
            this.pictureBoxPhoto.Name = "pictureBoxPhoto";
            this.pictureBoxPhoto.Size = new System.Drawing.Size(135, 135);
            this.pictureBoxPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxPhoto.TabIndex = 286;
            this.pictureBoxPhoto.TabStop = false;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.iconButton2);
            this.panelHeader.Controls.Add(this.exitButton);
            this.panelHeader.Controls.Add(this.label3);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1259, 40);
            this.panelHeader.TabIndex = 294;
            // 
            // iconButton2
            // 
            this.iconButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.iconButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.iconButton2.FlatAppearance.BorderSize = 0;
            this.iconButton2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.iconButton2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.iconButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconButton2.IconChar = FontAwesome.Sharp.IconChar.Minus;
            this.iconButton2.IconColor = System.Drawing.Color.White;
            this.iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton2.IconSize = 22;
            this.iconButton2.Location = new System.Drawing.Point(1187, 3);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.iconButton2.Size = new System.Drawing.Size(32, 32);
            this.iconButton2.TabIndex = 93;
            this.iconButton2.UseVisualStyleBackColor = false;
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
            this.exitButton.Location = new System.Drawing.Point(1223, 3);
            this.exitButton.Name = "exitButton";
            this.exitButton.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.exitButton.Size = new System.Drawing.Size(32, 32);
            this.exitButton.TabIndex = 92;
            this.exitButton.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1123, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "Crimial Match Result";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PersonMatchDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1259, 600);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.lblMatchPercentage);
            this.Controls.Add(this.lblMatchFoundFlag);
            this.Controls.Add(this.tbRefNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnNotMatch);
            this.Controls.Add(this.btnMatch);
            this.Controls.Add(this.tabControlLayout);
            this.Controls.Add(this.pictureBoxPhoto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PersonMatchDialogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PersonMatchDialogForm";
            this.tabControlLayout.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panelMatch1.ResumeLayout(false);
            this.panelMatch1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMatch1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMatchFoundFlag;
        private System.Windows.Forms.TextBox tbRefNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnNotMatch;
        private System.Windows.Forms.Button btnMatch;
        private System.Windows.Forms.TabControl tabControlLayout;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panelMatch1;
        private System.Windows.Forms.Label lblNumberOfMatches;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.TextBox tbOccupationMatch1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tbPhoneMatch1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbCriminalNameMatch1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbNickNameMatch1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbGenderMatch1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbDOBMatch1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbFullNameMatch1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbNIDmatch1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBoxMatch1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.PictureBox pictureBoxPhoto;
        private System.Windows.Forms.Label lblMatchPercentage;
        private System.Windows.Forms.Panel panelHeader;
        private FontAwesome.Sharp.IconButton iconButton2;
        private FontAwesome.Sharp.IconButton exitButton;
        private System.Windows.Forms.Label label3;
    }
}