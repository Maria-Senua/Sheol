using UnityEngine;
using UnityEngine.Events;

public class CircularMovementDetector : MonoBehaviour
{

    public Transform centerPoint;   
    public Transform trackedTransform;
    public GameObject[] subscenes;
    public GameObject[] dioramas;
    public float speedMultiplier = 0.3f;
    public float distance;

    private Vector3 lastPosition;
    private int direction = 0;
    private bool isInDiorama = false;
    //private bool isNearDiorama = false;

    //public UnityEvent onSpiralConnectorEnter;
    //public UnityEvent onSpiralConnectorExit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (trackedTransform == null) trackedTransform = Camera.main.transform; ;
        lastPosition = trackedTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isInDiorama) MoveSpiral();

        ManageDiorama();
    }

    private void MoveSpiral()
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
            float deltaY = direction * playerSpeed * speedMultiplier * Time.deltaTime;

            foreach (GameObject subscene in subscenes)
            {
                subscene.transform.position += new Vector3(0, deltaY, 0);
            }
        }


        lastPosition = currentPosition;
    }

    private void ManageDiorama()
    {
        foreach (GameObject diorama in dioramas)
        {
            if (Vector3.Distance(diorama.transform.position, trackedTransform.position) < distance)
            {
                diorama.SetActive(true);
            } else
            {
                diorama.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Entrance"))
        {
            Debug.Log("Entered Diorama");
           
            isInDiorama = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Entrance"))
        {
            Debug.Log("Exited Diorama");

            isInDiorama = false;

        }
    }
}
