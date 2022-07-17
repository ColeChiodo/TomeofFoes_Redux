using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    static string Name = "Default Name";
    static int MaxHealth;
    static int MaxMana;
    static string Element = "Fire";

    static public int Health;
    static public int Mana;
    static public int Strength;
    static public int Magic;
    static public int Speed;

    static public int Money;

    static public int Level;
    static public int Exp;
    static public int MaxExp;

    public void GenerateRandomStats()
    {
        MaxHealth = Random.Range(0, 6) + 14;
        Health = MaxHealth;
        MaxMana = Random.Range(0, 6) + 14;
        Mana = MaxMana;
        Strength = Random.Range(0, 10) + 5;
        Magic = Random.Range(0, 10) + 5;
        Speed = Random.Range(0, 15) + 5;

        Money = 10;

        Level = 1;
        Exp = 0;
        MaxExp = 10;
    }

    public void incExp(int n)
    {
        for(int i = 0; i < n; i++)
        {
            Exp++;
            if(Exp >= MaxExp)
                LevelUp();
        }
    }

    public void LevelUp()
    {
        int oldHPDiff = MaxHealth - Health;
        int oldManaDiff = MaxMana - Mana;

        //increase 3-5 random stats
        int numberOfStatIncrease = Random.Range(3, 5);
        for (int i = 0; i < numberOfStatIncrease; i++)
        {
            int temp = Random.Range(1, 5);
            switch (temp)
            {
                case 1:
                    MaxHealth++;
                    Health++;
                    break;
                case 2:
                    MaxMana++;
                    Mana++;
                    break;
                case 3:
                    addStrength(1);
                    break;
                case 4:
                    addMagic(1);
                    break;
                case 5:
                    addSpeed(1);
                    break;
                default:
                    Debug.Log("levelupError");
                    break;
            }
        }
        //inc level, reset exp, inc maxexp
        Level++;
        MaxExp += 2;
        Exp = 0;
    }

    public bool isDead()
    {
        if (Health <= 0)
            return true;
        else
            return false;
    }

    // Setters and Getters
    public void setName(string n)
    {
        Name = n;
    }

    public void setElement(string e)
    {
        Element = e;
        Debug.Log("Element Set To: " + e);
    }

    public void subHealth(int n)
    {
        Health -= n;
    }

    public void addHealth(int n)
    {
        Health += n;
    }

    public void subMana(int n)
    {
        Mana -= n;
    }

    public void addMana(int n)
    {
        Mana += n;
    }

    public void addStrength(int n)
    {
        Strength += n;
    }

    public void addMagic(int n)
    {
        Magic += n;
    }

    public void addSpeed(int n)
    {
        Speed += n;
    }

    public void setMana(int m)
    {
        Mana = m;
    }

    public void setStrength(int s)
    {
        Strength = s;
    }

    public void setMagic(int m)
    {
        Magic = m;
    }

    public void setSpeed(int s)
    {
        Speed = s;
    }

    public string getName()
    {
        return Name;
    }

    public string getElement()
    {
        return Element;
    }

    public int getHealth()
    {
        return Health;
    }

    public int getMaxHealth()
    {
        return MaxHealth;
    }

    public int getMana()
    {
        return Mana;
    }

    public int getMaxMana()
    {
        return MaxMana;
    }

    public int getStrength()
    {
        return Strength;
    }

    public int getMagic()
    {
        return Magic;
    }

    public int getSpeed()
    {
        return Speed;
    }

    public int getLevel()
    {
        return Level;
    }

    public int getMaxExp()
    {
        return MaxExp;
    }

    public int getExp()
    {
        return Exp;
    }

    public int getMoney()
    {
        return Money;
    }

    public void incMoney(int n)
    {
        Money += n;
    }

    public void decMoney(int n)
    {
        Money -= n;
    }
}
