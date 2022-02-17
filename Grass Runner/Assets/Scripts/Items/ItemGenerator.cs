using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Added on Script Holder
//Drag and drop item prefabs to the list
public class ItemGenerator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> itemPrefabs = new List<GameObject> ();

    private List<ItemType> itemTypeList = new List<ItemType>();

    [SerializeField]
    float[] itemPositionsX = { -1.5f, 0, 1.5f };
    [SerializeField]
    int maxItemsHorizontal = 3; //bura degisebilir

    int[] generationProbability = { 34, 33, 33 };//small diamond, large diamond, obstacle

    void Awake()
    {
        foreach (GameObject itemPrefab in itemPrefabs)
        {
            itemTypeList.Add(new ItemType(itemPrefab));
        }
    }
    private void Start()
    {
        maxItemsHorizontal = SaveLoad.instance.gameConfig.maxItemsHorizontal;
        itemPositionsX = SaveLoad.instance.gameConfig.itemPositionsX;
        int level = GetComponent<GameManager>().Level;
        generationProbability = (int[])SaveLoad.instance.gameConfig.GetType().GetField("generationProbabilityLevel" + level).GetValue(SaveLoad.instance.gameConfig);
    }
    public void Generate(Vector3 roadPosition)
    {
        if(itemTypeList.Count > 0  && !IsAllProbabilitiesZero())
        {
            int obstacleCount = 0;
            int itemsHorizontalCount = Random.Range(0, maxItemsHorizontal);
            List<float> positionsX = new List<float>(itemPositionsX);
            for (int i = 0; i < itemsHorizontalCount; i++) //numberOfItemPosition atma ihtimallerini yazmadik
            {
                int randomPositionIndex = Random.Range(0, positionsX.Count);
                //obstacle 3 olma ihtimalini yazmadik
                Vector3 generationPosition = roadPosition;
                generationPosition.x += positionsX[randomPositionIndex];
              
                ItemType _itemType = itemTypeList[RandomItemIndex()];
                if(_itemType.Name == ItemName.Obstacle)
                {
                    obstacleCount++;
                    while(obstacleCount >= 3)
                    {
                       _itemType = itemTypeList[RandomItemIndex()];
                       if(_itemType.Name != ItemName.Obstacle)
                       {
                            break;
                       }
                    }
                }
                
                if (_itemType.IsItemAtPool)
                {
                    _itemType.EnableItem(generationPosition);
                }
                else
                {
                    Instantiate(_itemType.ItemPrefab, generationPosition, Quaternion.identity);
                }
                positionsX.RemoveAt(randomPositionIndex);
                
            }
        }
        else if(itemTypeList.Count == 0)
        {
            Debug.Log("There is no prefab attaached to ItemGenerator script!");
        }
        else if (IsAllProbabilitiesZero())
        {
            Debug.Log("All generation probability values are zero.So, there is no item.");
        }
    }

    private int RandomItemIndex()
    {
        int randomProbability = Random.Range(1, generationProbability.Sum()+1);
        int threshold = 0;
        for (int i = 0; i < generationProbability.Length; i++)
        {
            threshold += generationProbability[i];
            if (randomProbability <= threshold)
            {
                return i;
            }
        }
        return 0;
    }
    private bool IsAllProbabilitiesZero()
    {
        foreach(int probability in generationProbability)
        {
            if(probability > 0)
            {
                return false;
            }
        }
        return true;
    }
    public ItemType FindItemType(ItemName itemName)
    {
        foreach (ItemType itemType in itemTypeList)
        {
            if (itemType.Name == itemName)
            {
                return itemType;
            }
        }
        return null;
    }



    class ItemProbability
    {
        int probabilityPercent;
        ItemType item;
    }
}
