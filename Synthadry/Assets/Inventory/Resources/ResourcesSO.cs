using UnityEngine;
[CreateAssetMenu(fileName = "New ResourcesPrice", menuName = "Create new ResourcesPrice")]
[System.Serializable]


public class ResourcesSO : ScriptableObject
{
    public int fuel = 0;
    public int cloth = 0;
    public int metal = 0;
    public int plastic = 0;
    public int chemical = 0;
    public int wires = 0;
}
