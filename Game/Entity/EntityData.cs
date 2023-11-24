using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New EntityData", menuName = "Entity Data", order = 0)]
public class EntityData : ScriptableObject
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
