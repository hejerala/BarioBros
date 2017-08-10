using UnityEngine;
using System.Collections;

public class Flag : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.GetComponent<Spike>() != null) {
            Destroy(gameObject);
        }
        else {
            Bario.ResetSavedLocation(transform.position);
        }
    }

    public void DestroyFlag() {
        Destroy(gameObject);
    }

}
