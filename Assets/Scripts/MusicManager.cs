using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    #region Variables
    private static MusicManager scriptInstance = null;
    #endregion

    #region Accessors
    public static MusicManager ScriptInstance
    {
        get
        {
            return scriptInstance;
        }
    }
    #endregion

    #region Functions
    private void Awake()
    {
        if (scriptInstance != null && scriptInstance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            scriptInstance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion
}

