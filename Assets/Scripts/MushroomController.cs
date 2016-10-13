using UnityEngine;
using System.Collections;

public class MushroomController : MonoBehaviour {
    MarioController _mario;
    bool eaten = false;

	// Use this for initialization
	void Start () {
        _mario = GameObject.FindGameObjectWithTag("Player").GetComponent<MarioController>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (eaten)
            return;

        if (coll.collider.gameObject.tag == "Player")
        {
            _mario.Grow();
            GetComponent<Renderer>().enabled = false;
            Destroy(gameObject);
        }            
    }

    
}
