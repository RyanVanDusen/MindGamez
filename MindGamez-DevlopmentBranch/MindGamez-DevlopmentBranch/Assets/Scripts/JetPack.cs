using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour {

    private JetFuelPickUp localFuel;
    private new Vector3 jetPack;

   
    [SerializeField]
    private float upForce = 9.81f;
    [SerializeField]
    private float fuel = 100;
    

    public float Fuel
    {
        get
        {
            return fuel;
        }
        set
        {
            fuel = value;
        }
    }


    
	// Use this for initialization
	void Start () {
        localFuel = new JetFuelPickUp();
        jetPack = new Vector3(0, upForce * Time.deltaTime, 0);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(fuel);
        if (Input.GetKey(KeyCode.Space) && fuel > 0)
        {   
            this.transform.Translate(jetPack);
            fuel--;
           
        }
		
	}
}
