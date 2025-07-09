using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DrawningController : MonoBehaviour
{
    public GameObject boat;
    public GameObject lake;
    public GameObject eva;
    public float drawningTime = 7f;

    private Animator boatAnimator;
    private Animator lakeAnimator;

    private void Awake()
    {
        boatAnimator = boat.gameObject.GetComponent<Animator>();
        lakeAnimator = lake.gameObject.GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void LookInWater()
    {
        boatAnimator.Play("MoveBoat");
        lakeAnimator.Play("LakeMove");
        Invoke("Drawn", drawningTime);
    }

    public void Drawn()
    {
        StartCoroutine(MoveEvaDown());
    }

    private IEnumerator MoveEvaDown()
    {
        float duration = 3f;
        float elapsed = 0f;
        Vector3 startPos = eva.transform.position;
        Vector3 targetPos = startPos + Vector3.down * 2f; 

        while (elapsed < duration)
        {
            eva.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        eva.transform.position = targetPos;
    }

}
