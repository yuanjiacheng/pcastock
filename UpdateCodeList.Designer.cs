namespace StockSwitch
{
    partial class UpdateCodeList
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
            this.Lab_url = new System.Windows.Forms.Label();
            this.Txt_Url = new System.Windows.Forms.TextBox();
            this.Btn_UpdateCodeList = new System.Windows.Forms.Button();
            this.Lab_State = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Lab_url
            // 
            this.Lab_url.AutoSize = true;
            this.Lab_url.Location = new System.Drawing.Point(13, 13);
            this.Lab_url.Name = "Lab_url";
            this.Lab_url.Size = new System.Drawing.Size(71, 12);
            this.Lab_url.TabIndex = 0;
            this.Lab_url.Text = "数据抓取url";
            // 
            // Txt_Url
            // 
            this.Txt_Url.Location = new System.Drawing.Point(82, 10);
            this.Txt_Url.Name = "Txt_Url";
            this.Txt_Url.Size = new System.Drawing.Size(586, 21);
            this.Txt_Url.TabIndex = 1;
            // 
            // Btn_UpdateCodeList
            // 
            this.Btn_UpdateCodeList.Location = new System.Drawing.Point(582, 37);
            this.Btn_UpdateCodeList.Name = "Btn_UpdateCodeList";
            this.Btn_UpdateCodeList.Size = new System.Drawing.Size(86, 23);
            this.Btn_UpdateCodeList.TabIndex = 2;
            this.Btn_UpdateCodeList.Text = "更新股票列表";
            this.Btn_UpdateCodeList.UseVisualStyleBackColor = true;
            this.Btn_UpdateCodeList.Click += new System.EventHandler(this.Btn_UpdateCodeList_Click);
            // 
            // Lab_State
            // 
            this.Lab_State.AutoSize = true;
            this.Lab_State.Location = new System.Drawing.Point(15, 112);
            this.Lab_State.Name = "Lab_State";
            this.Lab_State.Size = new System.Drawing.Size(0, 12);
            this.Lab_State.TabIndex = 3;
            // 
            // UpdateCodeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 430);
            this.Controls.Add(this.Lab_State);
            this.Controls.Add(this.Btn_UpdateCodeList);
            this.Controls.Add(this.Txt_Url);
            this.Controls.Add(this.Lab_url);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UpdateCodeList";
            this.Text = "UpdateCodeList";
            this.Load += new System.EventHandler(this.UpdateCodeList_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lab_url;
        private System.Windows.Forms.TextBox Txt_Url;
        private System.Windows.Forms.Button Btn_UpdateCodeList;
        private System.Windows.Forms.Label Lab_State;

    }
}