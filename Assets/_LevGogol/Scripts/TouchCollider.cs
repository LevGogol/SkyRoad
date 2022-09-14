using System;
using UnityEngine;

public class TouchCollider : MonoBehaviour
{
    public event Action TouchDowned;
    
    public void OnMouseDown()
    {
        TouchDowned?.Invoke();
    }
}
