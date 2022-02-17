using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Added on Canvas. Tag name: "UI"
public class UIController : MonoBehaviour
{
    GameManager _gameManager;

    //Folders
    public GameObject level;
    public GameObject life;
    public GameObject totalMoney;
    public GameObject levelMoney;

    //Text values
    public Text levelValue;
    public Text lifeValue;
    public Text totalMoneyValue;
    public Text levelMoneyValue;

    //Game over panel
    public GameObject gameOverPanel;
    public Text gameOverInfo;
    public Text nextGameButtonValue;

    void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<GameManager>();
    }

    public void SwitchGameUI()
    {
        GameState state = _gameManager.LevelProgress;
        switch (state)
        {         
            case GameState.Play:
                ShowPlayUI();
                break;
            case GameState.Victory:
                ShowVictoryUI();
                break;
            case GameState.Die:
                ShowDieUI();
                break;
            default:
                ShowStartUI();
                break;
        }
    }
    private void ShowStartUI()
    {
        //level
        //total money

        SetLevelUI(true);
        SetTotalMoneyUI(true);

        SetLifeUI(false);
        SetLevelMoneyUI(false);
        SetGameOverPanelUI(false);

        
    }
    private void ShowPlayUI()
    {
        //level
        //level money
        //life

        SetLifeUI(true);
        SetLevelMoneyUI(true);      

        SetTotalMoneyUI(false);
        

    }
    private void ShowVictoryUI()
    {
        //total money
        //level money
        //game over panel

        SetTotalMoneyUI(true);
        SetGameOverPanelUI(true);

        SetLevelUI(false);
        SetLifeUI(false);

        if (!_gameManager.IsLastLevel)
        {
            gameOverInfo.text = "Level completed!";
            nextGameButtonValue.text = "Continue";
        }
        else
        {
            gameOverInfo.text = "You passed all the levels!";
            nextGameButtonValue.text = "Reset Levels";
        }
    }
    public void ShowDieUI()
    {
        //level money
        //game over panel

        SetGameOverPanelUI(true);

        SetLevelUI(false);
        SetLifeUI(false);

        gameOverInfo.text = string.Format("You failed to gain {0} money in this level.",_gameManager.LevelMoney);
        nextGameButtonValue.text = "Restart";
    }

    #region Set UI
    private void SetLevelUI(bool state)
    {
        level.SetActive(state);
        if (state)
        {
            UpdateLevelValue();
        }      
    }
    private void SetLifeUI(bool state)
    {
        life.SetActive(state);
        if (state)
        {
            UpdateLifeValue();
        }       
    }
    private void SetTotalMoneyUI(bool state)
    {
        totalMoney.SetActive(state);
        if (state)
        {
            UpdateTotalMoneyValue();
        }
    }
    private void SetLevelMoneyUI(bool state)
    {
        levelMoney.SetActive(state);
        if (state)
        {
            UpdateLevelMoneyValue();
        }    
    }
    private void SetGameOverPanelUI(bool state)
    {
        gameOverPanel.SetActive(state);
    }
    #endregion

    #region Update value
    private void UpdateLevelValue()
    {
        levelValue.text = _gameManager.Level.ToString();
    }
    public void UpdateLifeValue()
    {
        lifeValue.text =_gameManager.Life.ToString();
    }
    public void UpdateTotalMoneyValue()
    {
        totalMoneyValue.text = _gameManager.TotalMoney.ToString();
    }
    public void UpdateLevelMoneyValue()
    {
        levelMoneyValue.text = _gameManager.LevelMoney.ToString();
    }
    #endregion
}
