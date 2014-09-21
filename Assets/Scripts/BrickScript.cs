using UnityEngine;
using System.Collections;

public class BrickScript : MonoBehaviour {

	static int numBricks = 0;
	public int pointValue = 1;
	public int hitPoints = 1;

	// Use this for initialization
	void Start () {
		numBricks++;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Update is called once per frame
	void OnCollisionEnter (Collision col) {
		hitPoints--;
		if (hitPoints <= 0) {
			Die();
		}
	}

	void Die() {
		Destroy (gameObject);
		PaddleScript paddleScript = GameObject.Find ("Paddle").GetComponent<PaddleScript> ();
		paddleScript.AddPoint(pointValue);

		//GameObject[] bricks = GameObject.FindGameObjectsWithTag ("Brick");
		numBricks--;
		//Debug.Log (numBricks);
		if (numBricks <= 0) {
			paddleScript.nextLevel();
		}
	}


}
