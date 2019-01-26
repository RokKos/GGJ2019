using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVNoiseController : MonoBehaviour
{

    [SerializeField] Material material;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += new Vector2(Time.deltaTime, 0);
    }
}
