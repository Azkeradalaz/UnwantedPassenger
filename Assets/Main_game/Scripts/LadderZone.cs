using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderZone : MonoBehaviour {

    private PlayerControl thePlayer;
	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerControl>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "HERO")
        {
            thePlayer.onLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "HERO")
        {
            thePlayer.onLadder = false;
        }
    }

}
