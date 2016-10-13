using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    GameObject player;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (player==null)
            player = GameObject.FindGameObjectWithTag("Player");

        if (player.transform.position.x > transform.position.x)
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
	}
}
