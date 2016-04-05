using UnityEngine;
using System.Collections;

public class Laz : MonoBehaviour {

	public Transform startPoint;
	public Transform endPoint;
	public LineRenderer laserLine;
	public ParticleSystem particalSys;
    public BoxCollider lazerCollider;
	public float growDuration = 0.5f;
	public float growTime = 0.0f;

	void Start()
    {
		laserLine.SetWidth (0.2f, 0.2f);
		laserLine.enabled = false;
	}

	void Update()
	{
		Vector3 endP =  Vector3.Lerp (startPoint.position, endPoint.position, Mathf.Clamp01(growTime / growDuration));

        Vector3 endPLocal = Vector3.Lerp(startPoint.localPosition, endPoint.localPosition, Mathf.Clamp01(growTime / growDuration));

        if (laserLine.enabled) 
		{
			growTime += Time.deltaTime;
			laserLine.SetPosition (0, startPoint.position);
			laserLine.SetPosition (1, endP);

		    lazerCollider.center = endPLocal - startPoint.localPosition;
		    float length = (endPLocal - startPoint.localPosition).magnitude;
            lazerCollider.size = new Vector3(lazerCollider.size.x, lazerCollider.size.y, length*2);
		}
	}

	public void startLaz()
	{
        laserLine.enabled = true;
		growTime = 0.0f;
		particalSys.Play ();
	}


	public void stopLaz()
	{
        lazerCollider.center = Vector3.zero;
	    lazerCollider.size = new Vector3(lazerCollider.size.x, lazerCollider.size.y, 0.1f);

        laserLine.enabled = false;
		growTime = 0.0f;
		particalSys.Stop (); 
	}
}
