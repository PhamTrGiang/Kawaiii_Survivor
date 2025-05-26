using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public bool IsSfxOn { get; private set; }
    public bool IsMusicOn { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        SettingsManager.onSFXStateChanged += SFXStateChangedCallback;
        SettingsManager.onMusicStateChanged += MusicStateChangedCallback;
    }

    private void OnDestroy()
    {
        SettingsManager.onSFXStateChanged -= SFXStateChangedCallback;
        SettingsManager.onMusicStateChanged -= MusicStateChangedCallback;
    }

    private void MusicStateChangedCallback(bool musicState)
    {
        IsMusicOn = musicState;
    }

    private void SFXStateChangedCallback(bool sfxstate)
    {
        IsSfxOn = sfxstate;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
