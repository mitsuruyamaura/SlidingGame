using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateObject : MonoBehaviour
{
    public float waitTime;


    void Start()
    {
        StartCoroutine(Rotate(Random.Range(2.0f, 4.0f)));
    }

    private IEnumerator Rotate(float duration) {
        yield return new WaitForSeconds(waitTime);

        transform.DORotate(new Vector3(0, 0, RandomRotate()), duration).SetEase(Ease.Linear);
    }

    private float RandomRotate() {
        float z = Random.Range(0, 2);
        return z == 0 ? 70 : -70;
    }
}
