using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Sliding();
    }

    private void Sliding() {
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    void Update()
    {
        
    }
}
