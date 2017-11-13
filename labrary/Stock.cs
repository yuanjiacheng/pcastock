using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockSwitch.labrary
{
    public class Stock
    {
        /// <summary>
        /// 股票的结构
        /// </summary>
        public struct StockInfo
        {
            public string coding;
            public string date;
            public decimal open;
            public decimal High;
            public decimal Low;
            public decimal close;
            public long volume;
        }
        /// <summary>
        /// 样本结构体
        /// </summary>
        public struct TrainSet
        {
            public StockInfo[] trainSet;
            public double result;
        }
    }
}
