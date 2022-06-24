using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObj;

    [SerializeField]
    private GameObject cameraOffsetObj;

    /// <summary>
    /// プレイヤーの情報を取得
    /// </summary>
    /// <returns></returns>
    public GameObject GetPlayerObj() {
        return playerObj;
    }

    /// <summary>
    /// カメラのオフセット位置の取得
    /// </summary>
    /// <returns></returns>
    public GameObject GetCameraOffsetObj() {
        return cameraOffsetObj;
    }

    /// <summary>
    /// プレイヤーとカメラのオフセット位置の両方の情報を取得
    /// </summary>
    /// <returns></returns>
    public (GameObject player, GameObject cameraObj) GetStageInfo() {
        return (playerObj, cameraOffsetObj);
    }
}