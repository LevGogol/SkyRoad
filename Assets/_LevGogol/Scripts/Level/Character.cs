using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character", order = 1)]
public class Character : ScriptableObject
{
    public int ID;
    public Sprite Sprite;
    public int LifeCount;
    public bool CanJump;
    public float JumpPower;
    public bool CanDestroyCloud;
    public bool IsBuy;
}