using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneDetection : MonoBehaviour {

    private ARPlaneManager arPlaneManager;
    private bool isPlaneVisible = true;

    //[SerializeField]
    //private UIManager uiManager;

    private bool isActivePlane;


    private void Awake() {
        arPlaneManager = GetComponent<ARPlaneManager>();

        //uiManager.InactiveARIntroductionText(false);

#if UNITY_ANDROID || UNITY_IOS
        //uiManager.DisplayARIntroduction("�J�����𑀍삵�ĕ��ʂ����m���悤�I");
#endif
    }

    void Update() {
        foreach (var plane in arPlaneManager.trackables) {
            plane.gameObject.SetActive(isPlaneVisible);
            if (plane.gameObject.activeSelf && !isActivePlane) {
                isActivePlane = true;
#if UNITY_ANDROID || UNITY_IOS
                //uiManager.DisplayARIntroduction("���m�����ꏊ���^�b�v���悤�I");
#endif
            }
        }
    }

    /// <summary>
    /// ARPlane �̕\���I���I�t�؂�ւ�
    /// </summary>
    public void SetAllPlaneActivate(bool isSwitch) {
        isPlaneVisible = isSwitch;

#if UNITY_ANDROID || UNITY_IOS
        if (isSwitch == false) {
            //uiManager.InactiveARIntroductionText(isSwitch);
#endif
        }
    }
}
