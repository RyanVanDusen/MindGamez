//Christopher Koester
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBehavior : MonoBehaviour
{
    [Range(0, 10)]
    public float speed = .5f;
    [Range(0, 1000)]
    public int rotationSpeed = 100;
    Rigidbody rb;
    private GameObject ovrCamRig;
    public GameObject OvrCamRig
    {
        get
        {
            if (ovrCamRig == null)
                ovrCamRig = GameObject.Find("OVRCameraRig");
            return ovrCamRig;
        }
    }
    [SerializeField]
    private Transform rotationAnchor;
    public Transform RotationAnchor
    {
        get
        {
            if (rotationAnchor == null)
                rotationAnchor = transform.Find("RightEyeAnchor");
            return rotationAnchor;
        }
    }

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
		RayCast ();
		Move ();
    }
	void RayCast()
	{//Raycasting in the direction that the player is facing
		Ray rayToCameraPos = new Ray(transform.position, Camera.main.transform.position - transform.position);
		Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
		RaycastHit hit;
		Debug.DrawRay(ray.origin, ray.direction, Color.green);
		//if (Input.GetButtonDown("Button A"))
		//{
		//	if (Physics.Raycast(ray.origin, ray.direction, out hit, 100))
			//{
			//}
		//}
	}

	void Move()
	{
		//move behavior
		//here we are moving the player based on the horizontal and vertical inputs
		float v = Input.GetAxis("Vertical");
		float h = Input.GetAxis("Horizontal");
//		float r = Input.GetAxis("Right_Y_Axis") * speed;

		transform.rotation = Quaternion.Euler(0, rotationAnchor.localEulerAngles.y, 0);
		//rb.velocity = ray.direction * (v * 5);
		float cameraYaw = rotationAnchor.rotation.eulerAngles.y;
		Vector3 velocity = new Vector3(h, 0, v);
		transform.position += (Quaternion.Euler(0, cameraYaw, 0) * velocity * speed * Time.deltaTime);
	}
}
