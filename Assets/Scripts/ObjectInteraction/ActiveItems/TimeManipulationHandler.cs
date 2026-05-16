using System.Collections;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class TimeManipulationHandler : MonoBehaviour
{
    [Header("Read comments in the code please")]
    //The script goes on the gameobject which has Animator component.
    //You will need the player gameobject reference which from template is named "XR origin(XR rig)".
    // Now go to Animator window in engine, find parameter add a float parameter and name it "reverser"
    //Now click on the animation node and check "multiplyer" paramerer below "Speed" and set it to "reverser" parameter.
    //"debugString" Textbox in the inspector would tell what is happening right now
    //Feel free to message me if it doesnt work I probably did not wrote correct steps :)
    
    [SerializeField, TextArea] private string debugString;
    
    [Header("Setup")] 
    [SerializeField] private GameObject bottomLimit;
    [SerializeField] private GameObject player;
    [SerializeField] private float animationSpeed = 1f;
    public bool canManipulateTime = false; 
    private bool isLocked = false;
    public Animator animation;
    private float previousY;
    private float distance;
    private Rigidbody rb;

    [Header("Audio")]
     private AudioSource audioSource;
    
    [Header("References")]
    [SerializeField] private InteractableGroupView handGrabGroup;
    [SerializeField] private GameObject fishBody;
    [SerializeField] private GameObject fishSkeleton;
    [SerializeField] private GameObject memoryOrb;


    [Header("Input Actions")]
    [SerializeField] private InputActionReference leftActivateAction;
    [SerializeField] private InputActionReference rightActivateAction;

    private Coroutine animationCoroutine;
    private bool isPlaying = false;


    private void Awake()
    {
        animation = GetComponent<Animator>();
        if(animation == null)
        {
            animation = GetComponentInChildren<Animator>();
        }
        if (fishBody != null)
        {
            animation = fishBody.GetComponent<Animator>();
        }

        rb = gameObject.GetComponent<Rigidbody>();
        if(rb == null)
        {
            rb = gameObject.GetComponentInParent<Rigidbody>();
        }
        rb.useGravity = false;
        rb.isKinematic = true;

        previousY = Vector3.Distance(bottomLimit.transform.position, player.transform.position);
        audioSource = GetComponent<AudioSource>();
        canManipulateTime = false;

        rightActivateAction.action.performed += Locking;
        leftActivateAction.action.performed += Locking;
    }
    
    private void Start()
    {
        animation.speed = 0f;
    }
    
    private void Update()
    {
        distance = Vector3.Distance(bottomLimit.transform.position, player.transform.position);
        
        ManipulateTime();
        
        Vector3 direction = player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
        
        debugString = isLocked ? "Time manipulation is locked." : "Time manipulation is unlocked.";
    }
    
    private void ManipulateTime()
    {
        if(isLocked) return;
    
        if (handGrabGroup.State == InteractableState.Select)
        {
            canManipulateTime = true;
            if (fishBody != null) fishBody.SetActive(true);
        }
        else
        {
            canManipulateTime = false;
        }
    
        if (!canManipulateTime) 
        {
            animation.speed = 0f;
            return;
        }

        float direction = Mathf.Sign(previousY - distance);
    
        AnimatorStateInfo stateInfo = animation.GetCurrentAnimatorStateInfo(0);
        float currentNormalizedTime = stateInfo.normalizedTime;
    
        if (!Mathf.Approximately(distance, previousY) || 
            (direction > 0 && currentNormalizedTime <= 0f) || 
            (direction < 0 && currentNormalizedTime >= 1f))
        {
            animation.SetFloat("reverser", direction);
            animation.SetFloat("motionTimer", 0);
            animation.speed = animationSpeed;

            if (!isPlaying)
            {
                float clipLength = animation.GetCurrentAnimatorClipInfo(0)[0].clip.length;
                if (animationCoroutine != null)
                    StopCoroutine(animationCoroutine);

                animationCoroutine = StartCoroutine(WaitForAnimation(clipLength, animationSpeed * direction));
            }
        }
        else if (Mathf.Approximately(distance, previousY))
        {
            animation.speed = 0f;
            
        }
        
        previousY = distance;
    }

    private IEnumerator WaitForAnimation(float clipLength, float speed)
    {
        isPlaying = true;

        float waitTime = clipLength / Mathf.Abs(speed); 
        yield return new WaitForSeconds(waitTime);

        isPlaying = false;
        OnAnimationFinished();
    }

    private void OnAnimationFinished()
    {
        if (fishSkeleton != null) fishSkeleton.SetActive(false);
        if (memoryOrb != null) memoryOrb.SetActive(true);
    }


    public void SubtitleCall(InputAction.CallbackContext context)
    {
        // SoundManager.instance.StartCoroutine(SoundManager.instance.TypeString(subtitles, audioClip, subtitleTMP, audioSource, xrGrabInteractable));
    }
    
    private void Locking(InputAction.CallbackContext context)
    {
        // if (xrGrabInteractable.isSelected)
        // {
        //     isLocked = !isLocked;
        // }
    }
    
    public void disableKinematic()
    {
        rb.isKinematic = false;
    }
    
    //Depricated
    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        rightActivateAction.action.performed += SubtitleCall;
        leftActivateAction.action.performed += SubtitleCall;
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        rightActivateAction.action.performed -= SubtitleCall;
        leftActivateAction.action.performed -= SubtitleCall;
    }
    
    private void OnEnable()
    {
        leftActivateAction.action.Enable();
        rightActivateAction.action.Enable();
    }
    
    private void OnDisable()
    {
        leftActivateAction.action.Disable();
        rightActivateAction.action.Disable();
    }

}