using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class VictoryMenuUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Unlocks the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClickedPlayAgainButton() {
        //Starts the game
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickedExitButton(){
        //Quits the game
        SceneManager.LoadScene("MenuScene");
    }

}
