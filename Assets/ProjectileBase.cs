using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public int damage;
    public virtual void OnHit()
    {
        Destroy(gameObject);
    }
}
