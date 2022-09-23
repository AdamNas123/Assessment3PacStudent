using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Tween tween;
    // Start is called before the first frame update
    void Start()
    {  
    }

    // Update is called once per frame
    void Update()
    {
        float elapsedTime = Time.time - tween.StartTime;
        float t = elapsedTime / tween.Duration;

        if (gameObject.transform.position.Equals(new Vector3(-16.5f, -1.5f, 0.0f)))
        {
            
            tween.Target.position = Vector3.Lerp(tween.StartPos, tween.EndPos, t);
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
