using UnityEngine;

[CreateAssetMenu(fileName = "New Buff", menuName = "Create new Buff")]
[System.Serializable]

public class Buffs : ScriptableObject
{
    public int id;

    public string itemName;

    public string description;

    public enum Types
    {
        hp,
        speed,
        armor
    };

    public enum Rarity
    {
        standart,
        rare,
        epic
    };

    public int increase;

    public GameObject prefab;
    public Sprite iconActive1K;

    public Types type;
    public Rarity rarity;

}
