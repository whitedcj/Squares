using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

// controller for player objective (green square)
public class ScorePoint : MonoBehaviour {

	public GameObject enemyPrefab;

	private int score, // current game's score
				best; // highest score across all games

	// Use this for initialization
	void Start () {
		score = 0;

		// for multiplayer, 'best' may be either player so read in the score from the GameObject
		string scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMesh>().text;
		var parts = Regex.Split(scoreText, @"\s+"); // ["Score:", "<int>", "Best:", "<int>"]
		best = int.Parse(parts[3]);
	}
	
	// Update is called once per frame
	void Update () {
		/* objective does not move */
	}
	
	void OnTriggerEnter (Collider other){
		if (other.tag.Equals ("Player")) {
			//move point to new location
			gameObject.transform.position = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0);

			//spawn an enemy
			var enemy = Instantiate (enemyPrefab, new Vector3 (Random.Range(-8, 8), Random.Range(-4, 4), 0), Quaternion.identity);

			//increment score
			score++;
			GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMesh>().text = "Score: " + score + "    Best: " + best;
		}
	}

	public void ResetScore(){
		score = 0;
	}
	
	public void SetBest(int bestScore){
		best = bestScore;
	}
}
