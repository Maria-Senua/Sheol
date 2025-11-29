using System.Collections;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerRigidbody;
    [SerializeField] private Vector3 newPosition;

    void Start()
    {
        StartCoroutine(RepositionRigidbodyAfterDelay(1f));
    }

    private IEnumerator RepositionRigidbodyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerRigidbody.transform.position = newPosition;
    }
}

