using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 2;
    public int WeaponHolding;
    public string projectileName;
    public float weaponSpeed;
    private bool canAttack = true;
    public virtual void Shoot()
    {
        canAttack = false;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 difference = mousePosition - transform.position;
        difference.Normalize();
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        GameObject projectile = GameObject.Instantiate((GameObject)Resources.Load(projectileName));
        projectile.transform.position = Vector3.Lerp(transform.position, projectile.transform.up, 0.1f);
        projectile.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90);
        StartCoroutine(ShootDelay());
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(weaponSpeed);
        canAttack = true;
    }

    void Update()
    {
        inputUpdate();
    }
    void inputUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(2, 0, 0), Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(-2, 0, 0), Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 2, 0), Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, -2, 0), Time.deltaTime * speed);
        }
        if (Input.GetMouseButton(0))
        {
            if (canAttack)
            {
                switch (WeaponHolding)
                {
                    case 0:
                        Shoot();
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                }
            }
        }
    }
}
