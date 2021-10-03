using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModeSwitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject editorCamera;

    [SerializeField]
    private GameObject arCamera;

    [SerializeField]
    private GameObject stage;

    void Awake()
    {
# if UNITY_EDITOR
        arCamera.SetActive(false);
# elif UNITY_ANDROID || UNITY_IOS
        editorCamera.SetActive(false);
        stage.SetActive(false);
# endif
    }
}
