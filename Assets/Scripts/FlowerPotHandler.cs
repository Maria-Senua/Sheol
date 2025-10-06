using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class FlowerPotHandler : MonoBehaviour
{
    [SerializeField] private string debugString;
    
    public GameObject daisyFlower;
    public GameObject planedFlower;
    [SerializeField] private Animator animator;

    public UnityEvent onPuzzleSolved;
    private bool animationCompleted = false;

    void Update()
    {
        if (animationCompleted) return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1.0f)
        {
            animationCompleted = true;

            debugString = "Flower pot animation completed.";
            planedFlower.SetActive(false);
            daisyFlower.SetActive(true);

            onPuzzleSolved?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        BucketHandler bucketHandler = other.gameObject.GetComponent<BucketHandler>();
        debugString = "Collision with: " + other.gameObject.name;
        if (bucketHandler != null && bucketHandler.hasWater)
        {
            debugString = "Watering the flower pot!";
            animator.enabled = true;
            bucketHandler.water.SetActive(false);
            StartCoroutine(RemoveBucket(other.gameObject));
        }
    }

    private IEnumerator RemoveBucket(GameObject bucket)
    {
        yield return new WaitForSeconds(2f);
        bucket.SetActive(false);
    }
}
