using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockSwitch
{
    public partial class StockSwitch : Form
    {
        /// <summary>
        /// 窗体初始化
        /// </summary>
        private FormWindowState fwsPrevious;
        ///<summary>
        ///悬浮窗口
        ///</summary>
        private FloatingWindow FW;
        public StockSwitch()
        {
            InitializeComponent();
        }

        private void StockSwitch_Load(object sender, EventArgs e)
        {
            fwsPrevious = this.WindowState;
            FW = new FloatingWindow(this);   //启动FloatingWindow窗体
            //默认打开选股
            ComputeStock CS = new ComputeStock();
            CS.MdiParent = this;
            CS.Show();
            CS.Dock = DockStyle.Fill;
            computestock.Enabled = false;
        }
        public void RestoreWindow() //还原窗口
        {
            this.WindowState = fwsPrevious;
            this.ShowInTaskbar = true;    //设置在任务栏中显示
        }
        /// <summary>
        /// 窗体大小变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StockSwitch_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                FW.Show();
                this.ShowInTaskbar = false; //设置不在任务栏中显示
            }
            else if (this.WindowState != fwsPrevious)
            {
                fwsPrevious = this.WindowState;
            }
        }
        /// <summary>
        /// 点击股票列表菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CodeList_Click(object sender, EventArgs e)
        {
            if (!ExistsMdiChildInstance("UpdateCodeList"))
            {
                UpdateCodeList ucl = new UpdateCodeList();
                ucl.MdiParent = this;
                ucl.Show();
                ucl.Dock = DockStyle.Fill;
            }
            computestock.Enabled = true;
            result.Enabled = true;
            successrate.Enabled = true;
            CodeList.Enabled = false;
        }
        /// <summary>
        /// 点击选股菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void computestock_Click(object sender, EventArgs e)
        {
            if (!ExistsMdiChildInstance("ComputeStock"))
            {
                ComputeStock CS = new ComputeStock();
                CS.MdiParent = this;
                CS.Show();
                CS.Dock = DockStyle.Fill;
            }
            CodeList.Enabled = true;
            result.Enabled = true;
            successrate.Enabled = true;
            computestock.Enabled = false;
        }
        /// <summary>
        /// 判断窗口是否活动
        /// </summary>
        /// <param name="MdiChildClassName">窗口名</param>
        /// <returns>布尔值</returns>
        private bool ExistsMdiChildInstance(string MdiChildClassName)
        {
            foreach (Form Childfrom in this.MdiChildren)
            {
                if (Childfrom.Text == MdiChildClassName)
                {
                    if (Childfrom.WindowState == FormWindowState.Minimized)
                    {
                        Childfrom.WindowState = FormWindowState.Maximized;
                    }
                    Childfrom.Activate();
                    return true;
                }
            }
            return false;
        }

        private void result_Click(object sender, EventArgs e)
        {
            if (!ExistsMdiChildInstance("Reslut"))
            {
                Reslut rs = new Reslut();
                rs.MdiParent = this;
                rs.Show();
                rs.Dock = DockStyle.Fill;
            }
            CodeList.Enabled = true;
            successrate.Enabled = true;
            computestock.Enabled = true;
            result.Enabled = false;
        }

        private void successrate_Click(object sender, EventArgs e)
        {
            if (!ExistsMdiChildInstance("SuccessRate"))
            {
                SuccessRate sr = new SuccessRate();
                sr.MdiParent = this;
                sr.Show();
                sr.Dock = DockStyle.Fill;
            }
            computestock.Enabled = true;
            CodeList.Enabled = true;
            result.Enabled = true;
            successrate.Enabled = false;
        }

    }
}
