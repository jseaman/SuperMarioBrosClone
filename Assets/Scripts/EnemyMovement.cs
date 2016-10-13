using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
    public bool goRight;

    [Range(0.0f, 10.0f)]
    public float moveSpeed = 3.0f;

    public LayerMask whatIsWall;

    private Rigidbody2D _rigidBody;

	// Use this for initialization
	void Start () {
        _rigidBody = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate ()
    {
        if (goRight)
            _rigidBody.velocity = new Vector2(moveSpeed, _rigidBody.velocity.y);
        else
            _rigidBody.velocity = new Vector2(-moveSpeed, _rigidBody.velocity.y);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if ((1 << coll.gameObject.layer & whatIsWall.value) != 0)
        {
            if (goRight && coll.gameObject.transform.position.x > transform.position.x)
                goRight = false;
            else if (!goRight && coll.gameObject.transform.position.x < transform.position.x)
                goRight = true;
        }
    }
}
