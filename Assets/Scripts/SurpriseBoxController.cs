using UnityEngine;
using System.Collections;

public class SurpriseBoxController : MonoBehaviour {
     
    public enum SurpriseType { Coin, Mushroom };
    public SurpriseType Type;
    public bool used = false;
    public GameObject Mushroom;
    Animator _animator;


	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Activate ()
    {
        if (used)
            return;

        Debug.Log("Box Activated");
        GameObject newMushroom = (GameObject) Instantiate(Mushroom, transform.position, transform.localRotation);
        used = true;
        _animator.SetBool("BoxOff", true);
    }
}
