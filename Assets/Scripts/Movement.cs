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
        movingNoPellet.loop = true;
        movingNoPellet.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //movingNoPellet.Play();
        if (Vector3.Distance(activeTween.Target.position, activeTween.EndPos) > 0.1f)
        {
            float elapsedTime = Time.time - activeTween.StartTime;
            float t = elapsedTime / activeTween.Duration;
            activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, t);
            SetAnimation();
        }
        else if (activeTween == rightTween)
        {
            downTween = new Tween(gameObject.transform, new Vector3(-11.5f, -1.5f, 0.0f), new Vector3(-11.5f, -5.5f, 0.0f), Time.time, 1.2f);
            activeTween = downTween;
        }
        else if (activeTween == downTween)
        {
            leftTween = new Tween(gameObject.transform, new Vector3(-11.5f, -5.5f, 0.0f), new Vector3(-16.5f, -5.5f, 0.0f), Time.time, 1.5f);
            activeTween = leftTween;
        }
        else if (activeTween == leftTween)
        {
            upTween = new Tween(gameObject.transform, new Vector3(-16.5f, -5.5f, 0.0f), new Vector3(-16.5f, -1.5f, 0.0f), Time.time, 1.2f);
            activeTween = upTween;
        }
        else if (activeTween == upTween)
        {
            rightTween = new Tween(gameObject.transform, new Vector3(-16.5f, -1.5f, 0.0f), new Vector3(-11.5f, -1.5f, 0.0f), Time.time, 1.5f);
            activeTween = rightTween;
        }
    }

    private void SetAnimation()
    {
        if (activeTween == rightTween)
        {
            pacStudentAnimator.Play("PacStudentRightAnim");
        }

        if (activeTween == leftTween)
        {
            pacStudentAnimator.Play("PacStudentLeftAnim");
        }

        if (activeTween == upTween)
        {
            pacStudentAnimator.Play("PacStudentUpAnim");
        }

        if (activeTween == downTween)
        {
            pacStudentAnimator.Play("PacStudentDownAnim");
        }
    }
}
