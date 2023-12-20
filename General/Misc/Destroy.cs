using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [SerializeField] private float delayTime;


    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, delayTime);
    }
}
