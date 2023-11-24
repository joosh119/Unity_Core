using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomJumping : MonoBehaviour
{
    [SerializeField]private bool ignoreTimeScale;
    [SerializeField]private float timeBetweenJumps;
    /// <summary>
    /// Random deviation from timeBetweenJumps
    /// </summary>
    [SerializeField]private float timeDeviation;
    [SerializeField]private float radius;

    
    private Transform thisTransform;
    private Vector2 centerPosition;
    private float timeToNextJump;
    
    void Start()
    {
        thisTransform = transform;
        centerPosition = thisTransform.localPosition;
        float time = timeBetweenJumps + timeDeviation*JMath.Random.RandomSigned();
        timeToNextJump = time;
    }
    
    // Update is called once per frame
    void Update()
    {
        float currentTime;
        if(ignoreTimeScale)
             currentTime = Time.unscaledTime;
        else
            currentTime = Time.time;


        if(currentTime >= timeToNextJump){
            float time = timeBetweenJumps + timeDeviation*JMath.Random.RandomSigned();
            timeToNextJump = currentTime + time;


            Vector2 nextPosition = centerPosition + radius*Random.insideUnitCircle;
            thisTransform.localPosition = nextPosition;
        }
    }
}
