using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousDamageCollider : MonoBehaviour
{
    [SerializeField]private int damage;
    [SerializeField]private float damageDelay;
    [SerializeField]private GameObject particlesPrefab;

    private List<Collider2D> currentCollisions;
    private List<float> currentCollisionsTimeOfLastDamage;

    void Awake()
    {
        currentCollisions = new List<Collider2D>();
        currentCollisionsTimeOfLastDamage = new List<float>();
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        Collider2D other = collision2D.collider;
        if(other.CompareTag("Entity")){
            //Entity entity = other.GetComponentInParent<Entity>();
            
            currentCollisions.Add(other);
            currentCollisionsTimeOfLastDamage.Add(0);
            DealDamage(currentCollisions.Count-1);
        }
    }

    void OnCollisionStay2D(Collision2D collision2D)
    {
        Collider2D other = collision2D.collider;
        if(other.CompareTag("Entity")){
            
            int index = currentCollisions.IndexOf(other);
            if(index == -1)
                return;

            if(Time.time - currentCollisionsTimeOfLastDamage[index] >  damageDelay){
                DealDamage(index);
            }
        }
        
    }


    void OnCollisionExit2D(Collision2D collision2D)
    {
        Collider2D other = collision2D.collider;
        if(other.CompareTag("Entity")){
            //Entity entity = other.GetComponentInParent<Entity>();
            
            int index = currentCollisions.IndexOf(other);
            if(index == -1)
                return;
            currentCollisions.RemoveAt(index);

            currentCollisionsTimeOfLastDamage.RemoveAt(index);

           //Debug.Log("destroy: " + index);
        }
        

    }


    private void DealDamage(int index){
        Collider2D other = currentCollisions[index];
        currentCollisionsTimeOfLastDamage[index] = Time.time;

        Entity entity = other.GetComponent<Entity>();
        entity.Damage(damage);
        
        if(particlesPrefab != null){
            GameObject particles = Instantiate(particlesPrefab, transform.position, Quaternion.identity);
            Destroy(particles, 2);
        }
    }
}
