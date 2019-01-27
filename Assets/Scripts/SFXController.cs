using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    [Header( "Timer" )]
    [SerializeField] float minTick = 2.0f;
    [SerializeField] float maxTick = 10.0f;
    [SerializeField] float currentTick;

    [Header( "Volume" )]
    [SerializeField] float minVolume = 0.1f;
    [SerializeField] float maxVolume = 0.5f;

    [Header( "Pitch" )]
    [SerializeField] float minPitch = 0.1f;
    [SerializeField] float maxPitch = 0.9f;

    [Header( "Stereo Pan" )]
    [SerializeField] float minPan = -1.0f;
    [SerializeField] float maxPan = 1.0f;

    [Header("Creepy SFX")]
    [SerializeField] List<AudioSource> creepyAudioSources;
    [SerializeField] List<AudioClip> creepyAudioClips;

    [Header("Static SFX")]
    [SerializeField] List<AudioSource> staticAudioSources;
    [SerializeField] List<AudioClip> staticAudioClips;

    AudioSource currentRandomSource;
    AudioClip currentRandomClip;

    [Header( "BG Music" )]
    [SerializeField] AudioSource bgMusicSource;

    private void Start()
    {
        currentTick = Random.Range( minTick, maxTick );
        StartCoroutine( Tick(currentTick) );

        bgMusicSource.loop = true;
        bgMusicSource.Play();
    }

    public void PlayRandomStaticSound(AudioSource givenSource = null)
    {
        if ( givenSource == null )
        {
            var sourceIndex = Random.Range( 0, staticAudioSources.Count - 1 );
            givenSource = staticAudioSources[sourceIndex];
        }
        var clipIndex = Random.Range( 0, staticAudioClips.Count - 1 );
        givenSource.clip = staticAudioClips[clipIndex];
        Debug.Log("STATIC clip: " + givenSource.clip.name);
        givenSource.Play();
    }

    private void StopRandomSounds()
    {
        StopAllCoroutines();
        currentRandomSource.Stop();
    }

    private void PlayRandomCreepySound()
    {
        var sourceIndex = Random.Range( 0, creepyAudioSources.Count - 1 );
        var clipIndex = Random.Range( 0, creepyAudioClips.Count - 1 );

        currentRandomSource = creepyAudioSources[sourceIndex];
        currentRandomClip = creepyAudioClips[clipIndex];
        currentRandomSource.clip = currentRandomClip;
        RandomizeSound( currentRandomSource );
        Debug.Log( "current random sound: " + currentRandomSource.clip + " (vol: " + currentRandomSource.volume + " - pitch: " + currentRandomSource.pitch + " - stereopan: " + currentRandomSource.panStereo + ")" );
        currentRandomSource.Play();
        StartCoroutine( WaitForAudioCompleted() );
    }

    private IEnumerator Tick(float theTick)
    {
        float counter = theTick;
        while ( counter > 0 )
        {
            yield return new WaitForSeconds( .1f );
            counter -= .1f;
        }
        Debug.Log("ticked for " + theTick + " seconds.");
        theTick = Random.Range( minTick, maxTick );
        PlayRandomCreepySound();
    }

    private IEnumerator WaitForAudioCompleted()
    {
        //yield return new WaitUntil( () => currentRandomSource.isPlaying == false );
        yield return new WaitForSeconds( currentRandomClip.length );
        Debug.Log("waited for " + currentRandomClip.length + " seconds (" + currentRandomClip.name + " -clip- from " + currentRandomSource.name + " -source-).");
        StartCoroutine( Tick( currentTick ) );
    }

    private void RandomizeSound(AudioSource source, bool randomizeVolume = true, bool randomizePitch = true, bool randomizeStereoPan = true)
    {
        if ( randomizeVolume )
        {
            var volume = Random.Range( minVolume, maxVolume );
            source.volume = volume;
        }

        if ( randomizePitch )
        {
            var pitch = Random.Range( minPitch, maxPitch );
            source.pitch = pitch;
        }

        if ( randomizeStereoPan )
        {
            var stereoPan = Random.Range( minPan, maxPan );
            source.panStereo = stereoPan;
        }
    }
}
