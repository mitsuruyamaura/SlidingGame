using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObj;

    public GameObject GetPlayerObj() {
        return playerObj;
    }
}
