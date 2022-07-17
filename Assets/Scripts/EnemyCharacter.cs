using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo, crits give extra turn

public class EnemyCharacter : MonoBehaviour
{
    string Name = "Default Name";
    int Health;
    int MaxHealth;
    int Mana;
    int Strength;
    int Magic;
    int Speed;
    int index;

    public void GenerateBoss()
    {
        MaxHealth = Random.Range(0, 10) + 20;
        Health = MaxHealth;
        Mana = Random.Range(0, 6) + 5;
        Strength = Random.Range(0, 3) + 5;
        Magic = Random.Range(0, 6) + 5;
        Speed = Random.Range(0, 6) + 5;
    }

    public void GenerateRandomStats()
    { 
        MaxHealth = Random.Range(0, 6) + 5;
        Health = MaxHealth;
        Mana = Random.Range(0, 6) + 5;
        Strength = Random.Range(0, 3) + 5;
        Magic = Random.Range(0, 6) + 5;
        Speed = Random.Range(0, 6) + 5;
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

    public void subHealth(int h)
    {
        Health -= h;
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

    public void setIndex(int i)
    {
        index = i;
    }

    public string getName()
    {
        return Name;
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

    public int getIndex()
    {
        return index;
    }
}
