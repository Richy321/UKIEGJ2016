using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BlendableItem : MonoBehaviour
{
    public float duration = 2.0f;
    public Renderer rend;
    private float curLerp = 0.0f;
    private bool doLerp = false;
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

    public void StartLerp()
    {
        if (!doLerp && curLerp < 1.0f)
        {
            doLerp = true;
        }
    }
	
	// Update is called once per frame
	void Update ()
	{
        if (doLerp)
        {
            if (curLerp < 1)
            {
                curLerp += Time.deltaTime / duration;
                curLerp = Mathf.Clamp01(curLerp);
            }
            else { doLerp = false; }
            rend.material.SetFloat("_Blend", curLerp);
        }
    }
}
