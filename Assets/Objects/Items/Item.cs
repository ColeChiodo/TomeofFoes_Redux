using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public Sprite icon;

    public int id;
    public new string name;
    public string description;
    public string type;
    public int value;

    public int price;
}
