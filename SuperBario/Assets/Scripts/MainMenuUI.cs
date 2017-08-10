using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        //Unlocks the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickedStartButton()
    {
        //Starts the game
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickedExitButton()
    {
        //Quits the game
        Application.Quit();
    }
}
