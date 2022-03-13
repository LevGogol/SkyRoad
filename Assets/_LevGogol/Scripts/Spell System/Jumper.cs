using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour, ISpell
{
    [SerializeField] private float _power;
    
    private Rigidbody2D playerRigidbody;
    
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
        playerRigidbody.AddForce(_power * 100 * new Vector2(playerRigidbody.velocity.x > 0 ? 1 : -1, 1));
    }
}