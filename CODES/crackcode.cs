using UnityEngine;
using System.Collections;

public class CrackCode : MonoBehaviour
{
    public SpriteRenderer sr;
    public float fadeDuration = 1f;

    void Start()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color startColor = sr.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            sr.color = Color.Lerp(startColor, endColor, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        sr.color = endColor;
        Destroy(gameObject);
    }

}
