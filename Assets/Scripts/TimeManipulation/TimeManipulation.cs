using UnityEngine;

public class TimeManipulation : MonoBehaviour
{
    [Header("Read comments in the code please")]
    //The script goes on the gameobject which has Animator component.
    //You will need the player gameobject reference which from template is named "XR origin(XR rig)".
    // Now go to Animator window in engine, find parameter add a float parameter and name it "reverser"
    //Now click on the animation node and check "multiplyer" paramerer below "Speed" and set it to "reverser" parameter.
    //"debugString" Textbox in the inspector would tell what is happening right now
    //Feel free to message me if it doesnt work I probably did not wrote correct steps :)

    [Header("Setup")]
    [SerializeField] private GameObject player;
    
    [SerializeField, TextArea] private string debugString;
    private Animator animation;
    private float previousY;

    private void Awake()
    {
        animation = GetComponent<Animator>();
        previousY = player.transform.position.y;
    }
    
    private void Start()
    {
        animation.speed = 0f;
    }

    private void Update()
    {
        float currentY = player.transform.position.y;
        
        // debugString = $"Current Y: {currentY}, Previous Y: {previousY}, Animation Speed: {animation.speed}";
        
        if (currentY > previousY)
        {
            animation.SetFloat("reverser", -1);
            animation.speed = 1f;
            debugString = "Reversing";
        }
        else if (currentY < previousY) 
        {
            animation.SetFloat("reverser", 1);
            animation.speed = 1f;
            debugString = "Forwarding";
        }
        else
        {
            animation.SetFloat("reverser", 1);
            animation.speed = 0f;
            debugString = "Paused";
        }

        previousY = currentY;
    }
}