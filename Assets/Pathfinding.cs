using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    // Сделать метод публичным, чтобы он был доступен для других классов
    public List<RoomNode> FindPath(RoomNode startNode, RoomNode targetNode)
    {
        Queue<RoomNode> queue = new Queue<RoomNode>();
        Dictionary<RoomNode, RoomNode> cameFrom = new Dictionary<RoomNode, RoomNode>();
        queue.Enqueue(startNode);
        cameFrom[startNode] = null;

        while (queue.Count > 0)
        {
            RoomNode current = queue.Dequeue();

            if (current == targetNode)
            {
                // Построение пути от targetNode до startNode
                List<RoomNode> path = new List<RoomNode>();
                while (current != null)
                {
                    path.Add(current);
                    current = cameFrom[current];
                }
                path.Reverse(); // Путь строится от цели к старту, поэтому нужно развернуть список
                return path;
            }

            foreach (RoomNode neighbor in current.neighbors)
            {
                if (!cameFrom.ContainsKey(neighbor))
                {
                    queue.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                }
            }
        }

        // Если путь не найден
        return new List<RoomNode>();
    }
}