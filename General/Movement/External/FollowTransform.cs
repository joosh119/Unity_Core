using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform transformToFollow;
    [SerializeField]private Rigidbody2D rb;


    [SerializeField]private float baseSpeed;
    [SerializeField]private float angularSpeedModifier;
    // Start is called before the first frame update

    public void SetVelocities(float v, float angularSpeedModifier){
        baseSpeed = v;
        this.angularSpeedModifier = angularSpeedModifier;
    }

    // Update is called once per frame
    void Update()
    {
        //Current pointing direction
        float currentAngle = transform.rotation.eulerAngles.z;

        //angle between this and the target
        Vector2 direction = transformToFollow.position - transform.position;
        float diffAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if(diffAngle > 360)
            diffAngle -= 360;


        //Get difference in angle
        float diff = JMath.GetShorterAngleDistance(currentAngle, diffAngle);
        
        //Set angular speed
        rb.angularVelocity = angularSpeedModifier * diff;




        //Set velocity
        currentAngle*=Mathf.Deg2Rad;
        Vector2 velocityDirection = new Vector2(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle));
        rb.velocity = baseSpeed * velocityDirection;
        

        
    }

}
