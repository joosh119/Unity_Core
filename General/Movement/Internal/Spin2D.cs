using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin2D : MonoBehaviour
{
    ///<summary>
    ///The angular speed the transform will rotate at
    ///</summary>
    public float spinSpeed;
    private Transform thisTransform;
    // Start is called before the first frame update
    void Start()
    {
        thisTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        float nextAngle = thisTransform.localRotation.eulerAngles.z + (spinSpeed * Time.deltaTime);
        thisTransform.localRotation = Quaternion.Euler(0, 0, nextAngle);
    }
}
