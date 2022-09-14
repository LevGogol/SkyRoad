using UnityEngine;

public class CloudDestroyer : MonoBehaviour, ISpell
{
    private LayerMask _layer;
    private float _distance = 0.11f;
    
    private void Awake()
    {
        _layer = LayerMask.GetMask("Clouds");
    }

    public void Execute() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _distance, _layer);
        
        if (hit.transform == null)
            return;

        var cloud = hit.transform.GetComponentInParent<Cloud>();
        if (cloud != null)
        {
            cloud.DestroySoft();
        }
    }
}
