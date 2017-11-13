using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockSwitch.labrary
{
    /// <summary>
    /// 矩阵类
    /// </summary>
    public sealed class Matrix
    {
        int row, column;            //矩阵的行列数
        double[,] data;            //矩阵的数据
        #region 构造函数
        /// <summary>
        /// 生成空的矩阵
        /// </summary>
        /// <param name="rowNum">矩阵行数</param>
        /// <param name="columnNum">矩阵列数</param>
        public Matrix(int rowNum, int columnNum)
        {
            row = rowNum;
            column = columnNum;
            data = new double[row, column];
        }
        /// <summary>
        /// 2维数组生成矩阵
        /// </summary>
        /// <param name="members">2维数组</param>
        public Matrix(double[,] members)
        {
            row = members.GetUpperBound(0) + 1;
            column = members.GetUpperBound(1) + 1;
            data = new double[row, column];
            Array.Copy(members, data, row * column);
        }
        /// <summary>
        /// 一行矩阵
        /// </summary>
        /// <param name="vector">一维数组</param>
        public Matrix(double[] vector)
        {
            row = 1;
            column = vector.GetUpperBound(0) + 1;
            data = new double[1, column];
            for (int i = 0; i < vector.Length; i++)
            {
                data[0, i] = vector[i];
            }
        }
        #endregion
        #region 属性和索引器
        public int rowNum { get { return row; } }
        public int columnNum { get { return column; } }
        public double this[int r, int c]
        {
            get { return data[r, c]; }
            set { data[r, c] = value; }
        }
        #endregion
        #region 转置
        /// <summary>
        /// 将矩阵转置，得到一个新矩阵（此操作不影响原矩阵）
        /// </summary>
        /// <param name="input">要转置的矩阵</param>
        /// <returns>原矩阵经过转置得到的新矩阵</returns>
        public static Matrix transpose(Matrix input)
        {
            double[,] inverseMatrix = new double[input.column, input.row];
            for (int r = 0; r < input.row; r++)
                for (int c = 0; c < input.column; c++)
                    inverseMatrix[c, r] = input[r, c];
            return new Matrix(inverseMatrix);
        }
        #endregion
        #region 得到行向量或者列向量
        public Matrix getRow(int r)
        {
            if (r > row || r <= 0) throw new Exception("没有这一行");
            double[] a = new double[column];
            Array.Copy(data, column * (row - 1), a, 0, column);
            Matrix m = new Matrix(a);
            return m;
        }
        public Matrix getColumn(int c)
        {
            if (c > column || c < 0) throw new Exception("没有这一列。");
            double[,] a = new double[row, 1];
            for (int i = 0; i < row; i++)
                a[i, 0] = data[i, c];
            return new Matrix(a);
        }
        #endregion
        #region 操作符重载  + - * / == !=
        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.row != b.row || a.column != b.column)
                throw new Exception("矩阵维数不匹配。");
            Matrix result = new Matrix(a.row, a.column);
            for (int i = 0; i < a.row; i++)
                for (int j = 0; j < a.column; j++)
                    result[i, j] = a[i, j] + b[i, j];
            return result;
        }
        public static Matrix operator -(Matrix a, Matrix b)
        {
            return a + b * (-1);
        }
        public static Matrix operator *(Matrix matrix, double factor)
        {
            Matrix result = new Matrix(matrix.row, matrix.column);
            for (int i = 0; i < matrix.row; i++)
                for (int j = 0; j < matrix.column; j++)
                    matrix[i, j] = matrix[i, j] * factor;
            return matrix;
        }
        public static Matrix operator *(double factor, Matrix matrix)
        {
            return matrix * factor;
        }
        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.column != b.row)
                throw new Exception("矩阵维数不匹配。");
            Matrix result = new Matrix(a.row, b.column);
            for (int i = 0; i < a.row; i++)
                for (int j = 0; j < b.column; j++)
                    for (int k = 0; k < a.column; k++)
                        result[i, j] += a[i, k] * b[k, j];

            return result;
        }
        public static bool operator ==(Matrix a, Matrix b)
        {
            if (object.Equals(a, b)) return true;
            if (object.Equals(null, b))
                return a.Equals(b);
            return b.Equals(a);
        }
        public static bool operator !=(Matrix a, Matrix b)
        {
            return !(a == b);
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Matrix)) return false;
            Matrix t = obj as Matrix;
            if (row != t.row || column != t.column) return false;
            return this.Equals(t, 10);
        }
        /// <summary>
        /// 按照给定的精度比较两个矩阵是否相等
        /// </summary>
        /// <param name="matrix">要比较的另外一个矩阵</param>
        /// <param name="precision">比较精度（小数位）</param>
        /// <returns>是否相等</returns>
        public bool Equals(Matrix matrix, int precision)
        {
            if (precision < 0) throw new Exception("小数位不能是负数");
            double test = Math.Pow(10.0, -precision);
            if (test < double.Epsilon)
                throw new Exception("所要求的精度太高，不被支持。");
            for (int r = 0; r < this.row; r++)
                for (int c = 0; c < this.column; c++)
                    if (Math.Abs(this[r, c] - matrix[r, c]) >= test)
                        return false;

            return true;
        }

        #endregion
        #region 求逆
        /// <summary>
        /// 矩阵求逆
        /// </summary>
        /// <param name="m">原矩阵</param>
        /// <returns>逆矩阵</returns>
        public static Matrix converse(Matrix m)
        {
            if (m.row != m.column)
            {
                return null;
            }
            //clone  
            Matrix a = new Matrix(m.row, m.column);
            for (int i = 0; i < a.row; i++)
            {
                for (int j = 0; j < a.column; j++)
                {
                    a[i, j] = m[i, j];
                }
            }
            Matrix c = new Matrix(a.row, a.column);
            for (int i = 0; i < a.row; i++)
            {
                for (int j = 0; j < a.column; j++)
                {
                    if (i == j) { c[i, j] = 1; }
                    else { c[i, j] = 0; }
                }
            }

            //i表示第几行，j表示第几列  
            for (int j = 0; j < a.row; j++)
            {
                bool flag = false;
                for (int i = j; i < a.row; i++)
                {
                    if (a[i, j] != 0)
                    {
                        flag = true;
                        double temp;
                        //交换i,j,两行  
                        if (i != j)
                        {
                            for (int k = 0; k < a.row; k++)
                            {
                                temp = a[j, k];
                                a[j, k] = a[i, k];
                                a[i, k] = temp;

                                temp = c[j, k];
                                c[j, k] = c[i, k];
                                c[i, k] = temp;
                            }
                        }
                        //第j行标准化  
                        double d = a[j, j];
                        for (int k = 0; k < a.row; k++)
                        {
                            a[j, k] = a[j, k] / d;
                            c[j, k] = c[j, k] / d;
                        }
                        //消去其他行的第j列  
                        d = a[j, j];
                        for (int k = 0; k < a.row; k++)
                        {
                            if (k != j)
                            {
                                double t = a[k, j];
                                for (int n = 0; n < a.row; n++)
                                {
                                    a[k, n] -= (t / d) * a[j, n];
                                    c[k, n] -= (t / d) * c[j, n];
                                }
                            }
                        }
                    }
                }
                if (!flag) return null;
            }
            return c;
        }
        #endregion
        /// <summary>
        /// 将第一天的样本数据表示为1，下一天的表示为前一天的变化量
        /// </summary>
        /// <param name="m">原始数据矩阵</param>
        /// <returns>数据规范化后的矩阵</returns>
        public static Matrix DataNormalization(Matrix m)
        {
            Matrix M = new Matrix(m.rowNum, m.columnNum);
            for (int i = 0; i < m.columnNum; i++)
            {
                for (int j = 0; j < m.rowNum; j++)
                {
                    if (j % 15 == 0)
                    {
                        M[j, i] = 1;
                    }
                    else
                    {
                        if (j / 15 != 4)
                        {
                            if (m[j - 1, i] == 0 || m[j, i] == 0)
                                M[j, i] = 1;
                            else
                                M[j, i] = m[j, i] / m[j - 1, i];
                        }
                        else
                        {
                            if (m[j - 1, i] == 0 || m[j, i] == 0)
                                M[j, i] = 1;
                            else
                                M[j, i] = (100 - ((m[j, i] > m[j - 1, i]) ? m[j, i] : m[j - 1, i]) / ((m[j, i] < m[j - 1, i]) ? m[j, i] : m[j - 1, i])) / 100;

                        }
                    }
                }
            }
            return M;
        }
        /// <summary>
        /// 将Matrix类型转换为double[,]类型
        /// </summary>
        /// <param name="m">矩阵类型m</param>
        /// <returns>double[,]M</returns>
        public static double[,] ConvertToDouble(Matrix m)
        {
            double[,] M = new double[m.rowNum, m.columnNum];
            for (int i = 0; i < M.GetLength(0); i++)
            {
                for (int j = 0; j < M.GetLength(1); j++)
                {
                    M[i, j] = m[i, j];
                }
            }
            return M;
        }
    }
}
