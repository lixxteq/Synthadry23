using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create new Item")]
[System.Serializable]

public class Items : ScriptableObject
{
    public enum Name
    {
        ak,
        revolver
    }


    public enum Type
    {
        firearms,
        coldWeapons,
        extra
    };

    public enum Rarity
    {
        standart,
        rare,
        epic
    };



    public Name name;
    public string description;
    public Type type;
    public Rarity rarity;

    public Sprite iconActive1K;
    public Sprite iconDisable1K;


}
