using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on Script Holder
//Calculates collectable prizes
public class MoneyCalculator : MonoBehaviour
{
    GameManager _gameManager;

    private int smallDiamondValue = 1;
    public int SmallDiamondValue { get; set; }
    private int largeDiamondValue = 2;
    public int LargeDiamondValue { get; set;}

    private void Awake()
    {
        _gameManager = this.GetComponent<GameManager>();
    }
    private void Start()
    {
        smallDiamondValue = SaveLoad.instance.gameConfig.smallDiamond;
        largeDiamondValue = SaveLoad.instance.gameConfig.largediamond;
    }
    public void IncreaseLevelMoney(ItemName itemName)
    {
        int value = 0;
        if(itemName == ItemName.SmallDiamond)
        {
            value = smallDiamondValue;
        }
        else if (itemName == ItemName.LargeDiamond)
        {
            value = largeDiamondValue;
        }
        _gameManager.UpdateLevelMoney(value);
    }


}
