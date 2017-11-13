namespace StockSwitch
{
    partial class ComputeStock
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.Lab_Progress = new System.Windows.Forms.Label();
            this.Btn_UpdateTrainSet = new System.Windows.Forms.Button();
            this.Btn_ComputeSHA = new System.Windows.Forms.Button();
            this.Btn_ComputeSSA = new System.Windows.Forms.Button();
            this.Btn_ComputeZXB = new System.Windows.Forms.Button();
            this.Btn_ComputeAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 333);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(656, 23);
            this.progressBar.TabIndex = 0;
            // 
            // Lab_Progress
            // 
            this.Lab_Progress.AutoSize = true;
            this.Lab_Progress.Location = new System.Drawing.Point(16, 362);
            this.Lab_Progress.Name = "Lab_Progress";
            this.Lab_Progress.Size = new System.Drawing.Size(0, 12);
            this.Lab_Progress.TabIndex = 1;
            // 
            // Btn_UpdateTrainSet
            // 
            this.Btn_UpdateTrainSet.Location = new System.Drawing.Point(12, 13);
            this.Btn_UpdateTrainSet.Name = "Btn_UpdateTrainSet";
            this.Btn_UpdateTrainSet.Size = new System.Drawing.Size(75, 23);
            this.Btn_UpdateTrainSet.TabIndex = 2;
            this.Btn_UpdateTrainSet.Text = "更新样本";
            this.Btn_UpdateTrainSet.UseVisualStyleBackColor = true;
            this.Btn_UpdateTrainSet.Click += new System.EventHandler(this.Btn_UpdateTrainSet_Click);
            // 
            // Btn_ComputeSHA
            // 
            this.Btn_ComputeSHA.Location = new System.Drawing.Point(12, 43);
            this.Btn_ComputeSHA.Name = "Btn_ComputeSHA";
            this.Btn_ComputeSHA.Size = new System.Drawing.Size(75, 23);
            this.Btn_ComputeSHA.TabIndex = 3;
            this.Btn_ComputeSHA.Text = "上证A股";
            this.Btn_ComputeSHA.UseVisualStyleBackColor = true;
            this.Btn_ComputeSHA.Click += new System.EventHandler(this.Btn_ComputeSHA_Click);
            // 
            // Btn_ComputeSSA
            // 
            this.Btn_ComputeSSA.Location = new System.Drawing.Point(12, 72);
            this.Btn_ComputeSSA.Name = "Btn_ComputeSSA";
            this.Btn_ComputeSSA.Size = new System.Drawing.Size(75, 23);
            this.Btn_ComputeSSA.TabIndex = 4;
            this.Btn_ComputeSSA.Text = "深圳A股";
            this.Btn_ComputeSSA.UseVisualStyleBackColor = true;
            this.Btn_ComputeSSA.Click += new System.EventHandler(this.Btn_ComputeSSA_Click);
            // 
            // Btn_ComputeZXB
            // 
            this.Btn_ComputeZXB.Location = new System.Drawing.Point(12, 102);
            this.Btn_ComputeZXB.Name = "Btn_ComputeZXB";
            this.Btn_ComputeZXB.Size = new System.Drawing.Size(75, 23);
            this.Btn_ComputeZXB.TabIndex = 5;
            this.Btn_ComputeZXB.Text = "中小板";
            this.Btn_ComputeZXB.UseVisualStyleBackColor = true;
            this.Btn_ComputeZXB.Click += new System.EventHandler(this.Btn_ComputeZXB_Click);
            // 
            // Btn_ComputeAll
            // 
            this.Btn_ComputeAll.Location = new System.Drawing.Point(12, 131);
            this.Btn_ComputeAll.Name = "Btn_ComputeAll";
            this.Btn_ComputeAll.Size = new System.Drawing.Size(75, 23);
            this.Btn_ComputeAll.TabIndex = 6;
            this.Btn_ComputeAll.Text = "All";
            this.Btn_ComputeAll.UseVisualStyleBackColor = true;
            this.Btn_ComputeAll.Click += new System.EventHandler(this.Btn_ComputeAll_Click);
            // 
            // ComputeStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 380);
            this.Controls.Add(this.Btn_ComputeAll);
            this.Controls.Add(this.Btn_ComputeZXB);
            this.Controls.Add(this.Btn_ComputeSSA);
            this.Controls.Add(this.Btn_ComputeSHA);
            this.Controls.Add(this.Btn_UpdateTrainSet);
            this.Controls.Add(this.Lab_Progress);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ComputeStock";
            this.Text = "ComputeStock";
            this.Load += new System.EventHandler(this.ComputeStock_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label Lab_Progress;
        private System.Windows.Forms.Button Btn_UpdateTrainSet;
        private System.Windows.Forms.Button Btn_ComputeSHA;
        private System.Windows.Forms.Button Btn_ComputeSSA;
        private System.Windows.Forms.Button Btn_ComputeZXB;
        private System.Windows.Forms.Button Btn_ComputeAll;
    }
}