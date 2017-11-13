namespace StockSwitch
{
    partial class StockSwitch
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockSwitch));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.CodeList = new System.Windows.Forms.ToolStripButton();
            this.computestock = new System.Windows.Forms.ToolStripButton();
            this.result = new System.Windows.Forms.ToolStripButton();
            this.successrate = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CodeList,
            this.computestock,
            this.result,
            this.successrate});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(684, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // CodeList
            // 
            this.CodeList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CodeList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CodeList.Name = "CodeList";
            this.CodeList.Size = new System.Drawing.Size(60, 22);
            this.CodeList.Text = "股票列表";
            this.CodeList.Click += new System.EventHandler(this.CodeList_Click);
            // 
            // computestock
            // 
            this.computestock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.computestock.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.computestock.Name = "computestock";
            this.computestock.Size = new System.Drawing.Size(36, 22);
            this.computestock.Text = "选股";
            this.computestock.Click += new System.EventHandler(this.computestock_Click);
            // 
            // result
            // 
            this.result.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.result.ForeColor = System.Drawing.SystemColors.ControlText;
            this.result.Image = ((System.Drawing.Image)(resources.GetObject("result.Image")));
            this.result.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(60, 22);
            this.result.Text = "选股结果";
            this.result.Click += new System.EventHandler(this.result_Click);
            // 
            // successrate
            // 
            this.successrate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.successrate.Image = ((System.Drawing.Image)(resources.GetObject("successrate.Image")));
            this.successrate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.successrate.Name = "successrate";
            this.successrate.Size = new System.Drawing.Size(72, 22);
            this.successrate.Text = "预测成功率";
            this.successrate.Click += new System.EventHandler(this.successrate_Click);
            // 
            // StockSwitch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 412);
            this.Controls.Add(this.toolStrip1);
            this.IsMdiContainer = true;
            this.Name = "StockSwitch";
            this.Text = "选股软件";
            this.Load += new System.EventHandler(this.StockSwitch_Load);
            this.SizeChanged += new System.EventHandler(this.StockSwitch_SizeChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton CodeList;
        private System.Windows.Forms.ToolStripButton computestock;
        private System.Windows.Forms.ToolStripButton result;
        private System.Windows.Forms.ToolStripButton successrate;
    }
}

