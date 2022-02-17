using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Added on Script Holder
public class GameManager : MonoBehaviour
{
    //Access other scripts
    UIController _uiController;
    PlayerAnimatorController _playerAnimatorController;

    GameState levelProgress = GameState.Start;
    public GameState LevelProgress { get => levelProgress; }

    private int level = 1; //current level
    public int Level { get => level; }
    private int maxlevel = 3;
    public int MaxLevel { get => level; } //final level
    public bool IsLastLevel { get { return level == maxlevel ? true : false; } }

    private int life = 3;
    public int Life { get => life; }

    private int totalMoney = 0; //total money gained from the game
    public int TotalMoney { get => totalMoney;}

    private int levelMoney = 0; //money collected in a level
    public int LevelMoney { get => levelMoney; }

    float playTime = 15f;
    void Awake()
    {
        _uiController = GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>();
        _playerAnimatorController = GameObject.FindGameObjectWithTag("CharacterPrefab").GetComponent<PlayerAnimatorController>();
    }
    private void Start()
    {
        level = SaveLoad.instance.LoadLevel();
        totalMoney = SaveLoad.instance.LoadMoney();

        maxlevel = SaveLoad.instance.gameConfig.maxLevel;
        life = SaveLoad.instance.gameConfig.life;
        playTime = SaveLoad.instance.gameConfig.playTime;

        _uiController.SwitchGameUI();

    }
    private void Update()
    {
        if (levelProgress == GameState.Start)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartGame();
            }
        }
        else if(levelProgress == GameState.Play)
        {
            playTime -= Time.deltaTime;
            if(playTime <= 0f)
            {
                PassLevel();
            }
        }
    }
    public void StartGame()
    {
        levelProgress = GameState.Play;
        _playerAnimatorController.SetRun();
        _uiController.SwitchGameUI();
    }
    public void PassLevel()
    {
        levelProgress = GameState.Victory;
        _playerAnimatorController.SetVictory();
               
        UpdateTotalMoney();

        _uiController.SwitchGameUI();

        //resetting level after completing all levels
        if(level == maxlevel)
        {
            level = 1;
        }
        else
        {
            level += 1;
        }

        SaveLoad.instance.SaveLevel();
        SaveLoad.instance.SaveMoney();
    }
    private void Die()
    {
        levelProgress = GameState.Die;

        _playerAnimatorController.SetIdle();

        _uiController.SwitchGameUI();
    }

    public void DecreaseLife()
    {
        life--;
        _uiController.UpdateLifeValue();
        if (life == 0)
        {
            Die();
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
    public void UpdateTotalMoney()
    {
        totalMoney += levelMoney;
        _uiController.UpdateTotalMoneyValue();
    }
    public void UpdateLevelMoney(int extraMoney)
    {
        levelMoney += extraMoney;
        _uiController.UpdateLevelMoneyValue();
    }

}
