using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject gameOverUI;
    
    public int numTributes;
    public int theseusTributes, minotaurTributes;
    public TributeSpawner spawner;
    public int numNeeded;


    // Use this for initialization
    void Start()
    {
        theseusTributes = 0;
        minotaurTributes = 0;
        spawner.SpawnTributes(numTributes);
    }

    public void TheseusGet()
    {
        theseusTributes++;
    }

    public void MinotaurGet()
    {
        minotaurTributes++;
    }

    public bool MinotaurHasEnough()
    {
        return minotaurTributes >= numNeeded;
    }

    public void GameOver(bool playerWon)
    {
        gameOverUI.SetActive(true);
        gameOverUI.GetComponentInChildren<Text>().text = playerWon ? "You Win" : "You Lose";

    }

    public void Retry()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
