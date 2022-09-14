using System.Collections;
using UnityEngine;

public class CameraFacade : MonoBehaviour {
    
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _shakeMagnitude;
    [SerializeField] private  float _shakeDuration = 0.1f;
    [SerializeField] private Camera _camera;

    private Player _player;
    private bool _isShake;

    public Camera Camera => _camera; 
    
    public void Init(Player player)
    {
        _player = player;
    }
    
    public void Shake() {
        StartCoroutine(ShakeCoroutune());
    }

    IEnumerator ShakeCoroutune() {
        _isShake = true;
        yield return new WaitForSeconds(_shakeDuration);
        _isShake = false;
    }
    
    void LateUpdate() {
        var targetPos = new Vector3(_player.transform.position.x + _offset.x, _player.transform.position.y + _offset.y, _offset.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, _speed * Time.deltaTime);
        
        if (_isShake) {
            transform.position += new Vector3(Random.Range(-_shakeMagnitude, _shakeMagnitude), Random.Range(-_shakeMagnitude, _shakeMagnitude));
        }
    }
}
