using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public TextMeshProUGUI Console;
    public PlayerCharacter player;
    public PlayerItems items;
    public Button NextButton;
    public Button EndButton;
    public Button[] InputButtons;
    public GameObject amountUI;

    int ClickedID;
    int amount;

    public DayCount days;

    // Start is called before the first frame update
    void Start()
    {
        Console.text = "You found a merchant";
        NextButton.gameObject.SetActive(true);
        for (int i = 0; i < InputButtons.Length; i++)
            InputButtons[i].interactable = false;
    }

    public void Next()
    {
        Console.text = "What will you buy?";
        NextButton.gameObject.SetActive(false);
        for (int i = 0; i < InputButtons.Length; i++)
            InputButtons[i].interactable = true;
    }

    public void Leave()
    {
        Console.text = "You left";
        EndButton.gameObject.SetActive(true);
        for (int i = 0; i < InputButtons.Length; i++)
            InputButtons[i].interactable = false;
    }

    public void BuyHealth()
    {
        ButtonClicked();
        ClickedID = 0;
    }

    public void BuyMana()
    {
        ButtonClicked();
        ClickedID = 1;
    }

    public void BuyStrength()
    {
        ButtonClicked();
        ClickedID = 2;
    }

    public void BuySpeed()
    {
        ButtonClicked();
        ClickedID = 3;
    }

    void ButtonClicked()
    {
        amount = 1;
        amountUI.gameObject.SetActive(true);
        for (int i = 0; i < InputButtons.Length; i++)
            InputButtons[i].interactable = false;
        ChangeUI();
    }

    public void incAmount()
    {
        if(items.getAmount(ClickedID) + amount + 1 != 100)
            amount++;
        ChangeUI();
    }

    public void decAmount()
    {
        if (items.getAmount(ClickedID) - amount - 1 != 0)
            amount--;
        ChangeUI();
    }

    public void ChangeUI()
    {
        amountUI.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = amount.ToString();
        amountUI.transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = (items.items[ClickedID].price * amount).ToString();
    }

    public void Back()
    {
        for (int i = 0; i < InputButtons.Length; i++)
            InputButtons[i].interactable = true;
        amountUI.gameObject.SetActive(false);
    }

    public void FinalizePurchase()
    {
        if (player.getMoney() - (items.items[ClickedID].price * amount) < 0)
            Console.text = "Not enough money";
        else
        {
            Console.text = "You purchased " + amount + " " + items.items[ClickedID].name;
            if (amount > 1)
                Console.text += "s";
            player.decMoney(items.items[ClickedID].price * amount);
            for (int i = 0; i < amount; i++)
                items.RecieveItem(ClickedID);
            NextButton.gameObject.SetActive(true);
            amountUI.gameObject.SetActive(false);
        }

    }

    public void StateEnd()
    {
        days.incActions(2);
        SceneManager.LoadScene("ScenarioPicker");
    }
}
