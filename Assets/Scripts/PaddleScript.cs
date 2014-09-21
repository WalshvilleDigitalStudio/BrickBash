using UnityEngine;
using System.Collections;

public class PaddleScript : MonoBehaviour {


	float paddleSpeed = 10f;
	public GameObject ballPrefab;

	int lives = 3;
	int score = 0;
	int levelMap = 1;

	GameObject attachedBall = null;
	GUIText guiLives;
	GUIText guiFinalScore;


	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		DontDestroyOnLoad(GameObject.Find ("guiLives"));
		//SPawn (instantiate) a new ball
		guiLives = GameObject.Find ("guiLives").GetComponent<GUIText>();
		guiLives.text = "Lives: " + lives;
		SpawnBall ();
	}


	public void OnLevelWasLoaded( int level) {
		SpawnBall ();
	}


	public void nextLevel() {
		//Load a new level
		if (levelMap <= 2) {
			levelMap++;
		} else {
			levelMap = 3;
		}


		Application.LoadLevel("level" + levelMap);
	}



	public void LoseLife(){
		lives--;
		guiLives.text = "Lives: " + lives;
		if (lives > 0) {
			SpawnBall ();
		}
		else {
			//Destroy (gameObject);
			Application.LoadLevel("GameOver");
			Debug.Log ("Your Score: " + score);
			//guiFinalScore = GameObject.Find ("guiScore").GetComponent<GUIText>();
			//guiFinalScore.text = "Your Score: " + score;
		}
	}

	public void AddPoint(int v) {
		score += v;
	}


	public void SpawnBall() {
		//SPawn (instantiate) a new ball
		if (ballPrefab == null) {
			Debug.Log ("Hey, dummy, you forgot to link the ball prefab in the inspector!");
			return;
		}
		attachedBall = (GameObject)Instantiate (ballPrefab, transform.position + new Vector3(0, .75f, 0), Quaternion.identity );
	}


	void OnGUI()
	{
		GUI.Label( new Rect(0, 10, 100, 100), "Score: " + score);
	}﻿
	
	// Update is called once per frame
	void Update () {
		//left-right motion
		transform.Translate( paddleSpeed * Time.deltaTime * Input.GetAxis( "Horizontal" ), 0, 0 );

		if (transform.position.x > 7.4f) {
			transform.position = new Vector3( 7.4f, transform.position.y, transform.position.z);
		}

		if (transform.position.x < -7.4f) {
			transform.position = new Vector3( -7.4f, transform.position.y, transform.position.z);
		}

		if( attachedBall ) {
			Rigidbody ballRigidbody = attachedBall.rigidbody;

			ballRigidbody.position = transform.position + new Vector3(0, .75f, 0);

			if (Input.GetButtonDown ("LaunchBall") && ballRigidbody.isKinematic == true) {
			// Fire the ball
				ballRigidbody.isKinematic = false;
				ballRigidbody.AddForce (300f * Input.GetAxis( "Horizontal" ), 300f, 0);
				attachedBall = null;
			}
		}

	
	}

	void OnCollisionEnter( Collision col ) {


		foreach (ContactPoint contact in col.contacts) {
			if( contact.thisCollider == collider ) {

			// This is the paddles's contact point
			float english = contact.point.x - transform.position.x;

			contact.otherCollider.rigidbody.AddForce( 300f * english, 0, 0);
			}
		}
	}
}