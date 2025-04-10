using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData; //scriptable object ref
    public GameObject obstaclePrefab; //obstacle prefab just a basic red sphere
    public Vector3 startPos = Vector3.zero; //grid start position
    public float spacing = 1f;
    public int gridSize = 10;
    // Start is called before the first frame update
    void Start()
    {
        if (obstacleData == null)
        {
            Debug.LogError("please assign obstacledata");
            return;
        }
        for (int y = 0; y < gridSize; y++) //double looping
        {

            for (int x = 0; x < gridSize; x++)
            {
                int index = y * gridSize + x; //gives the index for a tile like x,y
                if (obstacleData.obstacleArray[index]) //current index has obstacle then
                {
                    Vector3 pos = new Vector3(startPos.x + x * spacing, startPos.y + 0.5f, startPos.z + y * spacing); //calculates world position, 
                    Instantiate(obstaclePrefab, pos, Quaternion.identity); //spawning red sphere
                }
            }

        }


    }


}
