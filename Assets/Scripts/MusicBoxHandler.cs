using UnityEngine;
using UnityEngine.Events;

public class MusicBoxHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string danceAnimation;
    [SerializeField] private GameObject picture;

    public UnityEvent onPhotoAppear;

    void Start()
    {
        
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(danceAnimation))
        {
            float progress = stateInfo.normalizedTime % 1;
            Debug.Log($"Dance animation progress: {progress * 100}%");
            if(progress >= 0.99f)
            {
                animator.enabled = false;
                picture.SetActive(true);

                //unity event for demo
                Invoke("ReactToPhoto", 3f);
            }
        }
    }

    void ReactToPhoto()
    {
        onPhotoAppear?.Invoke();
    }
}
