using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager uiManager;

    [Header("ゲーム時間")]
    public int gameTime;

    private float timeCounter;

    void Start()
    {
        // ゲーム時間の表示を更新
        uiManager.UpdateDisplayGameTime(gameTime);
    }


    void Update()
    {
        // カウンターを加算
        timeCounter += Time.deltaTime;

        // 1秒経つごとに
        if (timeCounter >= 1.0f) {

            // カウンターを初期化。再度 0 から加算して上記の条件に入るようにする
            timeCounter = 0;

            // ゲーム時間を1秒ずつ減らす
            gameTime--;

            // ゲーム時間が 0 以下になったら
            if (gameTime <= 0) {

                // マイナスにならないように 0 に固定する
                gameTime = 0;
            }

            // ゲーム時間の表示を更新
            uiManager.UpdateDisplayGameTime(gameTime);
        }
    }
}
