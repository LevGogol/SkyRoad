using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Health _health;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private CloudDestroyer _cloudDestroyer;

    private Rigidbody2D _rigidbody2D;
    private PlayerCollisions _collisions;
    private Collider2D _lastCloud;
    private float _maxDepth = 3.9f; //todo refactor this

    public event Action<Coin> CoinCollected;
    public event Action CloudTouch;
    public event Action Damaged;
    public event Action Died;

    public void Initialization(Character character)
    {
        _spriteRenderer.sprite = character.Sprite;
        _health.Ended += Die;
        _health.Value = character.LifeCount;

        if (character.CanJump)
        {
            _jumper.enabled = true;
            _jumper.Power = character.JumpPower;
        }
        else
        {
            _jumper.enabled = false;
        }

        if (character.CanDestroyCloud)
        {
            _cloudDestroyer.enabled = true;
        }
        else
        {
            _cloudDestroyer.enabled = false;
        }
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public bool CanJump()
    {
        return _jumper.enabled;
    }

    public bool CanDestroyCloud()
    {
        return _cloudDestroyer.enabled;
    }

    public T GetSpell<T>() where T : ISpell
    {
        return GetComponent<T>();
    }

    public void TakeDamage()
    {
        _health.Value--;
        Damaged?.Invoke();
    }

    public void TakeLife()
    {
        _health.Value++;
    }

    public void ExtraDie()
    {
        _health.Value -= 99999;
    }

    private void OnCollisionEnter2D(Collision2D cloud)
    {
        if (cloud == null) return; //TODO why?
        if (cloud.collider == _lastCloud) return;

        if (cloud.transform.TryGetComponent<DamageCloud>(out var damageCloud))
        {
            TakeDamage();
            damageCloud.PlayParticles(transform.position);
        }
        else if (cloud.transform.GetComponent<Cloud>())
        {
            var position = cloud.transform.position;
            if (position.y < _maxDepth)
            {
                _maxDepth = position.y;
            }

            CloudTouch?.Invoke();
        }

        _lastCloud = cloud.collider;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            coin.gameObject.SetActive(false); //TODO NO! Make pool

            CoinCollected?.Invoke(coin);
        }
    }

    private void Die()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.freezeRotation = true; //todo check for delete

        Died?.Invoke();
    }
}