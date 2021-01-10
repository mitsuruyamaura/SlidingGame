using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObj;

    private Vector3 offset;

    void Start() {
        // カメラと追従対象のゲームオブジェクトとの距離を補正値として取得
        offset = transform.position - playerObj.transform.position;
    }


    void Update() {
        // 追従対象がいる場合
        if (playerObj != null) {

            // カメラの位置を追従対象の位置 + 補正値にする
            transform.position = playerObj.transform.position + offset;
        }
    }
}
