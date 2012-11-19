namespace serveruserinterface
{
    partial class main
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receiversToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.receiverDisplay = new System.Windows.Forms.TabControl();
            this.receiverStatus = new System.Windows.Forms.TabPage();
            this.stationName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comPort = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.serialNumber = new System.Windows.Forms.Label();
            this.receiverName = new System.Windows.Forms.Label();
            this.receiverConsole = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.receiverList = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.receiverDisplay.SuspendLayout();
            this.receiverStatus.SuspendLayout();
            this.receiverConsole.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverToolStripMenuItem,
            this.receiversToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(672, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.serverToolStripMenuItem.Text = "Server";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.startToolStripMenuItem.Text = "Start";
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            // 
            // receiversToolStripMenuItem
            // 
            this.receiversToolStripMenuItem.Name = "receiversToolStripMenuItem";
            this.receiversToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.receiversToolStripMenuItem.Text = "Receivers";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 421);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(672, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(184, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 5;
            // 
            // receiverDisplay
            // 
            this.receiverDisplay.Controls.Add(this.receiverStatus);
            this.receiverDisplay.Controls.Add(this.receiverConsole);
            this.receiverDisplay.Location = new System.Drawing.Point(184, 28);
            this.receiverDisplay.Name = "receiverDisplay";
            this.receiverDisplay.SelectedIndex = 0;
            this.receiverDisplay.Size = new System.Drawing.Size(488, 390);
            this.receiverDisplay.TabIndex = 6;
            // 
            // receiverStatus
            // 
            this.receiverStatus.Controls.Add(this.stationName);
            this.receiverStatus.Controls.Add(this.label3);
            this.receiverStatus.Controls.Add(this.comPort);
            this.receiverStatus.Controls.Add(this.label2);
            this.receiverStatus.Controls.Add(this.serialNumber);
            this.receiverStatus.Controls.Add(this.receiverName);
            this.receiverStatus.Location = new System.Drawing.Point(4, 22);
            this.receiverStatus.Name = "receiverStatus";
            this.receiverStatus.Padding = new System.Windows.Forms.Padding(3);
            this.receiverStatus.Size = new System.Drawing.Size(480, 364);
            this.receiverStatus.TabIndex = 0;
            this.receiverStatus.Text = "Status";
            this.receiverStatus.UseVisualStyleBackColor = true;
            // 
            // stationName
            // 
            this.stationName.AutoSize = true;
            this.stationName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stationName.Location = new System.Drawing.Point(6, 158);
            this.stationName.Name = "stationName";
            this.stationName.Size = new System.Drawing.Size(89, 16);
            this.stationName.TabIndex = 16;
            this.stationName.Text = "Station Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 16);
            this.label3.TabIndex = 15;
            this.label3.Text = "Station Name";
            // 
            // comPort
            // 
            this.comPort.AutoSize = true;
            this.comPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comPort.Location = new System.Drawing.Point(6, 94);
            this.comPort.Name = "comPort";
            this.comPort.Size = new System.Drawing.Size(65, 16);
            this.comPort.TabIndex = 14;
            this.comPort.Text = "COM Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "Model";
            // 
            // serialNumber
            // 
            this.serialNumber.AutoSize = true;
            this.serialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serialNumber.Location = new System.Drawing.Point(6, 35);
            this.serialNumber.Name = "serialNumber";
            this.serialNumber.Size = new System.Drawing.Size(94, 16);
            this.serialNumber.TabIndex = 12;
            this.serialNumber.Text = "Serial Number";
            // 
            // receiverName
            // 
            this.receiverName.AutoSize = true;
            this.receiverName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.receiverName.Location = new System.Drawing.Point(6, 6);
            this.receiverName.Name = "receiverName";
            this.receiverName.Size = new System.Drawing.Size(63, 16);
            this.receiverName.TabIndex = 11;
            this.receiverName.Text = "Receiver";
            // 
            // receiverConsole
            // 
            this.receiverConsole.Controls.Add(this.textBox1);
            this.receiverConsole.Location = new System.Drawing.Point(4, 22);
            this.receiverConsole.Name = "receiverConsole";
            this.receiverConsole.Padding = new System.Windows.Forms.Padding(3);
            this.receiverConsole.Size = new System.Drawing.Size(480, 364);
            this.receiverConsole.TabIndex = 1;
            this.receiverConsole.Text = "Console";
            this.receiverConsole.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.MinimumSize = new System.Drawing.Size(200, 200);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(479, 358);
            this.textBox1.TabIndex = 0;
            // 
            // receiverList
            // 
            this.receiverList.FormattingEnabled = true;
            this.receiverList.Location = new System.Drawing.Point(13, 28);
            this.receiverList.Name = "receiverList";
            this.receiverList.Size = new System.Drawing.Size(165, 381);
            this.receiverList.TabIndex = 7;
            this.receiverList.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 443);
            this.Controls.Add(this.receiverList);
            this.Controls.Add(this.receiverDisplay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "main";
            this.Load += new System.EventHandler(this.main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.receiverDisplay.ResumeLayout(false);
            this.receiverStatus.ResumeLayout(false);
            this.receiverStatus.PerformLayout();
            this.receiverConsole.ResumeLayout(false);
            this.receiverConsole.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem receiversToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl receiverDisplay;
        private System.Windows.Forms.TabPage receiverStatus;
        private System.Windows.Forms.Label stationName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label comPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label serialNumber;
        private System.Windows.Forms.Label receiverName;
        private System.Windows.Forms.TabPage receiverConsole;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox receiverList;
    }
}

