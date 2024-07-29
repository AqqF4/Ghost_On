using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    public GameObject Camera;
    public float moveSpeed;
    public bool GoRight;
    bool isMoving;
    public bool CanMove = true;

    public GameObject[] CurrentSigns;
    public GameObject[] LeftSigns;
    public GameObject[] RightSigns;

    public GameObject StopLeft, StopRight, StopCur;

    void Update()
    {
        if (isMoving)
        {
            if (GoRight)
            {
                StopRight.SetActive(true);
            }
            else
            {
                StopLeft.SetActive(true);
            }

            foreach (GameObject sign in CurrentSigns)
            {
                if (sign.GetComponent<Turn>() != null)
                {
                    sign.GetComponent<Turn>().CanMove = false;
                }
                else
                {
                    sign.SetActive(false);
                }
            }

            Vector2 direction = GoRight ? Vector2.right : Vector2.left;
            Camera.GetComponent<Transform>().Translate(direction * moveSpeed * Time.deltaTime);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(Camera.transform.position, 0.1f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("StopSign"))
                {
                    isMoving = false;

                    if (GoRight)
                    {
                        foreach (GameObject sign in RightSigns)
                        {
                            if (sign.GetComponent<Turn>() != null)
                            {
                                sign.GetComponent<Turn>().CanMove = true;
                            }
                            else
                            {
                                sign.SetActive(true);
                            }
                        }
                        StopRight.SetActive(false);
                        StopCur.SetActive(true);
                    }
                    else
                    {
                        foreach (GameObject sign in LeftSigns)
                        {
                            if (sign.GetComponent<Turn>() != null)
                            {
                                sign.GetComponent<Turn>().CanMove = true;
                            }
                            else
                            {
                                sign.SetActive(true);
                            }
                        }
                        StopLeft.SetActive(false);
                        StopCur.SetActive(true);
                    }

                    SnapCameraToClosestPosition();
                    break;
                }
            }
        }
    }

    public void OnMouseEnter()
    {
        if (CanMove)
        {
            isMoving = true;
        }
    }

    private void SnapCameraToClosestPosition()
    {
        Vector3[] targetPositions = {
            new Vector3(-17.75f, Camera.transform.position.y, Camera.transform.position.z),
            new Vector3(0f, Camera.transform.position.y, Camera.transform.position.z),
            new Vector3(17.75f, Camera.transform.position.y, Camera.transform.position.z)
        };

        Vector3 currentPos = Camera.transform.position;
        Vector3 closestPos = targetPositions[0];
        float minDistance = Vector3.Distance(currentPos, targetPositions[0]);

        foreach (Vector3 pos in targetPositions)
        {
            float distance = Vector3.Distance(currentPos, pos);
            if (distance < minDistance)
            {
                closestPos = pos;
                minDistance = distance;
            }
        }

        Camera.transform.position = closestPos;
    }
}