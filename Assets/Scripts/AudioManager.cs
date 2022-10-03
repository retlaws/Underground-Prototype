using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource audioSource;

    [SerializeField] AudioClip footStepAudio;
    [SerializeField] AudioClip DrillingAudio;

    [Header("Light Swap Audio")]
    [SerializeField] AudioSource swapLightAudioSource; 
    [SerializeField] AudioClip torchSwapAudio;
    [SerializeField] AudioClip lanternSwapAudio;
    [SerializeField] AudioClip flashLightSwapAudio;

    [Header("Tool Audio")]
    [SerializeField] AudioSource toolAudioSource;
    [SerializeField] AudioClip pickAxeAudio;
    [SerializeField] AudioClip drillAudio; 


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void SwapLight(LightType lightType)
    {
        switch (lightType)
        {
            case LightType.flamingTorch:
                swapLightAudioSource.clip = torchSwapAudio;
                swapLightAudioSource.Play();
                break;
            case LightType.flashLight:
                swapLightAudioSource.clip = flashLightSwapAudio;
                swapLightAudioSource.Play();
                break;
            case LightType.latern:
                swapLightAudioSource.clip = lanternSwapAudio;
                swapLightAudioSource.Play();
                break;
            case LightType.blueOrb:
                break;
            default:
                break;
        }
    }

    public void StopDiggingAudio()
    {
        toolAudioSource.Stop();
    }

    public void PlayDiggingAudio(ToolType toolType)
    {
        switch (toolType)
        {
            case ToolType.pickAxe:

                break;

            case ToolType.drill:

                if(toolAudioSource.clip == drillAudio && toolAudioSource.isPlaying) { return; }

                toolAudioSource.clip = drillAudio;
                toolAudioSource.Play();
                break;

            default:
                break;
        }
    }

    public void StopAudio()
    {
        audioSource.Stop(); 
    }

    public void StopLooping()
    {
        audioSource.loop = false; 
    }

    public void PlayFootStepSound()
    {
        audioSource.loop = true; 
        audioSource.clip = footStepAudio;
        audioSource.Play();
    }

    public void PlayDrillingSound()
    {
        audioSource.loop = true; 
        audioSource.clip = DrillingAudio;
        audioSource.Play();
    }

}
