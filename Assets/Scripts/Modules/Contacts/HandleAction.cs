using UnityEngine.Events;
[System.Serializable]
class HandleAction
{
    [UnityEngine.HideInInspector] public string name = "";
    [UnityEngine.SerializeField] private EntityData targetEntity;
    public UnityEvent<Entity> OnContactEnter;
    public UnityEvent<Entity> OnContactStay;
    public UnityEvent<Entity> OnContactExit;
    public EntityData TargetEntity => targetEntity;
}