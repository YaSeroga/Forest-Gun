using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;
using System;

public class OnCollisionHandler : MonoBehaviour
{
    [SerializeField, ReadOnly] private Dictionary<GameObject, Entity> currentContacts = new Dictionary<GameObject, Entity>();
    [SerializeField] private HandleAction[] actionHandlers;
    [Foldout("Events")] public UnityEvent<List<Entity>> OnCurrentContactsChanged;
    [Foldout("Events")] public UnityEvent<GameObject> OnCollisionEnterEvent;
    [Foldout("Events")] public UnityEvent<GameObject> OnCollisionStayEvent;
    [Foldout("Events")] public UnityEvent<GameObject> OnCollisionExitEvent;

    void OnValidate()
    {
        foreach (var handler in actionHandlers)
            if (handler.TargetEntity)
                handler.name = handler.TargetEntity.name;
            else
                handler.name = "Empty";
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject gameObject = collision.rigidbody ?
            collision.rigidbody.gameObject : collision.gameObject;

        if (!currentContacts.ContainsKey(gameObject))
        {
            if (gameObject.TryGetComponent(out Entity entity))
            {
                AddContact(gameObject, entity);
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        GameObject gameObject = collision.rigidbody ?
            collision.rigidbody.gameObject : collision.gameObject;

        if (currentContacts.ContainsKey(gameObject))
            HandleCollision(gameObject);
    }
    private void OnCollisionExit(Collision collision)
    {
        GameObject gameObject = collision.rigidbody ?
            collision.rigidbody.gameObject : collision.gameObject;

        if (currentContacts.ContainsKey(gameObject))
        {
            RemoveContact(gameObject);
        }
    }


    private void AddContact(GameObject gameObject, Entity entity)
    {
        currentContacts.Add(gameObject, entity);
        OnCurrentContactsChanged?.Invoke(currentContacts.Values.ToList());
        OnCollisionEnterEvent?.Invoke(gameObject);

        foreach (HandleAction handler in actionHandlers)
            if (entity.Contains(handler.TargetEntity))
                handler.OnContactEnter?.Invoke(entity);
    }
    private void HandleCollision(GameObject gameObject)
    {
        OnCollisionStayEvent?.Invoke(gameObject);

        foreach (HandleAction handler in actionHandlers)
            if (currentContacts[gameObject].Contains(handler.TargetEntity))
                handler.OnContactStay?.Invoke(currentContacts[gameObject]);
    }
    private void RemoveContact(GameObject gameObject)
    {
        OnCollisionExitEvent?.Invoke(gameObject);

        foreach (HandleAction handler in actionHandlers)
            if (currentContacts[gameObject].Contains(handler.TargetEntity))
                handler.OnContactExit?.Invoke(currentContacts[gameObject]);

        currentContacts.Remove(gameObject);
        OnCurrentContactsChanged?.Invoke(currentContacts.Values.ToList());
    }
}