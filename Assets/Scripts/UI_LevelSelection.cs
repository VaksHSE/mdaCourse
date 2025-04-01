using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_LevelSelection : MonoBehaviour
{
    [SerializeField] private UI_LevelBot buttonPrefab;
    [SerializeField] private Transform butPar;

    [SerializeField] private bool[] lvlsUnlocked;

    private void Start()
    {
        LoadLvlsInfo();
        CreateLevelBot();
    }
    private void CreateLevelBot()
    {
        int levelsAmount = SceneManager.sceneCountInBuildSettings - 1;

        for (int i = 1; i < levelsAmount; i++)
        {
            if (!IsLvlUnlocked(i))
                return;
            UI_LevelBot newBot = Instantiate(buttonPrefab, butPar);

            newBot.SetupBot(i);
        }
    }
    // Start is called before the first frame update
  
    private bool IsLvlUnlocked(int i)
    {
        return lvlsUnlocked[i];
    }
    private void LoadLvlsInfo()
    {
        int levelsAmount = SceneManager.sceneCountInBuildSettings - 1;
        lvlsUnlocked = new bool[levelsAmount];

        for (int i = 1; i < levelsAmount; i++)
        {
            
            bool lvlUnlocked = PlayerPrefs.GetInt("Level" + i + "Unlocked", 0) == 1;
            if (lvlUnlocked)
            {
                //Debug.Log(i);
                lvlsUnlocked[i] = true;
            }
        }
        lvlsUnlocked[1] = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
