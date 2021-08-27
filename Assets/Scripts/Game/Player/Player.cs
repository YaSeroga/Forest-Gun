using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover playerMover;
    [SerializeField] private GunHandler gunHandler;
    [SerializeField] private Camera camera;

    void Update()
    {
        CheckAimPoint();
    }

    private void CheckAimPoint()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        Plane plane = new Plane(Vector3.back, Vector3.zero);
        plane.Raycast(ray, out float hitDistance);

        Vector3 hitPoint = ray.origin + ray.direction * hitDistance;

        gunHandler.targetPoint = hitPoint;
    }

    public void OnCollisionCountChanged(List<Entity> entities)
    {
        Debug.Log(entities.Count);
        playerMover.IsGrounded = entities.Count > 0;
    }
}
