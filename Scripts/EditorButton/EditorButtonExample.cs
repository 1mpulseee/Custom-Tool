using UnityEngine;
using CustomButton;
public class EditorButtonExample : MonoBehaviour
{
    [EditorButton("Button")]
    public void SendMsg()
    {
        Debug.Log("Click");
    }
}
