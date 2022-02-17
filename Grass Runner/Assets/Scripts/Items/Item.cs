using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on item prefabs
public class Item : MonoBehaviour
{
    //Access other scripts
    GameManager _gameManager;
    ItemGenerator _itemGenerator;

    [SerializeField] 
    private GameObject body;  //solid visible part of item
    public GameObject Body { get => body; }
    [SerializeField] 
    private ParticleSystem particle;
    public ParticleSystem Particle { get => particle; }

    [SerializeField] new ItemName name;
    public ItemName Name { get => name; }
    ItemType _itemType;
    MoneyCalculator _moneyCalculator;

    private void Awake()
    {
        _itemGenerator = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<ItemGenerator>();
        _gameManager = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<GameManager>();
        _moneyCalculator = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<MoneyCalculator>();
    }

    private void Start()
    {
        _itemType = _itemGenerator.FindItemType(name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PassCollider"))
        {
            _itemType.DisableItem(this.transform);
        }
        else //character
        {
            _itemType.DisableItem(this.transform);

            if (name == ItemName.Obstacle)
            {
                _gameManager.DecreaseLife();
            }
            else //collectables
            {               
                _moneyCalculator.IncreaseLevelMoney(name);
            }
        }
    }
}
