using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(Collider2D))]
//[RequireComponent(typeof(SpriteRenderer))]
public class Entity : MonoBehaviour
{
    //Components
    public Rigidbody2D rb;
    public Collider2D entityCollider;


    //Modifiers
    [SerializeField]private EntityData entityData;
    [SerializeField]private int identifier;
    public int GetID(){
        return identifier;
    }


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



    [SerializeField]public UltEvents.UltEvent<Entity> _OnDamageEvent;
    public void Damage(int damage){

        if(Time.time - timeOfLastHit > entityData.iTime  &&  !invincible){
            timeOfLastHit = Time.time;
            
            
            currentHealth -= damage;
            if(currentHealth <= 0)
                currentHealth = 0;


            _OnDamageEvent.Invoke(this);


            if(currentHealth == 0)
                Death();
        }
    }
    
    [SerializeField]public UltEvents.UltEvent<Entity> _OnDeathEvent;
    public void Death(){

        if(entityData.destroyOnDeath)
            Destroy(gameObject);

        _OnDeathEvent.Invoke(this);
    }



    //do left, right, and then center
    static readonly int[] ORDER = {-1, 1, 0};
    private const float RAY_DISTANCE = .1f;
    public bool CheckIfOnGround(){
        
        float halfWdith = entityCollider.bounds.size.x/2;
        float yPos = transform.position.y - entityCollider.bounds.size.y/2;

        //Check each edge of the collider
        foreach(int i in ORDER){
            float xPos = transform.position.x + halfWdith*i;

            //get layers that this entity can collide with
            LayerMask collisionLayers = Physics2D.GetLayerCollisionMask(gameObject.layer);
            
            RaycastHit2D ray = Physics2D.Raycast(new Vector2(xPos, yPos), Vector2.down, RAY_DISTANCE, collisionLayers);

            if(ray.collider != null){
                return true;
            }
        }

        return false;
    }


    public float GetHealthRatio(){
        return ((float)currentHealth) / maxHealth;
    }

}
