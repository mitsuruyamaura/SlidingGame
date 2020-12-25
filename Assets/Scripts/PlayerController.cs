using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public float moveSpeed;

    [HideInInspector]
    public float jumpPower;

    [SerializeField,HideInInspector]
    private PhysicMaterial physicMaterialBrake;


    private int score;

    [SerializeField,HideInInspector]
    private UIManager uiManager;


    private bool isGameClear;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Tilt(float tiltRot) {
        transform.eulerAngles = new Vector3(0, 0, 5);
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

        //Tilt(x);
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

        if (isGameClear == true) {
            rb.velocity *= 0.985f;

            if (rb.velocity.z <= 2.5f) {
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
                // https://gyazo.com/3ff58b30e8bc2f8b12001e9199e503da 2.0f
                // https://gyazo.com/c1660bf33f2acccdf0f9d8431326dfdd 2.5f
            }
        }

    }

    void FixedUpdate() {
        if (isGameClear == true) {
            return;
        }

        Move();
        Brake();
        Accelerate();
    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Goal") {
            Debug.Log("Goal");

            isGameClear = true;
            // https://gyazo.com/3fb9f4febf3f679893820a318d9f77ba
            //rb.velocity = Vector3.zero;
            //rb.isKinematic = true;
        }

    }

    public void AddScore(int amount) {
        score += amount;
        Debug.Log(score);

        uiManager.UpdateDisplayScore(score);
    }
}
