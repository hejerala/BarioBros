using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
    private AudioSource coinSound;

    // Use this for initialization
    void Start() {
        coinSound = GetComponentInChildren<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.GetComponent<Bario>() != null) {
            //Debug.Log("Coin Colllected");
            col.GetComponent<Bario>().OnCoinCollected();
            coinSound.transform.SetParent(null);
            coinSound.Play();
            Destroy(gameObject);
            Destroy(coinSound.gameObject, coinSound.clip.length);
            ScoreManager.coinsLeft--;
            Debug.Log("Coins Left: "+ScoreManager.coinsLeft);
        }
    }

}
