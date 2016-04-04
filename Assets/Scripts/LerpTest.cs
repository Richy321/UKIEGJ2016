using UnityEngine;
using System.Collections;

public class LerpTest : MonoBehaviour {

    public BlendableItem blender;

	// Use this for initialization
	void Start () {
        blender = GetComponent<BlendableItem>();
        if (!blender) GetComponentInChildren<BlendableItem>();   
	}
	
	// Update is called once per frame
	void Update () {
	}
}
