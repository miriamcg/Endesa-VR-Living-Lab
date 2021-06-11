using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image button;

    void Start() {
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn() {
        Image img = button.GetComponent<Image>();
        Color colorB = img.color;
        for (float f = 0f; f <= 0.75f; f += Time.deltaTime) {
            colorB.a = f;
            img.color = colorB;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
