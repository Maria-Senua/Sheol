using UnityEngine;

public class CircularMovementDetector : MonoBehaviour
{

    public Transform centerPoint;   
    public Transform trackedTransform;
    public GameObject[] subscenes;
    public float moveSpeed = 0.5f;

    private Vector3 lastPosition;
    private int direction = 0;

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
        float playerSpeed = Vector3.Distance(currentPosition, lastPosition) / Time.deltaTime;

        Vector3 toLast = lastPosition - centerPoint.position;
        Vector3 toCurrent = currentPosition - centerPoint.position;

        Vector2 last2D = new Vector2(toLast.x, toLast.z);
        Vector2 current2D = new Vector2(toCurrent.x, toCurrent.z);

        float cross = last2D.x * current2D.y - last2D.y * current2D.x;

        Debug.DrawLine(centerPoint.position, trackedTransform.position, Color.green);


        if (Mathf.Abs(cross) > 0.001f)
        {
            if (cross > 0)
            {
                direction = -1; 
                Debug.Log("Counterclockwise");
            }
            else
            {
                direction = 1; 
                Debug.Log("Clockwise");
            }
        }
        else
        {
            direction = 0; 
        }

        if (direction != 0)
        {
            float deltaY = direction * playerSpeed * Time.deltaTime;

            foreach (GameObject subscene in subscenes)
            {
                subscene.transform.position += new Vector3(0, deltaY, 0);
            }
        }


        lastPosition = currentPosition;
    }
}
