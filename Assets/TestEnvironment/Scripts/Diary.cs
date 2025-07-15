using UnityEngine;
using UnityEngine.Events;

public class Diary : MonoBehaviour
{
    private Animator animator;
    public GameObject pageFlipper;

    public UnityEvent onPageFlipped;
    public UnityEvent onPageHidden;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.enabled = false;
        pageFlipper.SetActive(false);
        onPageHidden?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDiary()
    {
        animator.enabled = true;
        pageFlipper.SetActive(true);
    }

    public void FlipPage()
    {
        animator.SetTrigger("flip");
        pageFlipper.SetActive(false);
        onPageFlipped?.Invoke();
    }
}
