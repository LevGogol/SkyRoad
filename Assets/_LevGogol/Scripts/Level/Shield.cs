using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool IsEnable => gameObject.activeSelf;
    
    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
}