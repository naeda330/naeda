using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;
using OpenCvSharp.Blob;

public class NewBehaviourScript2 : MonoBehaviour {

	// Use this for initialization
	void Start() {
        using (var video = new VideoCapture(0))
        {
            //保存先行列
            var frame = new Mat();
            var gray = new Mat();
            //保存
            while (Cv2.WaitKey(1) == -1)
            {
                //画像読み込み
                video.Read(frame);
                //グレースケール変換
                Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
                //二値化
                using (var binary = gray.Threshold(100, 255, ThresholdTypes.Otsu | ThresholdTypes.Binary))
                {
                    // ラベリング実行
                    CvBlobs blobs = new CvBlobs(binary);

                    // 入力画像と同じサイズ、チャネル数の画像を生成
                    using (var render = new Mat(frame.Rows, frame.Cols, MatType.CV_8UC3, 3))
                    using (var img = new Mat(frame.Rows, frame.Cols, MatType.CV_8UC3, 3))
                    {
                        // ラベリング結果の描画
                        blobs.RenderBlobs(frame, render);
                        CvBlob maxBlob = blobs.LargestBlob();
                        Debug.Log(maxBlob.Rect);
                        Debug.Log(maxBlob.Centroid);
                        Debug.Log(maxBlob.Area);

                        // 各blob（輪郭要素）の情報の取得
                        //foreach (KeyValuePair<int, CvBlob> item in blobs)
                        //{
                        //    int labelValue = item.Key;
                        //    CvBlob blob = item.Value;
                        //    // 外接矩形（矩形の左上の座標(x,y),幅と高さ）
                        //    Console.WriteLine("外接矩形は、{0}", blob.Rect);

                        //    // 面積
                        //    Console.WriteLine("面積は、{0}", blob.Area);
                        //    // 重心
                        //    Console.WriteLine("重心は、{0}", blob.Centroid);
                        //    Debug.Log(blob.Centroid);
                        //    // 角度
                        //    Console.WriteLine("角度は、{0}", blob.Angle());
                        //    // ラベルの数値
                        //    Console.WriteLine("ラベルは、{0}", blob.Label);
                        //    // 輪郭情報を得る（ここではキーのみで特に意味なし）
                        //    Console.WriteLine("輪郭は、{0}", blob.Contour);

                        //    // 輪郭情報を得る
                        //    CvContourChainCode cc = blob.Contour;
                        //    // 描画
                        //    cc.Render(img);

                        //    // 周囲長の取得と表示
                        //    double perimeter = cc.Perimeter();
                        //    Console.WriteLine("周囲長は、{0}", perimeter);
                        //    // スペース（特に意味なし）
                        //    Console.WriteLine("");
                        //}

                        // using (new Window("frame", frame))
                        //using (new Window("binary", binary))
                        using (new Window("render", render)) ;
                        //using (new Window("img", img))
                        // {
                        //     Cv2.WaitKey();
                        // }
                    }

                }
            }
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
}
