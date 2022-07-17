using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateCharacterMenu : MonoBehaviour
{
    public PlayerCharacter player;
    public DayCount days;
    public PlayerItems items;
    public PlayerSpells spells;

    public void Start()
    {
        RandomizeStats();
        InitializeDays();
        InitializeItems();
        InitializeSpells();
    }
    public void BackGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextScene(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void RandomizeStats()
    {
        player.GenerateRandomStats();
    }

    public void InitializeDays()
    {
        days.Initialize();
    }

    public void InitializeItems()
    {
        items.Initialize();
    }

    public void InitializeSpells()
    {
        spells.Initialize();
    }
}
