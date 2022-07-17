using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Doctor : MonoBehaviour
{
    public TextMeshProUGUI Console;
    public PlayerCharacter player;
    public Button NextButton;
    public Button EndButton;
    public Button[] InputButtons;

    public DayCount days;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < InputButtons.Length; i++)
        {
            InputButtons[i].interactable = false;
        }
        Console.text = "You found a Doctor who will heal you for 10c";
        NextButton.gameObject.SetActive(true);
    }

    public void Next()
    {
        NextButton.gameObject.SetActive(false);
        if (player.getHealth() == player.getMaxHealth())
        {
            Console.text = "However, you are already at full health";
            EndButton.gameObject.SetActive(true);
        }
        else if (player.getMoney() < 10)
        {
            Console.text = "However, you do not have enough money";
            EndButton.gameObject.SetActive(true);
        }
        else
        {
            Console.text = "Do you accept their service?";
            for(int i = 0; i < InputButtons.Length; i++)
            {
                InputButtons[i].interactable = true;
            }
        }
    }

    public void Accept()
    {
        player.decMoney(10);

        int HealthGained;
        for(HealthGained = 0; HealthGained < player.getMaxHealth() - player.getHealth(); HealthGained++) { }
        player.addHealth(HealthGained);
        Console.text = "You were healed for " + HealthGained + "hp";
        EndButton.gameObject.SetActive(true);
        for (int i = 0; i < InputButtons.Length; i++)
        {
            InputButtons[i].interactable = false;
        }
        days.incActions(2);
    }

    public void Decline()
    {
        Console.text = "You declined";
        EndButton.gameObject.SetActive(true);
        for (int i = 0; i < InputButtons.Length; i++)
        {
            InputButtons[i].interactable = false;
        }
    }

    public void StateEnd()
    {
        days.incActions(1);
        SceneManager.LoadScene("ScenarioPicker");
    }
}
