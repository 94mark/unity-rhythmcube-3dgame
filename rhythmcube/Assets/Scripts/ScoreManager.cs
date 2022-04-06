using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text txtScore = null;
    [SerializeField] int increaseScore = 10;
    int currentScore = 0;

    [SerializeField] float[] weight = null;
    [SerializeField] int comboBonusScore = 10;

    Animator myAnim;
    string animScoreUp = "ScoreUp";

    ComboManager theCombo;

    void Start()
    {
        theCombo = FindObjectOfType<ComboManager>();
        myAnim = GetComponent<Animator>();
        currentScore = 0;
        txtScore.text = "0";        
    }

    public void Initialized()
    {
        currentScore = 0;
        txtScore.text = "0";
    }

    public void IncreaseScore(int p_JudgementState)
    {
        //콤보 증가
        theCombo.IncreaseCombo();

        //콤보 보너스 점수 계산
        int t_currentCombo = theCombo.GetCurrentCombo();
        int t_bonusComboScore = (t_currentCombo / 10) * comboBonusScore;

        //가중치 계산
        int t_increaesScore = increaseScore + t_bonusComboScore;
        t_increaesScore = (int)(t_increaesScore * weight[p_JudgementState]);

        //점수 반영
        currentScore += t_increaesScore;
        txtScore.text = string.Format("{0:#,##0}", currentScore);

        myAnim.SetTrigger(animScoreUp); 
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}
