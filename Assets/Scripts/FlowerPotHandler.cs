using System;
using UnityEngine;

public class FlowerPotHandler : MonoBehaviour
{
    [SerializeField] private string debugString;
    
    public GameObject daisyFlower;
    public GameObject planedFlower;
    [SerializeField] private Animator animator;
    
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.normalizedTime >= 1.0f)
        {
            debugString = "Flower pot animation completed.";
            planedFlower.SetActive(false);
            daisyFlower.SetActive(true);
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
        }
    }
}
