using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARGameManager : MonoBehaviour {

    [SerializeField]
    private GameObject stageObj;

    [SerializeField]
    private Stage stagePrefab;

    [SerializeField]
    private CameraController cameraController;

    private GameObject obj;

    private ARRaycastManager raycastManager;

    //private List<ARRaycastHit> raycastHitList = new List<ARRaycastHit>();

    public enum ARState {
        None,         // Editor でのデバッグ用
        Tracking,     // 平面感知中
        Preparate,    // ゲーム準備前。感知終了後、Ready の前の状態
        Ready,        // ゲーム準備開始
        Play,         // ゲーム中
        GameUp,       // ゲーム終了
    }

    public ARState currentARState;

    private PlaneDetection planeDetection;


    void Awake() {

        TryGetComponent(out raycastManager);
#if UNITY_EDITOR
        currentARState = ARState.None;
        //stageObj.GetComponent<Stage>().GetPlayerObj();
# elif UNITY_ANDROID || UNITY_IOS
        currentARState = ARState.Tracking;
        stageObj.SetActive(false);
# endif
    }

    void Update() {
        if (currentARState == ARState.None) {
            return;
        }

        if (Input.touchCount < 0) {
            return;
        }

        if (currentARState == ARState.Tracking) {

            // 平面感知
            TrackingPlane();
        } else if (currentARState == ARState.Preparate) {

            currentARState = ARState.Ready;
            LogDebugger.instance.DisplayLog(currentARState.ToString());

            // ゲーム開始の準備
            StartCoroutine(PraparateGameReady());

        } else if (currentARState == ARState.Play) {

            // ゲームプレイ中のデバッグ表示
            LogDebugger.instance.DisplayLog(currentARState.ToString());
        }
    }

    /// <summary>
    /// 平面感知とPlane生成
    /// </summary>
    private void TrackingPlane() {
        Touch touch = Input.GetTouch(0);

        //if (touch.phase != TouchPhase.Ended) {
        //    return;
        //}

        List<ARRaycastHit> raycastHitList = new List<ARRaycastHit>();

        if (raycastManager.Raycast(touch.position, raycastHitList, TrackableType.PlaneWithinPolygon)) {
            Pose hitPose = raycastHitList[0].pose;

            if (obj == null) {
                LogDebugger.instance.DisplayLog("Raycast 成功");

                //uiManager.DisplayDebug("Raycast 成功");
                obj = Instantiate(stageObj, hitPose.position, Quaternion.identity);


                // 平面感知を終了
                if (TryGetComponent(out planeDetection)) {
                    planeDetection.SetAllPlaneActivate(false);
                    LogDebugger.instance.DisplayLog("平面感知を終了");
                }

                // ペンギンの情報をカメラにセット
                //cameraController.SetPlayer(obj.GetComponent<Stage>().GetPlayerObj());

                (GameObject player, GameObject cameraOffsetObj) stageInfo = obj.GetComponent<Stage>().GetStageInfo();
                cameraController.SetStageInfo(stageInfo.player, stageInfo.cameraOffsetObj);

                LogDebugger.instance.DisplayLog("プレイヤーの設定完了");

                //obj = fieldObj;

                //fieldObj.SetActive(true);

                // UniRX の場合
                //ARStateReactiveProperty.Value = ARState.Ready;

                currentARState = ARState.Preparate;


            } else {
                //uiManager.DisplayDebug("Raycast 済");
                //obj.transform.position = hitPose.position;
            }
        } else {
            //uiManager.DisplayDebug("RayCast 失敗");
        }
    }

    /// <summary>
    /// ゲーム開始の準備
    /// </summary>
    private IEnumerator PraparateGameReady() {

        // TODO 準備処理を書く

        yield return new WaitForSeconds(2.0f);

        currentARState = ARState.Play;

        //ARStateReactiveProperty.Value = ARState.Play;

        LogDebugger.instance.DisplayLog(currentARState.ToString());

        //StartCoroutine(fieldAutoScroller.StartFieldScroll());

        // 平面検知を非表示
        //planeDetection.SetAllPlaneActivate(false);

        //yield return StartCoroutine(gameManager.SetStart());
    }
}