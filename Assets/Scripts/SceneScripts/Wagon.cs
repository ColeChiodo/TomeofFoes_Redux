using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Wagon : MonoBehaviour
{
    public DayCount days;

    public TextMeshProUGUI Console;
    public Button NextButton;
    public Button EndButton;
    public Button[] InputButtons;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < InputButtons.Length; i++)
        {
            InputButtons[i].interactable = false;
        }
        Console.text = "You see a wagon down the trail";
        NextButton.gameObject.SetActive(true);
    }

    public void Next()
    {
        days.incActions(1);
        NextButton.gameObject.SetActive(false);
        for (int i = 0; i < InputButtons.Length; i++)
        {
            InputButtons[i].interactable = true;
        }
        Console.text = "Do you approach?";
    }

    public void Accept()
    {
        ScenarioPicked currentScenario = ScenarioPicked.None;
        int temp = Random.Range(1, 3);
        switch (temp)
        {
            case 1:
                currentScenario = ScenarioPicked.Battle;
                break;
            case 2:
                currentScenario = ScenarioPicked.Doctor;
                break;
            case 3:
                currentScenario = ScenarioPicked.Shop;
                break;
            default:
                break;
        }

        switch (currentScenario)
        {
            case ScenarioPicked.Battle:
                SceneManager.LoadScene("Battle");
                break;
            case ScenarioPicked.Doctor:
                SceneManager.LoadScene("Doctor");
                break;
            case ScenarioPicked.Shop:
                SceneManager.LoadScene("Shop");
                break;
            default:
                Debug.Log("ERROR!");
                break;
        }
    }

    public void Decline()
    {
        Console.text = "You moved on";
        EndButton.gameObject.SetActive(true);
        for (int i = 0; i < InputButtons.Length; i++)
        {
            InputButtons[i].interactable = false;
        }
    }

    public void StateEnd()
    {
        SceneManager.LoadScene("ScenarioPicker");
    }
}
