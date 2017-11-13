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
    public partial class FloatingWindow : Form
    {
        public FloatingWindow(StockSwitch SS)
        {
            InitializeComponent();
            pParent = SS;
        }
        private StockSwitch pParent;
        private Point ptMouseCurrrnetPos, ptMouseNewPos, ptFormPos, ptFormNewPos;
        private bool blnMouseDown = false;
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FloatingWindow_Load(object sender, EventArgs e)
        {
            this.Show();
            this.Top = 100;
            this.Left = Screen.PrimaryScreen.Bounds.Width - 100;
            this.ShowInTaskbar = false;
            this.Height = 66;
            this.Width = 128;
        }
        /// <summary>
        /// 双击浮窗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FloatingWindow_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SwitchToMain();
        }
        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FloatingWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (blnMouseDown)   //如果鼠标按下
            {
                ptMouseNewPos = Control.MousePosition;
                ptFormNewPos.X = ptMouseNewPos.X - ptMouseCurrrnetPos.X + ptFormPos.X;  //新的窗口x坐标等于原来的窗口x坐标加上鼠标移动x的该变量
                ptFormNewPos.Y = ptMouseNewPos.Y - ptMouseCurrrnetPos.Y + ptFormPos.Y;  //同上
                Location = ptFormNewPos;        //应用新的坐标位置
                ptFormPos = ptFormNewPos;
                ptMouseCurrrnetPos = ptMouseNewPos;     //重新给窗口位置赋值
            }
        }
        /// <summary>
        /// 鼠标松开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FloatingWindow_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                blnMouseDown = false;
            }
        }
        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FloatingWindow_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                blnMouseDown = true;
                ptMouseCurrrnetPos = Control.MousePosition;
                ptFormPos = Location;
            }
        }
        private void SwitchToMain()
        {
            pParent.RestoreWindow();
            this.Hide();
        }
    }
}
