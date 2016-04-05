using UnityEngine;
using System.Collections;

public class StatePatternEnemy : MonoBehaviour {

	public float searchingSpeed = 120f;
	public float searchingDuration = 4f;
	public float sightRange = 20f;
	public Transform[] wayPoints;
	public Transform eyeHeight;
	public Vector3 offset = new Vector3 (0, 0.5f, 0);
	public MeshRenderer meshRendererFlag;
    public bool isFrozen = false;

	[HideInInspector] public Transform chaseTarget;
	[HideInInspector] public IEnemyState currentState;
	[HideInInspector] public ChaseState chaseState;
	[HideInInspector] public AlertState alertState;
	[HideInInspector] public PatrolState patrolState;
	[HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public FrozenState frozenState;
	//[HideInInspector] public CaptureableState captureableState;

	private void Awake(){

		chaseState = new ChaseState (this);
		alertState = new AlertState (this);
		patrolState = new PatrolState (this);
        frozenState = new FrozenState (this);
     //   captureableState = new CaptureableState(this);

        navMeshAgent = GetComponent<NavMeshAgent> ();

	
	}

	// Use this for initialization
	void Start ()
    {

		currentState = patrolState;

	}
	
	// Update is called once per frame
	void Update () {

		currentState.UpdateState ();
	}

	public void OnTriggerEnter (Collider other)
	{
		currentState.OnTriggerEnter (other);
	}
}
