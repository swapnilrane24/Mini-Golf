using UnityEngine;

/// <summary>
/// Script responsible for managing level, like spawning level, spawning balls, deciding game win/loss status and more
/// </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public GameObject ballPrefab;           //reference to ball prefab
    public Vector3 ballSpawnPos;            //reference to spawn position of ball

    public LevelData[] levelDatas;          //list of all the available levels

    private int shotCount = 0;              //count to store available shots

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Method to spawn level
    /// </summary>
    public void SpawnLevel(int levelIndex)
    {
        //we spawn the level prefab at required position
        Instantiate(levelDatas[levelIndex].levelPrefab, Vector3.zero, Quaternion.identity);
        shotCount = levelDatas[levelIndex].shotCount;                                   //set the available shots
        UIManager.instance.ShotText.text = shotCount.ToString();                        //set the ShotText text
                                                                   //then we Instantiate the ball at spawn position
        GameObject ball = Instantiate(ballPrefab, ballSpawnPos, Quaternion.identity);
        CameraFollow.instance.SetTarget(ball);                      //set the camera target
        GameManager.singleton.gameStatus = GameStatus.Playing;      //set the game status to playing
    }

    /// <summary>
    /// Method used to reduce shot
    /// </summary>
    public void ShotTaken()
    {
        if (shotCount > 0)                                          //if shotcount is more than 0
        {
            shotCount--;                                            //reduce it by 1
            UIManager.instance.ShotText.text = "" + shotCount;      //set the text

            if (shotCount <= 0)                                     //if shotCount is less than 0
            {
                LevelFailed();                                          //Level failed
            }
        }
    }

    /// <summary>
    /// Method called when player failed the level
    /// </summary>
    public void LevelFailed()
    {
        if (GameManager.singleton.gameStatus == GameStatus.Playing) //check if the gamestatus is playing
        {
            GameManager.singleton.gameStatus = GameStatus.Failed;   //set gamestatus to failed
            UIManager.instance.GameResult();                        //call GameResult method
        }
    }

    /// <summary>
    /// Method called when player win the level
    /// </summary>
    public void LevelComplete()
    {
        if (GameManager.singleton.gameStatus == GameStatus.Playing) //check if the gamestatus is playing
        {    //check if currentLevelIndex is less than total levels available
            if (GameManager.singleton.currentLevelIndex < levelDatas.Length)    
            {
                GameManager.singleton.currentLevelIndex++;  //increase the count by 1
            }
            else
            {
                //else start from level 0
                GameManager.singleton.currentLevelIndex = 0;
            }
            GameManager.singleton.gameStatus = GameStatus.Complete; //set gamestatus to Complete
            UIManager.instance.GameResult();                        //call GameResult method
        }
    }
}
