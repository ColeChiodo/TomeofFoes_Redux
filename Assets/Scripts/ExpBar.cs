using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
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
        SetMax(player.getMaxExp());
    }

    private void Update()
    {
        SetValue(player.getExp());
    }
}
