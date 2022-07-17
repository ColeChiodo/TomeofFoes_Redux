using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public PlayerCharacter player;

    public void SetMax(int val)
    {
        slider.maxValue = val;
        slider.value = val;
    }

    public void SetValue(int val)
    {
        slider.value = val;
    }

    private void Start()
    {
        SetMax(player.getMaxHealth());
    }

    private void Update()
    {
        SetValue(player.getHealth());
    }
}
