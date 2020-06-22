namespace 视觉框架
{
    partial class Frm_Cameras1
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
            this.dpWin1 = new ChoiceTech.Halcon.Control.DpWin();
            this.SuspendLayout();
            // 
            // dpWin1
            // 
            this.dpWin1.BackColor = System.Drawing.Color.Transparent;
            this.dpWin1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dpWin1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dpWin1.DrawModel = false;
            this.dpWin1.Image = null;
            this.dpWin1.Location = new System.Drawing.Point(0, 0);
            this.dpWin1.Name = "dpWin1";
            this.dpWin1.Size = new System.Drawing.Size(823, 468);
            this.dpWin1.StringHeight = 8;
            this.dpWin1.TabIndex = 0;
            // 
            // Frm_Cameras1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 468);
            this.Controls.Add(this.dpWin1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_Cameras1";
            this.Text = "Frm_Cameras1";
            this.ResumeLayout(false);

        }

        #endregion

        private ChoiceTech.Halcon.Control.DpWin dpWin1;
    }
}