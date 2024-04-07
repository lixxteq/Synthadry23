using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Create new Item")]
[System.Serializable]

public class ItemSO : ScriptableObject
{
    public enum Name
    {
        ak,
        revolver,
        sword,
        flashlight
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

    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    public Vector3 downPositionOffset;
    public Vector3 downRotationOffset;

    public Vector3 runPositionOffset;
    public Vector3 runRotationOffset;

    public Vector3 righHandIkPosition;
    public Vector3 righHandIkRotation;

    public Vector3 righHandIkPositionHint;
    public Vector3 righHandIkRotationHint;

    public Vector3 leftHandIkPosition;
    public Vector3 leftHandIkRotation;

    public Vector3 leftHandIkPositionHint;
    public Vector3 leftHandIkRotationHint;

    public Vector3 righHandIkPositionShoot;
    public Vector3 righHandIkRotationShoot;

    public Vector3 leftHandIkPositionShoot;
    public Vector3 leftHandIkRotationShoot;

    public float recoilSpeedMultiplier = 1;
    public float cameraShakeMultiplier = 0;
}
