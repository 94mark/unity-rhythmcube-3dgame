using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] goGameUI = null;
    [SerializeField] GameObject goTitleUI = null;

    public static GameManager instance;

    public bool isStartGame = false;

    ComboManager theCombo;
    ScoreManager theScore;
    TimingManager thetiming;
    StatusManager theStatus;
    PlayerController thePlayer;
    StageManager theStage;

    void Start()
    {
        instance = this;
        theStage = FindObjectOfType<StageManager>();
        theCombo = FindObjectOfType<ComboManager>();
        theScore = FindObjectOfType<ScoreManager>();
        thetiming = FindObjectOfType<TimingManager>();
        theStatus = FindObjectOfType<StatusManager>();
        thePlayer = FindObjectOfType<PlayerController>();
    }

    public void GameStart()
    {
        for(int i = 0; i < goGameUI.Length; i++)
        {
            goGameUI[i].SetActive(true);
        }

        theStage.RemoveStage();
        theStage.SettingStage();
        theCombo.ResetCombo();
        theScore.Initialized();
        thetiming.Initialized();
        theStatus.Initialized();
        thePlayer.Initialized();

        isStartGame = true;
    }

    public void MainMenu()
    {
        for (int i = 0; i < goGameUI.Length; i++)
        {
            goGameUI[i].SetActive(false);
        }

        goTitleUI.SetActive(true);
    }
}
