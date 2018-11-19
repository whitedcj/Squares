using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class EnemyController : MonoBehaviour {

	public bool vertical, // determines which axis enemy follows
				forward; // determines which direction (+/-) enemy moves along axis
	public float speed = 0.1f;
	public Vector3 position;
	
	void Start() {
		position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

		// enemy starts in a random direction
		int direction = Random.Range (0, 3);
		switch (direction) {
		case 1:
			vertical = true;
			forward = true;
			break;
		case 2:
			vertical = true;
			forward = false;
			break;
		case 3:
			vertical = false;
			forward = true;
			break;
		case 4:
			vertical = false;
			forward = false;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		float xPos = gameObject.transform.position.x;
		float yPos = gameObject.transform.position.y;

		if (vertical && forward) {
			yPos += speed;
			if(yPos > 4)
				forward = false;
		} else if (vertical && !forward) {
			yPos -= speed;
			if(yPos < -4)
				forward = true;
		} 
		if (!vertical && forward) {
			xPos += speed;
			if(xPos > 8)
				forward = false;
		} else if (!vertical && !forward) {
			xPos -= speed;
			if(xPos < -8)
				forward = true;
		}
		
		// enforce boundary
		position.y = Mathf.Clamp (yPos, -4, 4);
		position.x = Mathf.Clamp (xPos, -8, 8);
		
		gameObject.transform.position = position;
	}

	void OnTriggerEnter (Collider other){
		if (other.tag.Equals ("Player")) { //kill player, reset game
			//destroy all enemies
			var enemies = GameObject.FindGameObjectsWithTag("Enemy");
			foreach(GameObject enemy in enemies)
				Destroy (enemy);
			
			//update scores
			var scoreTextObj = GameObject.FindGameObjectWithTag("ScoreText");
			string scoreText = scoreTextObj.GetComponent<TextMesh>().text;
			var parts = Regex.Split(scoreText, @"\s+");

			int currentScore = int.Parse(parts[1]);
			int bestScore = int.Parse(parts[3]);
			bestScore = (currentScore > bestScore ? currentScore : bestScore);

			scoreTextObj.GetComponent<TextMesh>().text = "Score: 0    Best: " + bestScore;
		
			//reset script's score variable
			var scorePointComponent = GameObject.FindGameObjectWithTag("Point").GetComponent<ScorePoint>();
			scorePointComponent.ResetScore();
			scorePointComponent.SetBest(bestScore);
		}
	}
}
