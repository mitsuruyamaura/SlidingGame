using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARGameManager : MonoBehaviour {

    [SerializeField]
    private GameObject fieldObj;

    [SerializeField]
    private CameraController cameraController;

    private PlaneDetection planeDetection;

    private GameObject obj;

    private ARRaycastManager raycastManager;

    private List<ARRaycastHit> raycastHitList = new List<ARRaycastHit>();

    void Awake() {

        TryGetComponent(out raycastManager);
        TryGetComponent(out planeDetection);
    }


    void Start() {

    }


    void Update() {
        if (Input.touchCount < 0) {
            return;
        }

        // ���ʊ��m
        TrackingPlane();
    }

    /// <summary>
    /// ���ʊ��m��Plane����
    /// </summary>
    private void TrackingPlane() {
        Touch touch = Input.GetTouch(0);

        if (touch.phase != TouchPhase.Ended) {
            return;
        }

        if (raycastManager.Raycast(touch.position, raycastHitList, TrackableType.PlaneWithinPolygon)) {
            Pose hitPose = raycastHitList[0].pose;

            if (obj == null) {
                //uiManager.DisplayDebug("Raycast ����");
                obj = Instantiate(fieldObj, hitPose.position, hitPose.rotation);

                // ���ʊ��m���I��
                planeDetection.SetAllPlaneActivate(false);

                Debugger.instance.DisplayLog("���ʊ��m���I��");

                // �y���M���̏����J�����ɃZ�b�g
                cameraController.SetPlayer(obj.GetComponent<Stage>().GetPlayerObj());

                Debugger.instance.DisplayLog("�v���C���[�̐ݒ芮��");

                //obj = fieldObj;

                //fieldObj.SetActive(true);

                //uiManager.SwitchActivateTargetIcon(true);

                //currentARState = ARState.Ready;

                // UniRX �̏ꍇ
                //ARStateReactiveProperty.Value = ARState.Ready;

            } else {
                //uiManager.DisplayDebug("Raycast ��");
                obj.transform.position = hitPose.position;
            }
        } else {
            //uiManager.DisplayDebug("RayCast ���s");
        }
    }

    ///// <summary>
    ///// �Q�[���J�n�̏���
    ///// </summary>
    //private IEnumerator PraparateGameReady() {

    //    // TODO ��������������

    //    //yield return new WaitForSeconds(2.0f);

    //    currentARState = ARState.Play;

    //    ARStateReactiveProperty.Value = ARState.Play;

    //    uiManager.DisplayDebug(currentARState.ToString());

    //    //StartCoroutine(fieldAutoScroller.StartFieldScroll());

    //    // ���ʌ��m���\��
    //    planeDetection.SetAllPlaneActivate(false);

    //    yield return StartCoroutine(gameManager.SetStart());
    //}
}