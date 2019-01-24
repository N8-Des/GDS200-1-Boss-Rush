using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 2;
    public int WeaponHolding;
    public string projectileName;
    void Start()
    {
        
    }
    public virtual void Shoot()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        GameObject projectile = GameObject.Instantiate((GameObject)Resources.Load(projectileName));
        projectile.transform.rotation = transform.LookAt(mousePosition, Vector3.up);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(2, 0, 0), Time.deltaTime * speed);
        } if (Input.GetKey(KeyCode.A))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(-2, 0, 0), Time.deltaTime * speed);
        } if (Input.GetKey(KeyCode.W))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 2, 0), Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, -2, 0), Time.deltaTime * speed);
        }
    }
}
