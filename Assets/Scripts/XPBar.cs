using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxXP(float xp, float maxXP)
    {
        slider.maxValue = maxXP;
        slider.value = xp;
    }
    
    public void SetXP(float xp)
    {
        slider.value = xp;
    }
}
