using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    public Text label;
    public GameObject finalDoor;
	
	// Update is called once per frame
	void Update () {
        //label.text = ScoreManager.score.ToString();
        if (ScoreManager.coinsLeft <= 0) {
            label.text = "You have all the coins. Look for the door!";
            finalDoor.SetActive(true);
        } else {
            label.text = "Coins Left: " + ScoreManager.coinsLeft;
        } 
	}
}
