using UnityEngine;
using UnityEngine.SceneManagement;

public class PassthroughManager : MonoBehaviour
{
    public void SwitchScene()
    {
        Debug.Log("1234 ");
        SceneManager.LoadScene("Design_Prototyping");
    }
}
