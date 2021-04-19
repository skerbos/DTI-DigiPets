using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TOWBarFill : MonoBehaviour
{
    public Slider rightSlider;
    public Slider leftSlider;
    public int numberOfButtonPressesToWin = 5;
    public int overallScore = 0;


    public enum TOW
    {
        LeftPlayerId = -1,
        RightPlayerId = 1
    }

    public int? UpdateBar(int LeftOrRightPlayerId)
    {
        overallScore = overallScore + 1 * LeftOrRightPlayerId;
        if(overallScore > 0)
        {
            SetRightBar(overallScore);
        }
        else
        {
            SetLeftBar(overallScore);
        }

        if(System.Math.Abs(overallScore) == numberOfButtonPressesToWin)
        {
            if(overallScore > 0)
            {
                return (int)TOW.RightPlayerId;
            }
            else
            {
                return (int)TOW.LeftPlayerId;
            }
        }

        return null;
    }

    public void SetRightBar(float val)
    {
        rightSlider.value = val / numberOfButtonPressesToWin;
        leftSlider.value = 0;
    }

    public void SetLeftBar(float val)
    {
        leftSlider.value = -val / numberOfButtonPressesToWin;
        rightSlider.value = 0;
    }

    public void ResetBar()
    {
        SetLeftBar(0);
        overallScore = 0;
    }

}
