using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Conversation")]
public class Conversation{

	[XmlArray("Nodes")]
	[XmlArrayItem("Node")]
	public List<Node> nodes = new List<Node>();

	public class Option{
		[XmlAttribute("jumpTo")]
		public string id;

		[XmlAttribute("conditional")]
		public string conditional;
		
		[XmlAttribute("conditionalGreaterThan")]
		public int conditionalGreaterThan;

		[XmlAttribute("dataEdit")]
		public string dataEdit;
		
		[XmlAttribute("dataSet")]
		public int dataSet;
		
		[XmlAttribute("dataAddTo")]
		public int dataAddTo;

		[XmlAttribute("forceSelect")]
		public bool forceSelect;

		[XmlText]
		public string text;
	}

	public class Node{
		[XmlAttribute("ID")]
		public string id;

		[XmlText]
		public string text;

		[XmlAttribute("Start")]
		public bool Start;

		[XmlAttribute("End")]
		public bool End;

		[XmlAttribute("dataEdit")]
		public string dataEdit;
		
		[XmlAttribute("dataSet")]
		public int dataSet;
		
		[XmlAttribute("dataAddTo")]
		public int dataAddTo;

		[XmlAttribute("freezePlayer")]
		public bool freezePlayer;

		[XmlAttribute("script")]
		public string script;

		[XmlAttribute("hidden")]
		public bool hidden;

		[XmlArray("Options")]
		[XmlArrayItem("Option")]
		public List<Option> options = new List<Option>();
	}

	public static Conversation Load(TextAsset xml){

		XmlSerializer serializer = new XmlSerializer (typeof(Conversation));

		StringReader reader = new StringReader (xml.text);

		Conversation conversation = serializer.Deserialize (reader) as Conversation;

		reader.Close();

		return conversation;
	}

	public Node GetStartNode(){
		foreach(Node n in nodes){
			if(n.Start){
				return n;
			}
		}
		Debug.Log ("No starting node found!!");
		return null;
	}

	public Node GetEndNode(){
		foreach (Node n in nodes) {
			if(n.End){
				return n;
			}
		}
		Debug.Log ("No ending node found!!");
		return null;
	}

	public Node GetNodeById(string Id){
		foreach (Node n in nodes) {
			if(n.id.Equals(Id)){
				return n;
			}
		}
		Debug.Log ("No node with ID " + Id + " found!");
		return null;
	}
}
