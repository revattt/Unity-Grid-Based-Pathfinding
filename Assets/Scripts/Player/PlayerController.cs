using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int currentX = 0; //x current position of player
    public int currentY = 0; //y current
    public float speed = 5f; //player speed
    public bool isMove = false; //bool to check if the player is moving or 
    public IEnumerator MoveAlongPath(List<Vector2Int> path) //list from bfs
    {
        isMove = true;
        MovementManager.Instance.isAnyMoving = true; //helps to lock input when the player is moving
        foreach (Vector2Int step in path)
        {
            Vector3 targetPos = new Vector3(step.x, 0.5f, step.y); //converts basic grid pos like x, y in to the actual position 3d x,y,z
            while (Vector3.Distance(transform.position, targetPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime); //learned this from gpt, movetowards help transit smoothly unlike teleportation
                yield return null;
            }
            currentX = step.x; //position is updated after trransition
            currentY = step.y;
        }
        isMove = false;
        MovementManager.Instance.isAnyMoving = false;
        Debug.Log("cjhra");


    }
}
