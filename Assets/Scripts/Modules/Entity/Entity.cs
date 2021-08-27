using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private List<EntityData> entityTypes;
    [SerializeField] private MonoBehaviour mainComponent;

    public MonoBehaviour MainComponent => mainComponent;

    public bool Contains(EntityData targetEntity)
    {
        return entityTypes.Contains(targetEntity);
    }
}
