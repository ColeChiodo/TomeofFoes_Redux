using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeText : MonoBehaviour
{
    public DayCount days;
    [SerializeField] string type;
    [SerializeField] PlayerCharacter player;
    TextMeshProUGUI textMesh;
    int value;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        setValue();
    }

    void setValue()
    {
        if (type == "Health")
        {
            value = player.getHealth();
            int max = player.getMaxHealth();
            textMesh.text = value.ToString() + "/" + max;
        }
        else if (type == "Mana")
        {
            value = player.getMana();
            int max = player.getMaxMana();
            textMesh.text = value.ToString() + "/" + max;
        }
        else if (type == "Strength")
        {
            value = player.getStrength();
            textMesh.text = value.ToString();
        }
        else if (type == "Magic")
        {
            value = player.getMagic();
            textMesh.text = value.ToString();
        }
        else if (type == "Speed")
        {
            value = player.getSpeed();
            textMesh.text = value.ToString();
        }
        else if (type == "Money")
        {
            value = player.getMoney();
            textMesh.text = value.ToString() +"c";
        }
        else if (type == "Day")
        {
            value = days.getCurrentDay();
            textMesh.text = "DAY" + value.ToString();
        }
        else if(type == "Level")
        {
            value = player.getLevel();
            textMesh.text = value.ToString();
        }
        else if (type == "Name")
            textMesh.text = player.getName();
    }

    void Update()
    {
        setValue();
    }
}
