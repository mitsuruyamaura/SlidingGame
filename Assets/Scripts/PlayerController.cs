using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public float moveSpeed;

    public float jumpPower;

    [SerializeField]
    private TerrainCollider terrainCollider;

    [SerializeField]
    private PhysicMaterial physicMaterialBrake;


    void Start()
    {
        rb = GetComponent<Rigidbody>();



        //Sliding();
    }

    private void Sliding() {
        //rb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }


    /// <summary>
    /// ブレーキ
    /// </summary>
    private void Brake() {
        float z = Input.GetAxis("Vertical");

        if (z < 0) {
            physicMaterialBrake.dynamicFriction = Mathf.Abs(z);
        } else {
            physicMaterialBrake.dynamicFriction = 0;
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move() {
        float x = Input.GetAxis("Horizontal");

        rb.velocity = new Vector3(x * moveSpeed, rb.velocity.y, rb.velocity.z);
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    private void Jump() {

        rb.AddForce(transform.up * jumpPower);
    }

    private void Accelerate() {
        float z = Input.GetAxis("Vertical");

        if (z > 0) {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, moveSpeed);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }
        
    }

    void FixedUpdate() {
        Move();
        Brake();
        Accelerate();
    }
}
