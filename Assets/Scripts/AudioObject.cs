using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] SFXController sfxController;

    private void Start()
    {
        if ( sfxController == null )
            sfxController = GameObject.FindGameObjectWithTag( "SFXController" ).GetComponent<SFXController>();
    }

    private void OnTriggerEnter(Collider other) {
        //source.Play();
        if (!source.isPlaying)
            sfxController.PlayRandomStaticSound(source);
    }
}
