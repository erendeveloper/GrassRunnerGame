using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added on Player
//Player moves forward
//body part of the player moves horizontally
public class PlayerController : MonoBehaviour
{
    private GameManager _gameManager;
    private Swerve _swerve;
    private PlayerAnimatorController _playerAnimatorController;

    public Transform bodyHorizontal; //player body for horizontal movement

    private float speedForward = 5f;
    private float speedHorizontal = 20f;

    private const float MaxHorizontalDistance = 2f;//Leftest and rightest distance from the middle


    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<GameManager>();
        _swerve = GetComponent<Swerve>();
        _playerAnimatorController = bodyHorizontal.GetComponent<PlayerAnimatorController>();
        SaveLoad.instance = GameObject.FindGameObjectWithTag("ScriptHolder").GetComponent<SaveLoad>();
    }
    private void Start()
    {
        speedForward = SaveLoad.instance.gameConfig.characterForwardSpeed;
        speedHorizontal = SaveLoad.instance.gameConfig.characterHorizontalSpeed;
    }
    private void FixedUpdate()
    {
        if(_gameManager.LevelProgress  == GameState.Play)
        {
            MoveForward();

            if (Input.GetMouseButton(0))
            {
                MoveHorizontally();
            }
        }
        
    }

    private void MoveForward()
    {
        _playerAnimatorController.SetRunMultiplier(speedForward / 5f);
        transform.Translate(Vector3.forward * Time.deltaTime * speedForward);
    }
    private void MoveHorizontally()
    {
        Vector3 targetPosition = new Vector3(Mathf.Clamp(_swerve.LocalPositionX, -MaxHorizontalDistance, MaxHorizontalDistance), 0, 0);
        float distanceRatio = Mathf.Abs(Vector3.Distance(targetPosition, bodyHorizontal.localPosition)) / (MaxHorizontalDistance);
        bodyHorizontal.localPosition = Vector3.MoveTowards(bodyHorizontal.localPosition, targetPosition, Time.deltaTime * distanceRatio *speedHorizontal);

        if (Vector3.Distance(bodyHorizontal.localPosition, targetPosition) < 0.01f)
        {
            bodyHorizontal.localPosition = targetPosition;
        }
    }
}
