namespace ReceiverSetup
{
    partial class ReceiversForm
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
            this.ReceiversGridView = new System.Windows.Forms.DataGridView();
            this.StatusToggleBtn = new System.Windows.Forms.Button();
            this.Description = new System.Windows.Forms.TextBox();
            this.RedLabel = new System.Windows.Forms.Label();
            this.OffLabel = new System.Windows.Forms.Label();
            this.OnLabel = new System.Windows.Forms.Label();
            this.GreenLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.ReceiversGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReceiversGridView
            // 
            this.ReceiversGridView.AllowUserToAddRows = false;
            this.ReceiversGridView.AllowUserToDeleteRows = false;
            this.ReceiversGridView.BackgroundColor = System.Drawing.Color.White;
            this.ReceiversGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ReceiversGridView.Location = new System.Drawing.Point(15, 41);
            this.ReceiversGridView.MultiSelect = false;
            this.ReceiversGridView.Name = "ReceiversGridView";
            this.ReceiversGridView.ReadOnly = true;
            this.ReceiversGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.ReceiversGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ReceiversGridView.Size = new System.Drawing.Size(411, 261);
            this.ReceiversGridView.TabIndex = 0;
            this.ReceiversGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ReceiversGridView_CellMouseClicked);
            this.ReceiversGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.ReceiversGridView_DataBindingCompleted);
            // 
            // StatusToggleBtn
            // 
            this.StatusToggleBtn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.StatusToggleBtn.Location = new System.Drawing.Point(15, 308);
            this.StatusToggleBtn.Name = "StatusToggleBtn";
            this.StatusToggleBtn.Size = new System.Drawing.Size(75, 23);
            this.StatusToggleBtn.TabIndex = 1;
            this.StatusToggleBtn.Text = "Turn Off";
            this.StatusToggleBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.StatusToggleBtn.UseVisualStyleBackColor = false;
            this.StatusToggleBtn.Click += new System.EventHandler(this.StatusToggleBtn_Clicked);
            // 
            // Description
            // 
            this.Description.Location = new System.Drawing.Point(6, 19);
            this.Description.Multiline = true;
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Size = new System.Drawing.Size(400, 113);
            this.Description.TabIndex = 2;
            // 
            // RedLabel
            // 
            this.RedLabel.BackColor = System.Drawing.Color.Red;
            this.RedLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.RedLabel.Location = new System.Drawing.Point(407, 23);
            this.RedLabel.Name = "RedLabel";
            this.RedLabel.Size = new System.Drawing.Size(19, 15);
            this.RedLabel.TabIndex = 3;
            // 
            // OffLabel
            // 
            this.OffLabel.AutoSize = true;
            this.OffLabel.Location = new System.Drawing.Point(380, 25);
            this.OffLabel.Name = "OffLabel";
            this.OffLabel.Size = new System.Drawing.Size(24, 13);
            this.OffLabel.TabIndex = 5;
            this.OffLabel.Text = "Off:";
            // 
            // OnLabel
            // 
            this.OnLabel.AutoSize = true;
            this.OnLabel.Location = new System.Drawing.Point(325, 24);
            this.OnLabel.Name = "OnLabel";
            this.OnLabel.Size = new System.Drawing.Size(24, 13);
            this.OnLabel.TabIndex = 6;
            this.OnLabel.Text = "On:";
            // 
            // GreenLabel
            // 
            this.GreenLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.GreenLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GreenLabel.Location = new System.Drawing.Point(355, 23);
            this.GreenLabel.Name = "GreenLabel";
            this.GreenLabel.Size = new System.Drawing.Size(19, 15);
            this.GreenLabel.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Description);
            this.groupBox1.Location = new System.Drawing.Point(9, 337);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(414, 138);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Description";
            // 
            // ReceiversForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 486);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GreenLabel);
            this.Controls.Add(this.OnLabel);
            this.Controls.Add(this.OffLabel);
            this.Controls.Add(this.RedLabel);
            this.Controls.Add(this.ReceiversGridView);
            this.Controls.Add(this.StatusToggleBtn);
            this.Name = "ReceiversForm";
            this.Text = "Receivers Status";
            ((System.ComponentModel.ISupportInitialize)(this.ReceiversGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ReceiversGridView;
        private System.Windows.Forms.Button StatusToggleBtn;
        private System.Windows.Forms.TextBox Description;
        private System.Windows.Forms.Label RedLabel;
        private System.Windows.Forms.Label OffLabel;
        private System.Windows.Forms.Label OnLabel;
        private System.Windows.Forms.Label GreenLabel;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

