using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour {
    [SerializeField]
    private float healthPickUpAmount = 1;
  //  private Health hp;


    // Use this for initialization
    void Start()
    {
       // hp = gameObject.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HealthPickUp")
        {

            Debug.Log("Picked Up");
            Destroy(other.gameObject);
            //hp.Health =+ healthPickUpAmount ;

        }
    }
}
