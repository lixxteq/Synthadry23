using UnityEngine;

[CreateAssetMenu(fileName = "New Torch Peace", menuName = "Create new Torch Peace")]
[System.Serializable]

public class TorchPeaces : ScriptableObject
{
    public enum Types
    {
        fuel,
        component
    };
    public Types type;
    public int Percents;
}
