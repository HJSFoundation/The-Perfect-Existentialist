using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScriptController : MonoBehaviour {

	public Dictionary<string,Action> dictionary;
	public GameObject fader;
	public DestroyPlayer dp;
	public ObjectInteraction DeathEnding;

	public void Start(){
		dictionary = new Dictionary<string,Action> ();
		dictionary.Add ("endGame", endGame);
		dictionary.Add ("death", death);
		dictionary.Add ("comparator", comparator);
		dictionary.Add ("comparator2", comparator2);
		dictionary.Add ("escape", escape);
		dictionary.Add ("check", check);
	}

	public void runScript(string script){
		if(dictionary.ContainsKey(script)){
			dictionary[script]();
		}
	}

	public void death(){
		dp.killPlayer ();
	}

	public void endGame(){
		StartCoroutine (act());
	}

	IEnumerator act(){
		Image i = fader.GetComponent<Image> ();

		yield return new WaitForSeconds (1);
		i.color = new Color (1, 1, 1, 0.25f);
		yield return new WaitForSeconds (1);
		i.color = new Color (1, 1, 1, 0.5f);
		yield return new WaitForSeconds (1);
		i.color = new Color (1, 1, 1, 0.75f);
		yield return new WaitForSeconds (1);
		i.color = new Color (1, 1, 1, 1f);
		yield return new WaitForSeconds (1);
		Application.Quit ();
	}
	
	public void escape(){
		StartCoroutine (act2 ());
	}

	IEnumerator act2(){
		Image i = fader.GetComponent<Image> ();
		
		yield return new WaitForSeconds (1);
		i.color = new Color (1, 1, 1, 0.25f);
		yield return new WaitForSeconds (1);
		i.color = new Color (1, 1, 1, 0.5f);
		yield return new WaitForSeconds (1);
		i.color = new Color (1, 1, 1, 0.75f);
		yield return new WaitForSeconds (1);
		i.color = new Color (1, 1, 1, 1f);
		yield return new WaitForSeconds (1);
		Application.LoadLevel (1);
	}

	public void comparator(){
		string value;
		int biggest;
		value = "P";
		biggest = ObjectInteraction.dataValues ["P"];
		if(biggest < ObjectInteraction.dataValues ["S"]){
			value = "S";
			biggest = ObjectInteraction.dataValues ["S"];
		}
		if(biggest < ObjectInteraction.dataValues ["G"]){
			value = "G";
			biggest = ObjectInteraction.dataValues ["G"];
		}
		if(biggest < ObjectInteraction.dataValues ["M"]){
			value = "M";
			biggest = ObjectInteraction.dataValues ["M"];
		}

		if(value == "P"){
			DeathEnding.goToNode("2perfect");
			return;
		}
		if(value == "S"){
			DeathEnding.goToNode("2Stephen");
			return;
		}
		if(value == "G"){
			DeathEnding.goToNode("2Gregor");
			return;
		}
		if(value == "M"){
			DeathEnding.goToNode("2Meursault");
			return;
		}

	}

	public void comparator2(){
		string value;
		int biggest;
		value = "S";
		biggest = ObjectInteraction.dataValues ["S"];
		if(biggest < ObjectInteraction.dataValues ["G"]){
			value = "G";
			biggest = ObjectInteraction.dataValues ["G"];
		}
		if(biggest < ObjectInteraction.dataValues ["M"]){
			value = "M";
			biggest = ObjectInteraction.dataValues ["M"];
		}

		if(value == "S"){
			DeathEnding.goToNode("2pStephen");
			return;
		}
		if(value == "G"){
			DeathEnding.goToNode("2pGregor");
			return;
		}
		if(value == "M"){
			DeathEnding.goToNode("2pMeursault");
			return;
		}
	}

	public void check(){
		Debug.Log("P:" + ObjectInteraction.dataValues ["P"] + " S:" + ObjectInteraction.dataValues ["S"] + " G:" + ObjectInteraction.dataValues ["G"] + " M:" + ObjectInteraction.dataValues ["M"]); 
	}
}
