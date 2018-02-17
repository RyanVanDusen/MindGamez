using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [System.Serializable]
    public class Controls
    {
        [Tooltip("The rotation speed during idle state")]
        public float seekingSpeed = 30f;
        [Tooltip("The delay between rotations")]
        public float delay = .5f;
        [Range(0,360)]
        public float clamp;
        [Range(0, 1f)]
        public float fieldOfView;
    }
    [System.Serializable]
    public class Weapons
    {
        [Range(0, 2f)]
        public float fireRate;
        public float range;
        public int damage = 1;
        [Range(0, 5f)]
        public float accuracy;
        public enum WeaponType{ Normal, ThreeRoundBurst, ShotgunBlast};
        public WeaponType weaponType;
    }

    void Start()
    {
        curValue = transform.localRotation.y;
        min = curValue - controls.clamp;
        max = curValue + controls.clamp;
        ai = AI.Targeting;
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
                curValue -= Time.deltaTime * controls.seekingSpeed;
            else if (lastRot - transform.rotation.y < 0)
                curValue += Time.deltaTime * controls.seekingSpeed;
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
