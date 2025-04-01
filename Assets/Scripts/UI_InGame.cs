using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_InGame : MonoBehaviour
{
    public static UI_InGame instance;
    public UI_Fade fadeEff;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI fruitText;


    [SerializeField] private GameObject pauseUI;
    private bool isPaused = false;
    private void Awake()
    {
        instance = this;
        fadeEff = GetComponentInChildren<UI_Fade>();
    }
    // Start is called before the first frame update
    void Start()
    {
        fadeEff.ScreenFade(0, 1);
    }

    public void PauseBut()
    {
       if(isPaused){
            isPaused = false;
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseUI.SetActive(true);

        }

    }
    public void GoToMain()
    {
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            PauseBut();

    }

    public void UpdateFruitUI(int collFruits, int totalFruits)
    {
        fruitText.text = collFruits + "/" + totalFruits;
    }

    public void UpdateTimerUI(int timer)
    {
        timerText.text = timer.ToString() + "s";
    }
}
