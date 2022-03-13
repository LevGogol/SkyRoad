using UnityEngine;

[RequireComponent(typeof(Cloud))]
public class DamageCloud : MonoBehaviour
{
    [SerializeField] private ParticleSystem _sparksPrefab;

    private Cloud _cloud;

    public Cloud Cloud => _cloud;

    private void Awake()
    {
        _cloud = GetComponent<Cloud>();
    }

    public void PlayParticles(Vector3 playerPosition)
    {
        Instantiate(_sparksPrefab, playerPosition, _sparksPrefab.transform.rotation);
    }
}
