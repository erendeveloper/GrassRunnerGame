using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on Script Holder
//Uses PlayerPrefs for saving and loading.
//Assigns values from GameConfig 
public class SaveLoad : MonoBehaviour
{
    public static SaveLoad instance;
    public GameConfig gameConfig;  //ScriptableObject
    //public static GameConfig gameConfiguration;

    private GameManager _gameManager;

    [SerializeField]
    private bool resetData = false; //use it for debugging, deletes PlayerPrefs
    private void Awake()
    {
        instance = this;

        _gameManager = GetComponent<GameManager>();

        if (resetData)
        {
            Reset();
        }
    }

    #region write on disk and read from disk
    public void SaveLevel()
    {
        PlayerPrefs.SetInt("Level", _gameManager.Level);
        PlayerPrefs.Save();
    }
    public int LoadLevel()
    {
        return PlayerPrefs.GetInt("Level",1);
    }
    public void SaveMoney()
    {
        PlayerPrefs.SetInt("Money", _gameManager.TotalMoney);
        PlayerPrefs.Save();
    }
    public int LoadMoney()
    {
        return PlayerPrefs.GetInt("Money",0);
    }
    public void Reset()
    {
        Debug.Log("All PlayerPrefs have been deleted!");
        PlayerPrefs.DeleteAll();
    }
    #endregion
}
