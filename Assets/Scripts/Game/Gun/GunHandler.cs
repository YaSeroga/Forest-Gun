using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHandler : MonoBehaviour
{
    [SerializeField] private float rotationSensitivity = 10;
    public Vector3 targetPoint;
    void Update()
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSensitivity * Time.deltaTime);
    }
}
