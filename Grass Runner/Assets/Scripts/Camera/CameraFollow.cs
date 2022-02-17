using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on Main Camera
//follow and animate with lerp
public class CameraFollow : MonoBehaviour
{
    //Access other script
    private GameManager _gameManager;

    private Transform player;
    
    [SerializeField]
    private Vector3 firstPosition = new Vector3(0,2,-10f);
    [SerializeField]
    private Vector3 secondPosition = new Vector3(3.5f, 2f, -8f);
    [SerializeField]
    private Quaternion firstRotation = Quaternion.Euler(-10f,0f,0f);
    [SerializeField]
    private Quaternion secondRotation = Quaternion.Euler(-10f, -22f, 0f);

    private float distanceZ;

    //lerp
    private float lerpCurrentTime = 0;
    private float lerpTotalTime = 1f;
    bool isLerpStarted = false;

    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;       
    }
    private void Start()
    {
        firstPosition = SaveLoad.instance.gameConfig.cameraFirstPosition;
        firstRotation = SaveLoad.instance.gameConfig.cameraFirstRotation;
        secondPosition = SaveLoad.instance.gameConfig.cameraSecondPosition;
        secondRotation = SaveLoad.instance.gameConfig.cameraSecondRotation;

        this.transform.position = firstPosition;
        this.transform.rotation = firstRotation;
        distanceZ = firstPosition.z;
    }

    private void LateUpdate()
    {
        if (_gameManager.LevelProgress == GameState.Play)
        {
            //follow player
            transform.position = new Vector3(this.transform.position.x,
                                                this.transform.position.y,
                                                player.position.z + distanceZ);
        }
        else if (_gameManager.LevelProgress == GameState.Victory)
        {
            //move and rotate camera to target by lerp

            if (!isLerpStarted)
            {
                isLerpStarted = true;
                secondPosition.z = transform.position.z + (secondPosition.z - firstPosition.z);
                firstPosition = transform.position;
            }

            lerpCurrentTime += Time.deltaTime / lerpTotalTime;

            transform.position = Vector3.Lerp(firstPosition, secondPosition, lerpCurrentTime);
            transform.rotation = Quaternion.Lerp(firstRotation, secondRotation, lerpCurrentTime);
        }
    }
}
