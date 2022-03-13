using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    public UnityAction OnFail;

    [SerializeField] private float _spawnBorderFromPlayer;
    [SerializeField] private float _destroyBorderFromPlayer;
    [SerializeField] private Characters _characters;
    [SerializeField] private Screens _screens;
    [SerializeField] private InputFacade _input;
    [SerializeField] private CameraFacade _cameraFacade;
    [SerializeField] private Tutorial _tutorial;
    [SerializeField] private CloudSpawner _cloudSpawner;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private Player _player;

    private void Start()
    {
        _player.Initialization(_characters.GetCurrentCharacter());
        _cameraFacade.Init(_player);
        _tutorial.Show(_player);
        _player.Shield.Enable();
        StartCoroutine(BoardCoroutine());
        _cloudSpawner.DisableInputReaction();
    }

    public void StartLevel()
    {
        _tutorial.Hide();
        _screens.Get<LevelScreen>().Show();
        
        if (_player.HasSpell<Jumper>())
            _input.SwipeUped += _player.GetSpell<Jumper>().Execute;

        if (_player.HasSpell<CloudDestroyer>())
            _input.SwipeDowned += _player.GetSpell<CloudDestroyer>().Execute;

        var collisionHandler = new CollisionHandler(_player, _soundManager);

        _player.Died += OnDead;
        _player.Damaged += _cameraFacade.Shake;
        
        _cloudSpawner.EnableInputReaction();
    }

    private IEnumerator BoardCoroutine()
    {
        while (true)
        {
            if (_player.transform.position.y + _spawnBorderFromPlayer < _cloudSpawner.DownOffset)
            {
                _cloudSpawner.CreateLine();
            }
            
            if (_player.transform.position.y + _destroyBorderFromPlayer < _cloudSpawner.UpOffset)
            {
                _cloudSpawner.RemoveLine();
            }
            
            if (_player.transform.position.x < -25 || _player.transform.position.x > 25)
            {
                _player.Life -= 100;
                yield break;
            }

            yield return null;
        }
    }

    private void OnDead()
    {
        _screens.Get<EndScreen>().Show();
        OnFail.Invoke();

        _player.Died -= OnDead;
    }

    private void OnDestroy()
    {
        _player.Damaged -= _cameraFacade.Shake;
    }
}