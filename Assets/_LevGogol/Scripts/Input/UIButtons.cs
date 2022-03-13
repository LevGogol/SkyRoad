using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour
{
    public UnityAction OnShopButton;
    public UnityAction OnPlayButton;//todo make event

    [SerializeField] private Button _playButton;
    [SerializeField] private Button _shopButton;

    private void Start()
    {
        _playButton.onClick.AddListener(() => OnPlayButton.Invoke()); //TODO to late. Make onTouchDown
        _shopButton.onClick.AddListener(() => OnShopButton.Invoke());
    }
}