using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Create new Enemy")]
[System.Serializable]

public class EnemySO : ScriptableObject
{
    public enum Location
    {
        cyber,
        forest,
    };

    public enum Type
    {
        standart,
        boss,
    };

    public Location location;
    public Type type;
    public float health;
    public float damage; 
}
