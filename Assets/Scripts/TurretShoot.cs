using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform turretBarrel;
    [SerializeField] private float turretDamage;
    [SerializeField] private float turretRange;
    [SerializeField] private float fireRate;
    [SerializeField] private float spinTime;

    private Ray ray;
    private RaycastHit hit;
    #endregion

    #region Functions
    void Start ()
    {

	}
	
	void Update ()
    {
        ray = new Ray(turretBarrel.position, turretBarrel.forward);

        Debug.DrawRay(ray.origin, ray.direction, Color.blue);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, turretRange))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                StartCoroutine(Shoot(hit.transform.gameObject));
            }
        }
	}

    IEnumerator Shoot(GameObject _player)
    {
        yield return new WaitForSeconds(spinTime);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, turretRange))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                _player.GetComponent<PlayerHealth>().Health -= turretDamage / 60;
            }
        }
    }
    #endregion
}
