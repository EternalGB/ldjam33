﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject gameOverUI;
    public FillBar bar;
    public GameObject minotaurHUD;
    public StartPoint[] startingPoints;
    public GameObject theseusPrefab;
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
        StartPoint start = startingPoints[Random.Range(0,startingPoints.Length-1)];
        HeroController theseus = ((GameObject)Instantiate(theseusPrefab, 
            start.startingNavPoint.position, Quaternion.identity)).GetComponent<HeroController>();
        theseus.startPoint = start;

        
    }

    public void TheseusGet()
    {
        theseusTributes++;
        UpdateBars();
        if (numTributes - theseusTributes < numNeeded)
            GameOver(false);
    }

    public void MinotaurGet()
    {
        minotaurTributes++;
        UpdateBars();
    }

    void UpdateBars()
    {
        if(!MinotaurHasEnough())
            bar.SetFill(1,(float)theseusTributes / (numTributes - numNeeded));
        else
            bar.SetFill(1, (float)theseusTributes / (numTributes - minotaurTributes));
        bar.SetFill(0,(float)minotaurTributes / numNeeded);
    }

    public bool MinotaurHasEnough()
    {
        return minotaurTributes >= numNeeded;
    }

    public void GameOver(bool playerWon)
    {
        gameOverUI.SetActive(true);
        minotaurHUD.SetActive(false);
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
