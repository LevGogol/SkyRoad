using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Screens _screens;
    [SerializeField] private InputFacade _input;
    [SerializeField] private Audio _audio;
    [SerializeField] private Level _level;
    [SerializeField] private SaveData _saveData;
    [SerializeField] private Characters _characters;

    private ScoreStorage _scoreStorage;
    private MoneyStorage _moneyStorage;

    private ShopScreen _shop => _screens.Get<ShopScreen>();

    private void Awake()
    {
        Application.targetFrameRate = 60;

        _scoreStorage = new ScoreStorage(_saveData.MaxScore);
        _moneyStorage = new MoneyStorage(_saveData.MoneyCount);
    }

    private void Start()
    {
        for (int i = 0; i < _characters.Count; i++)
        {
            _characters.GetCharacter(i).IsBuy = _saveData.LoadCharacterIsBuy(i);
        }

        _shop.Initialize(_saveData.CurrentCharacter);

        _screens.Get<StartScreen>().Show();
        _screens.Get<StartScreen>().ChangeMoney(_moneyStorage.MoneyCount);
        _screens.Get<StartScreen>().ChangeMaxScore(_scoreStorage.MaxScore);
        _screens.Get<ShopScreen>().ChangeMoney(_moneyStorage.MoneyCount);

        _level.SetPlayerCharacter(_saveData.CurrentCharacter);
    }

    private void OnEnable()
    {
        _scoreStorage.MaxScoreChanged += SaveMaxScore;
        _moneyStorage.MoneyChanged += SaveMoney;
        _moneyStorage.MoneyChanged += _screens.Get<ShopScreen>().ChangeMoney;
        _level.Failed += OnFail;
        DOVirtual.DelayedCall(0.3f, () => _input._UIButtons.PlayDowned += EnableLevel);
        _input._UIButtons.MenuDowned += Restart;
        _input._UIButtons.ShopDowned += EnableShop;
        _input._UIButtons.ShopOffDowned += DisableShop;
        _input._UIButtons.PauseDowned += EnablePause;
        _input._UIButtons.PauseOffDowned += DisablePause;
        _input._UIButtons.BuyDowned += BuyDownded;
    }

    private void SaveMoney(int money)
    {
        _saveData.MoneyCount = money;
    }

    private void BuyDownded()
    {
        if(_moneyStorage.MoneyCount < _shop.BuyCost) 
            return;

        if (!_screens.Get<ShopScreen>().BuyRandom())
            return;
        
        _audio.PlayClipOneShot(TrackName.BuyCharacterInShop);
        _moneyStorage.MoneyCount -= _shop.BuyCost;
        _saveData.CurrentCharacter = _shop.SelectedCharacter;
        _saveData.SaveCharacterIsBuy(_shop.SelectedCharacter.ID);
    }

    private void SaveMaxScore(int score)
    {
        _saveData.MaxScore = score;
    }

    private void EnablePause()
    {
        Time.timeScale = 0; //TODO NO!!
        _screens.Get<PauseScreen>().Show();
    }

    private void DisablePause()
    {
        Time.timeScale = 1;
        _screens.Get<PauseScreen>().Hide();
    }

    private void EnableLevel()
    {
        _level.StartLevel(_input, _screens, _scoreStorage, _moneyStorage); //TODO to one argument
        _screens.Get<StartScreen>().Hide();
        _screens.Get<LevelScreen>().Show();
    }

    private void EnableShop()
    {
        _screens.Get<StartScreen>().Hide();
        _screens.Get<LevelScreen>().Hide();
        _screens.Get<ShopScreen>().Show();
    }

    private void DisableShop()
    {
        _screens.Get<StartScreen>().Show();
        _saveData.CurrentCharacter = _shop.SelectedCharacter;
        _screens.Get<ShopScreen>().Hide();
        _level.SetPlayerCharacter(_saveData.CurrentCharacter);
        _screens.Get<StartScreen>().ChangeMoney(_moneyStorage.MoneyCount);
        _screens.Get<StartScreen>().ChangeMaxScore(_scoreStorage.MaxScore);
        _screens.Get<ShopScreen>().ChangeMoney(_moneyStorage.MoneyCount);
    }

    private void OnFail()
    {
        _input.TouchDowned += Restart;
        _screens.Get<EndScreen>().Show();
    }

    private void Restart()
    {
        _input.TouchDowned -= Restart;
        Time.timeScale = 1; //TODO NO!!! NO!!! NO!!!
        ReleaseListeners();

        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        ReleaseListeners();
    }

    private void ReleaseListeners()
    {
        _scoreStorage.MaxScoreChanged -= SaveMaxScore;
        _moneyStorage.MoneyChanged -= SaveMoney;
        _moneyStorage.MoneyChanged -= _screens.Get<ShopScreen>().ChangeMoney;
        _level.Failed -= OnFail;
        _input._UIButtons.PlayDowned -= EnableLevel;
        _input._UIButtons.MenuDowned -= Restart;
        _input._UIButtons.ShopDowned -= EnableShop;
        _input._UIButtons.ShopOffDowned -= DisableShop;
        _input._UIButtons.PauseDowned -= EnablePause;
        _input._UIButtons.PauseOffDowned -= DisablePause;
        _input._UIButtons.BuyDowned -= BuyDownded;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            _moneyStorage.MoneyCount += 100;
        }
    }
#endif
}