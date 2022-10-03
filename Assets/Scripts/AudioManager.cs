using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource audioSource;

    [SerializeField] AudioClip footStepAudio;
    [SerializeField] AudioClip DrillingAudio;

    [Header("Light Swap Audio")] // all of this should be stored on the toolConfig
    [SerializeField] AudioSource swapLightAudioSource; 
    [SerializeField] AudioClip torchSwapAudio;
    [SerializeField] AudioClip lanternSwapAudio;
    [SerializeField] AudioClip flashLightSwapAudio;

    [Header("Tool Audio")] // this should also be stored on the toolconfig
    [SerializeField] AudioSource toolAudioSource;
    [SerializeField] List<AudioClip> pickAxeAudioClips;
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

    public void SwapLight(LightType lightType) // shouldn't of used a switch statement - should just feed in the clip to be played from a list on the tool/light config
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
        if(toolAudioSource.clip == pickAxeAudioClips[0]) { return; } //this only works with one audio clip in the list for the pickaxe - basically this is a massive bodge
        toolAudioSource.Stop();
    }

    int pickaxeAudioIndex = 0;

    public void PlayDiggingAudio(ToolType toolType)
    {
        switch (toolType)
        {
            case ToolType.pickAxe:
                if(toolAudioSource.isPlaying) { return; }
                toolAudioSource.volume = 1; // TODO this is a magic number
                toolAudioSource.clip = pickAxeAudioClips[pickaxeAudioIndex];
                toolAudioSource.Play();
                pickaxeAudioIndex++;

                if(pickaxeAudioIndex == pickAxeAudioClips.Count)
                {
                    pickaxeAudioIndex = 0;
                }

                break;

            case ToolType.drill:

                if(toolAudioSource.clip == drillAudio && toolAudioSource.isPlaying) { return; }
                toolAudioSource.volume = 0.2f; // TODO this is a magic number and just a bit lazy!
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

    public void PlayFootStepSound() // not implemented yet
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
