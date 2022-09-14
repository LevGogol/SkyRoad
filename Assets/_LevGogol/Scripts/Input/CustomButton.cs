using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour , IPointerDownHandler
{
    [SerializeField] private Button _button;

    public Button Button => _button;
    
    public event Action Downed;

    public void OnPointerDown(PointerEventData eventData)
    {
        Downed?.Invoke();
    }
}