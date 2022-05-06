using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    //Public Variables
    public float rotationSpeed = 2.0f;
    //Private Variables
    //Variables End
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime * rotationSpeed, 0,0));
    }
}
