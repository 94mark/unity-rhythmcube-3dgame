using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    [SerializeField] GameObject GoComboImage = null;
    [SerializeField] Text txtCombo = null;

    int currentCombo = 0;

    Animator myAnim;
    string animComboUp = "ComboUp";

    void Start()
    {
        myAnim = GetComponent<Animator>();
        txtCombo.gameObject.SetActive(false);
        GoComboImage.SetActive(false);
    }

    public void IncreaseCombo(int p_num = 1)
    {
        currentCombo += p_num;
        txtCombo.text = string.Format("{0:#,##0}", currentCombo);

        if(currentCombo > 2)
        {
            txtCombo.gameObject.SetActive(true);
            GoComboImage.SetActive(true);

            myAnim.SetTrigger(animComboUp);
        }
    }

    public int GetCurrentCombo()
    {
        return currentCombo;
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        txtCombo.text = "0";
        txtCombo.gameObject.SetActive(false);
        GoComboImage.SetActive(false);
    }
}
