using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObj;

    private Vector3 offset;

    private bool isSetup = false;

    [SerializeField]
    private GameObject cameraPos;


    void Start() {
        if (playerObj != null) {

            // カメラと追従対象のゲームオブジェクトとの距離を補正値として取得
            SetOffset();
        }
    }

    void Update() {
        // 追従対象がいる場合
        if (playerObj != null && isSetup) {

            // カメラの位置を追従対象の位置 + 補正値にする
            transform.position = playerObj.transform.position + offset;

            LogDebugger.instance.DisplayLog("カメラ移動");
        }
    }

    /// <summary>
    /// プレイヤーの情報を設定
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayer(GameObject player) {

        playerObj = player;

        LogDebugger.instance.DisplayLog(playerObj.name);

        SetOffset();
    }

    /// <summary>
    /// カメラと追従対象のゲームオブジェクトとの距離を補正値として設定
    /// </summary>
    private void SetOffset() {

        // カメラと追従対象のゲームオブジェクトとの距離を補正値として取得
        offset = cameraPos.transform.position - playerObj.transform.position;

        isSetup = true;
    }

    /// <summary>
    /// Player とカメラのオフセット位置の情報を設定
    /// </summary>
    /// <param name="player"></param>
    /// <param name="cameraOffset"></param>
    public void SetStageInfo(GameObject player, GameObject cameraOffset) {
        playerObj = player;
        cameraPos = cameraOffset;

        LogDebugger.instance.DisplayLog(playerObj.name);

        SetOffset();
    }
}
