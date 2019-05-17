using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(introPlay());
    }

    //letting the intro music play out
    IEnumerator introPlay()
    {
        yield return new WaitForSeconds(82f);
        SceneManager.LoadScene("HubWorld");
    }
    public void Skip()
    {
        SceneManager.LoadScene("HubWorld");
    }
}