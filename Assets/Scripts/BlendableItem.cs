using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class BlendableItem : MonoBehaviour
{
    public float duration = 2.0f;
    private float endLerp = 1.0f;
    private float curLerp = 0.0f;
    private bool doLerp = false;
    private float lerpDistance = 0.0f;
    private Shader shader;
    public List<Renderer> rends;

    // Use this for initialization
    void Start ()
    {
        shader = Shader.Find("Custom/BlendShader");
        Renderer rend = GetComponent<Renderer>();
        if (rend)
            rends.Add(rend);

        Renderer[] rendArr = GetComponentsInChildren<Renderer>();

        rends.AddRange(rendArr);
    }

    public void LerpTo(float toLerpTo)
    {
        endLerp = toLerpTo;
        lerpDistance = endLerp - curLerp;
        doLerp = true;
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (doLerp)
        {
            if(curLerp < endLerp)
            {
                curLerp += (Time.deltaTime / duration) * lerpDistance;
                curLerp = Mathf.Clamp01(curLerp);
            }
            else {
                if (curLerp > endLerp) curLerp = endLerp;
                doLerp = false;
            }

            foreach (Renderer item in rends)
            {
                item.material.SetFloat("_Saturation", curLerp);
            }
        }
    }
}
