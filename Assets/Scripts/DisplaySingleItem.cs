using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplaySingleItem : MonoBehaviour
{
    public Item item;
    //public GameObject amountMenu;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.name;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.description;
        transform.GetChild(2).GetComponent<Image>().sprite = item.icon;
        if (item.price == 0)
            transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "FREE";
        else if(item.price < 10)
            transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "0" + item.price.ToString();
        else
            transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = item.price.ToString();
        //name, desc, icon, moneyicon, price
    }
}
