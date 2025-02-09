﻿using UnityEngine;

public class ScreenBounds : MonoBehaviour
{
    static private ScreenBounds S;
    
    public float zScale = 10;

    Camera cam;
    BoxCollider boxColl;
    float cachedOrthographicSize, cachedAspect;
    Vector3 cachedCamScale;
    
    void Awake()
    {
        S = this;

        cam = Camera.main;
        
        boxColl = GetComponent<BoxCollider>();
        boxColl.size = Vector3.one;

        transform.position = Vector3.zero;
        ScaleSelf();
    }


    private void Update()
    {
        ScaleSelf();
    }

    
    private void ScaleSelf()
    {
        if (cam.orthographicSize != cachedOrthographicSize || cam.aspect != cachedAspect
            || cam.transform.localScale != cachedCamScale)
        {
            transform.localScale = CalculateScale();
        }
    }


    private Vector3 CalculateScale()
    {
        cachedOrthographicSize = cam.orthographicSize;
        cachedAspect = cam.aspect;
        cachedCamScale = cam.transform.localScale;

        Vector3 scaleDesired, scaleColl;

        scaleDesired.z = zScale;
        scaleDesired.y = cam.orthographicSize * 2;
        scaleDesired.x = scaleDesired.y * cam.aspect;
        
        scaleColl = scaleDesired.ComponentDivide(cachedCamScale);
        return scaleColl;
    }

    static public Vector3 RANDOM_ON_SCREEN_LOC
    {
        get
        {
            Vector3 min = S.boxColl.bounds.min;
            Vector3 max = S.boxColl.bounds.max;
            Vector3 loc = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0);
            return loc;
        }
    }

    static public Vector3 RANDOM_ON_EDGE_SCREEN_LOC
    {
        get
        {
            Vector3 min = S.boxColl.bounds.min;
            Vector3 max = S.boxColl.bounds.max;

            System.Random rnd = new System.Random();

            int randomInt = rnd.Next(4);
            if (randomInt == 1) 
            {
                return new Vector3(min.x, Random.Range(min.y, max.y), 0);
            } 
            else  if (randomInt == 2)
            {
                return new Vector3(max.x, Random.Range(min.y, max.y), 0);
            }
            else  if (randomInt == 3)
            {
                return new Vector3(Random.Range(min.x, max.x), min.y, 0);
            }
            else
            {
                return new Vector3(Random.Range(min.x, max.x), max.y, 0);
            }

            
        }
    }

    static public Bounds BOUNDS
    {
        get
        {
            if (S == null)
            {
                Debug.LogError("ScreenBounds.BOUNDS - ScreenBounds.S is null!");
                return new Bounds();
            }
            if (S.boxColl == null)
            {
                Debug.LogError("ScreenBounds.BOUNDS - ScreenBounds.S.boxColl is null!");
                return new Bounds();
            }
            return S.boxColl.bounds;
        }
    }

    static public bool OOB(Vector3 worldPos)
    {
        Vector3 locPos = S.transform.InverseTransformPoint(worldPos);
        // Find in which dimension the locPos is furthest from the origin
        float maxDist = Mathf.Max( Mathf.Abs(locPos.x), Mathf.Abs(locPos.y), Mathf.Abs(locPos.z) );
        // If that furthest distance is >0.5f, then worldPos is out of bounds
        return (maxDist > 0.5f);
    }

    static public int OOB_X(Vector3 worldPos)
    {
        Vector3 locPos = S.transform.InverseTransformPoint(worldPos);
        return OOB_(locPos.x);
    }
    static public int OOB_Y(Vector3 worldPos)
    {
        Vector3 locPos = S.transform.InverseTransformPoint(worldPos);
        return OOB_(locPos.y);
    }
    static public int OOB_Z(Vector3 worldPos)
    {
        Vector3 locPos = S.transform.InverseTransformPoint(worldPos);
        return OOB_(locPos.z);
    }

    static private int OOB_(float num)
    {
        if (num > 0.5f) return 1;
        if (num < -0.5f) return -1;
        return 0;
    }
}