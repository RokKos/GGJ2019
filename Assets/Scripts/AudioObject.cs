using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    [SerializeField] AudioSource source;
    

    private void OnTriggerEnter(Collider other) {
        source.Play();
    }
}
