using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public Transform newBoatPos;
    public float drawningTime = 12f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(MoveToPosition(newBoatPos.position, drawningTime));
    }

    private IEnumerator MoveToPosition(Vector3 target, float duration)
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPos, target, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = target; 
    }

}
