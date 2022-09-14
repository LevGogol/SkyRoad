using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour, ISpell
{
    [SerializeField] private float _power;
    
    private Rigidbody2D playerRigidbody;
    private LayerMask _layer;
    private float _distance = 0.2f;
    
    private void Awake()
    {
        _layer = LayerMask.GetMask("Clouds");
    }
    
    public float Power
    {
        set => _power = value;
    }

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }
    
    public void Execute()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _distance, _layer);
        
        if (hit.transform == null)
            return;
        
        playerRigidbody.AddForce(Vector2.up * _power, ForceMode2D.Impulse);
        Audio.Instance.PlayClipOneShot(TrackName.Jump);
    }
}