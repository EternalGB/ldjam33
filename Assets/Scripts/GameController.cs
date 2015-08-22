using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    public int numTributes;
    public int theseusTributes, minotaurTributes;
    public TributeSpawner spawner;

    // Use this for initialization
    void Start()
    {
        theseusTributes = 0;
        minotaurTributes = 0;
        spawner.SpawnTributes(numTributes);
    }

    void TheseusGet()
    {
        theseusTributes++;
    }

    void MinotaurGet()
    {
        minotaurTributes++;
    }

}
