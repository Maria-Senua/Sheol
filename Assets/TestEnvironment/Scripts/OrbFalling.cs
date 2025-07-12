using UnityEngine;
using UnityEngine.Events;

public class OrbFalling : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    public float startDelay = 5f;
    public GameObject splash;
    public GameObject ripple;

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
        //Invoke("RemoveOrb", 2f);
    }

    private void RemoveOrb()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            splash.SetActive(true);
            ripple.SetActive(true);
            Debug.Log("SPLASH");
            audioSource.Play();
            Invoke("RevealAfterSplash", 3f);
        }
    }

    private void RevealAfterSplash()
    {
        
        onOrbAway.Invoke();
        RemoveOrb();
    }
}
