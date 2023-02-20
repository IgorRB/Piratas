using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HudController : MonoBehaviour
{
    public static HudController HC { get; private set; }

    int score;
    float time;
    bool isOver = false;

    [SerializeField]
    GameDataSO data;

    [SerializeField]
    TMP_Text tmpTimer, tmpScore, tmpScoreGameOver;

    [SerializeField]
    GameObject mainHud, gameOverHud;

    private void Awake()
    {
        HC = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        time = 60 * data.timerValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOver)
        {
            int min = 0, sec = 0;
            time -= Time.deltaTime;
            min = Mathf.RoundToInt(time) / 60;
            sec = Mathf.RoundToInt(time) % 60;

            string stime;
            if(sec>9)
                stime = "Timer " + min.ToString() + ":" + sec.ToString();
            else
                stime = "Timer " + min.ToString() + ":0" + sec.ToString();

            tmpTimer.text = stime;

            if (time <= 0f)
            {
                GameEnd();
            }
        }
    }

    public void AddScore(int value)
    {
        score += value;

        tmpScore.text = "Score " + score.ToString();
    }

    public bool IsOver()
    {
        return isOver;
    }

    public void GameEnd()
    {
        isOver = true;
        tmpScoreGameOver.text = score.ToString();
        mainHud.SetActive(false);
        gameOverHud.SetActive(true);
    }

    public void ClickMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ClickPlayAgain()
    {
        SceneManager.LoadScene("Game");
    }
}
