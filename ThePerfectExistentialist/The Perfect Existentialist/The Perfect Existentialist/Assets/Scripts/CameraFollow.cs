using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject toFollow;
	public float maxXPos;
	public float minXPos;

	private float followPos;
	void Update () {
		followPos = toFollow.transform.position.x;
		followPos = Mathf.Clamp (followPos, minXPos, maxXPos);
		transform.position = new Vector3 (followPos, transform.position.y,transform.position.z);
	}

	public void OnDrawGizmosSelected(){
		Vector3 minXVec = new Vector3 (minXPos, transform.position.y, transform.position.z);
		Vector3 maxXvec = new Vector3 (maxXPos, transform.position.y, transform.position.z);
		Gizmos.DrawLine (minXVec, maxXvec);
	}
}
