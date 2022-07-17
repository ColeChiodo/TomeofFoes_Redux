using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public static class ButtonExt
{
    public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate ()
        {
            OnClick(param);
        });
    }
}

public class DisplayItems : MonoBehaviour
{
    public PlayerItems pi;
    public TextMeshProUGUI Console;
    public GameObject buttonTemplate;

    public void ShowItems()
    {
        buttonTemplate.gameObject.SetActive(true);

        for (int i = 0; i < transform.childCount; i++)
            Destroy(transform.GetChild(i).gameObject);

        GameObject g;
        for (int i = 0; i < pi.items.Length; i++)
            if (pi.hasItem(i))
            {
                g = Instantiate(buttonTemplate, transform);
                g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pi.items[i].name;
                g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = pi.items[i].description;
                g.transform.GetChild(2).GetComponent<Image>().sprite = pi.items[i].icon;
                g.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "x" + pi.getAmount(i);

                g.GetComponent<Button>().AddEventListener(i, ItemClicked);
            }
        buttonTemplate.gameObject.SetActive(false);
    }

    void ItemClicked (int i)
    {
        if(pi.UseItem(i) == 0)
        {
            Console.text = "Can't use item";
        }
        else
        {
            Console.text = "You used a " + pi.items[i].name;
        }
    }
}
