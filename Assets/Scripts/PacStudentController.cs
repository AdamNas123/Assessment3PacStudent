using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    //private Tween activeTween;
    [SerializeField]
    private Tweener tweener;

    private Animator pacStudentAnimator;
    private AudioSource movingNoPellet;
    private AudioSource movingWithPellet;

    // Start is called before the first frame update
    void Start()
    {
        pacStudentAnimator = GetComponent<Animator>();
        movingNoPellet = GetComponent<AudioSource>();
        //activeTween = new Tween(gameObject.transform, new Vector3(-16.5f, -1.5f, 0.0f), new Vector3(-11.5f, -1.5f, 0.0f), Time.time, 1.5f);
        //movingNoPellet.loop = true;
        //movingNoPellet.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y, 0.0f), 1.5f);
        }

        if (Input.GetKeyDown("d"))
        {
            tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y, 0.0f), 1.5f);
        }

        if (Input.GetKeyDown("s"))
        {
            tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, -2.0f), 1.5f);
        }

        if (Input.GetKeyDown("w"))
        {
            tweener.AddTween(gameObject.transform, gameObject.transform.position, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, 2.0f), 1.5f);
        }
    }
}
