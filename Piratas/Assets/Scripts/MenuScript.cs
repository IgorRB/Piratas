using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    GameObject mainPanel, opitionsPanel;

    [SerializeField]
    TMP_Text timerTxt, spawnTxt;

    int timerValue = 1, spawnValue = 1;

    [SerializeField]
    GameDataSO gameData;



    // Start is called before the first frame update
    void Start()
    {
        timerValue = gameData.timerValue;
        spawnValue = gameData.spawnValue;

        switch (spawnValue)
        {
            case 1:
                spawnTxt.text = "Slow";
                break;

            case 2:
                spawnTxt.text = "Normal";
                break;

            case 3:
                spawnTxt.text = "Fast";
                break;            
        }

        if (timerValue == 1)
            timerTxt.text = timerValue.ToString() + " min";
        else
            timerTxt.text = timerValue.ToString() + " mins";
    }

    public void ClickPlay()
    {
        SceneManager.LoadScene("Game");
    }

    public void ClickOptions()
    {
        mainPanel.SetActive(false);
        opitionsPanel.SetActive(true);
    }

    public void ClickBack()
    {
        mainPanel.SetActive(true);
        opitionsPanel.SetActive(false);
    }

    public void ClickTimeLower()
    {
        if (timerValue == 1)
            return;

        timerValue--;
        gameData.timerValue = timerValue;
        if (timerValue == 2)
            timerTxt.text = timerValue.ToString() + " mins";
        else
            timerTxt.text = timerValue.ToString() + " min";
    }
    public void ClickTimeHigher()
    {
        if (timerValue == 3)
            return;

        timerValue++;
        gameData.timerValue = timerValue;
        timerTxt.text = timerValue.ToString() + " mins";
    }

    public void ClickSpanwLower()
    {
        if (spawnValue == 1)
            return;

        spawnValue--;
        gameData.spawnValue = spawnValue;
        if (spawnValue == 2)
            spawnTxt.text = "Normal";
        else
            spawnTxt.text = "Slow";
    }

    public void ClickSpanwHigher()
    {
        if (spawnValue == 3)
            return;

        spawnValue++;
        gameData.spawnValue = spawnValue;
        if (spawnValue == 2)
            spawnTxt.text = "Normal";
        else
            spawnTxt.text = "Fast";
    }
}
