﻿namespace DaneZPlikuOkienko
{
    partial class Form2
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
            this.rtbGlowne = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbGlowne
            // 
            this.rtbGlowne.Location = new System.Drawing.Point(30, 26);
            this.rtbGlowne.Name = "rtbGlowne";
            this.rtbGlowne.Size = new System.Drawing.Size(527, 419);
            this.rtbGlowne.TabIndex = 0;
            this.rtbGlowne.Text = "";
            this.rtbGlowne.TextChanged += new System.EventHandler(this.rtbGlowne_TextChanged);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 457);
            this.Controls.Add(this.rtbGlowne);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbGlowne;
    }
}