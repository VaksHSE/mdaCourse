using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_LevelBot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelNemberText;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private TextMeshProUGUI fruitsNubberText;
    public int lvlIndex;

    public string sceneName;
    public void SetupBot(int newlevelIndex)
    {
        lvlIndex = newlevelIndex;

        sceneName = "Level_" + newlevelIndex;
        levelNemberText.text = "Level " + lvlIndex;

        bestTimeText.text = TimerInfoText();
        fruitsNubberText.text = FruitInfoText();
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(sceneName);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private string TimerInfoText()
    {
        int timerValue = (int)PlayerPrefs.GetFloat("Level" + lvlIndex + "BestTime", 999);
        if (timerValue!=999)
            return "Best Time: " + timerValue.ToString();
        return "Best Time: " + "No info";
    }
    private string FruitInfoText()
    {
        int totalF = PlayerPrefs.GetInt("Level" + lvlIndex + "TotalFruits", 0);
        string total;
        if (totalF == 0)
            total = "*";
        else
            total = totalF.ToString();

        int fCol = PlayerPrefs.GetInt("Level" + lvlIndex + "FruitsColl", 0);

        return "Fruits: "+fCol.ToString()+"/" + total;
    }

    
}
