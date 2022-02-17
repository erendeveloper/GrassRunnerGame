using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on road prefabs
//Player has Pass Collider at its back.When it triggers this, road moves to next position;
public class RoadPass : MonoBehaviour
{
    //Access the controller
    private RoadsController _roadsController;

    private void Awake()
    {
        _roadsController = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<RoadsController>();
    }

    private void OnTriggerEnter(Collider other)//Player ('Road Pass Box Collider' is inside of that) triggers
    {
        _roadsController.MoveRoad(this.transform);
    }
}
