using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	public float speed;

	public float maxXPos;
	public float minXPos;

	// Update is called once per frame
	void Update () {
		if (!ObjectInteraction.freezePlayer) {
			Movement ();
		}

		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
	}

	void Movement(){
		if (Input.GetKey (KeyCode.D) && transform.position.x <= maxXPos) {
			transform.Translate(Vector2.right * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.A) && transform.position.x >= minXPos) {
			transform.Translate(Vector2.left * speed * Time.deltaTime);
		}
	}

	public void OnDrawGizmosSelected(){
		Vector3 minXVec = new Vector3 (minXPos, transform.position.y, transform.position.z);
		Vector3 maxXvec = new Vector3 (maxXPos, transform.position.y, transform.position.z);
		Gizmos.DrawLine (minXVec, maxXvec);
	}
}
