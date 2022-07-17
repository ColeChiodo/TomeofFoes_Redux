using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum BattleState { NONE, PLAYER, ENEMY, WON, LOST}

public class Battle : MonoBehaviour
{
    public DayCount days;

    public BattleState bs = BattleState.NONE;
    GameObject[] order;
    int OrderIndex = 0;

    public GameObject ApproachUI;
    public GameObject BattleUI;
    public GameObject SelectEnemyUI;

    public Button BossBattleNext;

    public TextMeshProUGUI BattleConsoleText;
    public TextMeshProUGUI SelectionText;
    public Button[] InputButtons;
    public Button NextTurnButton;

    public Button EndStateButton;
    public Button QuitGameButton;
    public Button SuccessButton;
    public Button FailedButton;

    public GameObject playerPrefab;
    GameObject playerGO;
    PlayerCharacter player;
    PlayerSpells playerSpells;

    public int currentSelection;

    public GameObject enemyPrefab;
    const int MaxEnemy = 3;
    GameObject[] enemyGO;
    EnemyCharacter[] enemy;
    public Button enemySelection;

    void Start()
    {
        if(days.getCurrentDay() % 10 == 0)
            BossSetupBattle();
        else
            SetupBattle();
    }

    void BossSetupBattle()
    {
        for (int i = 0; i < InputButtons.Length; i++)
        {
            InputButtons[i].interactable = false;
        }

        playerGO = Instantiate(playerPrefab);
        player = playerGO.GetComponent<PlayerCharacter>();

        enemyGO = new GameObject[Random.Range(0, MaxEnemy) + 1];
        enemy = new EnemyCharacter[enemyGO.Length];

        //boss
        enemyGO[0] = Instantiate(enemyPrefab);
        enemy[0] = enemyGO[0].GetComponent<EnemyCharacter>();
        enemy[0].GenerateBoss();
        enemy[0].setIndex(0);
        enemy[0].setName("BOSS");

        //smaller enemies
        string EnemyType = "Bandit";
        for (int i = 1; i < enemyGO.Length; i++)
        {
            enemyGO[i] = Instantiate(enemyPrefab);
            enemy[i] = enemyGO[i].GetComponent<EnemyCharacter>();
            enemy[i].GenerateRandomStats();
            enemy[i].setIndex(i);
            enemy[i].setName(EnemyType + " " + (i));
            if (enemyGO.Length == 2)
                enemy[0].setName(EnemyType);
        }

        currentSelection = 0;
        SelectionText.text = currentSelection.ToString();

        BattleConsoleText.text = "A " + enemy[0].getName() + " stands before you";
        ApproachUI.SetActive(false);
        BattleUI.SetActive(true);
        BossBattleNext.gameObject.SetActive(true);
    }

    void SetupBattle()
    {
        for (int i = 0; i < InputButtons.Length; i++)
        {
            InputButtons[i].interactable = false;
        }

        //instantiate player
        playerGO = Instantiate(playerPrefab);
        player = playerGO.GetComponent<PlayerCharacter>();

        //generate 1-3 enemies
        enemyGO = new GameObject[Random.Range(0, MaxEnemy) + 1];
        enemy = new EnemyCharacter[enemyGO.Length];
        string EnemyType = "Bandit";
        for(int i = 0; i < enemyGO.Length; i++)
        {
            enemyGO[i] = Instantiate(enemyPrefab);
            enemy[i] = enemyGO[i].GetComponent<EnemyCharacter>();
            enemy[i].GenerateRandomStats();
            enemy[i].setIndex(i);
            enemy[i].setName(EnemyType + " " + (i + 1));
            if (enemyGO.Length == 1)
                enemy[0].setName(EnemyType);
        }

        currentSelection = 0;
        SelectionText.text = currentSelection.ToString();

        if (enemyGO.Length > 1)
            BattleConsoleText.text = enemyGO.Length.ToString() + " " + EnemyType + "s stand before you";
        else
            BattleConsoleText.text = "A " + enemy[0].getName() + " stands before you";
    }

    public void BattleStatePicker()
    {
        //reset order index if larger than length
        if (OrderIndex >= order.Length)
            OrderIndex = 0;

        //if current selection is a dead enemy, go to next in order, until finds alive enemy OR player
        while (true)
        {
            if (order[OrderIndex].CompareTag("Enemy"))
            {
                if (order[OrderIndex].GetComponent<EnemyCharacter>().isDead())
                {
                    OrderIndex++;
                    //reset order index if larger than length
                    if (OrderIndex >= order.Length)
                        OrderIndex = 0;
                }
                else
                    currentSelection = order[OrderIndex].GetComponent<EnemyCharacter>().getIndex();
                    break;
            }
            else
                break;
        }

        //get object type from order array
        if (order[OrderIndex].CompareTag("Player"))
            bs = BattleState.PLAYER;
        else
            bs = BattleState.ENEMY;

        //check for battle end
        BattleState tmp = bs;
        bs = BattleState.WON;
        for (int i = 0; i < enemyGO.Length; i++)
            if (!enemy[i].isDead())
            {
                bs = tmp;
                break;
            }
        if (player.isDead())
            bs = BattleState.LOST;
        switch (bs)
        {
            case BattleState.PLAYER:
                PlayerTurn();
                break;
            case BattleState.ENEMY:
                EnemyTurn(enemyGO[order[OrderIndex].GetComponent<EnemyCharacter>().getIndex()]);
                break;
            case BattleState.WON:
                PlayerWins();
                break;
            case BattleState.LOST:
                GameOver();
                break;
            default:
                Debug.Log("ERROR!");
                break;
        }

        //inc order index
        OrderIndex++;
    }      

    public void ApproachAttack()
    {
        //populate order array
        PopulateOrderArray();

        //sort order array
        SortOrderArray();

        BattleConsoleText.text = "You approach and get ready for battle!";
        NextTurnButton.gameObject.SetActive(true);
    }

    public void SneakAttack()
    {
        //populate order array
        PopulateOrderArray();

        //sort order array
        SortOrderArray();

        int temp = Random.Range(0, 10);
        bool SuccessfulAttempt = temp <= 8 ? true : false;
        //if successful, player takes 1 free turn
        if (SuccessfulAttempt)
        {
            BattleConsoleText.text = "You successfully snuck behind the enemy!";
            SuccessButton.gameObject.SetActive(true);
        }
        else //if not, all enemy goes first once
        {
            BattleConsoleText.text = "The enemy caught you trying to sneak behind them";
            FailedButton.gameObject.SetActive(true);
        }
    }

    public void RunAway() //todo runaway based on a player stat
    {
        if (days.getCurrentDay() % 10 == 0)
        {
            BattleConsoleText.text = "You cannot run away";
        }
        else
        {
            //populate order array
            PopulateOrderArray();

            //sort order array
            SortOrderArray();

            int temp = Random.Range(0, 10);
            bool SuccessfulAttempt = temp <= 8 ? true : false;
            //if successful, player runs away
            if (SuccessfulAttempt)
            {
                for (int i = 0; i < InputButtons.Length; i++)
                {
                    InputButtons[i].interactable = false;
                }
                BattleConsoleText.text = "You have Successfully Ran Away!";
                EndStateButton.gameObject.SetActive(true);
            }
            else //if not, all enemy goes first once
            {
                BattleConsoleText.text = "The enemy caught you trying to sneak around them";
                FailedButton.gameObject.SetActive(true);
            }
        }
    }

    public void PlayerTurn()
    {
        NextTurnButton.gameObject.SetActive(false);
        SuccessButton.gameObject.SetActive(false);

        //un-hide ui
        BattleConsoleText.text = "Choose An Action!";
        for(int i = 0; i < InputButtons.Length; i++)
        {
            InputButtons[i].interactable = true;
        }

        //check if current selected enemy is alive
        if (enemy[currentSelection].isDead())
            incEnemySelection();
    }

    void EnemyTurn(GameObject e)
    {
        //Enemy Attacks
        for (int i = 0; i < InputButtons.Length; i++)
        {
            InputButtons[i].interactable = false;
        }
        if(!e.GetComponent<EnemyCharacter>().isDead())
            Attack(e, playerGO);
    }

    public void StateEnd()
    {
        days.incActions(1);
        SceneManager.LoadScene("ScenarioPicker");
    }

    public void FailedAttack()
    {
        NextTurnButton.gameObject.SetActive(false);
        FailedButton.gameObject.SetActive(false);
        EnemyTurn(enemyGO[0]);
    }

    void PlayerWins()
    {
        int GainedMoney = 0;
        int GainedExp = 0;
        if (days.getCurrentDay() % 10 == 0)
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                GainedMoney += Random.Range(8, 12);
                GainedExp = Random.Range(20, 25);
            }
            days.SkipDay();
        }
        else
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                GainedMoney += Random.Range(2, 5);
                GainedExp += Random.Range(3, 5);
            }
            days.incActions(4);
        }
        player.incMoney(GainedMoney);
        player.incExp(GainedExp);
        BattleConsoleText.text = "YOU WIN!\nYou gained " + GainedMoney + "c and " + GainedExp + "exp";
        NextTurnButton.gameObject.SetActive(false);
        EndStateButton.gameObject.SetActive(true);
    }

    void GameOver()
    {
        //PlayerCharacter died
        BattleConsoleText.text = "YOU HAVE LOST...";
        NextTurnButton.gameObject.SetActive(false);
        QuitGameButton.gameObject.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void Attack(GameObject attacker, GameObject defender) //todo: miss/crit detection based on playerstat
    {
        int DMGDealt;
        string EnemyName;
        int AttackerStr;

        int temp = Random.Range(0, 20);
        bool Missed = temp <= 2 ? true : false;

        temp = Random.Range(0, 10);
        bool Crit = temp <= 1 ? true : false;

        if (attacker.CompareTag("Player")) //if player attacks
        {
            AttackerStr = attacker.GetComponent<PlayerCharacter>().getStrength();
            EnemyName = defender.GetComponent<EnemyCharacter>().getName();
            if (Missed)
            {
                BattleConsoleText.text = "You Missed";
            }
            else if (Crit)
            {
                DMGDealt = Random.Range(AttackerStr / 2, AttackerStr) * 2;
                defender.GetComponent<EnemyCharacter>().subHealth(DMGDealt);
                BattleConsoleText.text = "You have critically hit " + EnemyName + " for " + DMGDealt + " damage!";
                if (enemyGO.Length == 1)
                    BattleConsoleText.text = "You have critically hit the " + EnemyName + " for " + DMGDealt + " damage!";
            }
            else
            {
                DMGDealt = Random.Range(AttackerStr / 2, AttackerStr);
                defender.GetComponent<EnemyCharacter>().subHealth(DMGDealt);
                BattleConsoleText.text = "You have attacked " + EnemyName + " for " + DMGDealt + " damage!";
                if (enemyGO.Length == 1)
                    BattleConsoleText.text = "You have attacked the " + EnemyName + " for " + DMGDealt + " damage!";
            }
        }
        else //if enemy attacks
        {
            if (!attacker.GetComponent<EnemyCharacter>().isDead())
            {
                AttackerStr = attacker.GetComponent<EnemyCharacter>().getStrength();
                EnemyName = attacker.GetComponent<EnemyCharacter>().getName();
                if (Missed)
                {
                    BattleConsoleText.text = EnemyName + " Missed";
                    if(enemyGO.Length == 1)
                        BattleConsoleText.text = "The " + EnemyName + " Missed";
                }
                else if (Crit)
                {
                    DMGDealt = Random.Range(AttackerStr / 2, AttackerStr) * 2;
                    defender.GetComponent<PlayerCharacter>().subHealth(DMGDealt);
                    BattleConsoleText.text = "You have been critically hit by " + EnemyName + " for " + DMGDealt + " health!";
                    if (enemyGO.Length == 1)
                        BattleConsoleText.text = "You have been critically hit by the " + EnemyName + " for " + DMGDealt + " health!";
                }
                else
                {
                    DMGDealt = Random.Range(AttackerStr / 2, AttackerStr);
                    defender.GetComponent<PlayerCharacter>().subHealth(DMGDealt);
                    BattleConsoleText.text = "You have been hit by " + EnemyName + " for " + DMGDealt + " health!";
                    if (enemyGO.Length == 1)
                        BattleConsoleText.text = "You have been hit by the " + EnemyName + " for " + DMGDealt + " health!";
                }
            }
        }
        NextTurnButton.gameObject.SetActive(true);
    }


    public void PlayerAttack()
    {
        for (int i = 0; i < InputButtons.Length; i++)
        {
            InputButtons[i].interactable = false;
        }
        Attack(playerGO, enemyGO[currentSelection]);
    }

    public void PlayerSpell()
    {
        //TODO usespell
    }

    public void UsedItem()
    {
        for (int i = 0; i < InputButtons.Length; i++)
        {
            InputButtons[i].interactable = false;
        }
        NextTurnButton.gameObject.SetActive(true);
    }

    public void ShowSelectEnemyUI()
    {
        int AliveEnemies = 0;
        for(int i = 0; i < enemyGO.Length; i++)
            if (!enemyGO[i].GetComponent<EnemyCharacter>().isDead())
                AliveEnemies++;
        if (AliveEnemies > 1)
        {
            SelectEnemyUI.SetActive(true);
            for (int i = 0; i < InputButtons.Length; i++)
            {
                InputButtons[i].interactable = false;
            }
        }
        else
            PlayerAttack();
        ChangeSelectionUI();
    }

    public void BackSelectEnemyUI()
    {
        SelectEnemyUI.SetActive(false);
        for (int i = 0; i < InputButtons.Length; i++)
        {
            InputButtons[i].interactable = true;
        }
    }

    public void incEnemySelection()
    {
        if (currentSelection == enemy.Length - 1)
            currentSelection = 0;
        else
            currentSelection++;
        if (enemy[currentSelection].isDead())
            incEnemySelection();
        ChangeSelectionUI();
    }

    public void decEnemySelection()
    {
        if (currentSelection == 0)
            currentSelection = enemy.Length - 1;
        else
            currentSelection--;
        if (enemy[currentSelection].isDead())
            decEnemySelection();
        ChangeSelectionUI();
    }

    void PopulateOrderArray()
    {
        order = new GameObject[enemyGO.Length + 1];
        order[0] = playerGO;

        for (int i = 1; i < order.Length; i++)
            order[i] = enemyGO[i - 1];
    }

    void SortOrderArray()
    {
        for (int i = 0; i < order.Length; i++)
            for (int j = i + 1; i < order.Length; i++)
            {
                if (order[i].CompareTag("Player") && order[j].CompareTag("Enemy"))
                {
                    if (order[i].GetComponent<PlayerCharacter>().getSpeed() < order[j].GetComponent<EnemyCharacter>().getSpeed())
                    {
                        GameObject tmp = order[i];
                        order[i] = order[j];
                        order[j] = tmp;
                    }
                }
                else if (order[j].CompareTag("Player") && order[i].CompareTag("Enemy"))
                {
                    if (order[j].GetComponent<PlayerCharacter>().getSpeed() > order[i].GetComponent<EnemyCharacter>().getSpeed())
                    {
                        GameObject tmp = order[i];
                        order[i] = order[j];
                        order[j] = tmp;
                    }
                }
                else if (order[i].CompareTag("Enemy") && order[j].CompareTag("Enemy"))
                {
                    if (order[i].GetComponent<EnemyCharacter>().getSpeed() < order[j].GetComponent<EnemyCharacter>().getSpeed())
                    {
                        GameObject tmp = order[i];
                        order[i] = order[j];
                        order[j] = tmp;
                    }
                }
            }
    }

    void ChangeSelectionUI()
    {
        enemySelection.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = enemy[currentSelection].getName();
        enemySelection.transform.GetChild(1).GetComponent<Slider>().maxValue = enemy[currentSelection].getMaxHealth();
        enemySelection.transform.GetChild(1).GetComponent<Slider>().value = enemy[currentSelection].getHealth();
        enemySelection.transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = enemy[currentSelection].getHealth() + "/" + enemy[currentSelection].getMaxHealth();
    }
}
