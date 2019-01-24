using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public int damage;
    public int speed = 5;
    public virtual void OnHit()
    {
        Destroy(gameObject);
    }
    public void Update()
    {
        MovementUpdate();
    }
    public virtual void MovementUpdate()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }
}
