using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public bool LockInput;
    
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Collider2D _collider2D;
    [SerializeField] private TouchCollider _touchCollider;

    private float _rotateSpeed = 0.225f;
    private bool _isFirstTouch = true;
    private bool _isLeft;
    private Coroutine _rotateCoroutine;

    public SpriteRenderer Sprite => _sprite;
    public float SizeX() => _collider2D.bounds.size.x;

    private void Start()
    {
        _isFirstTouch = true;
        transform.rotation = Quaternion.identity;
    }

    private void OnEnable()
    {
        _touchCollider.TouchDowned += Rotate;
    }

    public void Flip()
    {
        _sprite.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void Rotate()
    {
        if(LockInput) return;
        
        var touch = Camera.main.ScreenToWorldPoint(Input.mousePosition); //TODO refactor

        if (_rotateCoroutine == null)
        {
            if (_isFirstTouch)
            {
                _isLeft = touch.x > transform.position.x;
                _isFirstTouch = false;
            }
            else
            {
                _isLeft = !_isLeft;
            }
            
            _rotateCoroutine = StartCoroutine(RotateCoroutine(_isLeft));
        }
    }

    public void DestroySoft()
    {
        _collider2D.enabled = false;
        _sprite.DOFade(0f, 0.5f);

        var audio = FindObjectOfType<Audio>(); //TODO refactor
        audio.PlayClipOneShot(TrackName.DestroyCloud);
    }

    private IEnumerator RotateCoroutine(bool left)
    {
        var sign = left ? -1 : 1;
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(new Vector3(0, 0, sign * 30));
        for (var t = 0f; t < 1; t += Time.deltaTime / _rotateSpeed)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            _touchCollider.transform.rotation = Quaternion.identity;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 30 * sign));
        _touchCollider.transform.rotation = Quaternion.identity;

        _rotateCoroutine = null;
    }

    private void OnDisable()
    {
        _touchCollider.TouchDowned -= Rotate;
    }
}