using System;
using UnityEngine;
using System.Collections;

public class BlendableItem : MonoBehaviour
{
    public Material darkMaterial;
    public Material lightMaterial;
    public float duration = 2.0f;
    public Renderer rend;
    private float curLerp = 0.0f;
    private bool doLerp = false;

    // Use this for initialization
    void Start ()
    {
        rend = GetComponent<Renderer>();
        rend.material = darkMaterial;
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

            rend.material.Lerp(darkMaterial, lightMaterial, curLerp);
        }
    }
}
