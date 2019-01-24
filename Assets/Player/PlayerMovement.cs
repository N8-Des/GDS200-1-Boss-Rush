using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 2;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(2, 0, 0), Time.deltaTime * speed);
        } else if (Input.GetKey(KeyCode.A))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(-2, 0, 0), Time.deltaTime * speed);
        } else if (Input.GetKey(KeyCode.W))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 2, 0), Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, -2, 0), Time.deltaTime * speed);
        }
    }
}
