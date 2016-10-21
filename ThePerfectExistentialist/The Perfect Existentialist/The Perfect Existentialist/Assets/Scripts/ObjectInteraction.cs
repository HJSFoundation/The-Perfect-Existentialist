using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using System;

public class ObjectInteraction : MonoBehaviour {

	public TextAsset Xml;
	public Camera mainCamera;

	public Transform canvasPanel;
	public GameObject buttonPrefab;
	public GameObject TextBox;
	public ScriptController sc;

	public static Dictionary<string,int> dataValues = new Dictionary<string,int>();
	public static bool freezePlayer = false;

	Conversation c;
	List<GameObject> uiObjects;
	bool started;
	bool currently;
	bool ended;

	void Start () {
		started = false;
		currently = false;
		ended = false;
		freezePlayer = false;
		c = Conversation.Load (Xml);
		uiObjects = new List<GameObject>();
		if(dataValues.Keys.Count != 0){
			dataValues.Clear();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		currently = true;
		if (other.gameObject.tag == "Player" && !started) {
			started = true;
			if(c.GetStartNode().freezePlayer){
				freezePlayer = true;
			}
			goToNode(c.GetStartNode().id);
		}
		else if(!ended){
			ContinueConversation();
		}
	}

	public void goToNode(string id){
		Conversation.Node node = c.GetNodeById (id);

		while (uiObjects.Count > 0) {
			GameObject o = uiObjects[0];
			uiObjects.Remove(o);
			Destroy(o);
		}

		if(node.script != null){
			sc.runScript(node.script);
		}

		if(node.End){
			ended = true;
			freezePlayer = false;
			return;
		}

		if(node.dataEdit != null){
			if(!dataValues.ContainsKey(node.dataEdit)){
				dataValues.Add(node.dataEdit,0);
			}

			if(node.dataSet != null){
				dataValues[node.dataEdit] = node.dataSet;
			}

			if(node.dataAddTo != null){
				dataValues[node.dataEdit] += node.dataAddTo;
			}
		}

		if(node.dataEdit != null){
			Debug.Log (node.dataEdit + " " + dataValues[node.dataEdit]);
		}

		if(node.hidden){
			return;
		}
		GameObject text = (GameObject)Instantiate (TextBox);
		uiObjects.Add (text);
		
		text.GetComponentInChildren<Text> ().text = node.text;
		text.GetComponent<RectTransform> ().sizeDelta = new Vector2 (400, 0);
		text.transform.SetParent (canvasPanel);


		foreach (Conversation.Option o in node.options) {
			if(o.conditional != null){
				if(!dataValues.ContainsKey(o.conditional)){
					dataValues.Add(o.conditional,0);

				}
			}

			if(o.conditional == null || dataValues[o.conditional] >= o.conditionalGreaterThan){
				if(o.forceSelect){
					goToNode(o.id);
					return;
				}

				GameObject button = (GameObject)Instantiate (buttonPrefab);
				uiObjects.Add (button);
			
				button.GetComponentInChildren<Text> ().text = o.text;
				button.GetComponent<RectTransform> ().sizeDelta = new Vector2 (400, 0);
				button.transform.SetParent (canvasPanel);

				string ID = o.id;

				button.GetComponent<Button> ().onClick.AddListener (() => {goToNode (ID);});

				if(o.dataEdit != null){
					if(!dataValues.ContainsKey(o.dataEdit)){
						dataValues.Add(o.dataEdit,0);
					}

					string dataEdit = o.dataEdit;
					
					if(node.dataAddTo != null){
						int dataAddTo = o.dataAddTo;
						button.GetComponent<Button> ().onClick.AddListener (() => {addToData(dataEdit,dataAddTo);});
					}
				}

			}
		}
	}

	void addToData(string id, int value){
		dataValues [id] += value;
	}

	void setData(string id, int value){
		dataValues [id] = value;
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			currently = false;
			foreach (GameObject go in uiObjects) {
				go.SetActive(false);
			}
		}
	}

	void ContinueConversation(){
		foreach (GameObject go in uiObjects) {
			go.SetActive(true);
		}
	}

	void Update(){
		if (currently) {
			float heightSum = -70;
			for(int i = uiObjects.Count-1; i >= 0; i--){
				Vector2 position = new Vector2();
				position.x = mainCamera.WorldToScreenPoint (transform.position).x-(mainCamera.pixelWidth/2);
				position.y = heightSum;
				heightSum += uiObjects[i].GetComponent<RectTransform>().rect.height + 10;
				uiObjects[i].GetComponent<RectTransform>().anchoredPosition = position;
			}

			if(currently && Input.GetKeyDown(KeyCode.P)){
				ended = false;
				while (uiObjects.Count > 0) {
					GameObject o = uiObjects[0];
					uiObjects.Remove(o);
					Destroy(o);
				}
				goToNode(c.GetStartNode().id);
			}
		}
	}
}
