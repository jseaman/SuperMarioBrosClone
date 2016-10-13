using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [HideInInspector]
    public static GameManager gameManager;

    public int score = 0;

	// Use this for initialization
	void Start () {
        gameManager = this;
	}
	
	// Update is called once per frame
	void Update () {
	  
	}

    public void Reset ()
    {
        SceneManager.LoadScene("Level1");
    }
}
