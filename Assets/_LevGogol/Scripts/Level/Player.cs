using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public Shield Shield;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private int _lifeCount;
    private Rigidbody2D _rigidbody2D;

    public UnityAction<Collision2D> Collisioned;
    public UnityAction Damaged;
    public UnityAction Died;

    public int Life
    {
        get { return _lifeCount; }
        set
        {
            _lifeCount = value;

            if (_lifeCount <= 0) 
                Die();
        }
    }

    public void Initialization(Character character)
    {
        _spriteRenderer.sprite = character.Sprite;

        if (character.CanJump)
        {
            var jumper = gameObject.AddComponent<Jumper>();
            jumper.Power = character.JumpPower;
        }
        if (character.CanDestroyCloud) gameObject.AddComponent<CloudDestroyer>();
        
        Life = character.LifeCount;
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        ScoreStorage.Count = 0;

    }

    public bool HasSpell<T>() where T : ISpell
    {
        return GetComponent<T>() != null;
    }

    public T GetSpell<T>() where T : ISpell
    {
        return GetComponent<T>();
    }

    public void TakeDamage()
    {
        if (Shield.IsEnable)
        {
            Shield.Disable();
        }
        else
        {
            Life--;
        }
        
        Damaged.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Collisioned?.Invoke(other);
    }

    private void Die()
    {
        if (ScoreStorage.Count > ScoreStorage.MaxCount)
        {
            ScoreStorage.MaxCount = ScoreStorage.Count;
        }

        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.freezeRotation = true; //todo check for delete

        Died.Invoke();
    }
}