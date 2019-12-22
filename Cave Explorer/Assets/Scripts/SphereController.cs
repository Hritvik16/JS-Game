using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    private Rigidbody rb;
    public Camera camera;
    public float x_speed;
    public float y_speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.AddForce(new Vector3(moveHorizontal * x_speed, 0.0f, moveVertical * x_speed));
        Vector3 currentPosition = this.transform.position;
        camera.transform.position = new Vector3(currentPosition.x, 3, currentPosition.z);
    }
}
