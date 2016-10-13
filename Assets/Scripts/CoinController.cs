using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {
    public AudioClip coinFx;

    AudioSource _audio;
    bool taken = false;

	// Use this for initialization
	void Start () {
        _audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame 
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (taken)
            return;

        if (other.gameObject.tag=="Player")
        {
            taken = true;
            _audio.PlayOneShot(coinFx);          
            GetComponent<Renderer>().enabled = false;
            Invoke("SelfDestroy", 2);

            GameManager.gameManager.score++;
        }
    }

    void SelfDestroy ()
    {
        Destroy(gameObject);
    }
}
