using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Town : MonoBehaviour
{
    public DayCount days;
    public Button[] InteractionButtons;
    public Button NextButton;
    public Button EndButton;
    public TextMeshProUGUI Console;

    void Start()
    {
        EndButton.gameObject.SetActive(false);
        for (int i = 0; i < InteractionButtons.Length; i++)
            InteractionButtons[i].gameObject.SetActive(false);
        Console.text = "You walk into a town";
        NextButton.gameObject.SetActive(true);
    }

    public void Next()
    {
        NextButton.gameObject.SetActive(false);
        int temp = Random.Range(1, 10);
        switch (temp)
        {
            case 1:
                SceneManager.LoadScene("Battle");
                break;
            default:
                Console.text = "What will you do?";
                for (int i = 0; i < InteractionButtons.Length; i++)
                    InteractionButtons[i].gameObject.SetActive(true);
                break;
        }
    }

    public void SkipDay()
    {
        days.SkipDay();
        Console.text = "You sleep for the night";
        for (int i = 0; i < InteractionButtons.Length; i++)
            InteractionButtons[i].interactable = false;
        EndButton.gameObject.SetActive(true);
    }

    public void Doctor()
    {
        SceneManager.LoadScene("Doctor");
    }

    public void Shop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void Leave()
    {
        days.incActions(1);
        Console.text = "You move on";
        for (int i = 0; i < InteractionButtons.Length; i++)
            InteractionButtons[i].interactable = false;
        EndButton.gameObject.SetActive(true);
    }

    public void SceneEnd()
    {
        SceneManager.LoadScene("ScenarioPicker");
    }
}