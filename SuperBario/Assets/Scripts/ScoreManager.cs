using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public static int coinsLeft;

    void OnEnable() {
        coinsLeft = FindObjectsOfType<Coin>().Length;
    }

}
