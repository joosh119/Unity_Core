using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Entity : MonoBehaviour
{
    //EDITOR VARIABLES
    [SerializeField]private EntityData entityData;
    [SerializeField]private int identifier;
    public int _identifier { get{ return identifier; }}

    public bool invincible;

    //COMPONENTS
    private Rigidbody2D rb;
    private Collider2D entityCollider;

    //PRIVATE VARIABLES
    private float timeOfLastHit;
    public int currentHealth;

    


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        entityCollider = GetComponent<Collider2D>();

        currentHealth = entityData.maxHealth;
    }



    [SerializeField]public UltEvents.UltEvent<int> _OnDamageEvent;
    public void Damage(int damage){

        if(Time.time - timeOfLastHit > entityData.iTime  &&  !invincible){
            timeOfLastHit = Time.time;
            
            
            currentHealth -= damage;
            if(currentHealth <= 0)
                currentHealth = 0;


            _OnDamageEvent.Invoke(damage);


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


    public void DestroySelf(){
        Destroy(gameObject);
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
        return ((float)currentHealth) / entityData.maxHealth;
    }

}
