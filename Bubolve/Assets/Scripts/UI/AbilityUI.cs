using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
    public TextMeshProUGUI number;
    public Slider cooldown;
    public Image mainImage;
    public Image activeImage;

    public Color runningColor;
    public Color endedColor;
    public Color readyColor;



    public void SetData(int number, float cooldown, bool ended, bool ready, bool active)
    {
        this.cooldown.value = cooldown;
        this.number.text = number.ToString();
        if (ready)
        {
            mainImage.color = readyColor;
        } else if(!ready && !ended)
        {
            mainImage.color = runningColor;
        } else
        {
            mainImage.color = endedColor;
        }

        if (active)
        {
            activeImage.color = readyColor;
        } else
        {
            activeImage.color = endedColor;
        }
    }
}
