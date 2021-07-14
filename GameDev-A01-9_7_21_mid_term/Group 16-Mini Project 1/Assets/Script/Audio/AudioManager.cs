using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("musicVolume");
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Play("MenuMusic");
        }
    }
    public void ButtonClickMusic()
    {
        //Debug.Log("Press");
        Play("ButtonClick");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        Debug.Log("Press");
        s.source.Play();
    }
    public void PlayWithFade(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        StartCoroutine(UpdateWithFade(s, 1f));
    }
    private IEnumerator UpdateWithFade(Sound s, float transitionTime = 1.0f)
    {
        if (!s.source.isPlaying)
        {
            s.source.Play();
        }
        float t = 0.0f;
        for (t = 0; t < transitionTime; t += Time.deltaTime)
        {
            s.source.volume = (1 - (t / transitionTime));
            yield return null;
        }
        s.source.Stop();
    }
}
