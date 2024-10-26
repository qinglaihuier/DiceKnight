using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager instance;
    public static AudioManager Instance{ get { return instance; } }

    AudioSource bgmAudioSource;

    AudioSource vfxAudioSource;

    public AllAudioData allAudioData;
    private void Awake()
    {
        if(instance !=null )
        {
            Destroy(this);

            Debug.LogWarning("创造了多个音效管理器单例");
        }
        instance = this;

        bgmAudioSource = gameObject.AddComponent<AudioSource>();

        vfxAudioSource = gameObject.AddComponent<AudioSource>();
    }
    public void playerBGM(AudioData data)
    {
        bgmAudioSource.loop = true;

        bgmAudioSource.clip = data.audioClip;

        bgmAudioSource.volume = data.volume;

        bgmAudioSource.pitch = data.pitch;

        bgmAudioSource.Play();
    }
    public void playVFX(AudioData data)
    {
        //vfxAudioSource.clip = data.audioClip;
        vfxAudioSource.pitch = data.pitch;

        vfxAudioSource.PlayOneShot(data.audioClip, data.volume);
    }
    public AudioData getAudioData(AudioName name)
    {
        foreach (AudioData data in allAudioData.datas)
        {
            if (data.audioName == name) return data;
        }
        return null;
    }
}
