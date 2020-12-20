using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpUpObject : MonoBehaviour
{
    private float height;

    // Start is called before the first frame update
    void Start()
    {
        height = transform.position.y;
        transform.position = new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z);

        //StartCoroutine(HeadUp());      
    }

    private IEnumerator HeadUp(float waitTime = 1.0f) {
        yield return new WaitForSeconds(waitTime);
        transform.DOMoveY(height, 0.25f).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter(Collider col) {
        // キャラが一定の距離に入ったら顔を出す
        if (col.gameObject.tag == "Player") {

            // TODO 
            StartCoroutine(HeadUp(0.0f));
        }
    }
}
