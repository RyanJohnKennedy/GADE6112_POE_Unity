using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed;
    public float rotationSpeed;
    

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3(transform.position.x, 2.1f , transform.position.z);

        if (transform.position.y > 2f)
        {
            transform.position += transform.forward * Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        }

        if(transform.position.y < 9f && transform.position.y > 2.2f)
        {
            transform.Rotate(Input.GetAxis("Mouse ScrollWheel") * rotationSpeed * -1f, 0f, 0f);
        }

        if (transform.position.y < 2f)
        {
            transform.position = position;
        }
        
    }
}
