using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform turretBarrel;
	TurretAI turretAI;
	float lastShot;
    private Ray ray;
    private RaycastHit hit;
	bool canFire = true;
	[SerializeField]
	AudioController ac;
    #endregion

    void Start ()
    {
		turretAI = gameObject.GetComponent<TurretAI>();
	}
	
	public void Fire()
    {
		if (canFire && Time.time > lastShot)
		{
			if (turretAI.weapons.weaponType == TurretAI.Weapons.WeaponType.Normal)
				Ray ();
			else if (turretAI.weapons.weaponType == TurretAI.Weapons.WeaponType.ShotgunBlast)
				for (int i = 0; i < 8; i++)
					Ray ();
			else
				StartCoroutine (Burst ());
			
		}
	}

	void Ray()
	{
		//playing the sound
		ac.Play();
		lastShot = Time.time + turretAI.weapons.fireRate;
		float x = turretAI.weapons.accuracy;
		Quaternion rot = Quaternion.Euler (turretBarrel.localRotation.x + Random.Range(-x,+x),
			turretBarrel.localRotation.y + Random.Range(-x,+x), 0);
		turretBarrel.transform.localRotation= rot;  
		ray = new Ray(turretBarrel.position, turretBarrel.forward);

		Debug.DrawRay(ray.origin, ray.direction, Color.blue);

		if (Physics.Raycast(ray.origin, ray.direction, out hit, turretAI.weapons.range))
		{
			if (hit.transform.gameObject.tag == "Player")
			{
				hit.transform.gameObject.GetComponent<PlayerHealth>().Health -= turretAI.weapons.damage;
			}
			Debug.DrawLine(ray.origin, ray.direction * turretAI.weapons.range, Color.yellow);
		}
	}
	IEnumerator Burst()
	{
		canFire = false;
		for (int i = 0; i < 3; i++) 
		{
			yield return new WaitForSeconds (.08f);
			Ray();
		}
		canFire = true;
	}
}
