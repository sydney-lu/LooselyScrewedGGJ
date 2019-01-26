using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioDictonary : MonoBehaviour
{
    private AudioSource levelSource;
    public AudioSource LevelSource
    {
        get { return levelSource; }
        set { levelSource = value; }
    }

    void Start()
    {
        if (GameManager.Instance)
        {
            GameManager.AudioManager = this;

            levelSource = GetComponent<AudioSource>();
            levelSource.spatialBlend = 0;
        }
    }

    public void playAudio(AudioClip clip)
    {
        levelSource.PlayOneShot(clip);
    }

    public void playAudio(Actor character, AudioClip clip)
    {
        character.SFXSource.PlayOneShot(clip);
    }

    public void playAudio(AudioSource ap, AudioClip clip)
    {
        if(ap) ap.PlayOneShot(clip);
    }

    public void AudioChance(AudioSource ap, AudioClip ac, float chance)
    {
        if (chance < Random.Range(0.0f, 100.0f))
            playAudio(ap, ac);
    }

    private IEnumerator BlendIntoSource(AudioSource a, AudioSource b, float blendTime)
    {
        while (a.volume > 0)
        {
            a.volume -= Time.deltaTime / blendTime;
            b.volume += Time.deltaTime / blendTime;
            yield return null;
        }
        a.Stop();
    }
}
