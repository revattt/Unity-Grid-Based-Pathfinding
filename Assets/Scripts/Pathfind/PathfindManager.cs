using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindManager : MonoBehaviour
{
    public PlayerController player; //access to trhe player
    public ObstacleData obstacleData; //access the obstacle data
    public int gridSize = 10;
    void Update()
    {
        if (Input.GetMouseButton(0) && !player.isMove && !MovementManager.Instance.isAnyMoving)
        //if click and player is not moving then...
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //ray from the mouse pos
            if (Physics.Raycast(ray, out RaycastHit hit)) //stores a hit 
            {
                GridStat clickedTile = hit.collider.GetComponent<GridStat>(); //checks which tile is hit and if it has gridstat attached
                if (clickedTile != null)
                {
                    TryMoveTo(clickedTile.x, clickedTile.y); //if tile is not null then, it passes the tiles coordinate to our necxt function trymoveto
                }

            }
        }
    }
    void TryMoveTo(int targetX, int targetY)
    {
        int targetIndex = targetY * gridSize + targetX; //again converts 2d to 1d array
        if (obstacleData.obstacleArray[targetIndex]) return; //if tile = has obstacle then return i.e dont move
        List<Vector2Int> path = BFS(new Vector2Int(player.currentX, player.currentY), new Vector2Int(targetX, targetY)); //calls bfs
        if (path != null)
        {
            StartCoroutine(player.MoveAlongPath(path)); //the path is set to players Move along path
        }
    }
    List<Vector2Int> BFS(Vector2Int start, Vector2Int goal) //main bfs function having 2 arguments start and goal (clicked tile)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>(); //queue for breadth first 
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>(); //dictionary cameFrom stores the path
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>(); //hash for visited tiles

        queue.Enqueue(start); //inserts into queue or starting position
        visited.Add(start);
        cameFrom[start] = start;

        Vector2Int[] directions = new Vector2Int[] //used vector2 to define easily the directions in which the player can move
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        while (queue.Count > 0) //i.e the queue not empty and tiles are in the queue
        {
            Vector2Int current = queue.Dequeue();

            if (current == goal) //current tile = tile u clicked 
            {
                List<Vector2Int> path = new List<Vector2Int>();
                while (current != start)
                {
                    path.Add(current);
                    current = cameFrom[current]; //if goal is reached, the path travels backward or kind of stores the path using out dictionary
                    Debug.Log("adding to current");
                }
                path.Reverse(); //reverse so path starts from the player to goal
                return path;
            }

            foreach (Vector2Int dir in directions)
            {
                Vector2Int neighbor = current + dir; //it checks for adjacent tiles 

                if (neighbor.x < 0 || neighbor.x >= gridSize || neighbor.y < 0 || neighbor.y >= gridSize) //checks if adjcent tiles are withing the bounds, if x/y less than 0 then its not in the map
                    continue; //if out of bounds then skip

                int index = neighbor.y * gridSize + neighbor.x; //again convertinfg (x,y) to 1d array
                if (obstacleData.obstacleArray[index] || visited.Contains(neighbor))
                    continue; //if adjacent tile has obstacle then skip

                queue.Enqueue(neighbor); //adds adjcent tile in the queue
                visited.Add(neighbor); //marks the adjacent tile as visited to avoid repetition
                cameFrom[neighbor] = current; //stores where we came from the path
            }
        }

        return null; // No path
    }
}



