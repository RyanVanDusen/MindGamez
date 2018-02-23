using System.Collections;
using UnityEngine;
  
[RequireComponent (typeof (LineRenderer))]  
  
public class RaycastReflection : MonoBehaviour  
{
<<<<<<< HEAD
    #region Variables
=======
>>>>>>> origin/Music
    private LineRenderer lineRenderer;

    private Ray ray;
    private RaycastHit hit;  
  
    //Reflection Direction
    private Vector3 inDirection;  
  
    //Number of Reflections
    public int nReflections = 2;

    //Points on the Line Renderer
    private int nPoints;

<<<<<<< HEAD
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
=======
    //Volumetric Variables
    private VolumetricLines.VolumetricMultiLineBehavior volumetricLines;
    public Vector3 points;

    //Scripts
    PlayerHealth playerHealth;

    //Burn Variables
    [SerializeField] private float burnTime;
    [SerializeField] private Material burnt;
    private Renderer rend;
  
    void Awake ()  
    {
        lineRenderer = GetComponent<LineRenderer>();
        volumetricLines = gameObject.GetComponent<VolumetricLines.VolumetricMultiLineBehavior>();
>>>>>>> origin/Music
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
<<<<<<< HEAD
=======
            volumetricLines.UpdateLineVertices(volumetricLines.m_lineVertices);
            volumetricLines.m_lineVertices[i] = lineRenderer.GetPosition(i);
>>>>>>> origin/Music
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
<<<<<<< HEAD
                        hit.transform.gameObject.GetComponent<PlayerHealth>().TakeDamage();
=======
                        hit.transform.position = hit.transform.gameObject.GetComponent<PlayerHealth>().SpawnPoint.position;
                        hit.transform.gameObject.GetComponent<PlayerHealth>().Health--;
                        print("hit");
>>>>>>> origin/Music
                    }

                    if (hit.transform.gameObject.tag == "Destructible")
                    {
<<<<<<< HEAD
=======
                        rend = hit.transform.gameObject.GetComponent<Renderer>();
>>>>>>> origin/Music
                        StartCoroutine(BurnObject(hit.transform.gameObject));
                    }
                }
            }  
        }
<<<<<<< HEAD
    }  

    IEnumerator BurnObject(GameObject go)
    {
        yield return new WaitForSeconds(burnTime);
        go.GetComponent<Renderer>().material = burnt;
        yield return new WaitForSeconds(burnTime);
        Destroy(go);
    }
    #endregion
=======
    }
    
    IEnumerator BurnObject(GameObject _burnObject)
    {
        yield return new WaitForSeconds(burnTime / 2);
        rend.material = burnt;
        yield return new WaitForSeconds(burnTime / 2);
        Destroy(_burnObject);
    }
>>>>>>> origin/Music
}  
