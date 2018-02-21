using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AISeek))]
[RequireComponent(typeof(AITarget))]
public class TurretAI : MonoBehaviour {
    //**inspector variables
    public Transform target;
    [Space(1)]
    [Header("Turret Controls")]
    public Controls controls;
    [Space(1)]
    [Header("Turret Weapons")]
    public Weapons weapons;

    //private variables
    [HideInInspector]
    public float curValue;
    [HideInInspector]
    public float min { get; private set; }
    [HideInInspector]
    public float max { get; private set; }
    [HideInInspector]
    public enum AI {Seeking, Targeting, Disabled }
    [HideInInspector]
    public AI ai;
    Transform leftCensor;
    Transform rightCensor;

    //AI components
    AISeek aiSeek;
    AITarget aiTarget;

    [System.Serializable]
    public class Controls
    {
        [Tooltip("The rotation speed during idle state")]
        public float seekingSpeed = 30f;
        [Tooltip("The delay between rotations")]
        public float delay = .5f;
        [Range(0,360)]
        public float clamp = 50f;
    }
    [System.Serializable]
    public class Weapons
    {
        [Range(0, 2f)]
        public float fireRate;
        public float range = 20f;
        [Range(.01f, .5f)]
        public float fieldOfView =.1f;
        public int damage = 1;
        [Range(0f, 5f)]
        public float accuracy = .5f;
        public enum WeaponType{ Normal, ThreeRoundBurst, ShotgunBlast};
        public WeaponType weaponType;
    }

    void Start()
    {
        curValue = transform.localRotation.y;
        min = curValue - controls.clamp;
        max = curValue + controls.clamp;
        ai = AI.Targeting;
        leftCensor = transform.Find("Censors/LeftCensor");
        rightCensor = transform.Find("Censors/RightCensor");
        aiSeek = gameObject.GetComponent<AISeek>();
        aiTarget = gameObject.GetComponent<AITarget>();
    }

    void FixedUpdate()
    {
        RaycastHit leftHit;
        RaycastHit rightHit;
        leftCensor.localPosition = new Vector3(+weapons.fieldOfView, 0, leftCensor.localPosition.z);
        rightCensor.localPosition = new Vector3(-weapons.fieldOfView, 0, rightCensor.localPosition.z);
        leftCensor.localRotation = Quaternion.Euler(0, -weapons.fieldOfView * 100, 0);
        rightCensor.localRotation = Quaternion.Euler(0, +weapons.fieldOfView * 100, 0);
        if (Physics.Raycast(leftCensor.position, leftCensor.forward * weapons.fieldOfView, out leftHit, weapons.range))
        {
            if (ai == AI.Seeking && (leftHit.transform.gameObject.tag == "Player"))
                ai = AI.Targeting;
        }
        else if (Physics.Raycast(rightCensor.position, rightCensor.forward * -weapons.fieldOfView, out rightHit, weapons.range))
        {
            if (ai == AI.Seeking && (rightHit.transform.gameObject.tag == "Player"))
                ai = AI.Targeting;
        }

        if (ai == AI.Seeking)
        {
            Debug.DrawLine(rightCensor.position, leftCensor.forward * weapons.range, Color.green);
            Debug.DrawLine(leftCensor.position, rightCensor.forward * weapons.range, Color.green);
        }
        else
        {
            Debug.DrawLine(rightCensor.position, leftCensor.forward * weapons.range, Color.red);
            Debug.DrawLine(leftCensor.position, rightCensor.forward * weapons.range, Color.red);
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        #region Finite
        switch (ai)
        {
            default:
                break;
            case AI.Seeking:
                aiSeek.Seeking();
                break;
            case AI.Targeting:
                if (!target)
                    goto case AI.Seeking;
                aiTarget.Targeting();
                break;
        }
        #endregion
    }
   

    bool Target(Transform _target)
    {
        if (target)
            return false;
        target = _target;
        return true;
    }
}
