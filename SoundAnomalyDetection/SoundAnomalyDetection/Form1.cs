using Microsoft.ML;
using System;
using System.Diagnostics;
//using Microsoft.ML.Transforms;
//using Microsoft.ML.Transforms.Audio;

namespace SoundAnomalyDetection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {
            var mlContext = new MLContext();                                                // 建立MLContext類別的物件
            var dataPath = "audio_features.csv";                                            //  存放聲音特徵向量的檔案名稱
            IDataView data = mlContext.Data.LoadFromTextFile<AudioFeature>(
                path: dataPath, hasHeader: true, separatorChar: ',');           // 載入存放聲音特徵向量的檔案的內容

            // 指定特徵向量的內容為訓練資料(即Features)
            var pipeline = mlContext.Transforms.Concatenate("Features", nameof(AudioFeature.Features))
                .Append(mlContext.Transforms.NormalizeMinMax("Features"))   // 將特徵向量的內容值轉換成0~1之間的數字
                .Append(mlContext.AnomalyDetection.Trainers.RandomizedPca(
                    featureColumnName: "Features", rank: 2));                                       // 使用RandomizedPca演算法進行訓練

            var model = pipeline.Fit(data);                                                                     // 執行訓練

            var transformedData = model.Transform(data);                                    // 取得訓練結果
            var metrics = mlContext.AnomalyDetection.Evaluate(transformedData, labelColumnName: "Label");   // 評估訓練結果

            Trace.WriteLine($"Area Under ROC: {metrics.AreaUnderRocCurve}");         // 顯示Area Under the ROC Curve (AUC-ROC)值
            Trace.WriteLine($@"Detection Rate at False Positive Count: 
                                        {metrics.DetectionRateAtFalsePositiveCount}");                      // 顯示Detection Rate At False Positive Count值

            var predictionEngine = mlContext.Model.CreatePredictionEngine<AudioFeature, AnomalyPrediction>(model);  // 建立預測引擎
            // 使用訓練資料的最後一筆當做測試資料
            var newSample = new AudioFeature
            {
                Features = mlContext.Data.CreateEnumerable<AudioFeature>(data, reuseRowObject: false).Last().Features
            };
            var prediction = predictionEngine.Predict(newSample);                     // 執行預測
            Trace.WriteLine($"Is Anomaly? {prediction.Prediction}");                 // 顯示預測結果
        }
    }
}
