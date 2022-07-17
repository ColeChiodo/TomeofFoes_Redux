using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public Item[] items;
    static public int[] amount;

    public PlayerCharacter player;

    public void Initialize()
    {
        amount = new int[items.Length];
        for (int i = 0; i < items.Length; i++)
            amount[i] = 0;

        amount[0] = 3;
        amount[1] = 1;
    }

    public int getAmount(int i)
    {
        return amount[i];
    }

    public int UseItem(int i)
    {
        int Gained = 0;
        if (amount[i] == 0)
            return Gained;
        switch (items[i].type)
        {
            case "Health":
                for (int j = 0; j < items[i].value; j++)
                    if (player.getHealth() != player.getMaxHealth())
                    {
                        player.addHealth(1);
                        Gained++;
                    }
                break;
            case "Mana":
                for (int j = 0; j < items[i].value; j++)
                    if (player.getMana() != player.getMaxMana())
                    {
                        player.addMana(1);
                        Gained++;
                    }
                break;
            case "Strength":
                for (int j = 0; j < items[i].value; j++)
                {
                    player.addStrength(1);
                    Gained++;
                }
                break;
            case "Speed":
                for (int j = 0; j < items[i].value; j++)
                {
                    player.addSpeed(1);
                    Gained++;
                }
                break;
            default:
                break;
        }
        if (Gained > 0)
            amount[i]--;
        return Gained;
    }

    public bool RecieveItem(int i)
    {
        bool Success = false;

        if (amount[i] != 99)
        {
            Success = true;
            amount[i]++;
        }

        return Success;
    }

    public bool hasItem(int i)
    {
        if (amount[i] != 0)
            return true;
        return false;
    }
}
