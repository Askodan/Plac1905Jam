using UnityEngine;
using System.Collections;

public class WaypointSystem : MonoBehaviour {
	public GameObject [] waypoints;
	public int wayNum=-1;
	public UnityEngine.AI.NavMeshAgent nma;
	public float pathEndThreshold = 0.1f;
	public bool hasPath = false, first=true, allowed=true;
	bool AtEndOfPath()
	{
		hasPath |= nma.hasPath;
		if (hasPath &&  nma.remainingDistance <= nma.stoppingDistance + pathEndThreshold )
		{
			// Arrived
			hasPath = false;
			return true;
		}

		return false;
	}
	void Start () {
		nma = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}
	// Update is called once per frame
	void Update ()
	{
		if(waypoints.Length>0)
		{
			if ((AtEndOfPath ()||first)&&(allowed)) {
				nma.SetDestination (waypoints[(int)(Random.Range(0, waypoints.Length))].transform.position);
				/*if (wayNum == waypoints.Length-1)
					wayNum = 0;*/
				first = false;
			}
		}
	}
	/*public GameObject [] waypoints;
	public int wayNum=0;
	public float speed=10, speedRotate=1, rotforwardSpeed=2.5, gravity=10;
	private CharacterController cc;
	// Use this for initialization
	void Start () {
		cc = GetComponent<CharacterController> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Vertical") > 0f) {
			cc.Move (transform.forward * speed * Time.deltaTime + Vector3.down*gravity*Time.deltaTime);
		}
		if()
			transform.Rotate (new Vector3 (0.0f, speedRotate * Time.deltaTime, 0.0f));
	}*/

}
