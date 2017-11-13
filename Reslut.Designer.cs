namespace StockSwitch
{
    partial class Reslut
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
            this.Lab_Result = new System.Windows.Forms.Label();
            this.Btn_Fresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Lab_Title
            // 
            this.Lab_Title.AutoSize = true;
            this.Lab_Title.Font = new System.Drawing.Font("宋体", 14F);
            this.Lab_Title.Location = new System.Drawing.Point(298, 9);
            this.Lab_Title.Name = "Lab_Title";
            this.Lab_Title.Size = new System.Drawing.Size(85, 19);
            this.Lab_Title.TabIndex = 0;
            this.Lab_Title.Text = "选股结果";
            // 
            // Lab_Result
            // 
            this.Lab_Result.AutoSize = true;
            this.Lab_Result.Location = new System.Drawing.Point(13, 53);
            this.Lab_Result.Name = "Lab_Result";
            this.Lab_Result.Size = new System.Drawing.Size(0, 12);
            this.Lab_Result.TabIndex = 1;
            // 
            // Btn_Fresh
            // 
            this.Btn_Fresh.Location = new System.Drawing.Point(302, 345);
            this.Btn_Fresh.Name = "Btn_Fresh";
            this.Btn_Fresh.Size = new System.Drawing.Size(75, 23);
            this.Btn_Fresh.TabIndex = 2;
            this.Btn_Fresh.Text = "刷新";
            this.Btn_Fresh.UseVisualStyleBackColor = true;
            this.Btn_Fresh.Click += new System.EventHandler(this.Btn_Fresh_Click);
            // 
            // Reslut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 380);
            this.Controls.Add(this.Btn_Fresh);
            this.Controls.Add(this.Lab_Result);
            this.Controls.Add(this.Lab_Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Reslut";
            this.Text = "结果";
            this.Load += new System.EventHandler(this.Reslut_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lab_Title;
        private System.Windows.Forms.Label Lab_Result;
        private System.Windows.Forms.Button Btn_Fresh;
    }
}