using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DrawningController : MonoBehaviour
{
    public GameObject boat;
    public GameObject lake;
    public GameObject eva;
    public float drawningTime = 7f;
    public GameObject reflection;
    public float realisingTime = 5f;
    public float startDelay = 3f;
    public GameObject ripple;

    private Animator boatAnimator;
    private Animator lakeAnimator;

    public UnityEvent afterBending;
    public UnityEvent onRealisation;

    private void Awake()
    {
        boatAnimator = boat.gameObject.GetComponent<Animator>();
        lakeAnimator = lake.gameObject.GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eva.SetActive(false);
        reflection.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        startDelay -= Time.deltaTime;
        if (startDelay <= 0) LookInWater();
    }

    public void LookInWater()
    {
        boatAnimator.Play("MoveBoat");
        lakeAnimator.Play("LakeMove");
        
        Invoke("ThrowOrb", 2f);
    }

    public void ThrowOrb()
    {
        afterBending?.Invoke();
    }

    public void SeeEva()
    {
        eva.SetActive(true);
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
            ripple.SetActive(true);
            eva.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        eva.transform.position = targetPos;
        //ripple.SetActive(false);
        reflection.SetActive(true);

        yield return new WaitForSeconds(realisingTime);

        onRealisation?.Invoke();
    }

}
