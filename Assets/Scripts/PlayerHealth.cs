using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int health;

    [SerializeField]
    private Transform spawnPoint;

    //Accessors
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    public Transform SpawnPoint
    {
        get
        {
            return spawnPoint;
        }
    }

    private void Start()
    {
        //Sets the Player position to the Spawn Point at the start of the Game
        transform.position = spawnPoint.transform.position;
    }

    private void Update()
    {
        //Kills Player if there are no Lives remaining
        if (health <= 0)
        {
            Destroy(gameObject);
            Application.Quit();
        }
    }
}
