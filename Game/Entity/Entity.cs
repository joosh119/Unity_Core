using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(Collider2D))]
//[RequireComponent(typeof(SpriteRenderer))]
public class Entity : MonoBehaviour
{
    //Components
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public Collider2D entityCollider;


    //Modifiers
    [SerializeField]private EntityData entityData;
    [SerializeField]private int identifier;
    public int GetID(){
        return identifier;
    }
    ///<summary>
    ///Whether or not to destroy the root object when the entity is killed
    ///</summary>
    public bool destroyRootOnDeath;
    public bool invincible;
    public int maxHealth;





    //private variables
    private float timeOfLastHit;
    public int currentHealth;

    


    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //rb = GetComponent<Rigidbody2D>();
        //entityCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    [SerializeField]public UltEvents.UltEvent<Entity> _OnDamageEvent;
    public void Damage(int damage){
        if(Time.time - timeOfLastHit > entityData.iTime  &&  !invincible){
            timeOfLastHit = Time.time;
            
            //Particles
            if(entityData.damageParticlesPrefab != null){
                ParticleSystem damageParticles = Instantiate(entityData.damageParticlesPrefab);
                Transform pTransform = damageParticles.transform;
                pTransform.SetParent(transform);
                pTransform.localPosition = Vector2.zero;
                pTransform.localScale = Vector2.one;
                Destroy(damageParticles.gameObject, 2);


                if(entityData.particlesMatchColor){
                    var main = damageParticles.main;
                    main.startColor = JMath.AverageColor(spriteRenderer.sprite);
                }
            }
            
            
            currentHealth -= damage;
            if(currentHealth <= 0)
                currentHealth = 0;



            //Shake
            GameHelper.Shake2(Camera.main.transform, entityData.damageCamShakeTime, entityData.damageCamShakeMagnitude, Vector2.zero);

            _OnDamageEvent.Invoke(this);




            if(currentHealth == 0)
                Death();

            
            
        }
    }
    [SerializeField]public UltEvents.UltEvent<Entity> _OnDeathEvent;
    public void Death(){

        //Particles
        if(entityData.deathParticlesPrefab != null){
            ParticleSystem deathParticles = Instantiate(entityData.deathParticlesPrefab);
            deathParticles.transform.localPosition = transform.position;
            Destroy(deathParticles.gameObject, 2);


            if(entityData.particlesMatchColor){
                var main = deathParticles.main;
                Sprite sprite = spriteRenderer.sprite;
                ParticleSystem.MinMaxGradient g = new ParticleSystem.MinMaxGradient(JMath.Random.RandomColorOnSprite(sprite, .2f),JMath.Random.RandomColorOnSprite(sprite, .2f));
                main.startColor = g;//JMath.AverageColor(spriteRenderer.sprite);
            }
        }


        if(entityData.destroyOnDeath)
            if(destroyRootOnDeath)
                Destroy(transform.root.gameObject);
            else
                Destroy(gameObject);


        //Shake
        GameHelper.Shake2(Camera.main.transform, entityData.deathCamShakeTime, entityData.deathCamShakeMagnitude, Vector2.zero);

        _OnDeathEvent.Invoke(this);
    }



    //do left, right, and then center
    static readonly int[] ORDER = {-1, 1, 0};
    public static LayerMask DEFAULT_LAYER = 1<<0;
    public static LayerMask SEMISOLID_LAYER = 256;
    public static LayerMask TERRAIN_LAYER = 257;
    private const float RAY_DISTANCE = .1f;
    public bool CheckIfOnGround(){
        
        float halfWdith = entityCollider.bounds.size.x/2;
        float yPos = transform.position.y - entityCollider.bounds.size.y/2;

        //Check each edge of the collider
        foreach(int i in ORDER){
            float xPos = transform.position.x + halfWdith*i;

            RaycastHit2D ray = Physics2D.Raycast(new Vector2(xPos, yPos), Vector2.down, RAY_DISTANCE, TERRAIN_LAYER);

            if(ray.collider != null)
                return true;
        }

        return false;
    }


    public float GetHealthRatio(){
        return ((float)currentHealth) / maxHealth;
    }

}
