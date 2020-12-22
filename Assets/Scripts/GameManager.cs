using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIManager uiManager;

    public int gameTime;

    private float timeCounter;

    void Start()
    {
        uiManager.UpdateDisplayGameTime(gameTime);
    }


    void Update()
    {
        timeCounter += Time.deltaTime;

        if (timeCounter >= 1.0f) {
            timeCounter = 0;
            gameTime--;

            if (gameTime <= 0) {
                gameTime = 0;
            }

            uiManager.UpdateDisplayGameTime(gameTime);
        }
    }
}
