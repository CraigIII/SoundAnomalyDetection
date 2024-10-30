using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundAnomalyDetection
{
    // 描述聲音特徵向量的類別
    public class AudioFeature
    {
        [LoadColumn(0, 12)]                     // 載入0~12, 共13個欄位的特徵向量的內容值
        [VectorType(13)]
        public float[] Features { get; set; }

        [LoadColumn(13)]                        // 載入Label檔案的內容值
        public float Label { get; set; }
    }
}
