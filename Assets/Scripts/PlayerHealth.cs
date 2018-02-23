<<<<<<< HEAD
﻿using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    #region Variables
=======
﻿using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
>>>>>>> origin/TurretSeeking
    [SerializeField]
    private float health;

    [SerializeField]
    private Transform spawnPoint;

<<<<<<< HEAD
    [SerializeField]
    private bool canHurt;
    #endregion

    #region Accessors
=======
    //Accessors
>>>>>>> origin/TurretSeeking
    public float Health
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
<<<<<<< HEAD
        set
        {
            spawnPoint = value;
        }
    }

    public bool CanHurt
    {
        get
        {
            return canHurt;
        }
    }
    #endregion

    #region Functions
=======
    }

>>>>>>> origin/TurretSeeking
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
<<<<<<< HEAD

    public void TakeDamage()
    {
        canHurt = false;
        transform.position = spawnPoint.position;
        health--;

        StartCoroutine(Wait());

        Debug.Log("HEALTH: " + health);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(.2f);
        canHurt = true;
    }
    #endregion
=======
>>>>>>> origin/TurretSeeking
}
