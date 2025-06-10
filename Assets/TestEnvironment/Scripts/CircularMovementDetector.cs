using UnityEngine;

public class CircularMovementDetector : MonoBehaviour
{

    public Transform centerPoint;   
    private Vector3 lastPosition;
    public Transform trackedTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (trackedTransform == null) trackedTransform = Camera.main.transform; ;
        lastPosition = trackedTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = trackedTransform.position;

        Vector3 toLast = lastPosition - centerPoint.position;
        Vector3 toCurrent = currentPosition - centerPoint.position;

        Vector2 last2D = new Vector2(toLast.x, toLast.z);
        Vector2 current2D = new Vector2(toCurrent.x, toCurrent.z);

        float cross = last2D.x * current2D.y - last2D.y * current2D.x;

        Debug.DrawLine(centerPoint.position, trackedTransform.position, Color.green);


        if (Mathf.Abs(cross) > 0.001f) 
        {
            if (cross > 0)
                Debug.Log("Counterclockwise");
            else
                Debug.Log("Clockwise");
        }

        lastPosition = currentPosition;
    }
}
