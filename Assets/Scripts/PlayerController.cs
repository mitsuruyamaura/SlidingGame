using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public float moveSpeed;

    public float jumpPower;

    [SerializeField]
    private PhysicMaterial physicMaterialBrake;

    private int score;

    [SerializeField]
    private UIManager uiManager;


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
        
    }

    void FixedUpdate() {
        Move();
        Brake();
        Accelerate();
    }

    public void AddScore(int amount) {
        score += amount;
        Debug.Log(score);

        uiManager.UpdateDisplayScore(score);
    }
}
