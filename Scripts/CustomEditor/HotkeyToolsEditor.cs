using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(HotkeyTools))]

public class HotkeyToolsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Enable/Disable"))
        {
            HotkeyTools.CamToEditorView();
        }

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("usage: shift + right mouse button");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        base.OnInspectorGUI();
        HotkeyTools tool = (HotkeyTools)target;

        switch (tool.Tool)
        {
            case HotkeyTools.DropDown.MoveSelectedObjectToMousePosition:
                GUILayout.BeginHorizontal();
                tool.Rotate = EditorGUILayout.Toggle("Rotate", tool.Rotate);
                tool.UseScale = EditorGUILayout.Toggle("UseScale", tool.UseScale);
                GUILayout.EndHorizontal();
                break;
            case HotkeyTools.DropDown.CreateObjectAtMousePosition:
                GUILayout.BeginHorizontal();
                tool.Rotate = EditorGUILayout.Toggle("Rotate", tool.Rotate);
                tool.UseScale = EditorGUILayout.Toggle("UseScale", tool.UseScale);
                GUILayout.EndHorizontal();
                tool.ObjectToSpawn = (GameObject)EditorGUILayout.ObjectField("ObjectToSpawn", tool.ObjectToSpawn, typeof(GameObject), true);
                break;
        }
    }
}
