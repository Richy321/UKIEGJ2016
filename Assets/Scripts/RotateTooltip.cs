using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RotateTooltip : MonoBehaviour {

    public GameObject player; 

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<Assets.Scripts.Character>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(player.transform);
	}
}
