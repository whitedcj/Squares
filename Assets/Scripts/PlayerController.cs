using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float playerSpeed = 1;
	public Vector3 playerPos;
	
	void Start() {
		playerPos = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		float yPos = gameObject.transform.position.y + (Input.GetAxis ("Vertical") * playerSpeed);
		float xPos = gameObject.transform.position.x + (Input.GetAxis ("Horizontal") * playerSpeed);

		// enforce boundary
		playerPos.y = Mathf.Clamp (yPos, -4, 4);
		playerPos.x = Mathf.Clamp (xPos, -8, 8);
		
		gameObject.transform.position = playerPos;
	}
}
