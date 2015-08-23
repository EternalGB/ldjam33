using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

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

}
