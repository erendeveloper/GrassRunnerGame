using System.Collections.Generic;
using UnityEngine;

//Game configuration for Debug

[CreateAssetMenu(fileName = "New GameConfig", menuName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Game")]
    public int maxLevel = 3;
    public int life = 3;
    public float playTime = 15f;

    [Header("Item Value")]
    public int smallDiamond = 1;
    public int largediamond = 2;

    [Header("Level Design")]
    public float[] itemPositionsX = { -1.5f, 0, 1.5f };
    public int maxItemsHorizontal = 3;

    [Header("Item Generation Probabilities")]
    [Tooltip("Order of items:\nSmall Diamond, Large Diamond, Obstacle")]
    public int[] generationProbabilityLevel1 = { 50, 0, 50 };
    public int[] generationProbabilityLevel2 = { 34, 33, 33 };
    public int[] generationProbabilityLevel3 = { 25, 25, 50 };

    [Header("Character")]
    public float characterForwardSpeed = 10f;
    public float characterHorizontalSpeed = 20f;

    [Header("Camera")]
    public Vector3 cameraFirstPosition = new Vector3(0, 2f, -10f);
    public Vector3 cameraSecondPosition = new Vector3(3.5f, 2f, -8f);
    public Quaternion cameraFirstRotation = Quaternion.Euler(-10f, 0, 0);
    public Quaternion cameraSecondRotation = Quaternion.Euler(-10f, -22f, 0);
}