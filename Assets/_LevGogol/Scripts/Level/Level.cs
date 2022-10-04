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
    
    public event Action Started;
    public event Action Failed;

    public Player Player => _player;

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
        _tutorial.Hide();
        screens.Get<LevelScreen>().ChangeScore(_scoreStorage.Score);
        screens.Get<LevelScreen>().ChangeMoney(moneyStorage.MoneyCount);
        moneyStorage.MoneyChanged += screens.Get<LevelScreen>().ChangeMoney;

        if (_player.CanJump())
            input.SwipeUped += _player.GetSpell<Jumper>().Execute;

        if (_player.CanDestroyCloud())
            input.SwipeDowned += _player.GetSpell<CloudDestroyer>().Execute;

        _player.Damaged += PlayerOnDamaged;

        _player.CloudTouch += () =>
        {
            _scoreStorage.Score++;
            screens.Get<LevelScreen>().ChangeScore(_scoreStorage.Score);
            _audio.PlayClipOneShot(TrackName.Cloud);
        };
        _player.CoinCollected += (coin) =>
        {
            screens.Get<LevelScreen>().PlayCoinAnimation(coin.transform.position);
            _moneyStorage.MoneyCount++;
            _audio.PlayClipOneShot(TrackName.Coin);
        };


        _player.Died += OnDead;

        _cloudSpawner.EnableInputReaction();
        _cloudSpawner.RotateFirstCloud();
        
        Started?.Invoke();
    }

    private void PlayerOnDamaged()
    {
        _cameraFacade.Shake();
        _audio.PlayClipOneShot(TrackName.Damage);
    }


    private void OnDead()
    {
        if (_scoreStorage.Score > _scoreStorage.MaxScore)
        {
            _scoreStorage.MaxScore = _scoreStorage.Score;
        }

        Failed?.Invoke();

        _player.Died -= OnDead;
    }

    private void OnDestroy()
    {
        _player.Damaged -= PlayerOnDamaged;
    }
}