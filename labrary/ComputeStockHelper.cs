using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using PCA;
using EulerDistance;
using MatrixMultiply;
using MathWorks.MATLAB.NET.Arrays;

namespace StockSwitch.labrary
{
    public class ComputeStockHelper
    {
        #region K-Mean聚类
        /// <summary>
        /// k-mean聚类算法
        /// </summary>
        /// <param name="trainSet">要进行聚类的训练样本</param>
        /// <param name="count">聚类数</param>
        /// <returns></returns>
        public List<Crowd> K_Mean(Stock.TrainSet[] trainSet, int count)
        {
            var crowds = new List<Crowd>(count);
            var step = trainSet.Count() / (count * 2);
            for (var i = 0; i < count; i++)
            {
                crowds.Add(new Crowd());
                crowds[i].Center = trainSet[step + step * 2 * i].result;
            }
            while (crowds.Sum(crowd => crowd.Change) > 0.01)
            {
                //Empty List and refresh Center
                crowds.ForEach(
                    crowd =>
                    {
                        if (!crowd.List.Any()) return;
                        crowd.RefreshCenter();
                        crowd.List.Clear();
                    });
                foreach (var TrainSet in trainSet)
                {
                    if (TrainSet.result < 1)
                    {
                        var index = 0; var minDistance = double.MaxValue;
                        for (var i = 0; i < count; i++)
                        {
                            var distance = Math.Abs(crowds[i].Center - TrainSet.result);
                            if (!(distance < minDistance)) continue;
                            index = i; minDistance = distance;
                        }
                        crowds[index].List.Add(TrainSet.result);
                    }
                }
            }
            crowds.ForEach(
                   crowd =>
                   {
                       if (!crowd.List.Any()) return;
                       crowd.RefreshCenter();
                       crowd.List.Clear();
                   });
            foreach (var TrainSet in trainSet)
            {
                if (TrainSet.result < 1)
                {
                    var index = 0; var minDistance = double.MaxValue;
                    for (var i = 0; i < count; i++)
                    {
                        var distance = Math.Abs(crowds[i].Center - TrainSet.result);
                        if (!(distance < minDistance)) continue;
                        index = i; minDistance = distance;
                    }
                    crowds[index].List.Add(TrainSet.result);
                    crowds[index].TrainSetList.Add(TrainSet);
                }
            }

            return crowds.OrderByDescending((i => i.Center)).ToList();
        }

        /// <summary>
        /// 集群
        /// </summary>
        public class Crowd
        {
            public List<Stock.TrainSet> TrainSetList = new List<Stock.TrainSet>();
            public List<double> List { get; set; }
            public double Average { get { return List.Average(); } }
            public double Center { get; set; }
            public double Change { get; private set; }
            public Crowd()
            {
                Change = double.MaxValue;
                List = new List<double>();
            }
            public void RefreshCenter()
            {
                Change = Math.Abs(Average - Center);
                Center = Average;
            }
        }
        #endregion
        #region 样本主成分分析
        /// <summary>
        /// pca算法步奏1：生成pca训练样本,其中排序方法为open，close，height，lowest，volume
        /// </summary>
        /// <param name="TrainSet">输入训练样本</param>
        /// <returns>X矩阵</returns>
        private Matrix createPCAMatrix(Stock.TrainSet[] TrainSet)
        {
            Matrix m = new Matrix(TrainSet[0].trainSet.Length * 5, TrainSet.Length);//新建训练矩阵
            for (int i = 0; i < TrainSet.Length; i++)
            {
                for (int j = 0; j < TrainSet[0].trainSet.Length * 5; j++)
                {
                    int k = j / TrainSet[0].trainSet.Length;
                    switch (k)
                    {
                        case 0:
                            m[j, i] = Convert.ToDouble(TrainSet[i].trainSet[j % TrainSet[0].trainSet.Length].open);
                            break;
                        case 1:
                            m[j, i] = Convert.ToDouble(TrainSet[i].trainSet[j % TrainSet[0].trainSet.Length].close);
                            break;
                        case 2:
                            m[j, i] = Convert.ToDouble(TrainSet[i].trainSet[j % TrainSet[0].trainSet.Length].High);
                            break;
                        case 3:
                            m[j, i] = Convert.ToDouble(TrainSet[i].trainSet[j % TrainSet[0].trainSet.Length].Low);
                            break;
                        case 4:
                            m[j, i] = Convert.ToDouble(TrainSet[i].trainSet[j % TrainSet[0].trainSet.Length].volume);
                            break;
                    }
                }
            }
            m = Matrix.DataNormalization(m);
            return m;
        }
        /// <summary>
        /// pac算法步奏2.1：计算x^(i)的平均值
        /// </summary>
        /// <param name="m">X矩阵</param>
        /// <returns>x^(i)的平均值</returns>
        private Matrix createAverageMatrix(Matrix m)
        {
            Matrix AverageMatrix = new Matrix(m.rowNum, 1);
            Matrix SumMatrix = new Matrix(m.rowNum, 1);
            for (int i = 0; i < m.columnNum; i++)
            {
                if (i == 0)
                {
                    SumMatrix = m.getColumn(i);
                }
                else
                {
                    SumMatrix = SumMatrix + m.getColumn(i);
                }
            }
            AverageMatrix = SumMatrix * (1 / m.columnNum);
            return AverageMatrix;
        }
        /// <summary>
        ///pca算法步骤2.2：X矩阵平均值归零
        /// </summary>
        /// <param name="m">X矩阵</param>
        /// <param name="AverageMatrix">x^(i)平均值</param>
        /// <returns>平均值归零的X矩阵</returns>
        private Matrix deleteAverageMatrix(Matrix m, Matrix AverageMatrix)
        {
            Matrix DeleteColumnMatrix = new Matrix(m.rowNum, m.columnNum);
            for (int i = 0; i < m.columnNum; i++)
            {
                DeleteColumnMatrix = m.getColumn(i) - AverageMatrix;
                for (int j = 0; j < m.rowNum; j++)
                {
                    m[j, i] = DeleteColumnMatrix[j, 0];
                }
            }
            return m;
        }
        /// <summary>
        /// pca算法步骤2.3：返回矩阵A，A=（x^1,x^2,,,x^3）,其中x^1=x^1-AverageMatrix,这样可以用A*A^T来表达sum（x^i*(x^i)^t）
        /// </summary>
        /// <param name="DeleteAverageMatrix">平局值归零的X矩阵</param>
        /// <param name="AverageMatrix">x^i的平局值</param>
        /// <returns>矩阵A，A=（x^1,x^2,,,x^3）</returns>
        private Matrix ctrateAMatrix(Matrix DeleteAverageMatrix, Matrix AverageMatrix)
        {
            Matrix A = new Matrix(DeleteAverageMatrix.rowNum, DeleteAverageMatrix.columnNum);
            for (int i = 0; i < DeleteAverageMatrix.columnNum; i++)
            {
                Matrix xi = DeleteAverageMatrix.getColumn(i) - AverageMatrix;
                for (int j = 0; j < xi.rowNum; j++)
                {
                    A[j, i] = xi[j, 0];
                }
            }
            return A;
        }
        /// <summary>
        /// 应用matlab进行PCA分析
        /// </summary>
        /// <param name="A">A矩阵</param>
        /// <returns>0:Y,1:V,2:E,3:D四个矩阵，Y:对X进行PCA分析后的投影矩阵,V:与X有关的协方差矩阵特征向量的白化矩阵,E:对应的特征向量（列）构成的矩阵D:D是对应的特征值构成的对角矩阵</returns>
        private List<Matrix> DoPCA(Matrix A)
        {
            PCAHelper PH = new PCAHelper();
            double[,] AMatrix = new double[A.rowNum, A.columnNum];
            for (int i = 0; i < AMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < AMatrix.GetLength(1); j++)
                {
                    AMatrix[i, j] = A[i, j];
                }
            }
            MWArray[] reslut = PH.PCA(4, (MWNumericArray)(AMatrix));
            Matrix V = new Matrix((double[,])reslut[1].ToArray());
            Matrix E = new Matrix((double[,])reslut[2].ToArray());
            Matrix D = new Matrix((double[,])reslut[3].ToArray());
            double SumEigenvalue = 0;
            for (int i = 0; i < D.rowNum; i++)
            {
                SumEigenvalue += D[i, i];
            }
            int k = 0;
            for (double Eigenvalue = 0; Eigenvalue < SumEigenvalue * 0.99; Eigenvalue += D[k - 1, k - 1])
            {
                k++;
            }
            Matrix e = new Matrix(E.rowNum, k);
            for (int i = 0; i < e.columnNum; i++)
            {
                for (int j = 0; j < e.rowNum; j++)
                {
                    e[j, i] = E[j, i];
                }
            }
            Matrix Y = Matrix.transpose(e) * A;//特征提取降维
            List<Matrix> Reslut = new List<Matrix>(4);
            Reslut.Add(Y);
            Reslut.Add(V);
            Reslut.Add(e);
            Reslut.Add(D);
            return Reslut;
        }
        /// <summary>
        /// 返归pca的计算结果，列均值，和均值化的训练矩阵
        /// </summary>
        /// <param name="TrainSet">原始训练样本</param>
        /// <returns></returns>
        public List<Matrix> PCA(Stock.TrainSet[] TrainSet)
        {
            Matrix PCAMatrix = createPCAMatrix(TrainSet);
            Matrix AverageMatrix = createAverageMatrix(PCAMatrix);
            Matrix delAverageMatrix = deleteAverageMatrix(PCAMatrix, AverageMatrix);
            Matrix AMatrix = ctrateAMatrix(delAverageMatrix, AverageMatrix);
            List<Matrix> result = DoPCA(AMatrix);
            result.Add(AverageMatrix);
            result.Add(delAverageMatrix);
            result.Add(AMatrix);
            return result;
        }
        /// <summary>
        /// 预测样本与训练样本的欧式距离
        /// </summary>
        /// <param name="TrainSet">要预测的样本</param>
        /// <param name="PCAMatrix">PCA函数返回的矩阵集合</param>
        /// <param name="AMatrix">去均值样本</param>
        /// <returns>预测样本去均值在PCA返回的特征空间E的投影后的向量与PCA返回的向量集合Y的欧式距离的平均值</returns>
        public double EulerDistance(Stock.TrainSet PredictionSample, List<Matrix> PCAMatrix, Matrix tranPCAMatrix, Matrix TrainSample, Matrix AMatrix, eulerdistance ed, matrixmultiply mm)
        {
            Matrix predictionSample = new Matrix(PredictionSample.trainSet.Length * 5, 1);//新建预测样本向量
            for (int i = 0; i < PredictionSample.trainSet.Length * 5; i++)
            {
                int k = i / PredictionSample.trainSet.Length;
                switch (k)
                {
                    case 0:
                        predictionSample[i, 0] = Convert.ToDouble(PredictionSample.trainSet[i % PredictionSample.trainSet.Length].open);
                        break;
                    case 1:
                        predictionSample[i, 0] = Convert.ToDouble(PredictionSample.trainSet[i % PredictionSample.trainSet.Length].close);
                        break;
                    case 2:
                        predictionSample[i, 0] = Convert.ToDouble(PredictionSample.trainSet[i % PredictionSample.trainSet.Length].High);
                        break;
                    case 3:
                        predictionSample[i, 0] = Convert.ToDouble(PredictionSample.trainSet[i % PredictionSample.trainSet.Length].Low);
                        break;
                    case 4:
                        predictionSample[i, 0] = Convert.ToDouble(PredictionSample.trainSet[i % PredictionSample.trainSet.Length].volume);
                        break;
                }
            }
            predictionSample = Matrix.DataNormalization(predictionSample);//预测样本标准化
            predictionSample = new Matrix((double[,])mm.MatrixMultiply((MWNumericArray)Matrix.ConvertToDouble(tranPCAMatrix), (MWNumericArray)Matrix.ConvertToDouble(predictionSample)).ToArray());//预测样本投影至特征空间
            MWArray val = ed.EulerDistance((MWNumericArray)Matrix.ConvertToDouble(predictionSample), (MWNumericArray)Matrix.ConvertToDouble(TrainSample));
            return ((double[,])val.ToArray())[0, 0];
        }
        #endregion
        #region 筛选股票
        /// <summary>
        /// 筛选股票
        /// </summary>
        /// <param name="ScreenData">筛选依据的数据</param>
        /// <param name="Num">返回前n个筛选结果</param>
        /// <returns></returns>
        public double[,] ScreenStock(double[,] ScreenData, int Num)
        {
            double[,] screenData = new double[ScreenData.GetLength(0), ScreenData.GetLength(1)];
            for (int i = 0; i < ScreenData.GetLength(1); i++)//给筛选数据按行大小排序
            {
                for (int j = 0; j < ScreenData.GetLength(0); j++)
                {
                    if (i == 0)
                    { screenData[j, i] = ScreenData[j, i]; }
                    else
                    {
                        int rank = 1;
                        for (int k = 0; k < ScreenData.GetLength(0); k++)
                        {
                            if (ScreenData[j, i] < ScreenData[k, i])
                            {
                                rank++;
                            }
                        }
                        screenData[j, i] = rank;
                    }
                }
            }
            double[] ranking = new double[Num];
            double[,] rankVal = new double[screenData.GetLength(0), 2];
            for (int i = 0; i < screenData.GetLength(0); i++)//量化指数
            {
                rankVal[i, 0] = screenData[i, 0];
                for (int j = 1; j < screenData.GetLength(1); j++)
                {
                    if (j < 4)
                    {
                        rankVal[i, 1] += 10 - ((screenData[i, j] / screenData.GetLength(0)) / 0.05);
                    }
                    if (j > 4)
                    {
                        rankVal[i, 1] += ((screenData[i, j] / screenData.GetLength(0)) / 0.05) - 10;
                    }
                }
            }
            for (int i = 0; i < rankVal.GetLength(0); i++)
            {
                int Rank = 1;
                for (int j = 0; j < rankVal.GetLength(0); j++)
                {
                    if (rankVal[i, 1] < rankVal[j, 1])
                        Rank++;
                }
                for (int k = 0; k < Num; k++)
                {
                    if (Rank == k + 1)
                        ranking[k] = i;
                }
            }
            double[,] result = new double[Num, screenData.GetLength(1)];
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = screenData[(int)ranking[i], j];
                }
            }
            return result;
        }
        #endregion
    }
}
