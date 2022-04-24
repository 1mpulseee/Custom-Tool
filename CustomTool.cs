using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class CustomTool : EditorWindow
{
    [MenuItem("CustomTool/Delete player prefs")]
    public static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    [MenuItem("CustomTool/Take screenshot")]
    public static void TakeScreenshot()
    {
        string path = "Screenshots";

        Directory.CreateDirectory(path);

        int i = 0;
        while (File.Exists(path + "/" + i + ".png")) i++;

        ScreenCapture.CaptureScreenshot(path + "/" + i + ".png");
    }
    [MenuItem("CustomTool/Find objects with shader/standard")]
    private static void FindObjectsWithStandardShader()
    {
        bool found = false;
        foreach (MeshRenderer renderer in Object.FindObjectsOfType<MeshRenderer>())
        {
            foreach (Material material in renderer.sharedMaterials)
            {
                if (material.shader.name == "Standard")
                { 
                    Debug.Log(material.shader.name, renderer);
                    found = true;
                }
            }
        }

        if (!found) Debug.Log("Well done, no any Standard materials on current scene!");
    }
    [MenuItem("CustomTool/Find objects with shader/Mobile(Diffuse) ")]
    private static void FindObjectsWithMobileShader()
    {
        bool found = false;
        foreach (MeshRenderer renderer in Object.FindObjectsOfType<MeshRenderer>())
        {
            foreach (Material material in renderer.sharedMaterials)
            {
                if (material.shader.name == "Mobile/Diffuse")
                {
                    Debug.Log(material.shader.name, renderer);
                    found = true;
                }
            }
        }

        if (!found) Debug.Log("Well done, no any Mobile materials on current scene!");
    }
    [MenuItem("CustomTool/Cam to Editor`s view ")]
    private static void CamToEditorView()
    {
        Transform cam = Camera.main.transform;
        Undo.RecordObject(cam, "OldCamPos");
        cam.position = SceneView.lastActiveSceneView.camera.transform.position;
        cam.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
    }
    [MenuItem("CustomTool/Math/Get distance from selected objects")]
    private static void GetDistance()
    {
        if (Selection.count > 1)
        {
            List<Transform> transforms = new List<Transform>();

            for (int i = 0; i < Selection.count; i++) {
                transforms.Add(Selection.gameObjects[i].GetComponent<Transform>());
            }
            for (int i = 0; i < transforms.Count-1; i++)
            {
                if (i == transforms.Count - 1)
                {

                }
                else 
                {
                    float Distance = Vector3.Distance(transforms[i].position, transforms[i + 1].position);
                    Debug.Log("Distance from: " + transforms[i].gameObject.name + " to " + transforms[i+1].gameObject.name + " = " + Distance.ToString("0.000"));
                }
            }

        }
        else { Debug.LogError("Objects not selected or less than 2"); }
        
    }
    [MenuItem("CustomTool/Component/Add Mesh Collider (Convex)")]
    private static void AddMeshCollider()
    {
        if (Selection.count > 0)
        {
            
            foreach (var item in Selection.gameObjects)
            {
                if (item.GetComponent<MeshFilter>() != null)
                {
                    if (item.GetComponent<MeshCollider>() == null)
                    {
                        Undo.AddComponent(item.gameObject,typeof(MeshCollider));
                        //item.AddComponent<MeshCollider>();
                        item.GetComponent<MeshCollider>().convex = true;
                    }
                }
                else { Debug.LogError("GameObject: " + item.name + " don`t have Mesh Filter"); }                           
            }
        }
        else { Debug.LogError("Objects not selected"); }
    }
}