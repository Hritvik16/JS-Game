using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    private BoxCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = this.GetComponent<BoxCollider>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject.Find("GameController").GetComponent<GameController>().wallHit();
        Debug.Log("hit");
        collider.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
