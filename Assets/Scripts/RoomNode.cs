using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode : MonoBehaviour
{
    public List<RoomNode> neighbors; // Соседние комнаты
    public int roomNumber;
    public bool isFree = true;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.1f);

        Gizmos.color = Color.yellow;
        foreach (var neighbor in neighbors)
        {
            if (neighbor != null)
            {
                Gizmos.DrawLine(transform.position, neighbor.transform.position);
            }
        }
    }
}