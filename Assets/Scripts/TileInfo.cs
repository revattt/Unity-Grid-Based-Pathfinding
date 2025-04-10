using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TileInfo : MonoBehaviour
{
    public TMP_Text tileInfoText; //the text responsible to show the current location when mouse hovered over a tile


    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //ray casting from mouse position
        if (Physics.Raycast(ray, out RaycastHit hit)) //hit stores the result if ray hits any collider 
        {
            GridStat tile = hit.collider.GetComponent<GridStat>(); //checks for tile basically if any component has gridstat script attached 
            if (tile != null)
            {
                tileInfoText.text = $"Tile:({tile.x}, {tile.y})"; //ui update if its tile
            }
        }

    }
}
