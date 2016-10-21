using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DestroyPlayer : MonoBehaviour {

	public GameObject player;
	public Sprite burning;
	public Sprite dying;
	public Sprite dead;
	public GameObject fader;

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			this.killPlayer();
		}
	}
	
	public void killPlayer(){
		StartCoroutine (act());
	}

	IEnumerator act(){
		ObjectInteraction.freezePlayer = true;
		SpriteRenderer sr = player.GetComponentInChildren<SpriteRenderer>();
		Image i = fader.GetComponent<Image> ();

		yield return new WaitForSeconds (2);
		sr.sprite = burning;
		i.color = new Color (0, 0, 0, 0.25f);
		yield return new WaitForSeconds (2);
		sr.sprite = dying;
		i.color = new Color (0, 0, 0, 0.5f);
		yield return new WaitForSeconds (2);
		sr.sprite = dead;
		i.color = new Color (0, 0, 0, 0.75f);
		yield return new WaitForSeconds (2);
		i.color = new Color (0, 0, 0, 1f);
		yield return new WaitForSeconds (2);
		Application.LoadLevel (0);
	}
}
