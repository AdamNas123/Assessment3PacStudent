using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Tween tween;
    // Start is called before the first frame update
    void Start()
    {
        tween = new Tween(gameObject.transform, gameObject.transform.position, new Vector3(-11.5f, -1.5f, 0.0f), Time.time, 1.5f);
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
        if (tween != null && Vector3.Distance(tween.Target.position, tween.EndPos) > 0.1f)
        {
            float elapsedTime = Time.time - tween.StartTime;
            float t = elapsedTime / tween.Duration;
            tween.Target.position = Vector3.Lerp(tween.StartPos, tween.EndPos, t);

        }
        else if (tween != null)
        {
            tween.Target.position = tween.EndPos;
            tween = null;
            //activeTweens.Remove(activeTweens[i]);
        }
    }

    public bool AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        if (!TweenExists(targetObject))
        {
            tween = new Tween(targetObject, startPos, endPos, Time.time, duration);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TweenExists(Transform target)
    {
            if (tween.Target == target)
            {
                return true;
            }
        return false;
    }
}
