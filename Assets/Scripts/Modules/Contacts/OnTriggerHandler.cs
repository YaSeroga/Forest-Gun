using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Linq;

public class OnTriggerHandler : MonoBehaviour
{
    [SerializeField, ReadOnly] private Dictionary<GameObject, Entity> currentContacts = new Dictionary<GameObject, Entity>();
    [SerializeField] private HandleAction[] actionHandlers;

    [Foldout("Events")] public UnityEvent<List<Entity>> OnCurrentTriggersChanged;
    [Foldout("Events")] public UnityEvent<GameObject> OnTriggerEnterEvent;
    [Foldout("Events")] public UnityEvent<GameObject> OnTriggerStayEvent;
    [Foldout("Events")] public UnityEvent<GameObject> OnTriggerExitEvent;

    void OnValidate()
    {
        if (actionHandlers != null)
            foreach (var handler in actionHandlers)
                if (handler.TargetEntity)
                    handler.name = handler.TargetEntity.name;
                else
                    handler.name = "Empty";
    }

    private void OnTriggerEnter(Collider collider)
    {
        GameObject gameObject = collider.attachedRigidbody ?
            collider.attachedRigidbody.gameObject : collider.gameObject;

        if (!currentContacts.ContainsKey(gameObject))
            if (gameObject.TryGetComponent(out Entity entity))
                AddTrigger(gameObject, entity);
    }
    private void OnTriggerStay(Collider collider)
    {
        GameObject gameObject = collider.attachedRigidbody ?
            collider.attachedRigidbody.gameObject : collider.gameObject;

        if (currentContacts.ContainsKey(gameObject))
            HandleTrigger(gameObject);
    }
    private void OnTriggerExit(Collider collider)
    {
        GameObject gameObject = collider.attachedRigidbody ?
            collider.attachedRigidbody.gameObject : collider.gameObject;

        if (currentContacts.ContainsKey(gameObject))
            RemoveTrigger(gameObject);
    }

    private void AddTrigger(GameObject gameObject, Entity entity)
    {
        currentContacts.Add(base.gameObject, entity);
        OnCurrentTriggersChanged.Invoke(currentContacts.Values.ToList());
        OnTriggerEnterEvent?.Invoke(gameObject);
        foreach (HandleAction handler in actionHandlers)
        {
            if (entity.Contains(handler.TargetEntity))
            {
                handler.OnContactEnter?.Invoke(entity);
            }
        }
    }
    private void HandleTrigger(GameObject gameObject)
    {
        OnTriggerStayEvent?.Invoke(gameObject);
        foreach (HandleAction handler in actionHandlers)
        {
            if (currentContacts[gameObject].Contains(handler.TargetEntity))
            {
                handler.OnContactStay?.Invoke(currentContacts[gameObject]);
            }
        }
    }
    private void RemoveTrigger(GameObject gameObject)
    {
        OnTriggerExitEvent?.Invoke(gameObject);
        foreach (HandleAction handler in actionHandlers)
        {
            if (currentContacts[gameObject].Contains(handler.TargetEntity))
            {
                handler.OnContactExit?.Invoke(currentContacts[gameObject]);
            }
        }
        currentContacts.Remove(gameObject);
        OnCurrentTriggersChanged.Invoke(currentContacts.Values.ToList());
    }

}