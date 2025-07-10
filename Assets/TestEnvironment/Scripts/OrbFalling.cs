using UnityEngine;
using UnityEngine.Events;

public class OrbFalling : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    public float startDelay = 5f;

    public UnityEvent onOrbAway;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        gameObject.SetActive(false);
    }

    public void FallIntoWater()
    {
        gameObject.SetActive(true);
        animator.Play("Fall");
        Invoke("RemoveOrb", 4f);
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
            audioSource.Play();
            Invoke("RevealAfterSplash", 1f);
        }
    }

    private void RevealAfterSplash()
    {
        onOrbAway.Invoke();
    }
}
