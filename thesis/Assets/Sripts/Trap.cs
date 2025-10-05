using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Trap : MonoBehaviour
{
    [SerializeField] private RawImage fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerStateList.HasCheckpoint)
        {
            StartCoroutine(woundSequence(other.gameObject));
        }
    }

    private IEnumerator woundSequence(GameObject player)
    {
        // fade to black
        yield return Fade(0f, 1f);

        // move player to saved transform
        player.transform.SetPositionAndRotation(
            PlayerStateList.SavedPosition,
            PlayerStateList.SavedRotation
        );

        // fade back to clear
        yield return Fade(1f, 0f);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float time = 0f;
        Color c = fadeImage.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeDuration;
            c.a = Mathf.Lerp(startAlpha, endAlpha, t);
            fadeImage.color = c;
            yield return null;
        }

        // ensure exact end value
        c.a = endAlpha;
        fadeImage.color = c;
    }
}
