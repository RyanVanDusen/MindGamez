using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed = 3.0f;
	
	void Update ()
    {
        //Rotates the object on the Y Axis
        transform.Rotate(0, speed, 0);
    }
}
