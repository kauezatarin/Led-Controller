namespace LedController
{
    partial class Firmware_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Firmware_form));
            this.tb_code = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tb_code
            // 
            this.tb_code.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tb_code.Location = new System.Drawing.Point(12, 12);
            this.tb_code.Multiline = true;
            this.tb_code.Name = "tb_code";
            this.tb_code.ReadOnly = true;
            this.tb_code.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_code.Size = new System.Drawing.Size(580, 409);
            this.tb_code.TabIndex = 0;
            this.tb_code.Text = resources.GetString("tb_code.Text");
            // 
            // Firmware_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 433);
            this.Controls.Add(this.tb_code);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Firmware_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Firmware";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_code;
    }
}