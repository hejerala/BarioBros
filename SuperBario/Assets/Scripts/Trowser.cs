using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Trowser : BaseUnit {

    public float direction = 1.0f;
    public float flatteningTime = 0.5f;
	// Use this for initialization
	//void Start () { base.Start(); }
	
	// Update is called once per frame
	void Update () {
        if (!IsGrounded(groundedOffset)) {
            direction = -1.0f;
        }
        else if (!IsGrounded(-groundedOffset)) {
            direction = 1.0f;
        }
            Move(direction);
	}

    public override void Die() {
        enabled = false;
        GetComponent<Collider2D>().enabled = false;
        rb.isKinematic = true;
        transform.DOScaleY(0.0f, flatteningTime);
        Destroy(gameObject, flatteningTime);
        //Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.GetComponent<Fireball>() != null) {
            Die();
        }
    }

}
