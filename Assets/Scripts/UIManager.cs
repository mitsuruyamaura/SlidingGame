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

    [SerializeField]
    private Button btnJump;

    private PlayerController playerController;


    /// <summary>
    /// スコアの表示更新
    /// </summary>
    /// <param name="score"></param>
    public void UpdateDisplayScore(int score) {
        txtScore.text = score.ToString();
    }

    /// <summary>
    /// ゲーム時間の表示更新
    /// </summary>
    /// <param name="time"></param>
    public void UpdateDisplayGameTime(int time) {
        txtTime.text = time.ToString();
    }

    /// <summary>
    /// 初期設定
    /// </summary>
    /// <param name="playerController"></param>
    public void SetUpUIManager(PlayerController playerController) {
        this.playerController = playerController;
        btnJump.onClick.AddListener(OnClickJump);
    }

    /// <summary>
    /// ジャンプボタンを押した際の処理
    /// </summary>
    private void OnClickJump() {

        // 接地している場合
        if (playerController.GetIsGrounded()) {

            // ジャンプ
            playerController.Jump();
        }      
    }
}
