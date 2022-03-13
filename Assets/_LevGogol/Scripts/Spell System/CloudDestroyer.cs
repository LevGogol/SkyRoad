using UnityEngine;

public class CloudDestroyer : MonoBehaviour, ISpell
{
    [SerializeField] private LayerMask _layer;
    
    public void Execute() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.1f, _layer);

        if (hit.transform.TryGetComponent<Cloud>(out var cloud))
        {
            cloud.DestroySoft();
        }
    }
}
