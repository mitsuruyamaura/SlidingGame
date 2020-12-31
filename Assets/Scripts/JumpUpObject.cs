using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpUpObject : MonoBehaviour
{
    private float startHeight;           // キャラのスタート位置の高さ
    private float hideHeight = 1.0f;     // キャラを隠すための高さの度合

    // Start is called before the first frame update
    void Start()
    {
        // 隠す
        Hide();
        
        //StartCoroutine(HeadUp());      
    }

    /// <summary>
    /// ゲームオブジェクトを隠す
    /// </summary>
    private void Hide() {
        startHeight = transform.position.y;
        transform.position = new Vector3(transform.position.x, transform.position.y - hideHeight, transform.position.z);
    }

    /// <summary>
    /// 顔を出す
    /// </summary>
    private IEnumerator HeadUp(float waitTime = 1.0f) {
        yield return new WaitForSeconds(waitTime);
        transform.DOMoveY(startHeight, 0.25f).SetEase(Ease.Linear);

        //transform.position = new Vector3(transform.position.x, startHeight, transform.position.z);
    }

    private void OnTriggerEnter(Collider col) {
        // キャラが一定の距離に入ったら顔を出す
        if (col.gameObject.tag == "Player") {

            // TODO 
            StartCoroutine(HeadUp(0.0f));
        }
    }
}
