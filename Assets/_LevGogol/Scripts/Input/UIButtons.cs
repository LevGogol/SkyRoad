using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButtons : MonoBehaviour //TODO May be make like Screens system? 
{
    public UnityAction ShopDowned; //todo make event (event Action)
    public event Action PlayDowned;
    public UnityAction ShopOffDowned;
    public UnityAction MenuDowned;
    public UnityAction PauseDowned;
    public UnityAction PauseOffDowned;
    public UnityAction BuyDowned;

    [SerializeField] private CustomButton _play;
    [SerializeField] private Button _shop;
    [SerializeField] private Button _shopOff;
    [SerializeField] private Button _menu;
    [SerializeField] private Button _pause;
    [SerializeField] private Button _pauseOff;
    [SerializeField] private Button _buy;

    private void Start()
    {
        _play.Downed += OnPlayDowned; //TODO to late. Make onTouchDown
        _shop.onClick.AddListener(ShopDowned.Invoke);
        _shopOff.onClick.AddListener(ShopOffDowned.Invoke);
        _menu.onClick.AddListener(MenuDowned.Invoke);
        _pause.onClick.AddListener(PauseDowned.Invoke);
        _pauseOff.onClick.AddListener(PauseOffDowned.Invoke);
        _buy.onClick.AddListener(BuyDowned.Invoke); //TODO DYR
    }

    private void OnDestroy()
    {
        _play.Downed -= OnPlayDowned;
    }

    private void OnPlayDowned()
    {
        PlayDowned?.Invoke();
    }
}