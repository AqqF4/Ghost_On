using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public static List<RoomNode> FindPath(RoomNode startNode, RoomNode targetNode)
    {
        List<RoomNode> openSet = new List<RoomNode> { startNode };
        HashSet<RoomNode> closedSet = new HashSet<RoomNode>();

        Dictionary<RoomNode, RoomNode> cameFrom = new Dictionary<RoomNode, RoomNode>();
        Dictionary<RoomNode, float> gScore = new Dictionary<RoomNode, float>();
        Dictionary<RoomNode, float> fScore = new Dictionary<RoomNode, float>();

        foreach (RoomNode node in FindObjectsOfType<RoomNode>())
        {
            gScore[node] = float.MaxValue;
            fScore[node] = float.MaxValue;
        }
        gScore[startNode] = 0;
        fScore[startNode] = Heuristic(startNode, targetNode);

        while (openSet.Count > 0)
        {
            RoomNode currentNode = GetLowestFScoreNode(openSet, fScore);

            if (currentNode == targetNode)
            {
                return ReconstructPath(cameFrom, currentNode);
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            foreach (RoomNode neighbor in currentNode.neighbors)
            {
                if (closedSet.Contains(neighbor)) continue;

                float tentativeGScore = gScore[currentNode] + Vector3.Distance(currentNode.transform.position, neighbor.transform.position);
                if (tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = currentNode;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, targetNode);

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        return new List<RoomNode>(); // Путь не найден
    }

    private static float Heuristic(RoomNode a, RoomNode b)
    {
        return Vector3.Distance(a.transform.position, b.transform.position);
    }

    private static RoomNode GetLowestFScoreNode(List<RoomNode> openSet, Dictionary<RoomNode, float> fScore)
    {
        RoomNode lowest = openSet[0];
        foreach (RoomNode node in openSet)
        {
            if (fScore[node] < fScore[lowest])
            {
                lowest = node;
            }
        }
        return lowest;
    }

    private static List<RoomNode> ReconstructPath(Dictionary<RoomNode, RoomNode> cameFrom, RoomNode currentNode)
    {
        List<RoomNode> path = new List<RoomNode> { currentNode };
        while (cameFrom.ContainsKey(currentNode))
        {
            currentNode = cameFrom[currentNode];
            path.Add(currentNode);
        }
        path.Reverse();
        return path;
    }
}