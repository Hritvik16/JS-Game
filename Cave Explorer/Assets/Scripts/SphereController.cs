using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SphereController : MonoBehaviour
{
    private Rigidbody rb;
    public Camera camera;
    public float speed;

    public float camera_y;

    private int back = 0;
    private int right = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void move(int dir)
    {
        if (dir == 0) // LEFT
        {
            right = -1;
            back = 0;
        }
        else if (dir == 1) // RIGHT
        {
            right = 1;
            back = 0;
        }
        if (dir == 2) // DOWN
        {
            back = -1;
            right = 0;
        }
        else if (dir == 3) // UP
        {
            back = 1;
            right = 0;
        }
    }

    private void FixedUpdate()
    {

        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        rb.AddForce(new Vector3(speed * right, 0.0f, speed * back));

        Vector3 currentPosition = this.transform.position;
        camera.transform.position = new Vector3(currentPosition.x, camera_y, currentPosition.z);
    }
}
