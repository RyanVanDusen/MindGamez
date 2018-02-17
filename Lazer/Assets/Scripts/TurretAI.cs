using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class TurretAI : MonoBehaviour {
    //**inspector variables
    [SerializeField]
    Transform target;
    [Space(1)]
    [Header("Turret Controls")]
    public Controls controls;
    [Space(1)]
    [Header("Turret Weapons")]
    public Weapons weapons;

    //private variables
    float curValue;
    float min;
    float max;
    bool turningRight = true;
    bool isDelaying;
    enum AI {Seeking, Targeting, Disabled }
    AI ai;
    Vector3 lastPos = Vector3.zero;
    Quaternion lookAt;
    float lastRot;
    Transform leftCensor;
    Transform rightCensor;


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
    }

    void FixedUpdate()
    {
        RaycastHit leftHit;
        RaycastHit rightHit;
        leftCensor.localPosition = new Vector3(+weapons.fieldOfView,0,leftCensor.localPosition.z);
        rightCensor.localPosition = new Vector3(-weapons.fieldOfView, 0, rightCensor.localPosition.z);
        leftCensor.localRotation = Quaternion.Euler(0,-weapons.fieldOfView*100, 0);
        rightCensor.localRotation = Quaternion.Euler(0, +weapons.fieldOfView * 100, 0);

        if (Physics.Raycast(leftCensor.position, leftCensor.forward * weapons.fieldOfView, out leftHit, weapons.range)||
            Physics.Raycast(rightCensor.position, rightCensor.forward * -weapons.fieldOfView, out rightHit, weapons.range))
        {
            Debug.DrawLine(rightCensor.position, leftCensor.forward * weapons.range, Color.green);
            Debug.DrawLine(leftCensor.position, rightCensor.forward * weapons.range, Color.green);
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
                Seeking();
                break;
            case AI.Targeting:
                if (!target)
                    goto case AI.Seeking;
                Targeting();
                break;
        }
        #endregion
    }
    public void Targeting()
    {
        if (lastPos != target.position)
        {
            lastPos = target.position;
            lookAt = Quaternion.LookRotation(lastPos - transform.position);
        }
        if (transform.rotation != lookAt)
        {
            if (lastRot - transform.rotation.y > 0)
                curValue -= 1f;
            else if (lastRot - transform.rotation.y < 0)
                curValue += 1f;
            lastRot = transform.rotation.y;

            if (curValue >= max || curValue <= min)
            {
                if (curValue >= max)
                    curValue = max;
                else if (curValue <= min)
                    curValue = min;
                turningRight = !turningRight;
                ai = AI.Seeking;
            }
            else
               transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            print("Fire!");
        }
    }

    public void Seeking()
    {
        if (isDelaying)
            return;
        curValue += turningRight ? +controls.seekingSpeed * Time.deltaTime : -controls.seekingSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, curValue, 0);

        if (curValue >= max || curValue <= min)
        {
            if (curValue >= max)
                curValue = max;
            else if (curValue <= min)
                curValue = min;
            turningRight = !turningRight;
            StartCoroutine(Delay());
        }
    }

    bool Target(Transform _target)
    {
        if (target)
            return false;
        target = _target;
        return true;
    }

    IEnumerator Delay()
    {
        isDelaying = true;
        yield return new WaitForSeconds(controls.delay);
        isDelaying = false;
    }
}
