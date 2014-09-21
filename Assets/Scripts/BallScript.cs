using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//rigidbody.AddForce (300f, 300f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// Update is called once per frame
	public void Die () {
		Destroy( gameObject );
		GameObject paddleObject = GameObject.Find ("Paddle");
		PaddleScript paddleScript = paddleObject.GetComponent<PaddleScript>();
		paddleScript.LoseLife();
	}
}
