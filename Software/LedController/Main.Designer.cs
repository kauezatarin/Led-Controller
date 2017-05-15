namespace LedController
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.gb_onOff = new System.Windows.Forms.GroupBox();
            this.cb_rgb2 = new System.Windows.Forms.CheckBox();
            this.cb_rgb1 = new System.Windows.Forms.CheckBox();
            this.cb_led4 = new System.Windows.Forms.CheckBox();
            this.cb_led3 = new System.Windows.Forms.CheckBox();
            this.cb_led2 = new System.Windows.Forms.CheckBox();
            this.cb_led1 = new System.Windows.Forms.CheckBox();
            this.bt_aplicar = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bt_changeCollorRGB2 = new System.Windows.Forms.Button();
            this.bt_changeCollorRGB1 = new System.Windows.Forms.Button();
            this.tb_log = new System.Windows.Forms.TextBox();
            this.conectWatch = new System.Windows.Forms.Timer(this.components);
            this.bt_visualizar = new System.Windows.Forms.Button();
            this.bt_cancelar = new System.Windows.Forms.Button();
            this.bt_firmware = new System.Windows.Forms.Button();
            this.bt_teste = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bt_sync = new System.Windows.Forms.Button();
            this.gb_onOff.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_onOff
            // 
            this.gb_onOff.Controls.Add(this.cb_rgb2);
            this.gb_onOff.Controls.Add(this.cb_rgb1);
            this.gb_onOff.Controls.Add(this.cb_led4);
            this.gb_onOff.Controls.Add(this.cb_led3);
            this.gb_onOff.Controls.Add(this.cb_led2);
            this.gb_onOff.Controls.Add(this.cb_led1);
            this.gb_onOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_onOff.Location = new System.Drawing.Point(13, 13);
            this.gb_onOff.Name = "gb_onOff";
            this.gb_onOff.Size = new System.Drawing.Size(176, 94);
            this.gb_onOff.TabIndex = 0;
            this.gb_onOff.TabStop = false;
            this.gb_onOff.Text = "LED\'s ON/OFF";
            // 
            // cb_rgb2
            // 
            this.cb_rgb2.AutoSize = true;
            this.cb_rgb2.Location = new System.Drawing.Point(97, 67);
            this.cb_rgb2.Name = "cb_rgb2";
            this.cb_rgb2.Size = new System.Drawing.Size(63, 17);
            this.cb_rgb2.TabIndex = 5;
            this.cb_rgb2.Text = "RGB 2";
            this.cb_rgb2.UseVisualStyleBackColor = true;
            // 
            // cb_rgb1
            // 
            this.cb_rgb1.AutoSize = true;
            this.cb_rgb1.Location = new System.Drawing.Point(7, 67);
            this.cb_rgb1.Name = "cb_rgb1";
            this.cb_rgb1.Size = new System.Drawing.Size(63, 17);
            this.cb_rgb1.TabIndex = 4;
            this.cb_rgb1.Text = "RGB 1";
            this.cb_rgb1.UseVisualStyleBackColor = true;
            // 
            // cb_led4
            // 
            this.cb_led4.AutoSize = true;
            this.cb_led4.Location = new System.Drawing.Point(97, 44);
            this.cb_led4.Name = "cb_led4";
            this.cb_led4.Size = new System.Drawing.Size(61, 17);
            this.cb_led4.TabIndex = 3;
            this.cb_led4.Text = "LED 4";
            this.cb_led4.UseVisualStyleBackColor = true;
            // 
            // cb_led3
            // 
            this.cb_led3.AutoSize = true;
            this.cb_led3.Location = new System.Drawing.Point(97, 20);
            this.cb_led3.Name = "cb_led3";
            this.cb_led3.Size = new System.Drawing.Size(61, 17);
            this.cb_led3.TabIndex = 2;
            this.cb_led3.Text = "LED 3";
            this.cb_led3.UseVisualStyleBackColor = true;
            // 
            // cb_led2
            // 
            this.cb_led2.AutoSize = true;
            this.cb_led2.Location = new System.Drawing.Point(7, 44);
            this.cb_led2.Name = "cb_led2";
            this.cb_led2.Size = new System.Drawing.Size(61, 17);
            this.cb_led2.TabIndex = 1;
            this.cb_led2.Text = "LED 2";
            this.cb_led2.UseVisualStyleBackColor = true;
            // 
            // cb_led1
            // 
            this.cb_led1.AutoSize = true;
            this.cb_led1.Location = new System.Drawing.Point(7, 20);
            this.cb_led1.Name = "cb_led1";
            this.cb_led1.Size = new System.Drawing.Size(61, 17);
            this.cb_led1.TabIndex = 0;
            this.cb_led1.Text = "LED 1";
            this.cb_led1.UseVisualStyleBackColor = true;
            // 
            // bt_aplicar
            // 
            this.bt_aplicar.Location = new System.Drawing.Point(402, 192);
            this.bt_aplicar.Name = "bt_aplicar";
            this.bt_aplicar.Size = new System.Drawing.Size(75, 57);
            this.bt_aplicar.TabIndex = 1;
            this.bt_aplicar.Text = "Aplicar";
            this.bt_aplicar.UseVisualStyleBackColor = true;
            this.bt_aplicar.Click += new System.EventHandler(this.bt_aplicar_Click);
            // 
            // colorDialog
            // 
            this.colorDialog.FullOpen = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bt_changeCollorRGB2);
            this.groupBox1.Controls.Add(this.bt_changeCollorRGB1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(196, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 94);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cores";
            // 
            // bt_changeCollorRGB2
            // 
            this.bt_changeCollorRGB2.Location = new System.Drawing.Point(110, 20);
            this.bt_changeCollorRGB2.Name = "bt_changeCollorRGB2";
            this.bt_changeCollorRGB2.Size = new System.Drawing.Size(75, 64);
            this.bt_changeCollorRGB2.TabIndex = 1;
            this.bt_changeCollorRGB2.Text = "RGB 2";
            this.bt_changeCollorRGB2.UseVisualStyleBackColor = true;
            this.bt_changeCollorRGB2.Click += new System.EventHandler(this.bt_changeCollorRGB2_Click);
            // 
            // bt_changeCollorRGB1
            // 
            this.bt_changeCollorRGB1.Location = new System.Drawing.Point(17, 20);
            this.bt_changeCollorRGB1.Name = "bt_changeCollorRGB1";
            this.bt_changeCollorRGB1.Size = new System.Drawing.Size(75, 64);
            this.bt_changeCollorRGB1.TabIndex = 0;
            this.bt_changeCollorRGB1.Text = "RGB 1";
            this.bt_changeCollorRGB1.UseVisualStyleBackColor = true;
            this.bt_changeCollorRGB1.Click += new System.EventHandler(this.bt_changeCollorRGB1_Click);
            // 
            // tb_log
            // 
            this.tb_log.Location = new System.Drawing.Point(13, 114);
            this.tb_log.Multiline = true;
            this.tb_log.Name = "tb_log";
            this.tb_log.ReadOnly = true;
            this.tb_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_log.Size = new System.Drawing.Size(383, 135);
            this.tb_log.TabIndex = 3;
            // 
            // conectWatch
            // 
            this.conectWatch.Interval = 10000;
            this.conectWatch.Tick += new System.EventHandler(this.connectionWatcher);
            // 
            // bt_visualizar
            // 
            this.bt_visualizar.Location = new System.Drawing.Point(402, 163);
            this.bt_visualizar.Name = "bt_visualizar";
            this.bt_visualizar.Size = new System.Drawing.Size(75, 23);
            this.bt_visualizar.TabIndex = 4;
            this.bt_visualizar.Text = "Visualizar";
            this.bt_visualizar.UseVisualStyleBackColor = true;
            this.bt_visualizar.Click += new System.EventHandler(this.bt_visualizar_Click);
            // 
            // bt_cancelar
            // 
            this.bt_cancelar.Location = new System.Drawing.Point(402, 134);
            this.bt_cancelar.Name = "bt_cancelar";
            this.bt_cancelar.Size = new System.Drawing.Size(75, 23);
            this.bt_cancelar.TabIndex = 5;
            this.bt_cancelar.Text = "Cancelar";
            this.bt_cancelar.UseVisualStyleBackColor = true;
            this.bt_cancelar.Click += new System.EventHandler(this.bt_cancelar_Click);
            // 
            // bt_firmware
            // 
            this.bt_firmware.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bt_firmware.BackgroundImage")));
            this.bt_firmware.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_firmware.Location = new System.Drawing.Point(455, 13);
            this.bt_firmware.Name = "bt_firmware";
            this.bt_firmware.Size = new System.Drawing.Size(32, 32);
            this.bt_firmware.TabIndex = 6;
            this.bt_firmware.UseVisualStyleBackColor = true;
            this.bt_firmware.Click += new System.EventHandler(this.bt_firmware_Click);
            // 
            // bt_teste
            // 
            this.bt_teste.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bt_teste.BackgroundImage")));
            this.bt_teste.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_teste.Location = new System.Drawing.Point(402, 13);
            this.bt_teste.Name = "bt_teste";
            this.bt_teste.Size = new System.Drawing.Size(32, 32);
            this.bt_teste.TabIndex = 7;
            this.bt_teste.UseVisualStyleBackColor = true;
            this.bt_teste.Click += new System.EventHandler(this.bt_teste_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(402, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 26);
            this.label1.TabIndex = 8;
            this.label1.Text = "Developed by \r\nKauê Zatarin";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_sync
            // 
            this.bt_sync.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bt_sync.BackgroundImage")));
            this.bt_sync.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bt_sync.Location = new System.Drawing.Point(455, 57);
            this.bt_sync.Name = "bt_sync";
            this.bt_sync.Size = new System.Drawing.Size(32, 32);
            this.bt_sync.TabIndex = 9;
            this.bt_sync.UseVisualStyleBackColor = true;
            this.bt_sync.Click += new System.EventHandler(this.bt_sync_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(489, 261);
            this.Controls.Add(this.bt_sync);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_teste);
            this.Controls.Add(this.bt_firmware);
            this.Controls.Add(this.bt_cancelar);
            this.Controls.Add(this.bt_visualizar);
            this.Controls.Add(this.tb_log);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bt_aplicar);
            this.Controls.Add(this.gb_onOff);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Led Controller";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.gb_onOff.ResumeLayout(false);
            this.gb_onOff.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_onOff;
        private System.Windows.Forms.CheckBox cb_rgb2;
        private System.Windows.Forms.CheckBox cb_rgb1;
        private System.Windows.Forms.CheckBox cb_led4;
        private System.Windows.Forms.CheckBox cb_led3;
        private System.Windows.Forms.CheckBox cb_led2;
        private System.Windows.Forms.CheckBox cb_led1;
        private System.Windows.Forms.Button bt_aplicar;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bt_changeCollorRGB2;
        private System.Windows.Forms.Button bt_changeCollorRGB1;
        private System.Windows.Forms.TextBox tb_log;
        private System.Windows.Forms.Timer conectWatch;
        private System.Windows.Forms.Button bt_visualizar;
        private System.Windows.Forms.Button bt_cancelar;
        private System.Windows.Forms.Button bt_firmware;
        private System.Windows.Forms.Button bt_teste;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bt_sync;
    }
}

