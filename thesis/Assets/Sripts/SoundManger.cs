using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class SoundManger : MonoBehaviour

{
    public static SoundManger instance;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource soundFXObject;
    [SerializeField] private AudioSource footsteps;
    private bool foot;
    [SerializeField] private PC2 PC;
    [SerializeField] private AudioSource jump;

    [SerializeField] private AudioSource Music;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
    }



    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0f)
        {
            if (!foot && PC.Grounded())
            {
                footsteps.Play();
                foot = true;
            }
        }
        else
        {
            footsteps.Stop();
            foot = false;
        }
        if (Input.GetButtonDown("Jump"))
        {
            jump.Play();
        }

    }


    public void PlaySoundFX(AudioClip aClip, Transform spawnT, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnT.position, Quaternion.identity);

        audioSource.clip = aClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }


    public void SetMasterVolume(float level)
    {
        Debug.Log($"Slider raw value: {level}");
        float db = Mathf.Log10(level) * 20f;
        Debug.Log($"MusicVolume set to {db} dB (from {level})");

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(level) *20f);

    }

    public void SetFXVolume(float level)
    {
        audioMixer.SetFloat("FXVolume", Mathf.Log10(level) * 20f);
    }

    public void SetMusicVolume(float level)
    {

        Debug.Log($"Slider raw value: {level}");
        float db = Mathf.Log10(level) * 20f;
        Debug.Log($"MusicVolume set to {db} dB (from {level})");

        audioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
    }

}
