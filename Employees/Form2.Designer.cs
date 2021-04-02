namespace Employees
{
    partial class statWindow
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
            this.statTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.statDGV = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.statDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // statTimePicker
            // 
            this.statTimePicker.Enabled = false;
            this.statTimePicker.Location = new System.Drawing.Point(72, 17);
            this.statTimePicker.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.statTimePicker.Name = "statTimePicker";
            this.statTimePicker.Size = new System.Drawing.Size(109, 20);
            this.statTimePicker.TabIndex = 1;
            this.statTimePicker.ValueChanged += new System.EventHandler(this.statTimePicker_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Данные на";
            // 
            // statDGV
            // 
            this.statDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.statDGV.Location = new System.Drawing.Point(11, 58);
            this.statDGV.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.statDGV.Name = "statDGV";
            this.statDGV.RowTemplate.Height = 28;
            this.statDGV.Size = new System.Drawing.Size(323, 322);
            this.statDGV.TabIndex = 3;
            // 
            // statWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 391);
            this.Controls.Add(this.statDGV);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statTimePicker);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "statWindow";
            this.Text = "Статистика";
            this.Load += new System.EventHandler(this.statWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.statDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker statTimePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView statDGV;
    }
}