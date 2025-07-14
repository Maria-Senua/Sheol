using UnityEngine;

public class SpitTile : MonoBehaviour
{
    public GameObject tile;
    private Animator tileAnimator;
    public Animator fishAnimator;
    public Transform fishPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tileAnimator = tile.GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            tile.SetActive(true);
            tileAnimator.Play("TileSpit");
            collision.gameObject.transform.position = fishPos.position;
            fishAnimator.Play("FishSwim");
        }
    }

}
