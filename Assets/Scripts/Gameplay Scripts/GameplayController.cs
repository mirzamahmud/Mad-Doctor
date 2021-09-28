using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    [SerializeField]
    private Text enemyKillCountText;

    private int enemyKillCount;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void EnemyKilled() // this will inform us that we have killed enemies
    {
        enemyKillCount++;

        enemyKillCountText.text = "Enemies Killed: " + enemyKillCount; // display how many enemies we have killed
    }

    public void RestartGame()
    {
        Invoke("Restart", 3f);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scene-1");
    }

}
