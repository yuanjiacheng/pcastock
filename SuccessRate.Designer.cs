namespace StockSwitch
{
    partial class SuccessRate
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
            this.Lab_Title = new System.Windows.Forms.Label();
            this.Lab_AllSuccessRete = new System.Windows.Forms.Label();
            this.Lab_SHAGuSuccessRte = new System.Windows.Forms.Label();
            this.Lab_SZAGuSucessRate = new System.Windows.Forms.Label();
            this.Lab_ZXBSuccessRate = new System.Windows.Forms.Label();
            this.Btn_Update = new System.Windows.Forms.Button();
            this.Pb_UpdateStep = new System.Windows.Forms.ProgressBar();
            this.Lab_UpdateStep = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Lab_Title
            // 
            this.Lab_Title.AutoSize = true;
            this.Lab_Title.Font = new System.Drawing.Font("宋体", 14F);
            this.Lab_Title.Location = new System.Drawing.Point(298, 9);
            this.Lab_Title.Name = "Lab_Title";
            this.Lab_Title.Size = new System.Drawing.Size(104, 19);
            this.Lab_Title.TabIndex = 0;
            this.Lab_Title.Text = "选股成功率";
            // 
            // Lab_AllSuccessRete
            // 
            this.Lab_AllSuccessRete.AutoSize = true;
            this.Lab_AllSuccessRete.Location = new System.Drawing.Point(13, 48);
            this.Lab_AllSuccessRete.Name = "Lab_AllSuccessRete";
            this.Lab_AllSuccessRete.Size = new System.Drawing.Size(65, 12);
            this.Lab_AllSuccessRete.TabIndex = 1;
            this.Lab_AllSuccessRete.Text = "总成功率：";
            // 
            // Lab_SHAGuSuccessRte
            // 
            this.Lab_SHAGuSuccessRte.AutoSize = true;
            this.Lab_SHAGuSuccessRte.Location = new System.Drawing.Point(13, 67);
            this.Lab_SHAGuSuccessRte.Name = "Lab_SHAGuSuccessRte";
            this.Lab_SHAGuSuccessRte.Size = new System.Drawing.Size(59, 12);
            this.Lab_SHAGuSuccessRte.TabIndex = 2;
            this.Lab_SHAGuSuccessRte.Text = "上海A股：";
            // 
            // Lab_SZAGuSucessRate
            // 
            this.Lab_SZAGuSucessRate.AutoSize = true;
            this.Lab_SZAGuSucessRate.Location = new System.Drawing.Point(14, 86);
            this.Lab_SZAGuSucessRate.Name = "Lab_SZAGuSucessRate";
            this.Lab_SZAGuSucessRate.Size = new System.Drawing.Size(59, 12);
            this.Lab_SZAGuSucessRate.TabIndex = 3;
            this.Lab_SZAGuSucessRate.Text = "深圳A股：";
            // 
            // Lab_ZXBSuccessRate
            // 
            this.Lab_ZXBSuccessRate.AutoSize = true;
            this.Lab_ZXBSuccessRate.Location = new System.Drawing.Point(14, 104);
            this.Lab_ZXBSuccessRate.Name = "Lab_ZXBSuccessRate";
            this.Lab_ZXBSuccessRate.Size = new System.Drawing.Size(53, 12);
            this.Lab_ZXBSuccessRate.TabIndex = 4;
            this.Lab_ZXBSuccessRate.Text = "中小板：";
            // 
            // Btn_Update
            // 
            this.Btn_Update.Location = new System.Drawing.Point(15, 120);
            this.Btn_Update.Name = "Btn_Update";
            this.Btn_Update.Size = new System.Drawing.Size(75, 23);
            this.Btn_Update.TabIndex = 5;
            this.Btn_Update.Text = "更新验证";
            this.Btn_Update.UseVisualStyleBackColor = true;
            this.Btn_Update.Click += new System.EventHandler(this.Btn_Update_Click);
            // 
            // Pb_UpdateStep
            // 
            this.Pb_UpdateStep.Location = new System.Drawing.Point(7, 320);
            this.Pb_UpdateStep.Name = "Pb_UpdateStep";
            this.Pb_UpdateStep.Size = new System.Drawing.Size(652, 23);
            this.Pb_UpdateStep.TabIndex = 6;
            // 
            // Lab_UpdateStep
            // 
            this.Lab_UpdateStep.AutoSize = true;
            this.Lab_UpdateStep.Location = new System.Drawing.Point(0, 352);
            this.Lab_UpdateStep.Name = "Lab_UpdateStep";
            this.Lab_UpdateStep.Size = new System.Drawing.Size(0, 12);
            this.Lab_UpdateStep.TabIndex = 7;
            // 
            // SuccessRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 380);
            this.Controls.Add(this.Lab_UpdateStep);
            this.Controls.Add(this.Pb_UpdateStep);
            this.Controls.Add(this.Btn_Update);
            this.Controls.Add(this.Lab_ZXBSuccessRate);
            this.Controls.Add(this.Lab_SZAGuSucessRate);
            this.Controls.Add(this.Lab_SHAGuSuccessRte);
            this.Controls.Add(this.Lab_AllSuccessRete);
            this.Controls.Add(this.Lab_Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SuccessRate";
            this.Text = "SuccessRate";
            this.Load += new System.EventHandler(this.SuccessRate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lab_Title;
        private System.Windows.Forms.Label Lab_AllSuccessRete;
        private System.Windows.Forms.Label Lab_SHAGuSuccessRte;
        private System.Windows.Forms.Label Lab_SZAGuSucessRate;
        private System.Windows.Forms.Label Lab_ZXBSuccessRate;
        private System.Windows.Forms.Button Btn_Update;
        private System.Windows.Forms.ProgressBar Pb_UpdateStep;
        private System.Windows.Forms.Label Lab_UpdateStep;
    }
}