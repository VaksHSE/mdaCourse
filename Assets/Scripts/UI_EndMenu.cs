using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI_EndMenu : MonoBehaviour
{
    public string sceneName;
    private UI_Fade fadeEff;

    private void Awake()
    {
        fadeEff = GetComponentInChildren<UI_Fade>();
        
    }

    private void Start()
    {
        fadeEff.ScreenFade(0, 1);
    }
    public void GoToMain()
    {
        AudioManager.instance.PlaySound(8);
        fadeEff.ScreenFade(1, 1, EndGame);
    }
    public void EndGame()
    {
        SceneManager.LoadScene(sceneName);
    }
}
