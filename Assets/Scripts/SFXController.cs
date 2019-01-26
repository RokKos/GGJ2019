using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    [Header( "Timer" )]
    [SerializeField] float minTick = 2.0f;
    [SerializeField] float maxTick = 10.0f;
    [SerializeField] float currentTick;

    [Header("Creepy SFX")]
    [SerializeField] List<AudioSource> creepyAudioSources;
    [SerializeField] List<AudioClip> creepyAudioClips;

    [Header("Static SFX")]
    [SerializeField] List<AudioSource> staticAudioSources;
    [SerializeField] List<AudioClip> staticAudioClips;

    AudioSource currentRandomSource;
    AudioClip currentRandomClip;

    private void Start()
    {
        currentTick = Random.Range( minTick, maxTick );
        StartCoroutine( Tick(currentTick) );
    }

    private void PlayRandomStaticSound()
    {
        var sourceIndex = Random.Range( 0, staticAudioSources.Count - 1 );
        var clipIndex = Random.Range( 0, staticAudioClips.Count - 1 );

        var source = staticAudioSources[sourceIndex];
        source.clip = staticAudioClips[clipIndex];
        source.Play();
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
}
