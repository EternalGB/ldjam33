using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioClipPlayer : MonoBehaviour
{

    AudioSource source;
    public AudioClip[] clips;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayRandom()
    {
        if(!source.isPlaying)
        {
            source.clip = clips[Random.Range(0, clips.Length)];
            source.Play();
        }
    }


}
