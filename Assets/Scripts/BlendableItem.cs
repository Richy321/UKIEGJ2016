using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BlendableItem : MonoBehaviour
{
    public float duration = 2.0f;
    public Renderer rend;
    private float endLerp = 1.0f;
    private float curLerp = 0.0f;
    private bool doLerp = false;
    private float lerpDistance = 0.0f;
    private Shader shader;

    // Use this for initialization
    void Start ()
    {
        shader = Shader.Find("Custom/BlendShader");
        rend = GetComponent<Renderer>();
        if(!rend)
        {
            rend = GetComponentInChildren<Renderer>();
        }
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
            rend.material.SetFloat("_Saturation", curLerp);
        }
    }
}
