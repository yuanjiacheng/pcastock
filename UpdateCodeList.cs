using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace StockSwitch
{
    public partial class UpdateCodeList : Form
    {
        /// <summary>
        /// 委托事件,为了可以访问控件Lab_State
        /// </summary>
        /// <param name="strUrl">获取地址</param>
        public delegate void ChangeState(string msg);
        /// <summary>
        /// 委托事件，为了可以访问Btn_UpdateCodeList按钮
        /// </summary>
        public delegate void ChangeButton();
        public ChangeState changeState;
        public ChangeButton changeButton;
        /// <summary>
        /// 改变Lab_State的方法
        /// </summary>
        /// <param name="msg"></param>
        public void ChangeStateMethod(string msg)
        {
            Lab_State.Text += msg + "\r\n";
        }
        /// <summary>
        /// 改变按钮可用性的方法
        /// </summary>
        public void ChangeButtonMethod()
        {
            Btn_UpdateCodeList.Enabled=true;
        }
        /// <summary>
        /// 更新股票列表方法
        /// </summary>
        public void updateCodeList()
        {
            string strUrl = Txt_Url.Text;
            Lab_State.BeginInvoke(changeState, "尝试获取数据中");
            string ResponseString = GetGeneralContent(strUrl);
            Lab_State.BeginInvoke(changeState, "数据处理中...");
            string[] shAgu = Reg60(ResponseString);
            string[] Zxb = Reg002(ResponseString);
            string[] szAgu = Reg000(ResponseString);
            labrary.DataHelper Dh = new labrary.DataHelper();
            Dh.WriteTxt("data/中小板.txt", Zxb);
            Dh.WriteTxt("data/上证A股.txt", shAgu);
            Dh.WriteTxt("data/深圳A股.txt", szAgu);
            Lab_State.BeginInvoke(changeState, "更新完成");
            Btn_UpdateCodeList.BeginInvoke(changeButton);
        }
        public UpdateCodeList()
        {
            InitializeComponent();
        }
        private void UpdateCodeList_Load(object sender, EventArgs e)
        {
            Txt_Url.Text = "http://bbs.10jqka.com.cn/codelist.html";
            //实例化委托事件
            changeState = new ChangeState(ChangeStateMethod);
            changeButton = new ChangeButton(ChangeButtonMethod);
        }
        /// <summary>
        /// 获取网页内容
        /// </summary>
        /// <param name="strUrl">网页地址</param>
        /// <returns>网页内容的字符串</returns>
        private string GetGeneralContent(string strUrl)
        {
            string strMsg = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(strUrl);
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312"));
                strMsg = reader.ReadToEnd();
                reader.Close();
                reader.Dispose();
                response.Close();
            }
            catch (Exception e)
            {
                Lab_State.Text += e + "\r\n";
            }
            return strMsg;
        }
        private void Btn_UpdateCodeList_Click(object sender, EventArgs e)
        {
            Btn_UpdateCodeList.Enabled = false;
            Thread objThread = new Thread(new ThreadStart(delegate
            {
                updateCodeList();
            }));
            objThread.Start();
        }
        /// <summary>
        /// 获取深圳A股股票代码
        /// </summary>
        /// <param name="Str">网页数据流</param>
        /// <returns>A股股票代码数组</returns>
        private string[] Reg60(string Str)
        {
            Regex reg = new Regex(@"60\d{4}");
            List<String> strArr = new List<String>();
            if (reg.IsMatch(Str))
            {
                for (int i = 0; i < reg.Matches(Str).Count; i++)
                {
                    strArr.Add(reg.Matches(Str)[i].Value);
                }
            }
            labrary.Basic Bs = new labrary.Basic();
            return Bs.GetString(strArr.ToArray());
        }
        /// <summary>
        /// 获取中小板股票代码
        /// </summary>
        /// <param name="Str">网页数据流</param>
        /// <returns>中小板股票代码数组</returns>
        private string[] Reg002(string Str)
        {
            Regex reg = new Regex(@"002\d{3}");
            List<String> strArr = new List<String>();
            if (reg.IsMatch(Str))
            {
                for (int i = 0; i < reg.Matches(Str).Count; i++)
                {
                    strArr.Add(reg.Matches(Str)[i].Value);
                }
            }
            labrary.Basic Bs = new labrary.Basic();
            return Bs.GetString(strArr.ToArray());
        }
        /// <summary>
        /// 获取深圳A股股票代码
        /// </summary>
        /// <param name="Str">网页数据流</param>
        /// <returns>深圳A股股票代码数组</returns>
        private string[] Reg000(string Str)
        {
            Regex reg = new Regex(@"000\d{3}");
            List<String> strArr = new List<String>();
            if (reg.IsMatch(Str))
            {
                for (int i = 0; i < reg.Matches(Str).Count; i++)
                {
                    strArr.Add(reg.Matches(Str)[i].Value);
                }
            }
            labrary.Basic Bs = new labrary.Basic();
            return Bs.GetString(strArr.ToArray());
        }
    }
}
