using System.Collections;
using UnityEngine;

public class CameraFacade : MonoBehaviour {
    [SerializeField] private float _speed;

    private Player _player;
    private bool _isShake;

    public void Init(Player player)
    {
        _player = player;
    }
    
    public void Shake() {
        StartCoroutine(ShakeCoroutune());
    }

    IEnumerator ShakeCoroutune() {
        _isShake = true;
        yield return new WaitForSeconds(0.1f);
        _isShake = false;
    }
    
    void Update() {
        var targetPos = new Vector3(_player.transform.position.x, _player.transform.position.y - 3, -10);
        transform.position = Vector3.Lerp(transform.position, targetPos, _speed);
        
        if (_isShake) {
            transform.position += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
        }
    }
}
