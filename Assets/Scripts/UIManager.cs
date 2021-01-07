using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text txtScore;

    [SerializeField, HideInInspector]
    private Text txtTime;

    /// <summary>
    /// スコアの表示更新
    /// </summary>
    /// <param name="score"></param>
    public void UpdateDisplayScore(int score) {
        txtScore.text = score.ToString();
    }


    // mi

    /// <summary>
    /// ゲーム時間の表示更新
    /// </summary>
    /// <param name="time"></param>
    public void UpdateDisplayGameTime(int time) {
        txtTime.text = time.ToString();
    }
}
