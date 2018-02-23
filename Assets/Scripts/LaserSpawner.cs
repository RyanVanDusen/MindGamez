using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    private GameObject reflectorCube;
    private bool isActive;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        RaycastHit laserHit;
        float distance;

        Vector3 up = transform.TransformDirection(Vector3.up);
        Debug.DrawRay(transform.position, up, Color.blue);

        if (Physics.Raycast(transform.position,(up), out laserHit))
        {
            distance = laserHit.distance;
            Debug.Log("DISTANCE: " + distance + " || " + "GAME OBJECT: " + laserHit.collider.gameObject.name);

            if (laserHit.transform.gameObject.tag == "Reflector Cube")
            {
                isActive = true;
            }
        }
	}
}
