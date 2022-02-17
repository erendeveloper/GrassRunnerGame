using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Instantiated by every item type
//Object pool functionality
public class ItemType
{
    private GameObject itemPrefab;
    public GameObject ItemPrefab { get => itemPrefab; }

    private ObjectPool objectPool = new ObjectPool();

    private ItemName name;
    public ItemName Name { get => name; }

    public bool IsItemAtPool { get => !objectPool.IsPoolEmpty; }

    public ItemType(GameObject itemPrefab)
    {
        this.itemPrefab = itemPrefab;
        name = itemPrefab.GetComponent<Item>().Name;
    }

    public void EnableItem(Vector3 position)
    {
        Transform _transform;
        _transform = objectPool.TakeFromPool();
        _transform.position = position;
        _transform.GetComponent<Item>().Body.SetActive(true);
    }
    public void DisableItem(Transform _transform)
    {
        //transform.gameObject.SetActive(false);
        Item item = _transform.GetComponent<Item>();
        item.Body.SetActive(false);
        item.Particle.Play();
        objectPool.AddToPool(_transform);
    }
}
