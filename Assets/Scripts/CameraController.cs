using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObj;

    private Vector3 offset;

    [SerializeField]
    private FrostEffect frostEffect;

    private float currentFrostValue;

    void Start() {
        // カメラと追従対象のゲームオブジェクトとの距離を補正値として取得
        offset = transform.position - playerObj.transform.position;

        // 霜エフェクトの初期化
        InitialFrostAmount();
    }


    void Update() {
        // 追従対象がいる場合
        if (playerObj != null) {

            // カメラの位置を追従対象の位置 + 補正値にする
            transform.position = playerObj.transform.position + offset;
        }

        if (Input.GetKeyDown(KeyCode.Z)) {
            UpdateFrostAmount(0.1f);
        }

        // 霜でおおわれているとき
        if (currentFrostValue > 0) {

            // 少しずつ見えるようにする
            currentFrostValue -= Time.deltaTime / 20;
            UpdateFrostAmount(0);

            if (currentFrostValue <= 0) {
                currentFrostValue = 0;
            }
        }
    }

    /// <summary>
    /// FrostEffectの状態を更新
    /// </summary>
    /// <param name="amount"></param>
    public void UpdateFrostAmount(float amount) {
        currentFrostValue += amount;
        frostEffect.FrostAmount = currentFrostValue;
    }

    /// <summary>
    /// FrostEffectの初期化
    /// </summary>
    public void InitialFrostAmount() {
        currentFrostValue = 0;
        frostEffect.FrostAmount = currentFrostValue;
    }
}
