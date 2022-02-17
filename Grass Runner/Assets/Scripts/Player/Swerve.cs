using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on Player prefab
//Sends swerve input to PlayerController.cs"
//How it works: There is a box a box collider on Player prefab.Sends rays through the collider and checks its x point;Swerve Box Collider's layer is called "Swerve"
public class Swerve : MonoBehaviour
{
    //Access to controller
    PlayerController playerControllerScript;

    private Camera mainCamera;

    LayerMask swerveLayerMask;

    private float raycastDistance = 100f;

    private float localPositionX = 0f;
    

    private void Awake()
    {
        mainCamera = Camera.main;
        swerveLayerMask = LayerMask.GetMask("Swerve");
    }
    public float LocalPositionX
    {
        get
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, raycastDistance, swerveLayerMask))
            {
                localPositionX = hit.point.x;
            }
            return localPositionX;
        }
    }

}