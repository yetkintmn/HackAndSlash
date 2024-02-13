using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UpgradeScriptable))]
public class ShowCharClassImageInspectorEditor : Editor
{
    private UpgradeScriptable _scriptable;
    private bool _isScriptableNull;
    private Sprite _sprite;

    private void OnEnable()
    {
        _scriptable = target as UpgradeScriptable;
        if (_scriptable == null) return;
        _isScriptableNull = _scriptable.icon == null;
        _sprite = _scriptable.icon;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (_isScriptableNull)
            return;

        var texture = AssetPreview.GetAssetPreview(_sprite);
        GUILayout.Label("", GUILayout.Height(80), GUILayout.Width(80));
        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
    }
}
