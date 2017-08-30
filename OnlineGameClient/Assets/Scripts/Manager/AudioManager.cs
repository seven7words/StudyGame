﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class AudioManager : BaseManager
{
    private const string Sound_Prefix = "Sounds/";
    public const string Sound_Alert = "Alert";
    public const string Sound_ArrowShoot = "ArrowShoot";
    public const string Sound_Bg_Fast = "Bg(fast)";
    public const string Sound_Bg_Moderate = "Bg(moderate)";
    public const string Sound_ButtonClick = "ButtonClick";
    public const string Sound_Miss = "Miss";
    public const string Sound_ShootPerson = "ShootPerson";
    public const string Sound_Timer = "Timer";

    private AudioSource bgAudioSource;
    private AudioSource normalAudioSource;
    public AudioManager(GameFacade facade):base(facade)
    {
        
    }

    public override void OnInit()
    {
       GameObject audioSourceGO = new GameObject("AudioSource(GameObject)");
        bgAudioSource = audioSourceGO.AddComponent<AudioSource>();
        normalAudioSource = audioSourceGO.AddComponent<AudioSource>();
        PlaySound(bgAudioSource,LoadSound(Sound_Bg_Moderate),0.5f,true);
    }

    public void PlayBgSound(string soundName)
    {
        PlaySound(bgAudioSource,LoadSound(soundName),0.5f,true);
    }

    public void PlayNormalSound(string soundName)
    {
        PlaySound(normalAudioSource,LoadSound(soundName),1f,false);
    }
    private void PlaySound(AudioSource audioSource, AudioClip clip,float volume, bool isLoop = false )
    {
        
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = isLoop;
        audioSource.Play();
    }
    private AudioClip LoadSound(string soundName)
    {
       AudioClip clip = Resources.Load<AudioClip>(Sound_Prefix + soundName);
        return clip;
    }
}
