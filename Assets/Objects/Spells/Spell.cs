using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Spell")]

public class Spell : ScriptableObject
{
    public int id;
    public new string name;
    public string description;
    public string type;
    public int mana;
    public int baseValue;
}
