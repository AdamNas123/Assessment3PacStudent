using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Tween rightTween;
    private Tween leftTween;
    private Tween downTween;
    private Tween upTween;

    private Tween activeTween;
    
    private Animator pacStudentAnimator;
    private AudioSource movingNoPellet;
    // Start is called before the first frame update
    void Start()
    {
        pacStudentAnimator = GetComponent<Animator>();
        movingNoPellet = GetComponent<AudioSource>();
        rightTween = new Tween(gameObject.transform, new Vector3(-16.5f, -1.5f, 0.0f), new Vector3(-11.5f, -1.5f, 0.0f), Time.time, 1.5f);
        activeTween = rightTween;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeTween == rightTween)
        {
            if (Vector3.Distance(activeTween.Target.position, activeTween.EndPos) > 0.1f)
            {
                float elapsedTime = Time.time - activeTween.StartTime;
                float t = elapsedTime / activeTween.Duration;
                activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, t);
                movingNoPellet.Play();
                pacStudentAnimator.Play("PacStudentRightAnim");

            }
            else 
            {
                activeTween.Target.position = activeTween.EndPos;
                downTween = new Tween(gameObject.transform, new Vector3(-11.5f, -1.5f, 0.0f), new Vector3(-11.5f, -5.5f, 0.0f), Time.time, 1.2f);
                activeTween = downTween;
            }
        }
        
        if (activeTween == downTween)
        {
            if (Vector3.Distance(activeTween.Target.position, activeTween.EndPos) > 0.1f)
            {
                float elapsedTime = Time.time - activeTween.StartTime;
                float t = elapsedTime / activeTween.Duration;
                activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, t);
                movingNoPellet.Play();
                pacStudentAnimator.Play("PacStudentDownAnim");

            }
            else
            {
                activeTween.Target.position = activeTween.EndPos;
                leftTween = new Tween(gameObject.transform, new Vector3(-11.5f, -5.5f, 0.0f), new Vector3(-16.5f, -5.5f, 0.0f), Time.time, 1.5f);
                activeTween = leftTween;
            }
        }

        if (activeTween == leftTween)
        {
            if (Vector3.Distance(activeTween.Target.position, activeTween.EndPos) > 0.1f)
            {
                float elapsedTime = Time.time - activeTween.StartTime;
                float t = elapsedTime / activeTween.Duration;
                activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, t);
                movingNoPellet.Play();
                pacStudentAnimator.Play("PacStudentLeftAnim");

            }
            else
            {
                activeTween.Target.position = activeTween.EndPos;
                upTween = new Tween(gameObject.transform, new Vector3(-16.5f, -5.5f, 0.0f), new Vector3(-16.5f, -1.5f, 0.0f), Time.time, 1.2f);
                activeTween = upTween;
            }
        }

        if (activeTween == upTween)
        {
            if (Vector3.Distance(activeTween.Target.position, activeTween.EndPos) > 0.1f)
            {
                float elapsedTime = Time.time - activeTween.StartTime;
                float t = elapsedTime / activeTween.Duration;
                activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, t);
                movingNoPellet.Play();
                pacStudentAnimator.Play("PacStudentUpAnim");

            }
            else
            {
                activeTween.Target.position = activeTween.EndPos;
                rightTween = new Tween(gameObject.transform, new Vector3(-16.5f, -1.5f, 0.0f), new Vector3(-11.5f, -1.5f, 0.0f), Time.time, 1.5f);
                activeTween = rightTween;
            }
        }
    }
}
