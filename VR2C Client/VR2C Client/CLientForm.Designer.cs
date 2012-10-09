namespace VR2C_Client
{
    partial class CLientForm
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
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Receiver1");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Receiver2");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Receivers", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Transmitter1");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Transmitter2");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Transmitters", new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode15});
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Manhattan Beach");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Huntington Beach");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Stations", new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode18});
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("All Detections", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode16,
            treeNode19});
            this.MainFormPanel = new System.Windows.Forms.Panel();
            this.FilterPanel = new System.Windows.Forms.Panel();
            this.TimeRangeToLabel = new System.Windows.Forms.Label();
            this.TimeRangeToTextBox = new System.Windows.Forms.TextBox();
            this.TimeRangeFromTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TransIdTextBox = new System.Windows.Forms.TextBox();
            this.TransIdLabel = new System.Windows.Forms.Label();
            this.DateRangeLabel = new System.Windows.Forms.Label();
            this.DateRangeToLabel = new System.Windows.Forms.Label();
            this.DateRangeFromTextBox = new System.Windows.Forms.TextBox();
            this.DateRangeToTextBox = new System.Windows.Forms.TextBox();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.DataTab = new System.Windows.Forms.TabPage();
            this.NumOfDetectionsTextBox = new System.Windows.Forms.TextBox();
            this.ClientDataGridView = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodeSpace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Transmitter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Receiver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClientTreeView = new System.Windows.Forms.TreeView();
            this.AbacusTab = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealTimeLabel = new System.Windows.Forms.Label();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.emailAlertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToReceiverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainFormPanel.SuspendLayout();
            this.FilterPanel.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.DataTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClientDataGridView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // MainFormPanel
            // 
            this.MainFormPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MainFormPanel.Controls.Add(this.dataGridView1);
            this.MainFormPanel.Controls.Add(this.FilterPanel);
            this.MainFormPanel.Controls.Add(this.ClientTreeView);
            this.MainFormPanel.Controls.Add(this.RealTimeLabel);
            this.MainFormPanel.Controls.Add(this.TabControl);
            this.MainFormPanel.Location = new System.Drawing.Point(1, 27);
            this.MainFormPanel.Name = "MainFormPanel";
            this.MainFormPanel.Size = new System.Drawing.Size(1081, 641);
            this.MainFormPanel.TabIndex = 1;
            this.MainFormPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MainFormPanel_Paint);
            // 
            // FilterPanel
            // 
            this.FilterPanel.BackColor = System.Drawing.Color.LightGray;
            this.FilterPanel.Controls.Add(this.SearchButton);
            this.FilterPanel.Controls.Add(this.TimeRangeToLabel);
            this.FilterPanel.Controls.Add(this.TimeRangeToTextBox);
            this.FilterPanel.Controls.Add(this.TimeRangeFromTextBox);
            this.FilterPanel.Controls.Add(this.label1);
            this.FilterPanel.Controls.Add(this.TransIdTextBox);
            this.FilterPanel.Controls.Add(this.TransIdLabel);
            this.FilterPanel.Controls.Add(this.DateRangeLabel);
            this.FilterPanel.Controls.Add(this.DateRangeToLabel);
            this.FilterPanel.Controls.Add(this.DateRangeFromTextBox);
            this.FilterPanel.Controls.Add(this.DateRangeToTextBox);
            this.FilterPanel.Location = new System.Drawing.Point(171, 3);
            this.FilterPanel.Name = "FilterPanel";
            this.FilterPanel.Size = new System.Drawing.Size(896, 61);
            this.FilterPanel.TabIndex = 5;
            // 
            // TimeRangeToLabel
            // 
            this.TimeRangeToLabel.AutoSize = true;
            this.TimeRangeToLabel.Location = new System.Drawing.Point(494, 31);
            this.TimeRangeToLabel.Name = "TimeRangeToLabel";
            this.TimeRangeToLabel.Size = new System.Drawing.Size(16, 13);
            this.TimeRangeToLabel.TabIndex = 12;
            this.TimeRangeToLabel.Text = "to";
            // 
            // TimeRangeToTextBox
            // 
            this.TimeRangeToTextBox.BackColor = System.Drawing.Color.LightBlue;
            this.TimeRangeToTextBox.Location = new System.Drawing.Point(516, 27);
            this.TimeRangeToTextBox.Name = "TimeRangeToTextBox";
            this.TimeRangeToTextBox.Size = new System.Drawing.Size(100, 20);
            this.TimeRangeToTextBox.TabIndex = 11;
            // 
            // TimeRangeFromTextBox
            // 
            this.TimeRangeFromTextBox.BackColor = System.Drawing.Color.LightBlue;
            this.TimeRangeFromTextBox.Location = new System.Drawing.Point(388, 28);
            this.TimeRangeFromTextBox.Name = "TimeRangeFromTextBox";
            this.TimeRangeFromTextBox.Size = new System.Drawing.Size(100, 20);
            this.TimeRangeFromTextBox.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(385, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Time Range:  (HH:MM)";
            // 
            // TransIdTextBox
            // 
            this.TransIdTextBox.BackColor = System.Drawing.Color.LightBlue;
            this.TransIdTextBox.Location = new System.Drawing.Point(690, 27);
            this.TransIdTextBox.Name = "TransIdTextBox";
            this.TransIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.TransIdTextBox.TabIndex = 8;
            // 
            // TransIdLabel
            // 
            this.TransIdLabel.AutoSize = true;
            this.TransIdLabel.Location = new System.Drawing.Point(687, 8);
            this.TransIdLabel.Name = "TransIdLabel";
            this.TransIdLabel.Size = new System.Drawing.Size(76, 13);
            this.TransIdLabel.TabIndex = 7;
            this.TransIdLabel.Text = "Transmitter ID:";
            // 
            // DateRangeLabel
            // 
            this.DateRangeLabel.AutoSize = true;
            this.DateRangeLabel.Location = new System.Drawing.Point(106, 8);
            this.DateRangeLabel.Name = "DateRangeLabel";
            this.DateRangeLabel.Size = new System.Drawing.Size(134, 13);
            this.DateRangeLabel.TabIndex = 3;
            this.DateRangeLabel.Text = "Date Range:  (YY-MM-DD)";
            // 
            // DateRangeToLabel
            // 
            this.DateRangeToLabel.AutoSize = true;
            this.DateRangeToLabel.Location = new System.Drawing.Point(215, 31);
            this.DateRangeToLabel.Name = "DateRangeToLabel";
            this.DateRangeToLabel.Size = new System.Drawing.Size(16, 13);
            this.DateRangeToLabel.TabIndex = 4;
            this.DateRangeToLabel.Text = "to";
            // 
            // DateRangeFromTextBox
            // 
            this.DateRangeFromTextBox.BackColor = System.Drawing.Color.LightBlue;
            this.DateRangeFromTextBox.Location = new System.Drawing.Point(109, 27);
            this.DateRangeFromTextBox.Name = "DateRangeFromTextBox";
            this.DateRangeFromTextBox.Size = new System.Drawing.Size(100, 20);
            this.DateRangeFromTextBox.TabIndex = 1;
            // 
            // DateRangeToTextBox
            // 
            this.DateRangeToTextBox.BackColor = System.Drawing.Color.LightBlue;
            this.DateRangeToTextBox.Location = new System.Drawing.Point(237, 27);
            this.DateRangeToTextBox.Name = "DateRangeToTextBox";
            this.DateRangeToTextBox.Size = new System.Drawing.Size(100, 20);
            this.DateRangeToTextBox.TabIndex = 2;
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.DataTab);
            this.TabControl.Controls.Add(this.AbacusTab);
            this.TabControl.Location = new System.Drawing.Point(171, 70);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(903, 367);
            this.TabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl.TabIndex = 0;
            // 
            // DataTab
            // 
            this.DataTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DataTab.Controls.Add(this.NumOfDetectionsTextBox);
            this.DataTab.Controls.Add(this.ClientDataGridView);
            this.DataTab.Location = new System.Drawing.Point(4, 22);
            this.DataTab.Name = "DataTab";
            this.DataTab.Padding = new System.Windows.Forms.Padding(3);
            this.DataTab.Size = new System.Drawing.Size(895, 341);
            this.DataTab.TabIndex = 0;
            this.DataTab.Text = "Data";
            // 
            // NumOfDetectionsTextBox
            // 
            this.NumOfDetectionsTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.NumOfDetectionsTextBox.Enabled = false;
            this.NumOfDetectionsTextBox.Location = new System.Drawing.Point(3, 6);
            this.NumOfDetectionsTextBox.Name = "NumOfDetectionsTextBox";
            this.NumOfDetectionsTextBox.Size = new System.Drawing.Size(886, 20);
            this.NumOfDetectionsTextBox.TabIndex = 2;
            this.NumOfDetectionsTextBox.Text = "0 Detections";
            this.NumOfDetectionsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ClientDataGridView
            // 
            this.ClientDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.ClientDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ClientDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ClientDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Date,
            this.Time,
            this.CodeSpace,
            this.Id,
            this.Transmitter,
            this.Receiver,
            this.Station,
            this.Data});
            this.ClientDataGridView.Location = new System.Drawing.Point(6, 29);
            this.ClientDataGridView.Name = "ClientDataGridView";
            this.ClientDataGridView.ReadOnly = true;
            this.ClientDataGridView.Size = new System.Drawing.Size(883, 306);
            this.ClientDataGridView.TabIndex = 1;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // Time
            // 
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // CodeSpace
            // 
            this.CodeSpace.HeaderText = "Code Space";
            this.CodeSpace.Name = "CodeSpace";
            this.CodeSpace.ReadOnly = true;
            // 
            // Id
            // 
            this.Id.HeaderText = "ID";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // Transmitter
            // 
            this.Transmitter.HeaderText = "Transmitter";
            this.Transmitter.Name = "Transmitter";
            this.Transmitter.ReadOnly = true;
            // 
            // Receiver
            // 
            this.Receiver.HeaderText = "Receiver";
            this.Receiver.Name = "Receiver";
            this.Receiver.ReadOnly = true;
            // 
            // Station
            // 
            this.Station.HeaderText = "Station";
            this.Station.Name = "Station";
            this.Station.ReadOnly = true;
            // 
            // Data
            // 
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            // 
            // ClientTreeView
            // 
            this.ClientTreeView.Location = new System.Drawing.Point(3, 3);
            this.ClientTreeView.Name = "ClientTreeView";
            treeNode11.Name = "Receiver1";
            treeNode11.Text = "Receiver1";
            treeNode12.Name = "Receiver2";
            treeNode12.Text = "Receiver2";
            treeNode13.Name = "Receivers";
            treeNode13.Text = "Receivers";
            treeNode14.Name = "Transmitter1";
            treeNode14.Text = "Transmitter1";
            treeNode15.Name = "Transmitter2";
            treeNode15.Text = "Transmitter2";
            treeNode16.Name = "Transmitters";
            treeNode16.Text = "Transmitters";
            treeNode17.Name = "ManhattanBeach";
            treeNode17.Text = "Manhattan Beach";
            treeNode18.Name = "HuntingtonBeach";
            treeNode18.Text = "Huntington Beach";
            treeNode19.Name = "Stations";
            treeNode19.Text = "Stations";
            treeNode20.Name = "AllDetections";
            treeNode20.Text = "All Detections";
            this.ClientTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode20});
            this.ClientTreeView.Size = new System.Drawing.Size(162, 631);
            this.ClientTreeView.TabIndex = 0;
            // 
            // AbacusTab
            // 
            this.AbacusTab.Location = new System.Drawing.Point(4, 22);
            this.AbacusTab.Name = "AbacusTab";
            this.AbacusTab.Padding = new System.Windows.Forms.Padding(3);
            this.AbacusTab.Size = new System.Drawing.Size(920, 318);
            this.AbacusTab.TabIndex = 1;
            this.AbacusTab.Text = "Abacus";
            this.AbacusTab.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.connectToReceiverToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1094, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.newUserToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.saveAsToolStripMenuItem1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emailAlertToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(10, 21);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 13;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dataGridView1.Location = new System.Drawing.Point(171, 468);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(899, 166);
            this.dataGridView1.TabIndex = 6;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Date";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Time";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Code Space";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "ID";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Transmitter";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Receiver";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Station";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Data";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // RealTimeLabel
            // 
            this.RealTimeLabel.BackColor = System.Drawing.Color.Turquoise;
            this.RealTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RealTimeLabel.Location = new System.Drawing.Point(171, 440);
            this.RealTimeLabel.Name = "RealTimeLabel";
            this.RealTimeLabel.Size = new System.Drawing.Size(899, 25);
            this.RealTimeLabel.TabIndex = 7;
            this.RealTimeLabel.Text = "Real Time";
            this.RealTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newProjectToolStripMenuItem.Text = "New Project";
            // 
            // newUserToolStripMenuItem
            // 
            this.newUserToolStripMenuItem.Name = "newUserToolStripMenuItem";
            this.newUserToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newUserToolStripMenuItem.Text = "Add User";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Remove User";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save";
            // 
            // saveAsToolStripMenuItem1
            // 
            this.saveAsToolStripMenuItem1.Name = "saveAsToolStripMenuItem1";
            this.saveAsToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem1.Text = "Save As";
            // 
            // emailAlertToolStripMenuItem
            // 
            this.emailAlertToolStripMenuItem.Name = "emailAlertToolStripMenuItem";
            this.emailAlertToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.emailAlertToolStripMenuItem.Text = "Email Alert";
            // 
            // connectToReceiverToolStripMenuItem
            // 
            this.connectToReceiverToolStripMenuItem.Name = "connectToReceiverToolStripMenuItem";
            this.connectToReceiverToolStripMenuItem.Size = new System.Drawing.Size(125, 20);
            this.connectToReceiverToolStripMenuItem.Text = "Connect to Receiver";
            // 
            // CLientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 680);
            this.Controls.Add(this.MainFormPanel);
            this.Controls.Add(this.menuStrip1);
            this.Name = "CLientForm";
            this.Text = "VR2C Client";
            this.MainFormPanel.ResumeLayout(false);
            this.FilterPanel.ResumeLayout(false);
            this.FilterPanel.PerformLayout();
            this.TabControl.ResumeLayout(false);
            this.DataTab.ResumeLayout(false);
            this.DataTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClientDataGridView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel MainFormPanel;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.TabPage DataTab;
        private System.Windows.Forms.TabPage AbacusTab;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.DataGridView ClientDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodeSpace;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Transmitter;
        private System.Windows.Forms.DataGridViewTextBoxColumn Receiver;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.TreeView ClientTreeView;
        private System.Windows.Forms.TextBox NumOfDetectionsTextBox;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.Panel FilterPanel;
        private System.Windows.Forms.Label DateRangeLabel;
        private System.Windows.Forms.Label DateRangeToLabel;
        private System.Windows.Forms.TextBox DateRangeFromTextBox;
        private System.Windows.Forms.TextBox DateRangeToTextBox;
        private System.Windows.Forms.TextBox TimeRangeToTextBox;
        private System.Windows.Forms.TextBox TimeRangeFromTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TransIdTextBox;
        private System.Windows.Forms.Label TransIdLabel;
        private System.Windows.Forms.Label TimeRangeToLabel;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.Label RealTimeLabel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem emailAlertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToReceiverToolStripMenuItem;

    }
}

