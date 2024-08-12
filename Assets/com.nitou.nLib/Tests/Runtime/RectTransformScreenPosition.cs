using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using nitou;

public class RectTransformExtensionTests {
    private GameObject canvasObject;
    private Canvas canvas;
    private RectTransform rectTransform;

    [SetUp]
    public void SetUp() {
        // Canvasのセットアップ
        canvasObject = new GameObject("TestCanvas");
        canvas = canvasObject.AddComponent<Canvas>();
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();

        // RectTransformのセットアップ
        GameObject rectObject = new GameObject("TestRectTransform");
        rectTransform = rectObject.AddComponent<RectTransform>();
        rectTransform.SetParent(canvasObject.transform);
        rectTransform.anchoredPosition = new Vector2(100, 100);

        // サイズとアンカーの設定
        rectTransform.sizeDelta = new Vector2(200, 200);
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
    }

    [TearDown]
    public void TearDown() {
        // オブジェクトを破棄
        Object.DestroyImmediate(canvasObject);
    }


}
