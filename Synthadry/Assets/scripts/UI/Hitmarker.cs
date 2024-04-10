using GameCreator.Runtime.Characters;
using System.Collections;
using TMPro;
using UnityEngine;

public class Hitmarker : MonoBehaviour
{
    public GameObject uiHitmarker;
    public float hitmarkerTimeUi;
    public float fadeoutTimeSpeed;
    public AudioSource hitmarkerAudio;
    public GameObject uiDamagePrefab;
    public Transform uiHitmarkerParent;
    public float transformMultiplierX = 1;
    public float transformMultiplierY = 1;

    public void DrawHitmarker(float damage)
    {
        if (hitmarkerAudio) hitmarkerAudio.Play();
        StartCoroutine(ShowHitmarker(hitmarkerTimeUi));
        DrawDamage(damage);
    }

    void DrawDamage(float damage)
    {
        GameObject uiDamage = Instantiate(uiDamagePrefab);
        uiDamage.transform.SetParent(uiHitmarkerParent);
        TextMeshProUGUI uiDamageTMPRO = uiDamage.GetComponent<TextMeshProUGUI>();
        uiDamageTMPRO.text = Mathf.Round(damage).ToString();
        uiDamage.transform.localPosition = Vector3.zero;
        StartCoroutine(FadeOutText(fadeoutTimeSpeed, uiDamageTMPRO));
    }


    private IEnumerator ShowHitmarker(float seconds)
    {
        uiHitmarker.SetActive(true);


        yield return new WaitForSeconds(seconds);


        uiHitmarker.SetActive(false);
    }

    Vector3 GenerateTransform(float multiplierX, float multiplierY)
    {
        return new Vector3(Random.Range(-1f, 1f) * multiplierX, Random.Range(1f, 2f) * multiplierY);
    }

    private IEnumerator FadeOutText(float timeSpeed, TextMeshProUGUI text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * timeSpeed));
            text.gameObject.transform.localPosition += GenerateTransform(transformMultiplierX, transformMultiplierY);
            yield return null;
        }
        if (text.color.a <= 0.0f) Destroy(text.gameObject);
    }

}
