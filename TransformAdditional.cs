#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
public class TransformAdditional : MonoBehaviour
{
    public void ResetTransform()
    {
        Undo.RecordObject(transform, "ResetTransform");
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = new Vector3(1, 1, 1);
    }
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
    public void SetHeightAndRotationByCentre()
    {
        int layer = gameObject.layer;
        gameObject.layer = 2;
        if (Physics.Raycast(transform.position + Vector3.up * 100, Vector3.down, out var hit, 20000))
        {
            Undo.RecordObject(transform, "SetHeightAndRotation");
            transform.position = hit.point;

            Vector3 normal = hit.normal;

            Vector3 fwd = Vector3.Cross(normal, Vector3.right).normalized;
            fwd = Quaternion.AngleAxis(Random.Range(0, 360), normal) * fwd;
            transform.rotation = Quaternion.LookRotation(fwd, normal);
        }
        gameObject.layer = layer;
    }
    public void SetHeightAndRotationByScale()
    {
        int layer = gameObject.layer;
        gameObject.layer = 2;
        if (Physics.Raycast(transform.position + Vector3.up * 100, Vector3.down, out var hit, 20000))
        {
            Undo.RecordObject(transform, "SetHeightAndRotation");
            transform.position = hit.point + new Vector3(0, transform.localScale.y / 2, 0);

            Vector3 normal = hit.normal;

            Vector3 fwd = Vector3.Cross(normal, Vector3.right).normalized;
            fwd = Quaternion.AngleAxis(Random.Range(0, 360), normal) * fwd;
            transform.rotation = Quaternion.LookRotation(fwd, normal);
        }
        gameObject.layer = layer;
    }
}
#endif