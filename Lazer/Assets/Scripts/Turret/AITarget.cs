using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITarget : MonoBehaviour {
    public TurretAI ai;
    Vector3 lastPos = Vector3.zero;
    Quaternion lookAt;
    float lastRot;

    // Use this for initialization
    void Start () {
        ai = gameObject.GetComponent<TurretAI>();
	}

    public void Targeting()
    {
        if (lastPos != ai.target.position)
        {
            lastPos = ai.target.position;
            lookAt = Quaternion.LookRotation(lastPos - transform.position);
        }
        if (transform.rotation != lookAt)
        {
            if (lastRot - transform.rotation.y > 0)
                ai.curValue -= 1f;
            else if (lastRot - transform.rotation.y < 0)
                ai.curValue += 1f;
            lastRot = transform.rotation.y;

            if (ai.curValue >= ai.max || ai.curValue <= ai.min)
            {
                if (ai.curValue >= ai.max)
                    ai.curValue = ai.max;
                else if (ai.curValue <= ai.min)
                    ai.curValue = ai.min;
                // turningRight = !turningRight;
                ai.ai = TurretAI.AI.Seeking;
            }
            else
                transform.LookAt(new Vector3(ai.target.position.x, transform.position.y, ai.target.position.z));
            print("Fire!");
        }
    }
}
