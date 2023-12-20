using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElasticFollowTransform : MonoBehaviour
{
    [SerializeField]private float followSpeed;
    public Transform transformToFollow;
    private Transform thisTransform;

    void Awake()
    {
        thisTransform = transform;
    }
    // Update is called once per frame
    void Update()
    {
        if(transformToFollow != null){
            Vector2 delta = (transformToFollow.position - thisTransform.position) * followSpeed * Time.deltaTime;
            thisTransform.position += (Vector3)delta;
        }
    }
}
