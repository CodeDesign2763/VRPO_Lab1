﻿namespace Employees
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
            // dateTimePicker1
            // 
            this.statTimePicker.Location = new System.Drawing.Point(108, 26);
            this.statTimePicker.Name = "dateTimePicker1";
            this.statTimePicker.Size = new System.Drawing.Size(161, 26);
            this.statTimePicker.TabIndex = 1;
            this.statTimePicker.ValueChanged += new System.EventHandler(this.statTimePicker_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Данные на";
            // 
            // dataGridView1
            // 
            this.statDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.statDGV.Location = new System.Drawing.Point(16, 77);
            this.statDGV.Name = "dataGridView1";
            this.statDGV.RowTemplate.Height = 28;
            this.statDGV.Size = new System.Drawing.Size(364, 284);
            this.statDGV.TabIndex = 3;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 373);
            this.Controls.Add(this.statDGV);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statTimePicker);
            this.Name = "Form2";
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