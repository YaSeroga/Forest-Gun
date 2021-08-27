using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMover playerMover;
    public void OnCollisionCountChanged(List<Entity> entities)
    {
        Debug.Log(entities.Count);
        playerMover.IsGrounded = entities.Count > 0;
    }
}
