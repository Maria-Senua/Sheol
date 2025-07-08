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

    [Header("Movement Settings")]
    [SerializeField] private float openZ = 0.3f;
    [SerializeField] private float moveDuration = 0.5f;
    [SerializeField] private float jiggleZ = 0.009f;
    [SerializeField] private float jiggleDuration = 0.05f;

    private Vector3 closedPos;
    private Vector3 openPos;


    //private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = DrawerStates.LOCKED;
        closedPos = transform.localPosition;
        openPos = closedPos + new Vector3(0, 0, openZ);

       
        //animator = gameObject.GetComponent<Animator>();
    }

    public void UnlockDrawer()
    {
        currentState = DrawerStates.CLOSED;
    }

    private void TryToOpenDrawer()
    {
        Vector3 jigglePos = closedPos + new Vector3(0, 0, jiggleZ);

        LeanTween.moveLocal(gameObject, jigglePos, jiggleDuration)
            .setEase(LeanTweenType.easeOutQuad)
            .setOnComplete(() =>
            {
                LeanTween.moveLocal(gameObject, closedPos, jiggleDuration)
                    .setEase(LeanTweenType.easeInQuad);
            });
    }

    private void OpenDrawer()
    {
        LeanTween.moveLocal(gameObject, openPos, moveDuration)
            .setEase(LeanTweenType.easeOutCubic);
    }

    private void CloseDrawer()
    {
        LeanTween.moveLocal(gameObject, closedPos, moveDuration)
            .setEase(LeanTweenType.easeInCubic);
    }

    public void PullDrawer()
    {
        switch (currentState)
        {
            case DrawerStates.LOCKED:
                Debug.Log("Drawer is locked");
                //animator.SetTrigger("Attempt");
                TryToOpenDrawer();
                break;
            case DrawerStates.CLOSED:
                Debug.Log("Drawer is closed");
                //animator.SetTrigger("Open");
                OpenDrawer();
                currentState = DrawerStates.OPEN;
                break;
            case DrawerStates.OPEN:
                Debug.Log("Drawer is open");
                //animator.SetTrigger("Close");
                CloseDrawer();
                currentState = DrawerStates.CLOSED;
                break;
        }
    }
}
