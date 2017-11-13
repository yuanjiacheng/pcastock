using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace StockSwitch.labrary
{
    /// <summary>
    /// 负责数据的更删改查
    /// </summary>
    public class DataHelper
    {
        #region 通用类操作
        /// <summary>
        /// 将数据流用分行写入txt文档
        /// </summary>
        /// <param name="path"></param>
        /// <param name="TxtWords"></param>
        public void AppendTxt(string path, string[] TxtWords)
        {
            if (TxtWords != null)
            {
                string txtWord = "";
                for (int i = 0; i < TxtWords.Length; i++)
                {
                    if (i < TxtWords.Length)
                        txtWord += TxtWords[i] + "\r\n";
                    else
                        txtWord += TxtWords[i];
                }
                if (File.Exists(path))
                    txtWord += File.ReadAllText(path);
                File.WriteAllText(path, txtWord);
            }
            else
            {
                File.Delete(path);
            }
        }
        /// <summary>
        /// 将数据流用逗号分开写入txt文档
        /// </summary>
        /// <param name="path">文本地址，注意格式为股票代码加修改日期</param>
        /// <param name="TxtWords">字符串数组</param>
        public void WriteTxt(string path, string[] TxtWords)   //写入txt
        {
            string txtWord = "";
            for (int i = 0; i < TxtWords.Length; i++)
            {
                if (i < TxtWords.Length - 1)
                    txtWord += TxtWords[i] + ',';
                else
                    txtWord += TxtWords[i];
            }
            File.WriteAllText(path, txtWord);
        }
        /// <summary>
        /// 删除指定文本中的第i行,并返回文本中的所有行数据
        /// </summary>
        /// <param name="path">文本地址，注意格式为股票代码加修改日期</param>
        /// <param name="i">第i行</param>
        /// <returns>字符串数组</returns>
        public string[] DelTxt(string path, int i)
        {
            string[] txtWord;
            txtWord = File.ReadAllLines(path);
            int Length = txtWord.Length;
            string[] txtword = new string[Length - 1];
            Array.Copy(txtWord, txtword, i);  //将原数组中第一个至i个元素复制到新数组中
            Array.Copy(txtWord, i + 1, txtword, i, Length - i - 1); //将数组中第i+1个元素至最后一个数复制到新数组中
            return txtword;
        }
        /// <summary>
        /// 矩阵写入
        /// </summary>
        /// <param name="path">写入地址</param>
        /// <param name="Matrix">写入的矩阵</param>
        public void MatrixWriteTxt(string path, double[,] Matrix)
        {
            string txtWord = "";
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        string code = Matrix[i, j].ToString();
                        if (code.Length == 4)
                        {
                            txtWord += "00";
                        }
                        if (code.Length == 3)
                        {
                            txtWord += "000";
                        }
                        if (code.Length == 2)
                        {
                            txtWord += "0000";
                        }
                        if (code.Length == 1)
                        {
                            txtWord += "00000";
                        }
                    }
                    if (j < Matrix.GetLength(1) - 1)
                        txtWord += Matrix[i, j].ToString() + ",";
                    else
                        txtWord += Matrix[i, j].ToString() + "\r\n";
                }
            }
            File.WriteAllText(path, txtWord);
        }
        #endregion
        #region stock操作
        /// <summary>
        /// 读取全部文本并生成stock结构
        /// </summary>
        /// <param name="path">文本地址，注意格式为股票代码加修改日期</param>
        /// <returns>stock结构的数组</returns>
        public Stock.StockInfo[] ReadTxt(string path)
        {
            string[] arr = File.ReadAllLines(path);
            Stock.StockInfo[] stock = new Stock.StockInfo[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                string[] stockInfo = arr[i].Split(',');
                stock[i].close = Convert.ToDecimal(stockInfo[0]);
                stock[i].coding = stockInfo[1];
                stock[i].date = stockInfo[2];
                stock[i].High = Convert.ToDecimal(stockInfo[3]);
                stock[i].Low = Convert.ToDecimal(stockInfo[4]);
                stock[i].open = Convert.ToDecimal(stockInfo[5]);
                stock[i].volume = Convert.ToInt64(stockInfo[6]);
            }
            return stock;
        }
        /// <summary>
        /// 将stock结构转换为string字符串
        /// </summary>
        /// <param name="stock">stock结构数据</param>
        /// <returns>string字符串</returns>
        public string[] StockToString(Stock.StockInfo[] stock)
        {
            if (stock != null)
            {
                string[] TxtWords = new string[stock.Length];
                for (int i = 0; i < stock.Length; i++)
                {
                    TxtWords[i] = stock[i].close + "," + stock[i].coding + "," + stock[i].date + "," + stock[i].High + "," + stock[i].Low + "," + stock[i].open + "," +
                        stock[i].volume;
                }
                return TxtWords;
            }
            else
            {
                return null;
            }
        }
        #endregion
        /// <summary>
        /// 该样本是否以计算过
        /// </summary>
        /// <param name="plate">板块</param>
        /// <returns>是否计算过</returns>
        public bool Computed(string plate)
        {
            DateTime today = DateTime.Now.AddDays(-1);
            if ((int)today.DayOfWeek == 0)
                today = today.AddDays(-2);
            else if ((int)today.DayOfWeek == 6)
                today = today.AddDays(-1);
            else
                today = today.Date;
            return File.Exists(@"data/stock/Result/" + plate + "-" + today.ToString("d").Remove(today.ToString("d").Length - 4).Replace("/", "-") + ".txt");
        }
        public string GetResult()
        {
            string result = "";
            DateTime today = DateTime.Now.AddDays(-1);
            if ((int)today.DayOfWeek == 0)
                today = today.AddDays(-2).Date;
            else if ((int)today.DayOfWeek == 6)
                today = today.AddDays(-1).Date;
            else
                today = today.Date;
            string date = today.ToString("d").Remove(today.ToString("d").Length - 4).Replace("/", "-");
            string path = @"data/stock/Result/";
            if(File.Exists(path+"SHA-"+date+".txt"))
                result+=File.ReadAllText(path+"SHA-"+date+".txt");
            if(File.Exists(path+"ZXB-"+date+".txt"))
                result+=File.ReadAllText(path+"ZXB-"+date+".txt");
            if(File.Exists(path+"SZA-"+date+".txt"))
                result+=File.ReadAllText(path+"SZA-"+date+".txt");
            return result;
        }
    }
}
