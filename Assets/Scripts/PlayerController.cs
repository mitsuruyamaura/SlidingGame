using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private bool isGoal;

    private float coefficient = 0.985f;    // 速度を減速させるための係数

    private float stopValue = 2.5f;        // 減速中に、この値以下になったら停止させる速度の値

    private Animator anim;

    private int score;

    [Header("移動速度")]
    public float moveSpeed;

    [Header("加速速度")]
    public float accelerationSpeed;

    [Header("ジャンプ力")]
    public float jumpPower;

    [SerializeField]
    private PhysicMaterial pmNoFriction;

    [SerializeField, Header("地面判定用レイヤー"), HideInInspector]
    private LayerMask groundLayer;

    [SerializeField, Header("地面の判定"), HideInInspector]
    private bool isGrounded;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    private Joystick joystick;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        uiManager.SetUpUIManager(this);
    }

    private void Tilt(float tiltRot) {
        transform.eulerAngles = new Vector3(0, 0, 5);
    }


    /// <summary>
    /// ブレーキ
    /// </summary>
    private void Brake() {
        float vertical;

# if UNITY_EDITOR
        vertical = Input.GetAxis("Vertical");
# elif UNITY_ANDROID
        vertical = joystick.Vertical;
# endif

        if (vertical < 0) {
            pmNoFriction.dynamicFriction += Time.deltaTime;

            if (pmNoFriction.dynamicFriction > 1.0f) {
                pmNoFriction.dynamicFriction = 1.0f;
            }

        } else {
            //if (pmNoFriction.dynamicFriction > 0) {
            //    pmNoFriction.dynamicFriction -= Time.deltaTime;

            //    if (pmNoFriction.dynamicFriction <= 0) {
                    pmNoFriction.dynamicFriction = 0;
            //    }
            //}

            
        }

        //pmNoFriction.dynamicFriction = Mathf.Clamp(pmNoFriction.dynamicFriction, 0, 1.0f);
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move() {
        float x;

# if UNITY_EDITOR
        x = Input.GetAxis("Horizontal");
# elif UNITY_ANDROID
        x = joystick.Horizontal;
# endif

        rb.velocity = new Vector3(x * moveSpeed, rb.velocity.y, rb.velocity.z);

        //Tilt(x);
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    public void Jump() {

        rb.AddForce(transform.up * jumpPower);
        anim.SetTrigger("jump");
    }

    /// <summary>
    /// 加速
    /// </summary>
    private void Accelerate() {
        float z;

#if UNITY_EDITOR
        z = Input.GetAxis("Vertical");
# elif UNITY_ANDROID
        z = joystick.Vertical;
# endif

        if (z > 0) {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, moveSpeed * 2);
        }
    }

    void Update()
    {
        Brake();
        Accelerate();

        if (isGoal == true) {
            rb.velocity *= coefficient;

            if (rb.velocity.z <= stopValue) {
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
            }
        }

        CheckGround();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true) {
            Jump();
        }
    }

    void FixedUpdate() {
        if (isGoal == true) {
            return;
        }

        Move();

    }

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.tag == "Goal") {
            Debug.Log("Goal");

            // ゲームクリアの状態にする
            isGoal = true;

            Debug.Log(isGoal);

            //rb.velocity = Vector3.zero;
            //rb.isKinematic = true;
        }
    }

    /// <summary>
    /// 斜面との接地判定。true なら接地している状態、false は接地していない状態と定義する
    /// </summary>
    private void CheckGround() {
        //  Linecastでキャラの足元に地面用のLayerを持つゲームオブジェクトがあるか判定。対象のLayerのときは true を返す
        isGrounded = Physics.Linecast(
                        transform.position,
                        transform.position - transform.up * 0.3f,
                        groundLayer);

        Debug.DrawLine(transform.position,
                        transform.position - transform.up * 0.3f,
                        Color.red);
    }

    /// <summary>
    /// スコア加算
    /// </summary>
    /// <param name="amount"></param>
    public void AddScore(int amount) {
        score += amount;
        Debug.Log("現在の得点 : " + score);

        uiManager.UpdateDisplayScore(score);
    }

    /// <summary>
    /// IsGrounded の取得
    /// </summary>
    /// <returns></returns>
    public bool GetIsGrounded() {
        return isGrounded;
    }
}
