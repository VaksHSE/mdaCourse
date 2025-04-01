using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Fade : MonoBehaviour
{

    [SerializeField] private Image fadeImage;

    public void ScreenFade(float targetAlpha, float dur, System.Action onComplete=null)
    {
        StartCoroutine(fadeCoroutine(targetAlpha, dur, onComplete));
    }
    private IEnumerator fadeCoroutine(float targetAlpha, float dur, System.Action onComplete)
    {
        float time = 0;
        Color currCol = fadeImage.color;

        float startAlpha = currCol.a;

        while (time < dur)
        {
            time+= Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / dur);

            fadeImage.color = new Color(currCol.r, currCol.g, currCol.b, alpha);
            yield return null;
        }

        fadeImage.color = new Color(currCol.r, currCol.g, currCol.b, targetAlpha);

        if(onComplete != null)
            onComplete.Invoke();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
