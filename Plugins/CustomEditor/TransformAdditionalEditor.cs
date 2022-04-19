using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(TransformAdditional))]
public class TransformAdditionalEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        TransformAdditional Target = (TransformAdditional)target;
        if (GUILayout.Button("ResetTransform"))
        {
            Target.ResetTransform();
        }

        GUILayout.Space(15);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("SetHeight");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("ByCentre"))
        {
            Target.SetHeightByCentre();
        }
        if (GUILayout.Button("ByScale"))
        {
            Target.SetHeightByScale();
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(15);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("SetHeightAndRotate");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("ByCentre"))
        {
            Target.SetHeightAndRotationByCentre();
        }
        if (GUILayout.Button("ByScale"))
        {
            Target.SetHeightAndRotationByScale();
        }
        GUILayout.EndHorizontal();
    }
}
