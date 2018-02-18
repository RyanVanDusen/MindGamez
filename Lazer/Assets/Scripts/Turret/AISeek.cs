using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AITarget))]
public class AISeek : MonoBehaviour {
    public TurretAI ai;
    bool turningRight = true;
    bool isDelaying;

    private void Start()
    {
        ai = gameObject.GetComponent<TurretAI>();
    }

    public void Seeking()
    {
        if (isDelaying)
            return;
        ai.curValue += turningRight ? +ai.controls.seekingSpeed * Time.deltaTime : -ai.controls.seekingSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, ai.curValue, 0);

        if (ai.curValue >= ai.max || ai.curValue <= ai.min)
        {
            if (ai.curValue >= ai.max)
                ai.curValue = ai.max;
            else if (ai.curValue <= ai.min)
                ai.curValue = ai.min;
            turningRight = !turningRight;
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        isDelaying = true;
        yield return new WaitForSeconds(ai.controls.delay);
        isDelaying = false;
    }
}
