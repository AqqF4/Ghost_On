using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitUntil : MonoBehaviour
{
    public float WaitAmount;
    void Start()
    {
        StartCoroutine(Waiting());
    }

    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(WaitAmount);
        Destroy (gameObject);
    }
}
