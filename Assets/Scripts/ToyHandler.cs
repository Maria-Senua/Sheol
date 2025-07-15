using UnityEngine;

public class ToyHandler : MonoBehaviour
{
    //THis is same script as MusicBoxHandler, remove one of them and use it on both the gameobjects
    [SerializeField] private Animator animator;
    [SerializeField] private string openAnimation;
    [SerializeField] private GameObject soundModule;
    
    void Start()
    {
        gameObject.transform.localPosition = new Vector3(-0.0294f, 0.5316f, 0.2025f);
        gameObject.transform.localRotation = Quaternion.Euler(-94.086f, -42.48999f, 36.34399f);
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsName(openAnimation))
        {
            float progress = stateInfo.normalizedTime % 1;
            Debug.Log($"Dance animation progress: {progress * 100}%");
            
            if(progress >= 0.99f)
            {
                animator.enabled = false;
                soundModule.SetActive(true);
            }
        }
    }
}
