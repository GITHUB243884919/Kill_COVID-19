using System.Collections;
using System.Collections.Generic;
using UFrame;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
    public float speed = 1f;
    bool isActive = false;
    float min_x = -9.860625f;
    float max_x = 9.860625f;
    float min_z = 7.011993f;
    float max_z = 17.52999f;

    bool isArrived = false;
    Vector3 target = Vector3.zero;
    Vector3 dir;

    Transform cacheTrans;
    private void Awake()
    {
        cacheTrans = transform;
    }

    public void Active()
    {
        isActive = true;
        
        NextTarget();
    }

    void NextTarget()
    {
        target.x = Random.Range(min_x, max_x);
        target.z = Random.Range(min_z, max_z);

        dir = (target - cacheTrans.position).normalized;
#if UNITY_EDITOR
        ////float dot = Vector3.Dot(Vector3.forward, target.normalized);
        ////var localPos = cacheTrans.InverseTransformPoint(target);
        ////Debug.LogFormat("target = {0}, dot = {1}, localPos = {2}",
        ////    target, dot, localPos);
        ////GameObject.CreatePrimitive(PrimitiveType.Sphere).transform.position = target;
#endif

    }

    void Update()
    {
        //return;
        if (!isActive)
        {
            return;
        }
        
        //if (Math_F.Approximate3D(transform.position, target) || IsPassed(target))
        if (IsPassed(target))
        {
            //Debug.Log("NextTarget");
            NextTarget();
        }
        
        //cacheTrans.position += dir * Time.deltaTime * speed;

        var delta = dir * Time.deltaTime * speed;
        var distance = target - cacheTrans.position;
        if (delta.sqrMagnitude > distance.sqrMagnitude)
        {
            cacheTrans.position += distance;
        }
        else
        {
            cacheTrans.position += delta;
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.LogFormat("OnCollisionEnter {0}", collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Buillet"))
        {
            //Debug.LogFormat("OnCollisionEnter {0}", collision.gameObject.tag);
            isActive = false;
            GameObject.Destroy(gameObject);
            GameObject.Destroy(collision.gameObject);
        }
    }

    protected bool IsPassed(Vector3 pos)
    {
        //var localPos = cacheTrans.InverseTransformPoint(pos);
        ////return localPos.y <= 0;
        //return localPos.z <= 0;

        return Math_F.Approximate3D(cacheTrans.position, pos);
    }
}
