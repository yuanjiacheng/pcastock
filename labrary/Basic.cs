using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace StockSwitch.labrary
{
    public class Basic
    {
        /// <summary>
        /// 移除数组中的某项
        /// </summary>
        /// <param name="array">需被移除项的数组</param>
        /// <param name="index">index为第几项</param>
        /// <returns></returns>
        public string[] RemoveArr(string[] array, int index)
        {
            int length = array.Length;
            string[] result = new string[length - 1];
            Array.Copy(array, result, index);
            Array.Copy(array, index + 1, result, index, length - index - 1);
            return result;
        }
        /// <summary>
        /// 移除trainset结构数组中的某项
        /// </summary>
        /// <param name="array">需被移除项的数组</param>
        /// <param name="index">index为第几项</param>
        /// <returns></returns>
        public labrary.Stock.TrainSet[] RemoveArr(labrary.Stock.TrainSet[] array, int index)
        {
            int length = array.Length;
            labrary.Stock.TrainSet[] result = new labrary.Stock.TrainSet[length - 1];
            Array.Copy(array, result, index);
            Array.Copy(array, index + 1, result, index, length - index - 1);
            return result;
        }
        /// <summary>
        /// 删除重复数组
        /// </summary>
        /// <param name="values">输入数组</param>
        /// <returns>无重复的数组</returns>
        public string[] GetString(string[] values)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < values.Length; i++)//遍历数组成员 
            {
                if (list.IndexOf(values[i].ToLower()) == -1)//对每个成员做一次新数组查询如果没有相等的则加到新数组 
                    list.Add(values[i]);
            }
            return list.ToArray();
        }
        /// <summary>
        /// 获取所有的文件名
        /// </summary>
        /// <param name="dir">目录地址</param>
        /// <returns>目录下所有文件名</returns>
        public ArrayList GetAll(DirectoryInfo dir)
        {
            ArrayList FileList = new ArrayList();

            FileInfo[] allFile = dir.GetFiles();
            foreach (FileInfo fi in allFile)
            {
                FileList.Add(fi.Name);
            }
            return FileList;
        }
    }
}
