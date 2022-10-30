using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource normalStateSource;
    public AudioSource scaredStateSource;
    // Start is called before the first frame update
    void Start()
    {
        normalStateSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
