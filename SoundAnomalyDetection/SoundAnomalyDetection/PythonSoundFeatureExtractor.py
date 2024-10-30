import librosa
import pandas as pd
import numpy as np
import os

# 讀取資料夾中的聲音檔案, 並將檔案的檔案名稱建立成字串陣列後傳回
def get_filenames_in_folder(folder_path):
    filenames = []
    for filename in os.listdir(folder_path):                        # 取得資料夾中的所有內容
        if os.path.isfile(os.path.join(folder_path, filename)):     # 如果是檔案
            filenames.append(filename)                              # 將檔案名稱加入到陣列
    return filenames

# 使用MFCC(Mel Frequency Cepstral Coefficients)演算法取得聲音的特徵向量
def extract_features(foldername, file_path):
    audio, sample_rate = librosa.load(f"{foldername}/{file_path}", sr=None) # 讀取聲音檔案
    mfccs = librosa.feature.mfcc(y=audio, sr=sample_rate, n_mfcc=13)    # 取得聲音的特徵
    mfccs_mean = np.mean(mfccs, axis=1)                                 # 計算平均值
    return mfccs_mean                                                   # 傳回平均值

# 取得NormalSounds資料夾中正常的聲音檔案, 將檔案名稱建立成字串陣列
folder_path = 'NormalSounds'  
normal_audio_files = get_filenames_in_folder(folder_path)
print(normal_audio_files)

# 取得正常聲音檔案的特徵向量
features = [extract_features(folder_path, f) for f in normal_audio_files]
dfNormal = pd.DataFrame(features)           # 將特徵向量建立成DataFrame  
dfNormal['Label'] = 0                       # 為DataFrame加入名稱為Label的欄位, 並將欄位內容值設定為0(正常)

# 取得AbnormalSounds資料夾中異常的聲音檔案, 將檔案名稱建立成字串陣列
folder_path = 'AbnormalSounds' 
abnormal_audio_files = get_filenames_in_folder(folder_path)
print(abnormal_audio_files)

# 取得異常聲音檔案的特徵向量
features = [extract_features(folder_path, f) for f in abnormal_audio_files]
dfAbnormal = pd.DataFrame(features)         # 將特徵向量建立成DataFrame 
dfAbnormal['Label'] = 1                     # 為DataFrame加入名稱為Label的欄位, 並將欄位內容值設定為1(異常)

df = pd.concat([dfNormal, dfAbnormal], ignore_index=True)   # 串連dfNormal, dfAbnormal兩個DataFrame
df.to_csv("audio_features.csv", index=False)                # 將DataFrame的內容寫成純文字文件供訓練使用