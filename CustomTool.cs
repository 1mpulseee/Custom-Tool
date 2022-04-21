using UnityEngine;
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
    [MenuItem("CustomTool/Cam to editor view ")]
    private static void CamToEditorView()
    {
        Transform cam = Camera.main.transform;
        Undo.RecordObject(cam, "OldCamPos");
        cam.position = SceneView.lastActiveSceneView.camera.transform.position;
        cam.rotation = SceneView.lastActiveSceneView.camera.transform.rotation;
    }
}