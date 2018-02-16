using System.Collections;
using UnityEngine;
  
[RequireComponent (typeof (LineRenderer))]  
  
public class RaycastReflection : MonoBehaviour  
{
    #region Variables
    private LineRenderer lineRenderer;

    private Ray ray;
    private RaycastHit hit;  
  
    //Reflection Direction
    private Vector3 inDirection;  
  
    //Number of Reflections
    public int nReflections = 2;

    //Points on the Line Renderer
    private int nPoints;

    //Scripts
    PlayerHealth playerHealth;

    [SerializeField]
    private float burnTime;

    public Material burnt;
    #endregion

    #region Functions
    void Awake ()  
    {
        lineRenderer = GetComponent<LineRenderer>();
    }  
  
    void Update ()  
    {  
        //Clamp the number of Reflections between 1 and int capacity
        nReflections = Mathf.Clamp(nReflections, 1, nReflections);

        //Create a new Raycast going upwards
        ray = new Ray(transform.position, transform.up);
  
        //Draw the Initial Ray 
        Debug.DrawRay(transform.position, transform.up * 100, Color.magenta);  
  
        //Set the number of Vertex Points equal to the number of Reflections
        nPoints = nReflections;

        //Set Line Renderer to have the same number of Points as there are Vertex's
        lineRenderer.SetVertexCount(nPoints);
        
        //Set first Point at the Current Transform Position
        lineRenderer.SetPosition(0, transform.position);
  
        for(int i=0;i<=nReflections;i++)
        {
            //If the Ray has not been Reflected yet 
            if (i==0)  
            {  
                //Check if the Ray has hit anything 
                if(Physics.Raycast(ray.origin,ray.direction, out hit, 100))
                {  
                    if (hit.transform.CompareTag("Reflector Cube"))
                    {
                        //Create new Raycast for Angle Bisector and Reflected Angle
                        inDirection = Vector3.Reflect(ray.direction, hit.normal);
                        ray = new Ray(hit.point, inDirection);
                        
                        //Draw Angle Bisector and Reflected Angle
                        Debug.DrawRay(hit.point, hit.normal * 3, Color.blue);
                        Debug.DrawRay(hit.point, inDirection * 100, Color.magenta);

                        //Print Hit Object 
                        //Debug.Log("Object name: " + hit.transform.name);

                        //If there is one Reflection
                        if (nReflections == 1)
                        {
                            //Adds a new Vertex to the Line Renderer
                            lineRenderer.SetVertexCount(++nPoints);
                        }

                        //Set next Vertex position at hit.point location
                        lineRenderer.SetPosition(i + 1, hit.point);
                    }
                }  
            }  
            //If the Ray is reflected
            else
            {  
                if(Physics.Raycast(ray.origin,ray.direction, out hit, 100))
                {
                    //Create new Raycast for Angle Bisector and Reflected Angle
                    inDirection = Vector3.Reflect(inDirection,hit.normal);
                    ray = new Ray(hit.point,inDirection);

                    //Draw Angle Bisector and Reflected Angle
                    Debug.DrawRay(hit.point, hit.normal*3, Color.blue);  
                    Debug.DrawRay(hit.point, inDirection*100, Color.magenta);  
  
                    //Print Hit Object
                    //Debug.Log("Object name: " + hit.transform.name);  
  
                    //Add a new Vertex to the Line Renderer 
                    lineRenderer.SetVertexCount(++nPoints);

                    //Set next Vertex position at hit.point location
                    lineRenderer.SetPosition(i+1,hit.point);

                    //Checks to see if the Laser hits the Player
                    if (hit.transform.CompareTag("Player"))
                    {
                        hit.transform.gameObject.GetComponent<PlayerHealth>().TakeDamage();
                    }

                    if (hit.transform.gameObject.tag == "Destructible")
                    {
                        StartCoroutine(BurnObject(hit.transform.gameObject));
                    }
                }
            }  
        }
    }  

    IEnumerator BurnObject(GameObject go)
    {
        yield return new WaitForSeconds(burnTime);
        go.GetComponent<Renderer>().material = burnt;
        yield return new WaitForSeconds(burnTime);
        Destroy(go);
    }
    #endregion
}  
