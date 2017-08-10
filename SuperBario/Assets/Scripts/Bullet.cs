using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    private Rigidbody2D rb;
    public float bulletSpeed = 500.0f;

    // Use this for initialization
    protected virtual void Start () {
        rb = GetComponent<Rigidbody2D>();
        Vector3 sp = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 dir = (Input.mousePosition - sp).normalized;
        rb.AddForce(dir * bulletSpeed);
    }

    // Update is called once per frame
    //protected virtual void Update () {
	
	//}

    //void OnCollisionEnter2D(Collision2D coll) {
    //    Debug.Log("Collided");
    //    Bario player = coll.collider.GetComponent<Bario>();
    //    if (player != null) {
    //        Debug.Log("Player");
    //        return;
    //    }
    //}

    public void DestroyBullet() {
        Destroy(gameObject);
    }

}
