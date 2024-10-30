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
            var mlContext = new MLContext();                                                // �إ�MLContext���O������
            var dataPath = "audio_features.csv";                                            //  �s���n���S�x�V�q���ɮצW��
            IDataView data = mlContext.Data.LoadFromTextFile<AudioFeature>(
                path: dataPath, hasHeader: true, separatorChar: ',');           // ���J�s���n���S�x�V�q���ɮת����e

            // ���w�S�x�V�q�����e���V�m���(�YFeatures)
            var pipeline = mlContext.Transforms.Concatenate("Features", nameof(AudioFeature.Features))
                .Append(mlContext.Transforms.NormalizeMinMax("Features"))   // �N�S�x�V�q�����e���ഫ��0~1�������Ʀr
                .Append(mlContext.AnomalyDetection.Trainers.RandomizedPca(
                    featureColumnName: "Features", rank: 2));                                       // �ϥ�RandomizedPca�t��k�i��V�m

            var model = pipeline.Fit(data);                                                                     // ����V�m

            var transformedData = model.Transform(data);                                    // ���o�V�m���G
            var metrics = mlContext.AnomalyDetection.Evaluate(transformedData, labelColumnName: "Label");   // �����V�m���G

            Trace.WriteLine($"Area Under ROC: {metrics.AreaUnderRocCurve}");         // ���Area Under the ROC Curve (AUC-ROC)��
            Trace.WriteLine($@"Detection Rate at False Positive Count: 
                                        {metrics.DetectionRateAtFalsePositiveCount}");                      // ���Detection Rate At False Positive Count��

            var predictionEngine = mlContext.Model.CreatePredictionEngine<AudioFeature, AnomalyPrediction>(model);  // �إ߹w������
            // �ϥΰV�m��ƪ��̫�@�������ո��
            var newSample = new AudioFeature
            {
                Features = mlContext.Data.CreateEnumerable<AudioFeature>(data, reuseRowObject: false).Last().Features
            };
            var prediction = predictionEngine.Predict(newSample);                     // ����w��
            Trace.WriteLine($"Is Anomaly? {prediction.Prediction}");                 // ��ܹw�����G
        }
    }
}
