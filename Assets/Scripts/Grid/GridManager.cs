using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 10; //grid rows number
    public int columns = 10; //grid column number
    public int scale = 1; //space tiles
    public GameObject gridPrefab; //tile prefab
    public Vector3 leftBottomLocation = new Vector3(0, 0, 0); //decides the starting position 000 of grid


    void Awake() //calls create grid when game starts
    {
        if (gridPrefab)
            CreateGrid();
        else print("missing grid prefab");
    }

    void CreateGrid() //spawns the cube at correct location using spacing
    {
        for (int i = 0; i < columns; i++) //looping over the grid for x axis 
        {
            for (int j = 0; j < rows; j++) //looping for y axis 
            {
                GameObject obj = Instantiate(gridPrefab, new Vector3(leftBottomLocation.x + scale * i, //i is column num, sets horizontal position
                leftBottomLocation.y, //verticle height is same so..
                leftBottomLocation.z + scale * j) //sets forward or backward
                , Quaternion.identity);
                obj.transform.SetParent(gameObject.transform);
                obj.GetComponent<GridStat>().x = i;
                obj.GetComponent<GridStat>().y = j;
            }
        }
    }


}
