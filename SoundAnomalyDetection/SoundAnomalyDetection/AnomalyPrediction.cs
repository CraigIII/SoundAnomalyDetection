using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundAnomalyDetection
{
    // 描述預測結果的類別
    public class AnomalyPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }
    }
}
