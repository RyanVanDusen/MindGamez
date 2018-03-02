using UnityEngine;

public class TurnCube : MonoBehaviour
{
    #region Variables
    [SerializeField] private float interactDist;

    Ray ray;
    RaycastHit hit;
    #endregion

    void Start ()
    {
        ray = new Ray(transform.position, transform.forward);
	}
	
	void Update ()
    {
        Interact();	
	}

    void Interact()
    {
        Debug.DrawRay(transform.position + new Vector3(0, 0, .6f), transform.forward * interactDist, Color.blue);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, interactDist))
        {
            Debug.Log(hit.transform.gameObject.name);

            if (hit.transform.gameObject.tag == "Reflector Cube" && Input.GetKeyDown(KeyCode.E))
            {
                hit.transform.localEulerAngles += new Vector3(0, 45, 0);
            }
        }
    }
}
