using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkAI : MonoBehaviour
{
    [SerializeField]private Entity entity;

    [SerializeField]private float walkForce;
    [SerializeField]private Vector2 startDirection;
    [SerializeField]private float minSpeed;//minimum speed before switching directions
    
    private Vector2 currentDirection;
    private bool hasReachedMinimumSpeed;

    void Start()
    {
        currentDirection = startDirection;
    }

    // Update is called once per frame
    void Update()
    {
        entity.rb.AddForce(walkForce* Time.deltaTime * currentDirection);

        float vX = entity.rb.velocity.x;
        //if it has been stopped
        if(Mathf.Abs(vX) > minSpeed){
            hasReachedMinimumSpeed = true;
        }
        else if(Mathf.Abs(vX) < minSpeed && hasReachedMinimumSpeed){
            currentDirection *= -1;

            if(vX > 0)
                entity.spriteRenderer.flipX = true;
            else
                entity.spriteRenderer.flipX = false;


                
            hasReachedMinimumSpeed = false;
        }
    }
}
