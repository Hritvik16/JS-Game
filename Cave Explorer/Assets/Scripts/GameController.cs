using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject plane;
    public GameObject sphere;
    public UnityEngine.UI.Text winLoseText;
    // Start is called before the first frame update
    void Start()
    {
        winLoseText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void wallHit()
    {
        sphere.GetComponent<SphereController>().enabled = false;
        winLoseText.text = "you lose";
        winLoseText.enabled = true;
        StartCoroutine(delay());
    }

    public void goalReached()
    {
        sphere.GetComponent<SphereController>().enabled = false;
        winLoseText.text = "you win";
        winLoseText.enabled = true;
        StartCoroutine(delay());
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
