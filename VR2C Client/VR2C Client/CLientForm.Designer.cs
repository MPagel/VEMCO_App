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
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Receivers");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Transmitters");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Stations");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("All Detections", new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode18,
            treeNode19});
            this.Form1Panel = new System.Windows.Forms.Panel();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.DataTab = new System.Windows.Forms.TabPage();
            this.AbacusTab = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.ClientDataGridView = new System.Windows.Forms.DataGridView();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodeSpace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Transmitter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Receiver = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumOfDetectionsTextBox = new System.Windows.Forms.TextBox();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DateRangeFromTextBox = new System.Windows.Forms.TextBox();
            this.DateRangeToTextBox = new System.Windows.Forms.TextBox();
            this.DateRangeLabel = new System.Windows.Forms.Label();
            this.DateRangeToLabel = new System.Windows.Forms.Label();
            this.FilterPanel = new System.Windows.Forms.Panel();
            this.CodeSpaceLabel = new System.Windows.Forms.Label();
            this.CodeSpaceTextBox = new System.Windows.Forms.TextBox();
            this.TransIdLabel = new System.Windows.Forms.Label();
            this.TransIdTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TimeRangeFromTextBox = new System.Windows.Forms.TextBox();
            this.TimeRangeToTextBox = new System.Windows.Forms.TextBox();
            this.TimeRangeToLabel = new System.Windows.Forms.Label();
            this.Form1Panel.SuspendLayout();
            this.TabControl.SuspendLayout();
            this.DataTab.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClientDataGridView)).BeginInit();
            this.FilterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Form1Panel
            // 
            this.Form1Panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Form1Panel.Controls.Add(this.FilterPanel);
            this.Form1Panel.Controls.Add(this.TabControl);
            this.Form1Panel.Location = new System.Drawing.Point(1, 27);
            this.Form1Panel.Name = "Form1Panel";
            this.Form1Panel.Size = new System.Drawing.Size(856, 389);
            this.Form1Panel.TabIndex = 1;
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.DataTab);
            this.TabControl.Controls.Add(this.AbacusTab);
            this.TabControl.Location = new System.Drawing.Point(3, 123);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(846, 218);
            this.TabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabControl.TabIndex = 0;
            // 
            // DataTab
            // 
            this.DataTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DataTab.Controls.Add(this.NumOfDetectionsTextBox);
            this.DataTab.Controls.Add(this.ClientDataGridView);
            this.DataTab.Controls.Add(this.treeView1);
            this.DataTab.Location = new System.Drawing.Point(4, 22);
            this.DataTab.Name = "DataTab";
            this.DataTab.Padding = new System.Windows.Forms.Padding(3);
            this.DataTab.Size = new System.Drawing.Size(838, 192);
            this.DataTab.TabIndex = 0;
            this.DataTab.Text = "Data";
            // 
            // AbacusTab
            // 
            this.AbacusTab.Location = new System.Drawing.Point(4, 22);
            this.AbacusTab.Name = "AbacusTab";
            this.AbacusTab.Padding = new System.Windows.Forms.Padding(3);
            this.AbacusTab.Size = new System.Drawing.Size(838, 192);
            this.AbacusTab.TabIndex = 1;
            this.AbacusTab.Text = "Abacus";
            this.AbacusTab.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(869, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode17.Name = "Receivers";
            treeNode17.Text = "Receivers";
            treeNode18.Name = "Transmitters";
            treeNode18.Text = "Transmitters";
            treeNode19.Name = "Stations";
            treeNode19.Text = "Stations";
            treeNode20.Name = "AllDetections";
            treeNode20.Text = "All Detections";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode20});
            this.treeView1.Size = new System.Drawing.Size(161, 310);
            this.treeView1.TabIndex = 0;
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
            this.ClientDataGridView.Location = new System.Drawing.Point(167, 28);
            this.ClientDataGridView.Name = "ClientDataGridView";
            this.ClientDataGridView.ReadOnly = true;
            this.ClientDataGridView.Size = new System.Drawing.Size(665, 278);
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
            // NumOfDetectionsTextBox
            // 
            this.NumOfDetectionsTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.NumOfDetectionsTextBox.Enabled = false;
            this.NumOfDetectionsTextBox.Location = new System.Drawing.Point(167, 6);
            this.NumOfDetectionsTextBox.Name = "NumOfDetectionsTextBox";
            this.NumOfDetectionsTextBox.Size = new System.Drawing.Size(665, 20);
            this.NumOfDetectionsTextBox.TabIndex = 2;
            this.NumOfDetectionsTextBox.Text = "0 Detections";
            this.NumOfDetectionsTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // DateRangeFromTextBox
            // 
            this.DateRangeFromTextBox.BackColor = System.Drawing.Color.LightBlue;
            this.DateRangeFromTextBox.Location = new System.Drawing.Point(9, 25);
            this.DateRangeFromTextBox.Name = "DateRangeFromTextBox";
            this.DateRangeFromTextBox.Size = new System.Drawing.Size(100, 20);
            this.DateRangeFromTextBox.TabIndex = 1;
            // 
            // DateRangeToTextBox
            // 
            this.DateRangeToTextBox.BackColor = System.Drawing.Color.LightBlue;
            this.DateRangeToTextBox.Location = new System.Drawing.Point(137, 25);
            this.DateRangeToTextBox.Name = "DateRangeToTextBox";
            this.DateRangeToTextBox.Size = new System.Drawing.Size(100, 20);
            this.DateRangeToTextBox.TabIndex = 2;
            // 
            // DateRangeLabel
            // 
            this.DateRangeLabel.AutoSize = true;
            this.DateRangeLabel.Location = new System.Drawing.Point(6, 9);
            this.DateRangeLabel.Name = "DateRangeLabel";
            this.DateRangeLabel.Size = new System.Drawing.Size(134, 13);
            this.DateRangeLabel.TabIndex = 3;
            this.DateRangeLabel.Text = "Date Range:  (YY-MM-DD)";
            // 
            // DateRangeToLabel
            // 
            this.DateRangeToLabel.AutoSize = true;
            this.DateRangeToLabel.Location = new System.Drawing.Point(115, 28);
            this.DateRangeToLabel.Name = "DateRangeToLabel";
            this.DateRangeToLabel.Size = new System.Drawing.Size(16, 13);
            this.DateRangeToLabel.TabIndex = 4;
            this.DateRangeToLabel.Text = "to";
            // 
            // FilterPanel
            // 
            this.FilterPanel.BackColor = System.Drawing.Color.LightGray;
            this.FilterPanel.Controls.Add(this.TimeRangeToLabel);
            this.FilterPanel.Controls.Add(this.TimeRangeToTextBox);
            this.FilterPanel.Controls.Add(this.TimeRangeFromTextBox);
            this.FilterPanel.Controls.Add(this.label1);
            this.FilterPanel.Controls.Add(this.TransIdTextBox);
            this.FilterPanel.Controls.Add(this.TransIdLabel);
            this.FilterPanel.Controls.Add(this.CodeSpaceTextBox);
            this.FilterPanel.Controls.Add(this.CodeSpaceLabel);
            this.FilterPanel.Controls.Add(this.DateRangeLabel);
            this.FilterPanel.Controls.Add(this.DateRangeToLabel);
            this.FilterPanel.Controls.Add(this.DateRangeFromTextBox);
            this.FilterPanel.Controls.Add(this.DateRangeToTextBox);
            this.FilterPanel.Location = new System.Drawing.Point(3, 3);
            this.FilterPanel.Name = "FilterPanel";
            this.FilterPanel.Size = new System.Drawing.Size(846, 101);
            this.FilterPanel.TabIndex = 5;
            // 
            // CodeSpaceLabel
            // 
            this.CodeSpaceLabel.AutoSize = true;
            this.CodeSpaceLabel.Location = new System.Drawing.Point(272, 9);
            this.CodeSpaceLabel.Name = "CodeSpaceLabel";
            this.CodeSpaceLabel.Size = new System.Drawing.Size(117, 13);
            this.CodeSpaceLabel.TabIndex = 5;
            this.CodeSpaceLabel.Text = "Frequency Codespace:";
            // 
            // CodeSpaceTextBox
            // 
            this.CodeSpaceTextBox.BackColor = System.Drawing.Color.LightBlue;
            this.CodeSpaceTextBox.Location = new System.Drawing.Point(275, 25);
            this.CodeSpaceTextBox.Name = "CodeSpaceTextBox";
            this.CodeSpaceTextBox.Size = new System.Drawing.Size(100, 20);
            this.CodeSpaceTextBox.TabIndex = 6;
            // 
            // TransIdLabel
            // 
            this.TransIdLabel.AutoSize = true;
            this.TransIdLabel.Location = new System.Drawing.Point(434, 9);
            this.TransIdLabel.Name = "TransIdLabel";
            this.TransIdLabel.Size = new System.Drawing.Size(76, 13);
            this.TransIdLabel.TabIndex = 7;
            this.TransIdLabel.Text = "Transmitter ID:";
            // 
            // TransIdTextBox
            // 
            this.TransIdTextBox.BackColor = System.Drawing.Color.LightBlue;
            this.TransIdTextBox.Location = new System.Drawing.Point(437, 25);
            this.TransIdTextBox.Name = "TransIdTextBox";
            this.TransIdTextBox.Size = new System.Drawing.Size(100, 20);
            this.TransIdTextBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Time Range:  (HH:MM)";
            // 
            // TimeRangeFromTextBox
            // 
            this.TimeRangeFromTextBox.BackColor = System.Drawing.Color.LightBlue;
            this.TimeRangeFromTextBox.Location = new System.Drawing.Point(9, 74);
            this.TimeRangeFromTextBox.Name = "TimeRangeFromTextBox";
            this.TimeRangeFromTextBox.Size = new System.Drawing.Size(100, 20);
            this.TimeRangeFromTextBox.TabIndex = 10;
            // 
            // TimeRangeToTextBox
            // 
            this.TimeRangeToTextBox.BackColor = System.Drawing.Color.LightBlue;
            this.TimeRangeToTextBox.Location = new System.Drawing.Point(137, 74);
            this.TimeRangeToTextBox.Name = "TimeRangeToTextBox";
            this.TimeRangeToTextBox.Size = new System.Drawing.Size(100, 20);
            this.TimeRangeToTextBox.TabIndex = 11;
            // 
            // TimeRangeToLabel
            // 
            this.TimeRangeToLabel.AutoSize = true;
            this.TimeRangeToLabel.Location = new System.Drawing.Point(115, 77);
            this.TimeRangeToLabel.Name = "TimeRangeToLabel";
            this.TimeRangeToLabel.Size = new System.Drawing.Size(16, 13);
            this.TimeRangeToLabel.TabIndex = 12;
            this.TimeRangeToLabel.Text = "to";
            // 
            // CLientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 419);
            this.Controls.Add(this.Form1Panel);
            this.Controls.Add(this.menuStrip1);
            this.Name = "CLientForm";
            this.Text = "VR2C Client";
            this.Form1Panel.ResumeLayout(false);
            this.TabControl.ResumeLayout(false);
            this.DataTab.ResumeLayout(false);
            this.DataTab.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClientDataGridView)).EndInit();
            this.FilterPanel.ResumeLayout(false);
            this.FilterPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Form1Panel;
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
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TextBox NumOfDetectionsTextBox;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.Panel FilterPanel;
        private System.Windows.Forms.TextBox CodeSpaceTextBox;
        private System.Windows.Forms.Label CodeSpaceLabel;
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

    }
}

