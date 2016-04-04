using UnityEngine;
using System.Collections;

public class Laz : MonoBehaviour {

	public Transform startPoint;
	public Transform endPoint;
	public LineRenderer laserLine;
	public ParticleSystem particalSys;
	public float growDuration = 0.5f;
	public float growTime = 0.0f;

	void Start(){
		laserLine.SetWidth (0.2f, 0.2f);
		laserLine.enabled = false;
	}

	void Update()
	{
		Vector3 endP =  Vector3.Lerp (startPoint.position, endPoint.position, Mathf.Clamp01(growTime / growDuration));

		if (laserLine.enabled) 
		{
			growTime += Time.deltaTime;
			laserLine.SetPosition (0, startPoint.position);
			laserLine.SetPosition (1, endP);
		}
	}

	public void startLaz(){
		laserLine.enabled = true;
		growTime = 0.0f;
		particalSys.Play ();
	}


	public void stopLaz(){
		
		laserLine.enabled = false;
		growTime = 0.0f;
		particalSys.Stop (); 
	}

}
