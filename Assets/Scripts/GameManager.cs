using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private UI_InGame inGameUI;
    public static GameManager Instance;
    public int fruitNum = 0;
    public Player player;
    [Header("Player")]
    [SerializeField] private GameObject playerPref;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private float respawnDelay;

    [Header("Level Managment")]
    [SerializeField] private int currLvlIndex;
    [SerializeField] private float lvlTimer;
    private int nextLvlIndex;

    public int totalFruits;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        currLvlIndex = SceneManager.GetActiveScene().buildIndex;
        //Debug.LogWarning(nextLvlIndex);
        //Debug.LogWarning(SceneManager.sceneCountInBuildSettings - 1);
        //Debug.LogWarning(currLvlIndex);
        if (currLvlIndex == (SceneManager.sceneCountInBuildSettings-2)){
            nextLvlIndex = currLvlIndex;
        }
        else{
            nextLvlIndex = currLvlIndex + 1;
        }
        //Debug.Log("---");
        //Debug.LogWarning(nextLvlIndex);
        //Debug.LogWarning(SceneManager.sceneCountInBuildSettings - 1);
        //Debug.LogWarning(currLvlIndex);
        inGameUI = UI_InGame.instance;
        //UI_InGame.instance.UpdateFruitUI(fruitNum, totalFruits);  ?? 
        CellectFruitsInfo();
        
        
    }

    
    private void CellectFruitsInfo()
    {
        Fruit[] allFruits = FindObjectsOfType<Fruit>();
        totalFruits = allFruits.Length;

        inGameUI = UI_InGame.instance;
        UI_InGame.instance.UpdateFruitUI(fruitNum, totalFruits);

        PlayerPrefs.SetInt("Level" + currLvlIndex + "TotalFruits", totalFruits);
    }

    // Update is called once per frame
    void Update()
    {
        lvlTimer += Time.deltaTime;
        inGameUI.UpdateTimerUI((int)lvlTimer);


    }
    public void PickFruit()
    {
        fruitNum++;
        inGameUI.UpdateFruitUI(fruitNum, totalFruits);
        Debug.Log(fruitNum);
    }

    public void SubFruit()
    {
        if (fruitNum == 0){
            player.Die();
            RespawnPlayer();
            return;
        }
        fruitNum--;
        inGameUI.UpdateFruitUI(fruitNum, totalFruits);
        Debug.Log(fruitNum);
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCourutine());
    }

    private IEnumerator RespawnCourutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        GameObject newPlayer = Instantiate(playerPref, respawnPoint.position, Quaternion.identity);
        player = newPlayer.GetComponent<Player>();
        Cinema.instance.linkCinema(player.transform);
    }

    public void UpdatePositionRespawn(Transform pos)
    {
        respawnPoint = pos;
    }
    public void LevelFinished()
    {
        PlayerPrefs.SetInt("Level" + nextLvlIndex + "Unlocked", 1);
        PlayerPrefs.SetInt("NextLvlNumber", nextLvlIndex);
        SaveBestTime();
        SaveBestFruits();
        LoadNextScene();

    }
    private void SaveBestTime()
    {
        float timeLast = PlayerPrefs.GetFloat("Level" + currLvlIndex + "BestTime", 999);
        if (timeLast > lvlTimer)
            PlayerPrefs.SetFloat("Level" + currLvlIndex + "BestTime", lvlTimer);
        //Debug.LogWarning(timeLast);
        //Debug.LogWarning(lvlTimer);
        //PlayerPrefs.SetFloat("Level" + currLvlIndex + "BestTime", lvlTimer); ?? 
    }
    private void SaveBestFruits()
    {
        int fruitsLast = PlayerPrefs.GetInt("Level" + currLvlIndex + "FruitsColl", 0);
        if (fruitsLast < fruitNum)
            PlayerPrefs.SetInt("Level" + currLvlIndex + "FruitsColl", fruitNum);

    }
    public void LevelNextLvl()
    {
        //int nextLvl = currLvlIndex + 1;
        SceneManager.LoadScene("Level_" + nextLvlIndex);
    }

    public void LoadTheEnd()
    {
        SceneManager.LoadScene("Final");

    }
    public void LoadNextScene()
    {
        bool endlvl = currLvlIndex + 2 == SceneManager.sceneCountInBuildSettings;
        if (endlvl)
            UI_InGame.instance.fadeEff.ScreenFade(1, 1.5f, LoadTheEnd);
        else
            UI_InGame.instance.fadeEff.ScreenFade(1, 1.5f, LevelNextLvl);

    }

}
