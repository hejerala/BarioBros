using UnityEngine;
using System.Collections;

public class Fireball : Bullet {

    // Use this for initialization
    protected override void Start () {
        base.Start();
        Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update () {
	
	}

}
