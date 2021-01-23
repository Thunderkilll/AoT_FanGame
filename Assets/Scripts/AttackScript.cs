using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {

	public AudioSource musicPlayer;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
			 
			musicPlayer.Play();
		}
	}
}
