using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private CameraFacade _cameraFacade;
    [SerializeField] private Tutorial _tutorial; //TODO hide to startScreen
    [SerializeField] private CloudSpawner _cloudSpawner;
    [SerializeField] private Player _player;
    [SerializeField] private Audio _audio;
    
    private ScoreStorage _scoreStorage;
    private MoneyStorage _moneyStorage;
    private BorderRules _borderRules;
    private Screens _screens;
    
    public event Action Failed;

    private void Start()
    {
        _cameraFacade.Init(_player);
        _borderRules = new BorderRules();
        StartCoroutine(_borderRules.BoardCoroutine(_player, _cloudSpawner));
        _cloudSpawner.DisableInputReaction();
        _audio.PlayMusic(TrackName.Background);
    }

    public void SetPlayerCharacter(Character character)
    {
        _player.Initialization(character);
        if (_player.CanJump())
        {
            _tutorial.ShowUp();
        }
        else if (_player.CanDestroyCloud())
        {
            _tutorial.ShowDown();
        }
        else
        {
            _tutorial.ShowDefault();
        }
    }

    public void StartLevel(InputFacade input, Screens screens, ScoreStorage scoreStorage, MoneyStorage moneyStorage)
    {
        _scoreStorage = scoreStorage;
        _moneyStorage = moneyStorage;
        _screens = screens;
        _tutorial.Hide();
        _screens.Get<LevelScreen>().SetScore(_scoreStorage.Score);
        _screens.Get<LevelScreen>().SetMoney(moneyStorage.MoneyCount);

        moneyStorage.MoneyChanged += _screens.Get<LevelScreen>().SetMoney;
        
        if (_player.CanJump())
            input.SwipeUped += _player.GetSpell<Jumper>().Execute;

        if (_player.CanDestroyCloud())
            input.SwipeDowned += _player.GetSpell<CloudDestroyer>().Execute;

        _player.Damaged += OnPlayerDamaged;
        _player.CloudTouch += OnPlayerCloudTouch;
        _player.CoinCollected += OnPlayerCoinCollected;
        _player.Health.Changed += OnPlayerHealthChanged;
        _player.Died += OnPlayerDead;
        
        OnPlayerHealthChanged(_player.Health.Value);

        _cloudSpawner.EnableInputReaction();
        _cloudSpawner.RotateFirstCloud();
    }

    private void OnPlayerCoinCollected(Coin coin)
    {
        _screens.Get<LevelScreen>().PlayCoinAnimation(coin.transform.position);
        _moneyStorage.MoneyCount++;
        _audio.PlayClipOneShot(TrackName.Coin);
    }

    private void OnPlayerCloudTouch()
    {
        _scoreStorage.Score++;
        _screens.Get<LevelScreen>().SetScore(_scoreStorage.Score);
        _audio.PlayClipOneShot(TrackName.Cloud);
    }

    private void OnPlayerHealthChanged(int count)
    {
        _screens.Get<LevelScreen>().SetHearts(count);
    }

    private void OnPlayerDamaged()
    {
        _cameraFacade.Shake();
        _audio.PlayClipOneShot(TrackName.Damage);
    }

    private void OnPlayerDead()
    {
        if (_scoreStorage.Score > _scoreStorage.MaxScore)
        {
            _scoreStorage.MaxScore = _scoreStorage.Score;
        }

        Failed?.Invoke();

        _player.Died -= OnPlayerDead;
    }

    private void OnDestroy()
    {
        _player.Damaged -= OnPlayerDamaged;
    }
}