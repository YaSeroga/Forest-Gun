using UnityEngine;

[CreateAssetMenu(menuName = "yaseroga/Entity data")]
public class EntityData : ScriptableObject
{
    [SerializeField, Multiline] private string metadata;
    public string Metadata => metadata;
}
