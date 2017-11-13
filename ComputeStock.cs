using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Collections;
using MatrixMultiply;
using EulerDistance;
using MathWorks.MATLAB.NET.Arrays;

namespace StockSwitch
{
    public partial class ComputeStock : Form
    {
        labrary.GetData GD = new labrary.GetData();
        labrary.DataHelper DH = new labrary.DataHelper();
        labrary.Basic BC = new labrary.Basic();
        labrary.ComputeStockHelper CSH = new labrary.ComputeStockHelper();
        public ComputeStock()
        {
            InitializeComponent();
        }
        #region 事件
        /// <summary>
        /// 载入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComputeStock_Load(object sender, EventArgs e)
        {
            changeProgerssBar = new changeProgress(changeProgressBarMothed);
            changeProgressLab = new changeProgress(changeProgressLabMethed);
            changeProgressBarMaximun = new changeProgress(changeProgressBarMaxumunMothed);
            ChangeState = new changeState(ChangeStateMothed);
            if (NewTrainSet())
            {
                Btn_UpdateTrainSet.Text = "重更样本";
            }
            else
            {
                Btn_ComputeSHA.Enabled = false;
                Btn_ComputeSSA.Enabled = false;
                Btn_ComputeZXB.Enabled = false;
                Btn_ComputeAll.Enabled = false;
            }
            if (DH.Computed("SHA"))
            {
                Btn_ComputeSHA.Enabled = false;
                Btn_ComputeSHA.Text = "已计算";
            }
            if (DH.Computed("SZA"))
            {
                Btn_ComputeSSA.Enabled = false;
                Btn_ComputeSSA.Text = "已计算";
            }
            if (DH.Computed("ZXB"))
            {
                Btn_ComputeZXB.Enabled = false;
                Btn_ComputeZXB.Text = "已计算";
            }
            if (DH.Computed("SHA") && DH.Computed("SZA") && DH.Computed("ZXB"))
                Btn_ComputeAll.Enabled = false;
        }
        /// <summary>
        /// 判断样本数据是否需要更新
        /// </summary>
        /// <returns>布尔值</returns>
        private bool NewTrainSet()
        {
            string Today = "";
            DateTime today = DateTime.Today.AddDays(-1);//获取前一天的日期
            if ((int)today.DayOfWeek == 0)
            {
                Today = today.AddDays(-2).ToShortDateString();
            }
            else if ((int)today.DayOfWeek == 6)
            {
                Today = today.AddDays(-1).ToShortDateString();
            }
            else
            {
                Today = today.ToShortDateString();
            }
            string UpdateDate = File.ReadAllText("data/stock/UpdateDate.txt");
            StartDate = DateTime.Today.AddDays(-90);
            EndDate = Convert.ToDateTime(Today);
            if (UpdateDate == Today || UpdateDate == DateTime.Today.ToShortDateString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新样本按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_UpdateTrainSet_Click(object sender, EventArgs e)
        {
            Lab_Progress.Text = "正在清除旧数据";
            Btn_ComputeSHA.Enabled = false;
            Btn_ComputeSSA.Enabled = false;
            Btn_ComputeZXB.Enabled = false;
            Btn_ComputeAll.Enabled = false;
            Directory.Delete("data/stock/SHAGu", true);
            Directory.CreateDirectory("data/stock/SHAGu");
            Directory.Delete("data/stock/SZAGu", true);
            Directory.CreateDirectory("data/stock/SZAGu");
            Directory.Delete("data/stock/ZXB", true);
            Directory.CreateDirectory("data/stock/ZXB");
            File.Delete("data/stock/UpdateDate.txt");
            File.CreateText("data/stock/UpdateDate.txt");
            Btn_UpdateTrainSet.Enabled = false;
            UpdateTrainSet();
        }
        /// <summary>
        /// 点击预测上证A股
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ComputeSHA_Click(object sender, EventArgs e)
        {
            Btn_ComputeZXB.Enabled = false;
            Btn_ComputeSSA.Enabled = false;
            Btn_ComputeSHA.Enabled = false;
            Btn_ComputeAll.Enabled = false;
            Btn_UpdateTrainSet.Enabled = false;
            Thread objThread = new Thread(new ThreadStart(delegate
            {
                GetTrainSet("SHAGu", 30, 15, 5);
            }));
            objThread.Start();
        }
        /// <summary>
        /// 点击预测深圳A股
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ComputeSSA_Click(object sender, EventArgs e)
        {
            Btn_ComputeZXB.Enabled = false;
            Btn_ComputeSSA.Enabled = false;
            Btn_ComputeSHA.Enabled = false;
            Btn_ComputeAll.Enabled = false;
            Btn_UpdateTrainSet.Enabled = false;
            Thread objThread = new Thread(new ThreadStart(delegate
            {
                GetTrainSet("SZAGu", 30, 15, 5);
            }));
            objThread.Start();
        }
        /// <summary>
        /// 点击预测中小板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ComputeZXB_Click(object sender, EventArgs e)
        {
            Btn_ComputeZXB.Enabled = false;
            Btn_ComputeSSA.Enabled = false;
            Btn_ComputeSHA.Enabled = false;
            Btn_ComputeAll.Enabled = false;
            Btn_UpdateTrainSet.Enabled = false;
            Thread objThread = new Thread(new ThreadStart(delegate
            {
                GetTrainSet("ZXB", 30, 15, 5);
            }));
            objThread.Start();
        }
        /// <summary>
        /// 计算全部按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_ComputeAll_Click(object sender, EventArgs e)
        {
            Btn_ComputeZXB.Enabled = false;
            Btn_ComputeSSA.Enabled = false;
            Btn_ComputeSHA.Enabled = false;
            Btn_ComputeAll.Enabled = false;
            Btn_UpdateTrainSet.Enabled = false;
            Thread objThread = new Thread(new ThreadStart(delegate
            {
                GetTrainSet("All", 30, 15, 5);
            }));
            objThread.Start();
        }
        /// <summary>
        /// 点击预测所有
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #endregion
        #region 创建委托
        public delegate void changeProgress(string msg);  //创建修改进度信息的委托
        public changeProgress changeProgerssBar;
        public changeProgress changeProgressBarMaximun;
        public changeProgress changeProgressLab;
        /// <summary>
        /// 修改进度条的方法，使用前要设置最大长度值
        /// </summary>
        /// <param name="step">当前进度</param>
        public void changeProgressBarMothed(string step)
        {
            progressBar.Value = Convert.ToInt32(step);
        }
        /// <summary>
        /// 修改进度说明的方法
        /// </summary>
        /// <param name="msg">进度说明</param>
        public void changeProgressLabMethed(string msg)
        {
            Lab_Progress.Text = msg;
        }
        /// <summary>
        /// 修改进度条最大值
        /// </summary>
        /// <param name="i">进度条最大值</param>
        public void changeProgressBarMaxumunMothed(string i)
        {
            progressBar.Maximum = Convert.ToInt32(i);
        }
        public delegate void changeState();//改变计算按钮可用性的委托
        public changeState ChangeState;
        /// <summary>
        /// 改变计算按钮可用性方法
        /// </summary>
        public void ChangeStateMothed()
        {
            Btn_UpdateTrainSet.Text = "重更样本";
            Btn_ComputeSHA.Enabled = true;
            Btn_ComputeSSA.Enabled = true;
            Btn_ComputeZXB.Enabled = true;
            Btn_ComputeAll.Enabled = true;
            Lab_Progress.Text = "已完成";
        }
        #endregion
        #region 更新样本
        private int GetDataThreading = 0;
        string[] CodeSHAgu = null;
        string[] CodeSZAgu = null;
        string[] CodeZXB = null;
        int Task = 0;
        int Num_SHAgu = 0;
        int Num_SZAgu = 0;
        int Num_ZXB = 0;
        int Num_Threads = 0;
        DateTime StartDate;
        DateTime EndDate;
        public void UpdateTrainSet()
        {
            GetDataThreading = 40;//定义线程数为40
            if (File.Exists("data/上证A股.txt") && File.Exists("data/深圳A股.txt") && File.Exists("data/中小板.txt"))
            {
                CodeSHAgu = File.ReadAllText("data/上证A股.txt").Split(',');
                CodeSZAgu = File.ReadAllText("data/深圳A股.txt").Split(',');
                CodeZXB = File.ReadAllText("data/中小板.txt").Split(',');
                Task = CodeSZAgu.Length + CodeSHAgu.Length + CodeZXB.Length;//任务总数量，用于计数
                progressBar.Maximum = Task;
                Thread[] GetAndAppendThreads = new Thread[GetDataThreading];
                for (int i = 0; i < GetDataThreading; i++)
                {
                    GetAndAppendThreads[i] = new Thread(GetSHAGu);
                    GetAndAppendThreads[i].Start();
                    Num_Threads++;
                }
            }
            else
            {
                MessageBox.Show("请先更新股票代码");
                return;
            }
        }
        /// <summary>
        /// 获得上证A股方法
        /// </summary>
        public void GetSHAGu()
        {
            string Code = "";
            while (CodeSHAgu.Length > Num_SHAgu)
            {
                lock (this)
                {
                    Code = CodeSHAgu[Num_SHAgu];
                    Num_SHAgu++;
                }
                DH.AppendTxt("data/stock/SHAGu/" + Code + ".txt", DH.StockToString(GD.GetDate(Code, StartDate, EndDate, "SHAGu")));
                progressBar.Invoke(changeProgerssBar, Num_SHAgu.ToString());
                Lab_Progress.Invoke(changeProgressLab, "正在更新上证A股样本：" + Num_SHAgu + "/" + CodeSHAgu.Length);
            }
            lock (this)
            {
                Num_Threads--;
            }
            if (Num_Threads == 0)
            {
                Thread[] GetAndAppendThreads = new Thread[GetDataThreading];
                for (int i = 0; i < GetDataThreading; i++)
                {
                    GetAndAppendThreads[i] = new Thread(GetSZAGu);
                    GetAndAppendThreads[i].Start();
                    Num_Threads++;
                }
            }
        }
        /// <summary>
        /// 获得深圳A股方法
        /// </summary>
        public void GetSZAGu()
        {
            string Code = "";
            while (CodeSZAgu.Length > Num_SZAgu)
            {
                lock (this)
                {
                    Code = CodeSZAgu[Num_SZAgu];
                    Num_SZAgu++;
                }
                DH.AppendTxt("data/stock/SZAGu/" + Code + ".txt", DH.StockToString(GD.GetDate(Code, StartDate, EndDate, "SZAGu")));
                progressBar.Invoke(changeProgerssBar, (Num_SHAgu + Num_SZAgu).ToString());
                Lab_Progress.Invoke(changeProgressLab, "正在更新深圳A股样本：" + Num_SZAgu + "/" + CodeSZAgu.Length);
            }
            lock (this)
            {
                Num_Threads--;
            }
            if (Num_Threads == 0)
            {
                Thread[] GetAndAppendThreads = new Thread[GetDataThreading];
                for (int i = 0; i < GetDataThreading; i++)
                {
                    GetAndAppendThreads[i] = new Thread(GetZXB);
                    GetAndAppendThreads[i].Start();
                    Num_Threads++;
                }
            }
        }
        public void GetZXB()
        {
            string Code = "";
            while (CodeZXB.Length > Num_ZXB)
            {
                lock (this)
                {
                    Code = CodeZXB[Num_ZXB];
                    Task--;
                    Num_ZXB++;
                }
                DH.AppendTxt("data/stock/ZXB/" + Code + ".txt", DH.StockToString(GD.GetDate(Code, StartDate, EndDate, "中小板")));
                progressBar.Invoke(changeProgerssBar, (Num_SHAgu + Num_SZAgu + Num_ZXB).ToString());
                Lab_Progress.Invoke(changeProgressLab, "正在更新深圳A股样本：" + Num_ZXB + "/" + CodeZXB.Length);
            }
            lock (this)
            {
                Num_Threads--;
            }
            if (Num_Threads == 0)
            {
                string Today = "";
                DateTime today = DateTime.Today.AddDays(-1);//获取前一天的日期
                if ((int)today.DayOfWeek == 0)
                {
                    Today = today.AddDays(-2).ToShortDateString();
                }
                else if ((int)today.DayOfWeek == 6)
                {
                    Today = today.AddDays(-1).ToShortDateString();
                }
                else
                {
                    Today = today.ToShortDateString();
                }
                File.WriteAllText("data/stock/UpdateDate.txt", Today);
                int NumSHAGu = Directory.GetFiles("data/stock/SHAGu").Length;
                int NumSZAGu = Directory.GetFiles("data/stock/SZAGu").Length;
                int NumZXB = Directory.GetFiles("data/stock/ZXB").Length;
                if (Num_SHAgu < 100 || Num_SZAgu < 100 || NumZXB < 100)
                {
                    MessageBox.Show("由于网络原因，获取样本不足。共获取样本上证A股：" + NumSHAGu + ";深圳A股：" + NumSZAGu + ";中小版：" + NumZXB + "请稍后重新更新样本");
                }
                else
                {
                    this.Invoke(ChangeState);
                }
            }
        }
        #endregion
        #region 读取样本并对样本进行聚类,然后进行pca主成分提取
        /// <summary>
        /// 从txt文档中读取样本
        /// </summary>
        /// <param name="plate">板块</param>
        /// <param name="Num">每支股票提供样本数量</param>
        /// <param name="Length">样本包含股票n日内交易数据</param>
        /// <param name="forecast">预测时间</param>
        private void GetTrainSet(string plate, int Num, int Length, int forecast)
        {
            string path = "";
            string msg = "";
            switch (plate)
            {
                case "SHAGu":
                    path = @"data/stock/SHAGu";
                    msg = "正在读取上海A股样本：";
                    break;
                case "SZAGu":
                    path = @"data/stock/SZAGu";
                    msg = "正在读取深圳A股样本";
                    break;
                case "ZXB":
                    path = @"data/stock/ZXB";
                    msg = "正在读取中小板样本";
                    break;
                case "All":
                    if (!DH.Computed("SHA"))
                    {
                        path = @"data/stock/SHAGu";
                        msg = "正在读取上海A股样本：";
                    }
                    else if (!DH.Computed("SZA"))
                    {
                        path = @"data/stock/SZAGu";
                        msg = "正在读取深圳A股样本";
                    }
                    else
                    {
                        path = @"data/stock/ZXB";
                        msg = "正在读取中小板样本";
                    }
                    break;
            }
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
            ArrayList FileName = BC.GetAll(dir);
            int Num_Train = Num * FileName.Count;
            labrary.Stock.TrainSet[] TrainSet = new labrary.Stock.TrainSet[Num_Train];
            for (int i = 0; i < Num * FileName.Count; i++)
            {
                TrainSet[i].trainSet = new labrary.Stock.StockInfo[Length];//定义样本包含股票num日内交易数据
            }
            progressBar.Invoke(changeProgerssBar, "0");
            progressBar.Invoke(changeProgressBarMaximun, Num_Train.ToString());
            Lab_Progress.Invoke(changeProgressLab, "");
            for (int i = 0; i < FileName.Count; i++)
            {
                string Path = path + "/" + FileName[i].ToString();
                labrary.Stock.StockInfo[] stock = DH.ReadTxt(Path);//获取到每支股票的历史数据
                if (stock.Length < Num + Length + forecast)//数据长度不够无法生成样本,5为预测时间
                {
                    File.Delete(Path);
                    FileName.RemoveAt(i);
                    Num_Train = Num * FileName.Count;
                    progressBar.Invoke(changeProgressBarMaximun, Num_Train.ToString());
                    for (int n = 0; n < Num; n++)
                    {
                        BC.RemoveArr(TrainSet, i * Num + n);
                    }
                    i--;
                    continue;
                }
                for (int j = 0; j < Num; j++)
                {
                    try
                    {
                        for (int n = 0; n < Length; n++)
                        {
                            try
                            {
                                TrainSet[i * Num + j].trainSet[n] = stock[n + j + forecast];
                                TrainSet[i * Num + j].result = Convert.ToDouble(stock[n + j].close - stock[n + j + forecast].close) / Convert.ToDouble(stock[n + j + forecast].close);
                            }
                            catch
                            { continue; }
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                if ((i + 1) < FileName.Count)
                {
                    progressBar.Invoke(changeProgerssBar, ((i + 1) * Num).ToString());
                    progressBar.Invoke(changeProgressLab, msg + (i + 1) * Num + "/" + Num_Train);
                }
                else
                {
                    progressBar.Invoke(changeProgerssBar, ((i + 1) * Num).ToString());
                    progressBar.Invoke(changeProgressLab, "已完成");
                }
            }
            List<labrary.ComputeStockHelper.Crowd> crowd = CSH.K_Mean(TrainSet, 7);
            List<List<labrary.Matrix>> pcaList = DoPCA(crowd);//pca主成分识别
            labrary.Stock.TrainSet[] PredictionSample = new labrary.Stock.TrainSet[FileName.Count];
            for (int i = 0; i < FileName.Count; i++)
            {
                PredictionSample[i].trainSet = new labrary.Stock.StockInfo[Length];//定义样本包含股票num日内交易数据
            }
            progressBar.Invoke(changeProgressLab, "正在读取预测样本");
            progressBar.Invoke(changeProgerssBar, "0");
            progressBar.Invoke(changeProgressBarMaximun, PredictionSample.Length.ToString());

            for (int i = 0; i < PredictionSample.Length; i++)
            {
                string Path = path + "/" + FileName[i].ToString();
                labrary.Stock.StockInfo[] stock = DH.ReadTxt(Path);//获取到每支股票的历史数据
                for (int j = 0; j < Length; j++)
                {
                    PredictionSample[i].trainSet[j] = stock[j];
                }
                progressBar.Invoke(changeProgerssBar, (i + 1).ToString());
                Lab_Progress.Invoke(changeProgressLab, (i + 1).ToString() + "/" + PredictionSample.Length.ToString());
            }
            AverageEulerDistance(pcaList, PredictionSample, plate, crowd);
        }
        #endregion
        #region 主成分识别
        /// <summary>
        /// 返回七个聚类数据的PCA分析结果
        /// </summary>
        /// <param name="crowd">聚类样本</param>
        /// <returns>聚类过后七类数据的PCA分析结果</returns>
        private List<List<labrary.Matrix>> DoPCA(List<labrary.ComputeStockHelper.Crowd> crowd)
        {
            List<labrary.Stock.TrainSet> TrainSet1 = crowd[0].TrainSetList;
            List<labrary.Stock.TrainSet> TrainSet2 = crowd[1].TrainSetList;
            List<labrary.Stock.TrainSet> TrainSet3 = crowd[2].TrainSetList;
            List<labrary.Stock.TrainSet> TrainSet4 = crowd[3].TrainSetList;
            List<labrary.Stock.TrainSet> TrainSet5 = crowd[4].TrainSetList;
            List<labrary.Stock.TrainSet> TrainSet6 = crowd[5].TrainSetList;
            List<labrary.Stock.TrainSet> TrainSet7 = crowd[6].TrainSetList;
            progressBar.Invoke(changeProgressBarMaximun, "7");
            progressBar.Invoke(changeProgerssBar, "0");
            progressBar.Invoke(changeProgressLab, "pca主成分分析中...");
            List<labrary.Matrix> result1 = CSH.PCA(TrainSet1.ToArray());
            progressBar.Invoke(changeProgerssBar, "1");
            List<labrary.Matrix> result2 = CSH.PCA(TrainSet2.ToArray());
            progressBar.Invoke(changeProgerssBar, "2");
            List<labrary.Matrix> result3 = CSH.PCA(TrainSet3.ToArray());
            progressBar.Invoke(changeProgerssBar, "3");
            List<labrary.Matrix> result4 = CSH.PCA(TrainSet4.ToArray());
            progressBar.Invoke(changeProgerssBar, "4");
            List<labrary.Matrix> result5 = CSH.PCA(TrainSet5.ToArray());
            progressBar.Invoke(changeProgerssBar, "5");
            List<labrary.Matrix> result6 = CSH.PCA(TrainSet6.ToArray());
            progressBar.Invoke(changeProgerssBar, "6");
            List<labrary.Matrix> result7 = CSH.PCA(TrainSet7.ToArray());
            progressBar.Invoke(changeProgerssBar, "7");
            List<List<labrary.Matrix>> pcaList = new List<List<labrary.Matrix>>();
            pcaList.Add(result1);
            pcaList.Add(result2);
            pcaList.Add(result3);
            pcaList.Add(result4);
            pcaList.Add(result5);
            pcaList.Add(result6);
            pcaList.Add(result7);
            progressBar.Invoke(changeProgressLab, "pca主成分分析完成...");
            return pcaList;
        }
        /// <summary>
        /// 计算每个预测样本的pca投影向量与不同类别训练向量欧拉距离的平均值，并记录与文档中
        /// </summary>
        /// <param name="pcaList">训练样本pca处理后返回的集合</param>
        /// <param name="PredictionSample">预测样本</param>
        /// <param name="plate">板块名称</param>
        private void AverageEulerDistance(List<List<labrary.Matrix>> pcaList, labrary.Stock.TrainSet[] PredictionSample, string plate, List<labrary.ComputeStockHelper.Crowd> crowd)
        {
            string platename = "";
            string Plate = "";
            switch (plate)
            {
                case "SHAGu":
                    platename = "上证A股";
                    Plate = "SHA";
                    break;
                case "SZAGu":
                    platename = "深圳A股";
                    Plate = "SZA";
                    break;
                case "ZXB":
                    platename = "中小板";
                    Plate = "ZXB";
                    break;
                case "All":
                    if (!DH.Computed("SHA"))
                    {
                        platename = "上证A股";
                        Plate = "SHA";
                    }
                    else if (!DH.Computed("SZA"))
                    {
                        platename = "深圳A股";
                        Plate = "SZA";
                    }
                    else
                    {
                        platename = "中小板";
                        Plate = "ZXB";
                    }
                    break;
            }
            progressBar.Invoke(changeProgressLab, platename + "正在计算欧拉距离");
            progressBar.Invoke(changeProgerssBar, "0");
            progressBar.Invoke(changeProgressBarMaximun, PredictionSample.Length.ToString());
            List<labrary.Matrix> tranPCAMatrixList = new List<labrary.Matrix>();
            List<labrary.Matrix> TrainSampleList = new List<labrary.Matrix>();
            matrixmultiply mm = new matrixmultiply();
            eulerdistance ed = new eulerdistance();
            for (int i = 0; i < pcaList.ToArray().Length; i++)
            {
                labrary.Matrix tranPCAMatrix = labrary.Matrix.transpose(pcaList.ToArray()[i][2]);
                labrary.Matrix TrainSample = new labrary.Matrix((double[,])mm.MatrixMultiply((MWNumericArray)labrary.Matrix.ConvertToDouble(tranPCAMatrix), (MWNumericArray)labrary.Matrix.ConvertToDouble(pcaList.ToArray()[i][6])).ToArray());
                tranPCAMatrixList.Add(tranPCAMatrix);
                TrainSampleList.Add(TrainSample);
            }
            double[,] ScreenData = new double[PredictionSample.Length, pcaList.ToArray().Length + 1];
            for (int i = 0; i < PredictionSample.Length; i++)
            {
                for (int j = 0; j < pcaList.ToArray().Length; j++)
                {
                    labrary.Matrix AMatrix = pcaList.ToArray()[j].ToArray()[6];
                    if (j == 0)
                    {
                        ScreenData[i, j] = Convert.ToDouble(PredictionSample[i].trainSet[0].coding);
                    }
                    ScreenData[i, j + 1] = CSH.EulerDistance(PredictionSample[i], pcaList.ToArray()[j], tranPCAMatrixList.ToArray()[j], TrainSampleList.ToArray()[j], AMatrix, ed, mm);
                }
                progressBar.Invoke(changeProgerssBar, (i + 1).ToString());
                Lab_Progress.Invoke(changeProgressLab, platename + (i + 1).ToString() + "/" + PredictionSample.Length.ToString());
            }
            Lab_Progress.Invoke(changeProgressLab, platename + "正在计算排名");
            double[,] reslut = CSH.ScreenStock(ScreenData, 5);
            DateTime today = DateTime.Now.AddDays(-1);
            if ((int)today.DayOfWeek == 0)
                today = today.AddDays(-2).Date;
            else if ((int)today.DayOfWeek == 6)
                today = today.AddDays(-1).Date;
            else
                today = today.Date;
            DH.MatrixWriteTxt(@"data/stock/Result/" + Plate + "-" + today.ToString("d").Remove(today.ToString("d").Length - 4).Replace("/", "-") + ".txt", reslut);
            switch (plate)
            {
                case "SHAGu":
                    Btn_ComputeSHA.Enabled = false;
                    Btn_ComputeSHA.Text = "已计算";
                    if (!DH.Computed("SZA"))
                        Btn_ComputeSSA.Enabled = true;
                    if (!DH.Computed("ZXB"))
                        Btn_ComputeZXB.Enabled = true;
                    break;
                case "SZAGu":
                    Btn_ComputeSSA.Enabled = false;
                    Btn_ComputeSSA.Text = "已计算";
                    if (!DH.Computed("SHA"))
                        Btn_ComputeSHA.Enabled = true;
                    if (!DH.Computed("ZXB"))
                        Btn_ComputeZXB.Enabled = true;
                    break;
                case "ZXB":
                    Btn_ComputeZXB.Enabled = false;
                    Btn_ComputeZXB.Text = "已计算";
                    if (!DH.Computed("SHA"))
                        Btn_ComputeSHA.Enabled = true;
                    if (!DH.Computed("SZA"))
                        Btn_ComputeSSA.Enabled = true;
                    break;
                case "All":
                    if (!DH.Computed("SHA"))
                    {
                        Thread objThread = new Thread(new ThreadStart(delegate
                        {
                            GetTrainSet("All", 30, 15, 5);
                        }));
                        objThread.Start();
                    }
                    else if (!DH.Computed("SZA"))
                    {
                        Thread objThread = new Thread(new ThreadStart(delegate
                        {
                            GetTrainSet("All", 30, 15, 5);
                        }));
                        objThread.Start();
                    }
                    else if (!DH.Computed("ZXB"))
                    {
                        Thread objThread = new Thread(new ThreadStart(delegate
                        {
                            GetTrainSet("All", 30, 15, 5);
                        }));
                        objThread.Start();
                    }
                    break;
            }
            if (DH.Computed("SHA") && DH.Computed("SZA") && DH.Computed("ZXB"))
                Btn_ComputeAll.Enabled = false;
            else
            {
                if (plate != "All")
                {
                    Btn_ComputeAll.Enabled = true;
                    Btn_UpdateTrainSet.Enabled = true;
                }
            }
            Lab_Progress.Invoke(changeProgressLab, platename + "计算完成");
        }
        #endregion


    }
}
