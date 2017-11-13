using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;

namespace StockSwitch
{
    public partial class SuccessRate : Form
    {
        labrary.Basic BC = new labrary.Basic();
        labrary.GetData GD = new labrary.GetData();
        public SuccessRate()
        {
            InitializeComponent();
        }

        private void SuccessRate_Load(object sender, EventArgs e)
        {
            changeLabMsg = new ChangeLabMsg(ChangeLabMsgMothed);
            changeMaxProgerssBar = new ChangeMaxProgerssBar(ChangeMaxProgerssBarMothed);
            changeProgerssBarStep = new ChangeProgerssBarStep(ChangeProgressBarStepMothed);
            changeLabAllMsg = new ChangeLabAllMsg(ChangeLabAllMsgMothed);
            changeLabSHAMsg = new ChangeLabSHAMsg(ChangeLabSHAMsgMothed);
            changeLabSZAMsg = new ChangeLabSZAMsg(ChangeLabSZAMsgMothed);
            changeLabZXBMsg = new ChangeLabZXBMsg(ChangeLabZXBMothed);
            changeBtnEnable = new ChangeBtnEnable(ChangeBtnEnableMothed);
            Thread objThread = new Thread(new ThreadStart(delegate
            {
                SummaryResult();
            }));
            objThread.Start();
        }
        /// <summary>
        /// 总结选股成功率
        /// </summary>
        private void SummaryResult()
        {
            Btn_Update.Invoke(changeBtnEnable, false);
            string path = @"data/stock/Result";
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            ArrayList FileName = BC.GetAll(dir);
            Pb_UpdateStep.Invoke(changeMaxProgerssBar, FileName.Count * 5);
            Lab_UpdateStep.Invoke(changeLabMsg, "正在读取结果:0/" + FileName.Count * 5);
            int Num_AllChecked = 0;
            int Num_AllWin = 0;
            double Average_All = 0;
            int Num_SZAChecked = 0;
            int Num_SZAWin = 0;
            double Average_SZA = 0;
            int Num_SHAChecked = 0;
            int Num_SHAWin = 0;
            double Average_SHA = 0;
            int Num_ZXBChecked = 0;
            int Num_ZXBWin = 0;
            double Average_ZXB = 0;
            for (int i = 0; i < FileName.Count; i++)
            {
                string plate = FileName[i].ToString().Split('-')[0];
                string Path = path + "/" + FileName[i].ToString();
                switch (plate)
                {
                    case "SHA":
                        if (IfChecked(FileName[i].ToString()))
                        {
                            string[] result = File.ReadAllLines(Path);
                            for (int j = 0; j < result.Length; j++)
                            {
                                Num_SHAChecked++;
                                Num_AllChecked++;
                                try
                                {
                                    string[] data = result[j].Split(',');
                                    double stockResult = Convert.ToDouble(data[8]) - Convert.ToDouble(data[9]);
                                    if (stockResult > 0)
                                    {
                                        Num_AllWin++;
                                        Num_SHAWin++;
                                    }
                                    Average_All += stockResult;
                                    Average_SHA += stockResult;
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                            Pb_UpdateStep.Invoke(changeProgerssBarStep, i * 5);
                            Lab_UpdateStep.Invoke(changeLabMsg, "正在读取结果:" + i * 5 + "/" + FileName.Count);
                        }

                        break;
                    case "SZA":
                        if (IfChecked(FileName[i].ToString()))
                        {
                            string[] result = File.ReadAllLines(Path);
                            for (int j = 0; j < result.Length; j++)
                            {
                                Num_SZAChecked++;
                                Num_AllChecked++;
                                try
                                {
                                    string[] data = result[j].Split(',');
                                    double stockResult = Convert.ToDouble(data[8]) - Convert.ToDouble(data[9]);
                                    if (stockResult > 0)
                                    {
                                        Num_AllWin++;
                                        Num_SZAWin++;
                                    }
                                    Average_All += stockResult;
                                    Average_SZA += stockResult;
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                            Pb_UpdateStep.Invoke(changeProgerssBarStep, i * 5);
                            Lab_UpdateStep.Invoke(changeLabMsg, "正在读取结果:" + i * 5 + "/" + FileName.Count);
                        }
                        break;
                    case "ZXB":
                        if (IfChecked(FileName[i].ToString()))
                        {
                            string[] result = File.ReadAllLines(Path);
                            for (int j = 0; j < result.Length; j++)
                            {
                                Num_ZXBChecked++;
                                Num_AllChecked++;
                                try
                                {
                                    string[] data = result[j].Split(',');
                                    double stockResult = Convert.ToDouble(data[8]) - Convert.ToDouble(data[9]);
                                    if (stockResult > 0)
                                    {
                                        Num_AllWin++;
                                        Num_ZXBWin++;
                                    }
                                    Average_All += stockResult;
                                    Average_ZXB += stockResult;
                                }
                                catch (Exception)
                                {
                                    continue;
                                }
                            }
                            Pb_UpdateStep.Invoke(changeProgerssBarStep, i * 5);
                            Lab_UpdateStep.Invoke(changeLabMsg, "正在读取结果:" + i * 5 + "/" + FileName.Count);
                        }
                        break;
                }
            }
            Pb_UpdateStep.Invoke(changeProgerssBarStep, FileName.Count * 5);
            Lab_UpdateStep.Invoke(changeLabMsg, "已完成");
            if (Num_AllChecked == 0)
                Average_All = 0;
            else
                Average_All = Average_All / Num_AllChecked;
            if (Num_SHAChecked == 0)
                Average_SHA = 0;
            else
                Average_SHA = Average_SHA / Num_SHAChecked;
            if (Num_SZAChecked == 0)
                Average_SZA = 0;
            else
                Average_SZA = Average_SZA / Num_SZAChecked;
            if (Num_ZXBChecked == 0)
                Average_ZXB = 0;
            else
                Average_ZXB = Average_ZXB / Num_ZXBChecked;
            Lab_AllSuccessRete.Invoke(changeLabAllMsg, "总体未预测数量：" + (FileName.Count * 5 - Num_AllChecked) + ";已预测数量：" + Num_AllChecked + ";跑赢大盘次数：" + Num_AllWin + ";预测相对于大盘赢亏：" + Average_All);
            Lab_SHAGuSuccessRte.Invoke(changeLabSHAMsg, "上证A股已预测数量：" + Num_SHAChecked + ";跑赢大盘次数：" + Num_SHAWin + ";预测相对于大盘赢亏：" + Average_SHA);
            Lab_SZAGuSucessRate.Invoke(changeLabSZAMsg, "深圳A股已预测数量：" + Num_SZAChecked + ";跑赢大盘次数：" + Num_SZAWin + ";预测相对于大盘赢亏：" + Average_SZA);
            Lab_ZXBSuccessRate.Invoke(changeLabZXBMsg, "中小板已预测数量：" + Num_ZXBChecked + ";跑赢大盘次数：" + Num_ZXBWin + ";预测相对于大盘赢亏：" + Average_ZXB);
            Btn_Update.Invoke(changeBtnEnable, true);
        }
        /// <summary>
        /// 是否检验过(检验过文件后面加C）
        /// </summary>
        /// <param name="FileName">文件名</param>
        private bool IfChecked(string FileName)
        {
            string str = FileName.Split('-')[FileName.Split('-').Length - 1];
            if (str == "C.txt")
                return true;
            else
                return false;
        }
        #region 委托事件
        public delegate void ChangeLabMsg(string msg);
        public delegate void ChangeMaxProgerssBar(int i);
        public delegate void ChangeProgerssBarStep(int i);
        public delegate void ChangeBtnEnable(bool boolen);
        public delegate void ChangeLabAllMsg(string msg);
        public delegate void ChangeLabSHAMsg(string msg);
        public delegate void ChangeLabSZAMsg(string msg);
        public delegate void ChangeLabZXBMsg(string msg);
        public void ChangeLabMsgMothed(string msg)
        {
            Lab_UpdateStep.Text = msg;
        }
        public void ChangeMaxProgerssBarMothed(int i)
        {
            Pb_UpdateStep.Maximum = i;
        }
        public void ChangeProgressBarStepMothed(int i)
        {
            Pb_UpdateStep.Value = i;
        }
        public void ChangeLabAllMsgMothed(string msg)
        {
            Lab_AllSuccessRete.Text = msg;
        }
        public void ChangeLabSHAMsgMothed(string msg)
        {
            Lab_SHAGuSuccessRte.Text = msg;
        }
        public void ChangeLabSZAMsgMothed(string msg)
        {
            Lab_SZAGuSucessRate.Text = msg;
        }
        public void ChangeLabZXBMothed(string msg)
        {
            Lab_ZXBSuccessRate.Text = msg;
        }
        public void ChangeBtnEnableMothed(bool boolen)
        {
            Btn_Update.Enabled = boolen;
        }
        public ChangeLabMsg changeLabMsg;
        public ChangeMaxProgerssBar changeMaxProgerssBar;
        public ChangeProgerssBarStep changeProgerssBarStep;
        public ChangeLabAllMsg changeLabAllMsg;
        public ChangeLabSHAMsg changeLabSHAMsg;
        public ChangeLabSZAMsg changeLabSZAMsg;
        public ChangeLabZXBMsg changeLabZXBMsg;
        public ChangeBtnEnable changeBtnEnable;
        #endregion
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Update_Click(object sender, EventArgs e)
        {
            Btn_Update.Enabled = false;
            Thread objThread = new Thread(new ThreadStart(delegate
            {
                CheckReslut();
            }));
            objThread.Start();
        }
        /// <summary>
        /// 检验预期
        /// </summary>
        private void CheckReslut()
        {
            string path = @"data/stock/Result";
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            ArrayList FileName = BC.GetAll(dir);
            ArrayList fileName = new ArrayList();
            int Num_UpdateSuccess = 0;
            int Num_UpdateFail = 0;
            for (int i = 0; i < FileName.Count; i++)
            {
                if (!IfChecked(FileName[i].ToString()))
                {
                    fileName.Add(FileName[i]);
                }
            }
            Pb_UpdateStep.Invoke(changeMaxProgerssBar, fileName.Count * 5);
            Pb_UpdateStep.Invoke(changeProgerssBarStep, 0);
            Lab_UpdateStep.Invoke(changeLabMsg, "正在收集数据:0/" + fileName.Count * 5);
            for (int i = 0; i < fileName.Count; i++)
            {
                string plate = fileName[i].ToString().Split('-')[0];
                DateTime startDate = Convert.ToDateTime(fileName[i].ToString().Split('-')[1] + "/" + fileName[i].ToString().Split('-')[2] + "/" + (fileName[i].ToString().Split('-')[3].Split('.')[0]));
                if ((int)startDate.DayOfWeek == 0)
                {
                    startDate = startDate.AddDays(-2);
                }
                else if ((int)startDate.DayOfWeek == 6)
                {
                    startDate = startDate.AddDays(-1);
                }
                if (DateTime.Today.AddDays(-30) < startDate)
                {
                    Num_UpdateFail += 5;
                    continue;
                }
                else
                {
                    DateTime endDate = startDate.AddDays(15);
                    string Path = path + "/" + fileName[i].ToString();
                    switch (plate)
                    {
                        case "SHA":
                            labrary.Stock.StockInfo[] SHA = GD.GetDate("000001", startDate, endDate, "SHAGu");
                            int SHA_Length = 0;
                            if (SHA != null)
                                SHA_Length = SHA.Length;
                            if (SHA_Length <= 5)
                            {
                                Num_UpdateFail += 5;
                                Lab_UpdateStep.Invoke(changeLabMsg, "正在收集数据:" + (i + 1) * 5 + "/" + fileName.Count * 5);
                                continue;
                            }
                            else
                            {
                                string[] reslut = File.ReadAllLines(Path);
                                string Reslut = "";
                                bool sussess = true;
                                for (int j = 0; j < reslut.Length; j++)
                                {
                                    string code = reslut[j].Split(',')[0];
                                    labrary.Stock.StockInfo[] data = GD.GetDate(code, startDate, endDate, "SHAGu");
                                    int data_Length =data==null?0:data.Length;
                                    if (data == null || data_Length <= 5)
                                    {
                                        Num_UpdateFail += 5;
                                        Lab_UpdateStep.Invoke(changeLabMsg, "正在收集数据:" + (i + 1) * 5 + "/" + fileName.Count * 5);
                                        sussess = false;
                                        continue;
                                    }
                                    else
                                    {
                                        Reslut += reslut[j] + "," + ((data[data_Length - 6].close - data[data_Length - 1].close) / data[data_Length - 6].close) + "," + ((SHA[SHA_Length - 6].close - SHA[SHA_Length - 1].close) / SHA[SHA_Length - 6].close) + "\r\n";
                                    }
                                }
                                if (sussess)
                                {
                                    Pb_UpdateStep.Invoke(changeProgerssBarStep, (i + 1) * 5);
                                    Lab_UpdateStep.Invoke(changeLabMsg, "正在收集数据:" + (i + 1) * 5 + "/" + fileName.Count * 5);
                                    if (i + 1 == fileName.Count)
                                        Lab_UpdateStep.Invoke(changeLabMsg, "已完成:" + (i + 1) * 5 + "/" + fileName.Count * 5);
                                    File.Delete(Path);
                                    File.WriteAllText(Path.Split('.')[0] + "-C.txt", Reslut);
                                    Num_UpdateSuccess += 5;
                                }
                            }
                            break;
                        case "SZA":
                            labrary.Stock.StockInfo[] SZA = GD.GetDate("399001", startDate, endDate, "SZAGu");
                            int SZA_Length = 0;
                            if (SZA != null)
                                SZA_Length = SZA.Length;
                            if (SZA_Length <= 5)
                            {
                                Num_UpdateFail += 5;
                                Lab_UpdateStep.Invoke(changeLabMsg, "正在收集数据:" + (i + 1) * 5 + "/" + fileName.Count * 5);
                                continue;
                            }
                            else
                            {
                                string[] reslut = File.ReadAllLines(Path);
                                string Reslut = "";
                                bool sussess = true;
                                for (int j = 0; j < reslut.Length; j++)
                                {
                                    string code = reslut[j].Split(',')[0];
                                    labrary.Stock.StockInfo[] data = GD.GetDate(code, startDate, endDate, "SZAGu");
                                    int data_Length = data == null ? 0 : data.Length;
                                    if (data == null || data_Length <= 5)
                                    {
                                        Num_UpdateFail += 5;
                                        Lab_UpdateStep.Invoke(changeLabMsg, "正在收集数据:" + (i + 1) * 5 + "/" + fileName.Count * 5);
                                        sussess = false;
                                        continue;
                                    }
                                    else
                                    {
                                        Reslut += reslut[j] + "," + ((data[data_Length - 6].close - data[data_Length - 1].close) / data[data_Length - 6].close) + "," + ((SZA[SZA_Length - 6].close - SZA[SZA_Length - 1].close) / SZA[SZA_Length - 6].close) + "\r\n";
                                    }
                                }
                                if (sussess)
                                {
                                    Pb_UpdateStep.Invoke(changeProgerssBarStep, (i + 1) * 5);
                                    Lab_UpdateStep.Invoke(changeLabMsg, "正在收集数据:" + (i + 1) * 5 + "/" + fileName.Count * 5);
                                    if (i + 1 == fileName.Count)
                                        Lab_UpdateStep.Invoke(changeLabMsg, "已完成:" + (i + 1) * 5 + "/" + fileName.Count * 5);
                                    File.Delete(Path);
                                    File.WriteAllText(Path.Split('.')[0] + "-C.txt", Reslut);
                                    Num_UpdateSuccess += 5;
                                }
                            }
                            break;
                        case "ZXB":
                            labrary.Stock.StockInfo[] ZXB = GD.GetDate("399005", startDate, endDate, "ZXB");
                            int ZXB_Length = 0;
                            if (ZXB != null)
                                ZXB_Length = ZXB.Length;
                            if (ZXB_Length <= 5)
                            {
                                Num_UpdateFail += 5;
                                Lab_UpdateStep.Invoke(changeLabMsg, "正在收集数据:" + (i + 1) * 5 + "/" + fileName.Count * 5);
                                continue;
                            }
                            else
                            {
                                string[] reslut = File.ReadAllLines(Path);
                                string Reslut = "";
                                bool sussess = true;
                                for (int j = 0; j < reslut.Length; j++)
                                {
                                    string code = reslut[j].Split(',')[0];
                                    labrary.Stock.StockInfo[] data = GD.GetDate(code, startDate, endDate, "ZXB");
                                    int data_Length = data == null ? 0 : data.Length;
                                    if (data == null || data_Length <= 5)
                                    {
                                        Num_UpdateFail += 5;
                                        sussess = false;
                                        Lab_UpdateStep.Invoke(changeLabMsg, "正在收集数据:" + (i + 1) * 5 + "/" + fileName.Count * 5);
                                        continue;
                                    }
                                    else
                                    {
                                        Reslut += reslut[j] + "," + ((data[data_Length - 6].close - data[data_Length - 1].close) / data[data_Length - 6].close) + "," + ((ZXB[ZXB_Length - 6].close - ZXB[ZXB_Length - 1].close) / ZXB[ZXB_Length - 6].close) + "\r\n";
                                    }
                                }
                                if (sussess)
                                {
                                    Pb_UpdateStep.Invoke(changeProgerssBarStep, (i + 1) * 5);
                                    Lab_UpdateStep.Invoke(changeLabMsg, "正在收集数据:" + (i + 1) * 5 + "/" + fileName.Count * 5);
                                    if (i + 1 == fileName.Count)
                                        Lab_UpdateStep.Invoke(changeLabMsg, "已完成:" + (i + 1) * 5 + "/" + fileName.Count * 5);
                                    File.Delete(Path);
                                    File.WriteAllText(Path.Split('.')[0] + "-C.txt", Reslut);
                                    Num_UpdateSuccess += 5;
                                }
                            }
                            break;
                    }
                }
            }
            MessageBox.Show("成功获取了：" + Num_UpdateSuccess + "次预测的实际成果由于网络原因或时间未到失败了：" + Num_UpdateFail);
            Thread objThread = new Thread(new ThreadStart(delegate
            {
                SummaryResult();
            }));
            objThread.Start();
        }
    }
}
