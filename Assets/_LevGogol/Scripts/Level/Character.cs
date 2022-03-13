using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character", order = 1)]
public class Character : ScriptableObject
{
    [HideInInspector] public int ID;
    public Sprite Sprite;
    public int Price;
    public int LifeCount;
    public bool CanJump;
    public float JumpPower;
    public bool CanDestroyCloud;
    public bool IsBuy;
}