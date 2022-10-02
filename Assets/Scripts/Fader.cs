using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{

    Image image; 

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public IEnumerator FadeDown()
    {
        float alpha = 0;
        image.enabled = true;
        while(alpha < 0.9f)
        {
            alpha += Time.deltaTime; 
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }

    public IEnumerator FadeUp()
    {
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        image.enabled = false; 
        yield return null;
    }
}
