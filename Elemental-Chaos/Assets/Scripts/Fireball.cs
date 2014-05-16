using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

	public float speed = 6;
	public GameObject explosionPrefab = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward * Time.deltaTime*speed);
	}

	void explode() {
		GameObject explosion = (GameObject)Instantiate (explosionPrefab, transform.position, Quaternion.identity);
		explosion.audio.Play ();
		Destroy (gameObject);
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag == "player") {
				}
		explode ();

	}
}
