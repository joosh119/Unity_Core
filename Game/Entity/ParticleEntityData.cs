using UnityEngine;
[CreateAssetMenu(fileName = "New ParticleEntityData", menuName = "Particle Entity Data", order = 0)]
public class ParticleEntityData : ScriptableObject
{
    //Damage
    public ParticleSystem damageParticlesPrefab;
    public float damageCamShakeMagnitude;
    public float damageCamShakeTime;
    public float iTime;
    //Death
    public ParticleSystem deathParticlesPrefab;
    public float deathCamShakeMagnitude;
    public float deathCamShakeTime;
    public bool destroyOnDeath;
    public bool particlesMatchColor;

}
