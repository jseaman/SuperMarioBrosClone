using UnityEngine;
using System.Collections;

public class MarioKillScript : MonoBehaviour {
    MarioController _mario;
    Rigidbody2D _rigidBody;

	// Use this for initialization
	void Start () {
        _mario = transform.parent.GetComponent<MarioController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.gameObject.tag=="EnemyHead")
        {
            EnemyController enemy = coll.gameObject.GetComponent<EnemyController>();
            enemy.Kill();
            _mario.Bounce();
        }
    }
}
