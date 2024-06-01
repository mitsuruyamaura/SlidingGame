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
        None,         // Editor �ł̃f�o�b�O�p
        Tracking,     // ���ʊ��m��
        Preparate,    // �Q�[�������O�B���m�I����AReady �̑O�̏��
        Ready,        // �Q�[�������J�n
        Play,         // �Q�[����
        GameUp,       // �Q�[���I��
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

            // ���ʊ��m
            TrackingPlane();
        } else if (currentARState == ARState.Preparate) {

            currentARState = ARState.Ready;
            LogDebugger.instance.DisplayLog(currentARState.ToString());

            // �Q�[���J�n�̏���
            StartCoroutine(PraparateGameReady());

        } else if (currentARState == ARState.Play) {

            // �Q�[���v���C���̃f�o�b�O�\��
            LogDebugger.instance.DisplayLog(currentARState.ToString());
        }
    }

    /// <summary>
    /// ���ʊ��m��Plane����
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
                LogDebugger.instance.DisplayLog("Raycast ����");

                //uiManager.DisplayDebug("Raycast ����");
                obj = Instantiate(stageObj, hitPose.position, Quaternion.identity);


                // ���ʊ��m���I��
                if (TryGetComponent(out planeDetection)) {
                    planeDetection.SetAllPlaneActivate(false);
                    LogDebugger.instance.DisplayLog("���ʊ��m���I��");
                }

                // �y���M���̏����J�����ɃZ�b�g
                //cameraController.SetPlayer(obj.GetComponent<Stage>().GetPlayerObj());

                (GameObject player, GameObject cameraOffsetObj) stageInfo = obj.GetComponent<Stage>().GetStageInfo();
                cameraController.SetStageInfo(stageInfo.player, stageInfo.cameraOffsetObj);

                LogDebugger.instance.DisplayLog("�v���C���[�̐ݒ芮��");

                //obj = fieldObj;

                //fieldObj.SetActive(true);

                // UniRX �̏ꍇ
                //ARStateReactiveProperty.Value = ARState.Ready;

                currentARState = ARState.Preparate;


            } else {
                //uiManager.DisplayDebug("Raycast ��");
                //obj.transform.position = hitPose.position;
            }
        } else {
            //uiManager.DisplayDebug("RayCast ���s");
        }
    }

    /// <summary>
    /// �Q�[���J�n�̏���
    /// </summary>
    private IEnumerator PraparateGameReady() {

        // TODO ��������������

        yield return new WaitForSeconds(2.0f);

        currentARState = ARState.Play;

        //ARStateReactiveProperty.Value = ARState.Play;

        LogDebugger.instance.DisplayLog(currentARState.ToString());

        //StartCoroutine(fieldAutoScroller.StartFieldScroll());

        // ���ʌ��m���\��
        //planeDetection.SetAllPlaneActivate(false);

        //yield return StartCoroutine(gameManager.SetStart());
    }
}