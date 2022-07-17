using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

enum ScenarioPicked { None, Battle, Town, Doctor, Shop, Wagon }

public class ScenarioPicker : MonoBehaviour
{
    public DayCount days;
    public TextMeshProUGUI DayCount;
    public Button skipButton;

    // Start is called before the first frame update
    void Start()
    {
        skipButton.gameObject.SetActive(false);
        if(days.getPrevDay() < days.getCurrentDay())
        {
            StartCoroutine(Wait());
        }
        else 
        {
            DayCount.text = "";
            ScenarioChanger();
        }
    }

    private IEnumerator Wait()
    {
        DayCount.text = "DAY " + days.getCurrentDay();
        skipButton.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(5);
        ScenarioChanger();
    }

    public void ScenarioChanger()
    {
        if (days.getCurrentDay() % 10 == 0)
        {
            SceneManager.LoadScene("Battle");
        }
        else
        {
            ScenarioPicked currentScenario = ScenarioPicked.None;
            int _scenario = Random.Range(1, 9);
            if (_scenario >= 1 && _scenario <= 4)
                currentScenario = ScenarioPicked.Battle;
            else if (_scenario >= 5 && _scenario <= 6)
                currentScenario = ScenarioPicked.Town;
            else if (_scenario >= 7 && _scenario <= 9)
                currentScenario = ScenarioPicked.Wagon;

            switch (currentScenario)
            {
                case ScenarioPicked.Battle:
                    SceneManager.LoadScene("Battle");
                    break;
                case ScenarioPicked.Town:
                    SceneManager.LoadScene("Town");
                    break;
                case ScenarioPicked.Wagon:
                    SceneManager.LoadScene("Wagon");
                    break;
                default:
                    Debug.Log("ERROR!");
                    break;
            }
        }
    }
}
