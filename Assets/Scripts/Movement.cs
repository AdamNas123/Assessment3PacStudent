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
    // Start is called before the first frame update
    void Start()
    {
        pacStudentAnimator = GetComponent<Animator>();
        rightTween = new Tween(gameObject.transform, new Vector3(-16.5f, -1.5f, 0.0f), new Vector3(-11.5f, -1.5f, 0.0f), Time.time, 1.5f);
        //downTween = new Tween(gameObject.transform, new Vector3(-11.5f, -1.5f, 0.0f), new Vector3(-11.5f, -5.5f, 0.0f), Time.time, 1.0f);
        //leftTween = new Tween(gameObject.transform, new Vector3(-11.5f, -5.5f, 0.0f), new Vector3(-16.5f, -5.5f, 0.0f), Time.time, 1.5f);
        //upTween = new Tween(gameObject.transform, new Vector3(-16.5f, -5.5f, 0.0f), new Vector3(-16.5f, -1.5f, 0.0f), Time.time, 1.0f);
        activeTween = rightTween;
        //AddTween(gameObject.transform, gameObject.transform.position, new Vector3(-11.5f, -1.5f, 0.0f), 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //if (gameObject.transform.position.Equals(new Vector3(-16.5f, -1.5f, 0.0f)))
        //{
        //    AddTween(gameObject.transform, gameObject.transform.position, new Vector3(-11.5f, -1.5f, 0.0f), 1.5f);
        //    float elapsedTime = Time.time - tween.StartTime;
        //    float t = elapsedTime / tween.Duration;
        //    tween.Target.position = Vector3.Lerp(tween.StartPos, tween.EndPos, t);
        //}   
        if (activeTween == rightTween)
        {
            if (Vector3.Distance(activeTween.Target.position, activeTween.EndPos) > 0.1f)
            {
                float elapsedTime = Time.time - activeTween.StartTime;
                float t = elapsedTime / activeTween.Duration;
                activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, t);
                pacStudentAnimator.Play("PacStudentRightAnim");

            }
            else //if (gameObject.transform.position == activeTween.EndPos)
            {
                activeTween.Target.position = activeTween.EndPos;
                //activeTween.Target.position = downTween.StartPos;
                downTween = new Tween(gameObject.transform, new Vector3(-11.5f, -1.5f, 0.0f), new Vector3(-11.5f, -5.5f, 0.0f), Time.time, 1.0f);
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
                pacStudentAnimator.Play("PacStudentDownAnim");

            }
            else //if(gameObject.transform.position == activeTween.EndPos)
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
                pacStudentAnimator.Play("PacStudentLeftAnim");

            }
            else //if (activeTween != null)
            {
                activeTween.Target.position = activeTween.EndPos;
                upTween = new Tween(gameObject.transform, new Vector3(-16.5f, -5.5f, 0.0f), new Vector3(-16.5f, -1.5f, 0.0f), Time.time, 1.0f);
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
                pacStudentAnimator.Play("PacStudentUpAnim");

            }
            else //if (activeTween != null)
            {
                activeTween.Target.position = activeTween.EndPos;
                rightTween = new Tween(gameObject.transform, new Vector3(-16.5f, -1.5f, 0.0f), new Vector3(-11.5f, -1.5f, 0.0f), Time.time, 1.5f);
                activeTween = rightTween;
            }
        }
    }

    public bool AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        if (!TweenExists(targetObject))
        {
            activeTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TweenExists(Transform target)
    {
            if (activeTween.Target == target)
            {
                return true;
            }
        return false;
    }
}
