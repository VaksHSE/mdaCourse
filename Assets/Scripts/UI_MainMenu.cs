using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UI_MainMenu : MonoBehaviour
{
    public string sceneName;
    private UI_Fade fadeEffect;
    [SerializeField] private GameObject[] uiElements;
    [SerializeField] private GameObject contBut;
    private void Awake()
    {
        Time.timeScale = 1.0f;
        fadeEffect = GetComponentInChildren<UI_Fade>();
    }
    private void Start()
    {
        AudioManager.instance.PlaySound(2);
        if (HasLvlProgress())
            contBut.SetActive(true);
        fadeEffect?.ScreenFade(0, 1.5f);
        Debug.LogWarning("fade");
        Debug.LogWarning(fadeEffect);
    }
    public void SwitchUI(GameObject uiToEnable)
    {
        AudioManager.instance.PlaySound(8);
        foreach (GameObject ui in uiElements)
        {
            ui.SetActive(false);
        }
        uiToEnable.SetActive(true);
    }

    public void NewGame()
    {
        fadeEffect.ScreenFade(1, 1.5f, LoadLevelScene);
        AudioManager.instance.PlaySound(8);
        //AudioManager.instance.PlaySound(8);
        //SceneManager.LoadScene(sceneName);
    }

    
    public void LoadLevelScene()
    {
        
        SceneManager.LoadScene(sceneName);
    }

    private bool HasLvlProgress()
    {
        bool hasLevelP = PlayerPrefs.GetInt("NextLvlNumber", 0)!=0;
        return hasLevelP;
    }

    public void LoadContLvl()
    {
        AudioManager.instance.PlaySound(8);
        int nextLvl = PlayerPrefs.GetInt("NextLvlNumber", 0);
        SceneManager.LoadScene("Level_" + nextLvl);
    }
}
