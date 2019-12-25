using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

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
        StartCoroutine(uploadData());
        StartCoroutine(getInstruction());
    }


    IEnumerator uploadData()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        int state_x = (int)Mathf.Round(sphere.transform.position.x);
        int state_z = (int)Mathf.Round(sphere.transform.position.z);

        int reward = GameObject.Find("Plane").GetComponent<CaveGenerator>().Reward((state_x, state_z));
        int finished = GameObject.Find("Plane").GetComponent<CaveGenerator>().isWall((state_x, state_z));
        int stateMap = ((state_x * 5) + state_z);
        formData.Add(new MultipartFormDataSection("source", "GAME"));
        formData.Add(new MultipartFormDataSection("state",  stateMap + "" ));
        formData.Add(new MultipartFormDataSection("reward", reward + ""));
        
        formData.Add(new MultipartFormDataSection("finished", finished + ""));
        formData.Add(new MultipartFormDataSection("action", "-1"));

        UnityWebRequest request = UnityWebRequest.Post("http://127.0.0.1:5000/Server", formData);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        { 
            Debug.Log("State updated!");
        }
    }

    IEnumerator getInstruction()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://127.0.0.1:5000/Server?source=GAME");
        yield return request.SendWebRequest();

        int action = int.Parse(request.downloadHandler.text);
        if(action != -1)
            sphere.GetComponent<SphereController>().move(action);
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
