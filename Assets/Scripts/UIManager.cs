using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text txtScore;

    [SerializeField]
    private Text txtTime;

    public void UpdateDisplayScore(int score) {
        txtScore.text = score.ToString();
    }

    public void UpdateDisplayGameTime(int time) {
        txtTime.text = time.ToString();
    }
}
