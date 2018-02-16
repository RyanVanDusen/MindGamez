using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour {

	[SerializeField]
	Transform target;

	[SerializeField]
	float minClamp;
	[SerializeField]
	float maxClamp;
	[SerializeField]
	float rotationSpeed;

	float curValue;
	float min;
	float max;
	bool turningRight = true;

	void Start(){
		curValue = transform.localRotation.y;
		min = curValue - minClamp;
		max = curValue + minClamp;

		print (curValue);
	}
		

	// Update is called once per frame
	void Update () {
		Turning ();
	}

	public void Turning()
	{
		curValue += turningRight ? +rotationSpeed*Time.deltaTime : -rotationSpeed*Time.deltaTime;
		transform.localRotation = Quaternion.Euler (0, curValue, 0);

			if (curValue >= max) {
				curValue = max;
				turningRight = false;
			}
			else if (curValue <= min) {
				curValue = min;
				turningRight = true;
			}
		print (curValue);
	}
}


