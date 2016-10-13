using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    [Range(0.0f, 10.0f)]
    public float destroyTime = 1.0f;

    public AudioClip dieFx;

    public GameObject headCheck;
    MarioController _mario;
    Animator _animator;    
    EnemyMovement _enemyMovement;
    AudioSource _audio;

    bool isDead = false;

	// Use this for initialization
	void Start () {
        _mario = GameObject.FindGameObjectWithTag("Player").GetComponent<MarioController>();
        _animator = GetComponent<Animator>();
        _enemyMovement = GetComponent<EnemyMovement>();
        _audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (isDead)
            return;

        if (coll.collider.gameObject.tag == "Player")
            _mario.Kill();
    }

    public void Kill()
    {
        if (isDead)
            return;

        isDead = true;
        
        _animator.SetTrigger("Die");

        if (_enemyMovement != null)
            _enemyMovement.moveSpeed = 0;

        headCheck.GetComponent<Collider2D>().enabled = false;

        _audio.PlayOneShot(dieFx);

        Invoke("Destroy", destroyTime);
    }

    void Destroy ()
    {
        transform.DetachChildren();
        Destroy(gameObject);
    }
}
