using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using CustomButton;

public class Transform_additional : MonoBehaviour
{
#if UNITY_EDITOR
    [EditorButton("ResetTransform")]
    public void ResetTransform()
    {
        Undo.RecordObject(transform, "SetHeight");
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = new Vector3(1, 1, 1);
    }
    [EditorButton("SetHeightByCentre")]
    public void SetHeightByCentre()
    {
        int layer = gameObject.layer;
        gameObject.layer = 2;
        if (Physics.Raycast(transform.position + Vector3.up * 100, Vector3.down, out var hit, 20000))
        {
            Undo.RecordObject(transform, "SetHeight");
            transform.position = hit.point;
            transform.rotation = Quaternion.identity;
        }
        gameObject.layer = layer;
    }
    [EditorButton("SetHeightByScale")]
    public void SetHeightByScale()
    {
        int layer = gameObject.layer;
        gameObject.layer = 2;
        if (Physics.Raycast(transform.position + Vector3.up * 100, Vector3.down, out var hit, 20000))
        {
            Undo.RecordObject(transform, "SetHeight");
            transform.position = hit.point + new Vector3(0, transform.localScale.y / 2, 0);
            transform.rotation = Quaternion.identity;
        }
        gameObject.layer = layer;
    }
    [EditorButton("SetHeightAndRotationByCentre")]
    public void SetHeightAndRotationByCentre()
    {
        int layer = gameObject.layer;
        gameObject.layer = 2;
        if (Physics.Raycast(transform.position + Vector3.up * 100, Vector3.down, out var hit, 20000))
        {
            Undo.RecordObject(transform, "SetHeight");
            transform.position = hit.point;

            Vector3 normal = hit.normal;

            Vector3 fwd = Vector3.Cross(normal, Vector3.right).normalized;
            fwd = Quaternion.AngleAxis(Random.Range(0, 360), normal) * fwd;
            transform.rotation = Quaternion.LookRotation(fwd, normal);
        }
        gameObject.layer = layer;
    }
    [EditorButton("SetHeightAndRotationByScale")]
    public void SetHeightAndRotationByScale()
    {
        int layer = gameObject.layer;
        gameObject.layer = 2;
        if (Physics.Raycast(transform.position + Vector3.up * 100, Vector3.down, out var hit, 20000))
        {
            Undo.RecordObject(transform, "SetHeight");
            transform.position = hit.point + new Vector3(0, transform.localScale.y / 2, 0);

            Vector3 normal = hit.normal;

            Vector3 fwd = Vector3.Cross(normal, Vector3.right).normalized;
            fwd = Quaternion.AngleAxis(Random.Range(0, 360), normal) * fwd;
            transform.rotation = Quaternion.LookRotation(fwd, normal);
        }
        gameObject.layer = layer;
    }
#endif
}
