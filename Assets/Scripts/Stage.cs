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
    /// �v���C���[�̏����擾
    /// </summary>
    /// <returns></returns>
    public GameObject GetPlayerObj() {
        return playerObj;
    }

    /// <summary>
    /// �J�����̃I�t�Z�b�g�ʒu�̎擾
    /// </summary>
    /// <returns></returns>
    public GameObject GetCameraOffsetObj() {
        return cameraOffsetObj;
    }

    /// <summary>
    /// �v���C���[�ƃJ�����̃I�t�Z�b�g�ʒu�̗����̏����擾
    /// </summary>
    /// <returns></returns>
    public (GameObject player, GameObject cameraObj) GetStageInfo() {
        return (playerObj, cameraOffsetObj);
    }
}