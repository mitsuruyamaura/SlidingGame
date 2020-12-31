using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateObject : MonoBehaviour
{
    [Header("回転する時間")]
    public float duration;

    private bool isRotate = false;
   
    private void OnTriggerEnter(Collider col) {
        // キャラが一定の距離に入ったら
        if (col.gameObject.tag == "Player" && !isRotate) {

            // 木を回転させて倒す
            Rotate();

            // 回転して転倒した状態にする
            isRotate = true;
        }
    }


    /// <summary>
    /// 木を回転させる
    /// </summary>
    private void Rotate() {

        // Z 軸のみ duration 分の時間をかけて回転
        transform.DORotate(new Vector3(0, 0, RandomAngle()), duration);
    }

    /// <summary>
    /// 木の回転角度をランダムに設定
    /// </summary>
    /// <returns></returns>
    private float RandomAngle() {
        int value = Random.Range(0, 2);

        if (value == 0) {
            return 70.0f;
        } else {
            return -70.0f;
        }

        //return Random.Range(0, 2) == 0 ? 70 : -70;
    }
}
