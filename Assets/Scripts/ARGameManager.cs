using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject fieldObj;

    private PlaneDetection planeDetection;

    private GameObject obj;

    private ARRaycastManager raycastManager;

    private List<ARRaycastHit> raycastHitList = new List<ARRaycastHit>();

    void Awake() {

        TryGetComponent(out raycastManager);
        TryGetComponent(out planeDetection);
    }


    void Start()
    {
        
    }


    void Update()
    {
        if (Input.touchCount < 0) {
            return;
        }

        // 平面感知
        TrackingPlane();
    }

    /// <summary>
    /// 平面感知とPlane生成
    /// </summary>
    private void TrackingPlane() {
        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Ended) {
            return;
        }

        if (raycastManager.Raycast(touch.position, raycastHitList, TrackableType.PlaneWithinPolygon)) {
            Pose hitPose = raycastHitList[0].pose;

            if (obj == null) {
                //uiManager.DisplayDebug("Raycast 成功");
                //obj = Instantiate(objPrefab, hitPose.position, hitPose.rotation);

                obj = fieldObj;

                fieldObj.SetActive(true);
                
                //uiManager.SwitchActivateTargetIcon(true);


                //currentARState = ARState.Ready;

                // UniRX の場合
                //ARStateReactiveProperty.Value = ARState.Ready;

            } else {
                //uiManager.DisplayDebug("Raycast 済");
                obj.transform.position = hitPose.position;
            }
        } else {
            //uiManager.DisplayDebug("RayCast 失敗");
        }
    }

    ///// <summary>
    ///// ゲーム開始の準備
    ///// </summary>
    //private IEnumerator PraparateGameReady() {

    //    // TODO 準備処理を書く

    //    //yield return new WaitForSeconds(2.0f);

    //    currentARState = ARState.Play;

    //    ARStateReactiveProperty.Value = ARState.Play;

    //    uiManager.DisplayDebug(currentARState.ToString());

    //    //StartCoroutine(fieldAutoScroller.StartFieldScroll());

    //    // 平面検知を非表示
    //    planeDetection.SetAllPlaneActivate(false);

    //    yield return StartCoroutine(gameManager.SetStart());
    //}
}
