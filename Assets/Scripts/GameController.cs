using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public GameObject gameOverUI;
    public Text endText, explanationText;
    public FillBar bar;
    public GameObject minotaurHUD;
    public StartPoint[] startingPoints;
    public GameObject theseusPrefab;
    public int numTributes;
    public int theseusTributes, minotaurTributes;
    public TributeSpawner spawner;
    public int numNeeded;

    public AudioSource eatSound;
    AudioClipPlayer theseusGetSounds;

    public AudioSource loseSound, winSound;

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

        theseusGetSounds = theseus.transform.FindChild("GetSounds").GetComponent<AudioClipPlayer>();
    }

    public void TheseusGet()
    {
        theseusTributes++;
        theseusGetSounds.PlayRandom();
        UpdateBars();
        if (numTributes - theseusTributes < numNeeded)
            GameOver(false, "Theseus collected too many tributes. He can no longer be killed");
    }

    public void MinotaurGet()
    {
        minotaurTributes++;
        eatSound.Play();
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

    public bool TheseusAllCollected()
    {
        return numTributes - theseusTributes - minotaurTributes == 0;
    }

    public void GameOver(bool playerWon, string message)
    {
        gameOverUI.SetActive(true);
        minotaurHUD.SetActive(false);
        if (playerWon && !(winSound.isPlaying || loseSound.isPlaying))
            winSound.Play();
        else
            loseSound.Play();

        endText.text = playerWon ? "You Win" : "You Lose";
        explanationText.text = message;

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
