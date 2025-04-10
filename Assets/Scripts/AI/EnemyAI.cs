using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, InterfaceAI //implementing our interface
{
    public Vector2Int currentPos; //current enemy pos
    public float speed = 5f;
    public PlayerController player;
    public ObstacleData obstacleData;
    public int gridSize = 10;
    private Vector2Int lastKnownPos; //it is the last pos of our player
    public void MakeDecision()
    {
        if (player == null || player.isMove) return; //player not assigned or player is moving then donot move the enemy 
        Vector2Int playerPos = new Vector2Int(player.currentX, player.currentY); //used vector2 for x, y pos, it gets the players current tile position
        if (playerPos != lastKnownPos) //this means the player has moved
        {
            Vector2Int target = GetAdjacentTile(playerPos); //this function helps to find adjecant tile of players current tile
            if (target != currentPos) //enemy will not move if its already standing on the goal 
            {
                List<Vector2Int> path = BFS(currentPos, target); //bfs function takes 2 arguments again
                if (path != null && path.Count > 0)
                    StartCoroutine(MoveAlongPath(path)); //we want the player to be visible when moving thorugh path
            }
            lastKnownPos = playerPos;
        }
    }
    public IEnumerator MoveAlongPath(List<Vector2Int> path)
    {
        foreach (Vector2Int step in path)
        {
            Vector3 targetPos = new Vector3(step.x, 0.5f, step.y);
            while (Vector3.Distance(transform.position, targetPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
                yield return null; //waits for the next frame before continuing, without coroutine the movement would happen in one single framem i.e teleport
            }
            currentPos = step;
        }

    }
    List<Vector2Int> BFS(Vector2Int start, Vector2Int goal)
    {
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

        queue.Enqueue(start);
        visited.Add(start);
        cameFrom[start] = start;

        Vector2Int[] directions = new Vector2Int[]
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            if (current == goal)
            {
                List<Vector2Int> path = new List<Vector2Int>();
                while (current != start)
                {
                    path.Add(current);
                    current = cameFrom[current];
                    Debug.Log("adding to current");
                }
                path.Reverse();
                return path;
            }

            foreach (Vector2Int dir in directions)
            {
                Vector2Int neighbor = current + dir;

                if (neighbor.x < 0 || neighbor.x >= gridSize || neighbor.y < 0 || neighbor.y >= gridSize)
                    continue;

                int index = neighbor.y * gridSize + neighbor.x;
                if (obstacleData.obstacleArray[index] || visited.Contains(neighbor))
                    continue;

                queue.Enqueue(neighbor);
                visited.Add(neighbor);
                cameFrom[neighbor] = current;
            }
        }

        return null; // No path
    }
    Vector2Int GetAdjacentTile(Vector2Int playerPos) //getadjacent tile function
    {
        Vector2Int[] direction = new Vector2Int[] //again same, checks 4 direction
        {
            Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
        };
        foreach (Vector2Int dir in direction)
        {
            Vector2Int adjacent = playerPos + dir; //dir is kind of offset whcih gets added to the player pos, eg, if player pos = (5,5) then with addition of dir it becomes (5,6)
            if (adjacent.x < 0 || adjacent.x >= gridSize || adjacent.y < 0 || adjacent.y >= gridSize)
                continue; //grid bounds 
            int index = adjacent.y * gridSize + adjacent.x;
            if (!obstacleData.obstacleArray[index]) return adjacent;
        }
        return currentPos;
    }
    void Update()
    {
        MakeDecision(); //calling the logic every sec/frame
    }

}
