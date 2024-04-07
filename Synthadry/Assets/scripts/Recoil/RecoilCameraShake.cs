using UnityEngine;

public class RecoilCameraShake : MonoBehaviour
{
    
    Vector2 GenerateRecoilValues(float recoilMultiplier)
    {
        Vector2 recoilRotation;
        recoilRotation.x = Random.Range(0.0f, 1.0f) * recoilMultiplier;
        recoilRotation.y = Random.Range(0.0f, 1.0f) * recoilMultiplier;

        return recoilRotation;
    }

    public void RotateCamera(float recoilMultiplier)
    {
       gameObject.transform.Rotate(GenerateRecoilValues(recoilMultiplier), 0, Space.Self);
    }


}
