using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    public void Create(Vector3 flyingVector)
    {
        rigidbody.velocity = flyingVector;
        gameObject.SetActive(true);
    }
    public void Destroy()
    {
        gameObject.SetActive(false);
    }
}
