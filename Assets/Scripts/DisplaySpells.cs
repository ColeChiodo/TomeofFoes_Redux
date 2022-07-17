using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplaySpells : MonoBehaviour
{
    public PlayerSpells playerSpells;
    public GameObject SpellMenu;
    public TextMeshProUGUI Console;

    public void Display()
    {

        for(int i = 0; i < SpellMenu.transform.childCount; i++)
            if (SpellMenu.transform.GetChild(i).CompareTag("SpellButton"))
            {
                Transform button = SpellMenu.transform.GetChild(i);
                Spell spell = playerSpells.GetSpell(i);
                if (spell != null)
                {
                    button.GetChild(0).GetComponent<TextMeshProUGUI>().text = spell.name;
                    button.GetChild(1).GetComponent<TextMeshProUGUI>().text = spell.description;
                    button.GetChild(2).GetComponent<TextMeshProUGUI>().text = spell.mana.ToString();
                }
                else
                    button.GetComponent<Button>().interactable = false;
            }
    }
}
