#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using CustomButton;
[ExecuteAlways]
public class HotkeyTools : MonoBehaviour
{
    public Event @event;
    [System.Serializable] public enum DropDown { MoveSelectedObjectToMousePosition , CreateObjectAtMousePosition}
    [SerializeField] public DropDown Tool;
    
    private List<int> SelectedObjsLayerIndex = new List<int>();
    public static bool IsEnabled = true;

    //custom
    [HideInInspector] public bool Rotate;
    [HideInInspector] public bool UseScale;
    [HideInInspector] public GameObject ObjectToSpawn;
    void OnGUI()
    {
        @event = Event.current;
    }
    public void Update()
    {
        if (Application.isPlaying)
        {
            Destroy(this);
        }
        if (@event == null)
        {
            return;
        }
        if (@event.shift && @event.button == 1 && IsEnabled)
        {
            switch (Tool)
            {
                case DropDown.MoveSelectedObjectToMousePosition:
                    if (Selection.count > 0)
                    {
                        Vector3 mousePosition = @event.mousePosition;
                        mousePosition.x /= SceneView.lastActiveSceneView.position.width; //scale to size
                        mousePosition.y /= SceneView.lastActiveSceneView.position.height;
                        mousePosition.z = 1; //set Z to a sensible non-zero value so the raycast goes in the right direction
                        mousePosition.y = 1 - mousePosition.y; //invert Y because UIs are top-down and cameras are bottom-up
                        Ray ray = SceneView.lastActiveSceneView.camera.ViewportPointToRay(mousePosition);
                        RaycastHit hit;
                        for (int i = 0; i < SelectedObjsLayerIndex.Count; i++)
                        {
                            SelectedObjsLayerIndex.RemoveAt(i);
                        }
                        for (int i = 0; i < Selection.gameObjects.Length; i++)
                        {
                            SelectedObjsLayerIndex.Add(Selection.gameObjects[i].layer);
                            Selection.gameObjects[i].layer = 2;
                        }
                        if (Physics.Raycast(SceneView.lastActiveSceneView.camera.transform.position, ray.direction, out hit, 10000))
                        {
                            for (int i = 0; i < Selection.gameObjects.Length; i++)
                            {
                                Undo.RecordObject(Selection.gameObjects[i].transform, "Move Selection Objects");
                                
                                if (UseScale)
                                {
                                    Selection.gameObjects[i].transform.position = hit.point + new Vector3(0, Selection.gameObjects[i].transform.localScale.y / 2, 0);
                                }
                                else
                                {
                                    Selection.gameObjects[i].transform.position = hit.point;
                                }
                                if (Rotate)
                                {
                                    Vector3 normal = hit.normal;
                                    Vector3 fwd = Vector3.Cross(normal, Vector3.right).normalized;
                                    fwd = Quaternion.AngleAxis(Random.Range(0, 360), normal) * fwd;
                                    Selection.gameObjects[i].transform.rotation = Quaternion.LookRotation(fwd, normal);
                                }
                                else
                                {
                                    Selection.gameObjects[i].transform.rotation = Quaternion.identity;
                                }    
                            }
                        }
                        else
                        {
                            Debug.LogWarning("The place for the object was not found, you may have clicked into the void, or on the selected object");
                        }
                        for (int i = 0; i < Selection.gameObjects.Length; i++)
                        {
                            Selection.gameObjects[i].layer = SelectedObjsLayerIndex[i];
                        }
                    }
                    else
                    {
                        Debug.LogWarning("No objects selected");
                    }
                    break;
                case DropDown.CreateObjectAtMousePosition:
                    Vector3 mousePosition2 = @event.mousePosition;
                    mousePosition2.x /= SceneView.lastActiveSceneView.position.width; //scale to size
                    mousePosition2.y /= SceneView.lastActiveSceneView.position.height;
                    mousePosition2.z = 1; //set Z to a sensible non-zero value so the raycast goes in the right direction
                    mousePosition2.y = 1 - mousePosition2.y; //invert Y because UIs are top-down and cameras are bottom-up
                    Ray ray2 = SceneView.lastActiveSceneView.camera.ViewportPointToRay(mousePosition2);
                    RaycastHit hit2;
                    if (Physics.Raycast(SceneView.lastActiveSceneView.camera.transform.position, ray2.direction, out hit2, 10000))
                    {
                        for (int i = 0; i < Selection.gameObjects.Length; i++)
                        {
                            GameObject NewObj = Instantiate(ObjectToSpawn, hit2.point, Quaternion.identity);
                            Undo.RegisterCreatedObjectUndo(NewObj, "Create child");

                            if (UseScale)
                            {
                                NewObj.transform.position = hit2.point + new Vector3(0, Selection.gameObjects[i].transform.localScale.y / 2, 0);
                            }
                            if (Rotate)
                            {
                                Vector3 normal = hit2.normal;
                                Vector3 fwd = Vector3.Cross(normal, Vector3.right).normalized;
                                fwd = Quaternion.AngleAxis(Random.Range(0, 360), normal) * fwd;
                                NewObj.transform.rotation = Quaternion.LookRotation(fwd, normal);
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("The place for the object was not found, you may have clicked into the void, or on the selected object");
                    }
                    break;
            }
        }
    }
    public void DebugThis()
    {
        Debug.Log("HotkeyTools");
    }
    [MenuItem("CustomTool/HotkeyTools/Find Hotkey Tools")]
    public static void FindHotkeyTools()
    {
        List<HotkeyTools> _hotKeyTools = new List<HotkeyTools>();
        for (int i = 0; i < FindObjectsOfType<HotkeyTools>().Length; i++) { _hotKeyTools.Add(FindObjectsOfType<HotkeyTools>()[i]); }
        if (_hotKeyTools.Count == 0) {
            Debug.LogError("The CustomTool could not be found");
        }
        if (_hotKeyTools.Count >= 2)
        {
            Debug.LogError("The CustomTool count must be less 2");
        }
    }
    [MenuItem("CustomTool/HotkeyTools/Enable or Disable")]
    public static void CamToEditorView()
    {
        IsEnabled = !IsEnabled;
        if (IsEnabled)
        {
            Debug.LogWarning("HotkeyTools is enabled");
        }
        else
        {
            Debug.LogWarning("HotkeyTools is disabled");
        }
    }
}
#endif
