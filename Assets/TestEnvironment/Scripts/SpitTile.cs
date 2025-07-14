using UnityEngine;

public class SpitTile : MonoBehaviour
{
    public GameObject tile;
    private Animator tileAnimator;
    private Animator fishAnimator;
    public GameObject swimmingFish;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tileAnimator = tile.GetComponent<Animator>();
        fishAnimator = swimmingFish.GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            tile.SetActive(true);
            tileAnimator.Play("TileSpit");
            collision.gameObject.SetActive(false);
            swimmingFish.SetActive(true);
            fishAnimator.SetTrigger("swim");
        }
    }

}
