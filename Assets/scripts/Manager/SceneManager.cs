using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public GameObject coinObject;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		Application.LoadLevel("TestScene");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
