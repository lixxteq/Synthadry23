using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Create new enemy")]
[System.Serializable]

public class EnemySO : ScriptableObject
{
    public enum Name
    {
        test
    }


    public enum Type
    {
        regular,
        boss,
        test
    };

    public Name name;
    public string description;
    public Type type;

    public float health;
    public float damage;

}

