using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer mixer;
    public AudioSource bgm;
    public AudioClip[] bgmList;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SceneManager.sceneLoaded += onSceneLoad;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void onSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bgmList.Length; i++)
        {
            if(arg0.name == bgmList[i].name) 
            {
                bgmPlayer(bgmList[i]);
            }
        }
    }

    public void sfxPlayer(string sfxName, AudioClip clip)
    {
        GameObject bgmObj = new GameObject(sfxName + "Sound");
        bgmObj.transform.parent = transform;
        AudioSource audioSource = bgmObj.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Sfx")[0];
        audioSource.clip = clip;
        audioSource.volume = DataManager.instance.curPlayer.sfxVolume;
        audioSource.Play();

        Destroy(bgmObj, clip.length);
    }

    public void bgmPlayer(AudioClip clip)
    {
        bgm.outputAudioMixerGroup = mixer.FindMatchingGroups("Bgm")[0];
        bgm.volume = DataManager.instance.curPlayer.bgmVolume;
        bgm.clip = clip;
        bgm.loop = true;
        bgm.Play();
    }

    public void bgmVolume(float volume)
    {
        mixer.SetFloat("BgmSound", Mathf.Log10(volume) * 20);
    }

    public void sfxVolume(float volume)
    {
        mixer.SetFloat("SfxSound", Mathf.Log10(volume) * 20);
    }
}
