using UnityEngine;

public class ParticleOnTrigger2D : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(_particle, transform.position, _particle.transform.rotation);
    }
}
