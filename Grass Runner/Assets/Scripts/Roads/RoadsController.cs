using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on Script holder
//Calculates road position, spawns and moves,
//No need object pooling since continuously moves
//There are 2 roads in the scene back of Vector3.zero, new roads get spawned from Vector3.zero to further
public class RoadsController : MonoBehaviour
{
    //Access other script
    ItemGenerator _itemGenerator;

    public GameObject roadPrefab;

    private float roadSizeZ;  //to calculate next position
    private const int NumberOfRoadsToCreate = 20;

    private const int numberOfFirstRoads = 2; //first roads which are empty after the start line
    private Vector3 nextRoadPosition = Vector3.zero;
    private Quaternion roadQuaternion = Quaternion.identity;

    int roadCount = 0;

    private void Awake()
    {
        _itemGenerator = GetComponent<ItemGenerator>();
        roadSizeZ = roadPrefab.transform.localScale.z * 10f;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnRoads();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnRoads()
    { 
        for(int i = 0; i < NumberOfRoadsToCreate; i++)
        {        
            GameObject road = Instantiate(roadPrefab, Vector3.zero, roadQuaternion);
            MoveRoad(road.transform);
        }
    }
    
    public void MoveRoad(Transform road)
    {
        road.transform.position = nextRoadPosition;
        GenerateItemOnRoad();

        nextRoadPosition.z += roadSizeZ;
    }

    private void GenerateItemOnRoad()
    {
        roadCount++;
        if (roadCount >= numberOfFirstRoads)
        {
            _itemGenerator.Generate(nextRoadPosition);
        }
    }
}
