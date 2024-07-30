using System.Collections.Generic;
using UnityEngine;

public class RoomNode : MonoBehaviour
{
    public List<RoomNode> neighbors; // Соседние комнаты

    // Возвращает случайного соседа, используемого в дальнейшем для случайного передвижения
    public RoomNode GetRandomNeighbor()
    {
        if (neighbors.Count == 0) return null;
        return neighbors[Random.Range(0, neighbors.Count)];
    }
}