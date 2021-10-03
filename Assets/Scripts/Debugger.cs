using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debugger : MonoBehaviour
{
    public static Debugger instance;

    [SerializeField]
    private Text txtDebug;


    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void DisplayLog(string message) {
        txtDebug.text += message + "\n";
    }

}
