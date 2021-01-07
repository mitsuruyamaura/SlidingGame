using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostEffectObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Player") {
            Camera.main.gameObject.GetComponent<CameraController>().UpdateFrostAmount(0.3f);
            GetComponent<Collider>().enabled = false;

            Destroy(gameObject, 0.5f);
        }
    }
}
