using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TransformAdditional))]
public class TransformAdditionalEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical("box");
        
        GUI.backgroundColor = Color.white;
        TransformAdditional Target = (TransformAdditional)target;
        var style = new GUIStyle(GUI.skin.button);
        style.normal.textColor = new Color(Color.cyan.r,Color.cyan.g,Color.cyan.b, (170/255f));
        //var styleRes = new GUIStyle();
        //styleRes.normal.textColor = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, (170 / 255f));
        //styleRes.fixedWidth = ;
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("ResetTransform",style))
        {
            Target.ResetTransform();
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Label("SetHeight", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 16 });
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("ByCentre",style ))
        {
            Target.SetHeightByCentre();
        }
        
        if (GUILayout.Button("ByScale",style ))
        {
            Target.SetHeightByScale();
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.Label("SetHeightAndRotate", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 16 });

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("ByCentre", style))
        {
            Target.SetHeightAndRotationByCentre();
        }
        if (GUILayout.Button("ByScale", style))
        {
            Target.SetHeightAndRotationByScale();
        }

        GUILayout.EndHorizontal();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.EndVertical();
    }
}
