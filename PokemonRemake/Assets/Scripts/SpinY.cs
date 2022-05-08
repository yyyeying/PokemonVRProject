using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinY : MonoBehaviour
{
    //Public Variables
    private float rotationSpeed = 20.0f;
    //Private Variables
    //Variables End
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * rotationSpeed, 0));
    }
}
