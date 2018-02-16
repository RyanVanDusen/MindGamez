using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetFuelPickUp : MonoBehaviour {
    [SerializeField]
    private float fuelPickUpAmount = 100;
    private JetPack jetpack;
  

	// Use this for initialization
	void Start () {
        jetpack = gameObject.GetComponent<JetPack>();
	}
	
	// Update is called once per frame
	void Update () {

		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FuelPickUp")
        {

            Debug.Log("Picked Up");
            Destroy(other.gameObject);
            jetpack.Fuel = fuelPickUpAmount;
        
        }
    }
}
