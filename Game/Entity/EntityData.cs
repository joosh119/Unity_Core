using UnityEngine;

[CreateAssetMenu(fileName = "New EntityData", menuName = "Entity Data", order = 0)]
public class EntityData : ScriptableObject
{
    //Damage
    public int maxHealth;
    //Invincibility time
    public float iTime;
    //Death
    public bool destroyOnDeath;

}