#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class ColoredProjectView
{
    const string MenuPath = "NXLab/ColoredProjectView";
    const string PrefKey = "NXLab_ColoredProjectView_Enabled";

    [MenuItem(MenuPath)]
    static void ToggleEnabled()
    {
        bool isEnabled = !Menu.GetChecked(MenuPath);
        Menu.SetChecked(MenuPath, isEnabled);
        EditorPrefs.SetBool(PrefKey, isEnabled);
        EditorApplication.RepaintProjectWindow();
    }

    [InitializeOnLoadMethod]
    static void Initialize()
    {
        SetEvent();
        bool isEnabled = EditorPrefs.GetBool(PrefKey, true);  // 初期値をtrueに設定
        Menu.SetChecked(MenuPath, isEnabled);
    }

    static void SetEvent()
    {
        EditorApplication.projectWindowItemOnGUI += OnGUI;
    }

    static void OnGUI(string guid, Rect selectionRect)
    {
        if (!Menu.GetChecked(MenuPath))
        {
            return;
        }

        string assetPath = AssetDatabase.GUIDToAssetPath(guid);
        int depth = assetPath.Split('/').Length - 1;
        Color colorToUse = GetColorByDepth(depth);

        // アルファ値を設定して色を適用
        colorToUse.a = GetAlphaByDepth(depth);
        EditorGUI.DrawRect(selectionRect, colorToUse);
    }

    private static Color GetColorByDepth(int depth)
    {
        // 指定された階層に基づいて色を返す
        switch (depth)
        {
            case 1: return Color.red;
            case 2: return Color.green;
            case 3: return Color.blue;
            case 4: return Color.yellow;
            case 5: return Color.magenta;
            case 6: return Color.cyan;
            case 7: return Color.grey;
            case 8: return new Color(1f, 0.5f, 0f); // オレンジ
            case 9: return new Color(0.5f, 0f, 0.5f); // パープル
            case 10: return new Color(0f, 0.5f, 0f); // ダークグリーン
            case 11: return new Color(0.5f, 0.5f, 0f); // オリーブ
            default: return new Color(0.5f, 0.5f, 0.5f); // デフォルトの色
        }
    }

    private static float GetAlphaByDepth(int depth)
    {
        // 階層の深さに応じてアルファ値を補間する
        // 例：最深部でアルファ値を最も低くする
        const float maxDepth = 10f;  // この深さ以降はアルファ値が最小になる
        const float minAlpha = 0.01f; // アルファ値の最小値
        const float maxAlpha = 0.05f; // アルファ値の最大値

        // 階層の深さに基づいてアルファ値を計算する
        float alpha = maxAlpha - ((depth - 1) / maxDepth) * (maxAlpha - minAlpha);
        return Mathf.Clamp(alpha, minAlpha, maxAlpha);
    }
}
#endif

