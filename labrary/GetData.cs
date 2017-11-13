using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace StockSwitch.labrary
{
    /// <summary>
    /// 用于向yahoo接口请求数据
    /// </summary>
    public class GetData
    {
        
        private string UrlStr = "http://table.finance.yahoo.com/table.csv?";
        /// <summary>
        /// 向yahoo接口获取股票历史数据
        /// </summary>
        /// <param name="Code">请求的股票代码</param>
        /// <param name="StartDate">请求历史记录开始时间</param>
        /// <param name="EndDate">请求历史记录结束时间</param>
        /// <returns>一支股票的数组</returns>
        public Stock.StockInfo[] GetDate(string Code, DateTime StartDate, DateTime EndDate,string plate)
        {
            Basic basic = new Basic();
            string RequestString = RequestStr(UrlStr, Code, StartDate, EndDate,plate);
            WebClient wc = new WebClient();
            try
            {
                string data = wc.DownloadString(RequestString);
                string[] dataline = basic.RemoveArr(data.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries), 0);
                Stock.StockInfo[] stock = new Stock.StockInfo[dataline.Length];
                for (int i = 0; i < stock.Length; i++)
                {
                    string[] datarow = dataline[i].ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    stock[i].coding = Code;
                    stock[i].date = datarow[0];
                    stock[i].open = Convert.ToDecimal(datarow[1]);
                    stock[i].High = Convert.ToDecimal(datarow[2]);
                    stock[i].Low = Convert.ToDecimal(datarow[3]);
                    stock[i].close = Convert.ToDecimal(datarow[4]);
                    stock[i].volume = Convert.ToInt64(datarow[5]);
                }
                return stock;
            }
            catch (Exception)
            {
                Stock.StockInfo[] stock = null;
                return stock;
            }
        }
        /// <summary>
        /// 生成请求字符串
        /// </summary>
        /// <param name="UrlStr">请求地址</param>
        /// <param name="Code">请求的股票代码</param>
        /// <param name="StartDate">请求历史记录开始时间</param>
        /// <param name="EndDate">请求历史记录结束时间</param>
        /// <returns>返回请求字符串</returns>
        private string RequestStr(string UrlStr, string Code, DateTime StartDate, DateTime EndDate,string plate)    
        {
            string a = EndDate.Day.ToString();
            string b = (EndDate.Month - 1).ToString();//yahoo请求中月份要减一
            string c = EndDate.Year.ToString();
            string d = StartDate.Day.ToString();
            string e = (StartDate.Month - 1).ToString();//yahoo请求中月份要减一
            string f = StartDate.Year.ToString();
            string param = "&a=" + e + "&b=" + d + "&c=" + f + "&d=" + b + "&e=" + a + "&f=" + c;
            string Url = UrlStr + "s=" + Code + ".sz" + param;
            if (plate=="SHAGu")
                Url = UrlStr + "s=" + Code + ".ss" + param;
            return Url;
        }
    }
}
