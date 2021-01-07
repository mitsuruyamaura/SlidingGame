using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public int point;

    private void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player") {
            //Debug.Log(point);
            //Debug.Log("キャラ侵入");

            if (col.gameObject.TryGetComponent(out PlayerController player)) {
                player.AddScore(point);
            }

            Destroy(gameObject, 0.5f);
        }
    }
}
