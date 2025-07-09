using UnityEngine;
using UnityEngine.Events;

public class OrbFalling : MonoBehaviour
{
    private Animator animator;
    public float startDelay = 5f;

    public UnityEvent onOrbAway;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        startDelay -= Time.deltaTime;

        if (startDelay <= 0)
        {
            animator.Play("Fall");
            Invoke("RemoveOrb", 4f);
        }
           
    }

    private void RemoveOrb()
    {
        gameObject.SetActive(false);
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            //activate splash
            Debug.Log("SPLASH");
            onOrbAway.Invoke();
        }
    }
}
