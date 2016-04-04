using UnityEngine;
using System.Collections;

public class LerpTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetButton("FireP1"))
        {
            GetComponentInParent<BlendableItem>().StartLerp();
        }
	}
}
