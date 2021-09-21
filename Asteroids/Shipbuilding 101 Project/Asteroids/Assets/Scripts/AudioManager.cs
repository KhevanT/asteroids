﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The audio manager
/// </summary>
public static class AudioManager
{
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    /// <summary>
    /// Initializes the audio manager
    /// </summary>
    /// <param name="source">audio source</param>
    public static void Initialize(AudioSource source)
    {
        audioSource = source;
        audioClips.Add(AudioClipName.AsteroidHitBig, 
            Resources.Load<AudioClip>("Audio/BigExplosion"));
        audioClips.Add(AudioClipName.AsteroidHitSmall,
            Resources.Load<AudioClip>("Audio/SmallExplosion"));
        audioClips.Add(AudioClipName.PlayerDeath,
            Resources.Load<AudioClip>("Audio/MediumExplosion"));
        audioClips.Add(AudioClipName.PlayerShot,
            Resources.Load<AudioClip>("Audio/FireBullet"));
        audioClips.Add(AudioClipName.ShipThrust,
            Resources.Load<AudioClip>("Audio/Thrust"));
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
}
