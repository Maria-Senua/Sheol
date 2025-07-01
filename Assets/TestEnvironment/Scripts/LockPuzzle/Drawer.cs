using UnityEngine;

public class Drawer : MonoBehaviour
{
    private enum DrawerStates
    {
        LOCKED,
        CLOSED,
        OPEN
    }

    private DrawerStates currentState = DrawerStates.LOCKED;

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = DrawerStates.LOCKED;
        animator = gameObject.GetComponent<Animator>();
    }

    public void UnlockDrawer()
    {
        currentState = DrawerStates.CLOSED;
    }

    public void PullDrawer()
    {
        switch (currentState)
        {
            case DrawerStates.LOCKED:
                Debug.Log("Drawer is locked");
                animator.SetTrigger("Attempt");
                break;
            case DrawerStates.CLOSED:
                Debug.Log("Drawer is closed");
                animator.SetTrigger("Open");
                currentState = DrawerStates.OPEN;
                break;
            case DrawerStates.OPEN:
                Debug.Log("Drawer is open");
                animator.SetTrigger("Close");
                currentState = DrawerStates.CLOSED;
                break;
        }
    }
}
