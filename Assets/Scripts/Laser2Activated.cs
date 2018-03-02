using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser2Activated : MonoBehaviour {

	public GameObject DC;
	public GameObject Laser1;
	public GameObject Laser2;

	void Update () {
		if (DC == null) {
			Laser1.SetActive (false);
			Laser2.SetActive (true);
		}
	}
}
