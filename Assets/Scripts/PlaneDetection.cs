using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneDetection : MonoBehaviour {

    private ARPlaneManager arPlaneManager;
    private bool isVisible = true;

    private bool isActivePlane;


    private void Awake() {
        TryGetComponent(out arPlaneManager);
    }

    void Update() {
        foreach (ARPlane plane in arPlaneManager.trackables) {
            plane.gameObject.SetActive(isVisible);

            if (plane.gameObject.activeSelf && !isActivePlane) {
                isActivePlane = true;
            }
        }
    }

    /// <summary>
    /// ARPlane �̕\���I���I�t�؂�ւ�
    /// </summary>
    public void SetAllPlaneActivate(bool isSwitch) {
        isVisible = isSwitch;
    }
}
