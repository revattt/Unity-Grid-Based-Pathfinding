using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleData", menuName = "Scriptables/ObstacleData")]
public class ObstacleData : ScriptableObject
{
    public bool[] obstacleArray = new bool[100];
}
