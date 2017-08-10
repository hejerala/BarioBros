using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MovingPlatform : MonoBehaviour {

    public Vector3 targetPosition;
    public float duration = 2.0f;

	// Use this for initialization
	void Start () {
        Tweener moveTween = transform.DOMove(transform.position + targetPosition, duration);
        //moveTween.SetLoops(-1);
        moveTween.SetLoops(-1, LoopType.Yoyo);
        moveTween.SetEase(Ease.InOutCubic);
    }
	
	// Update is called once per frame
	//void Update () { }
}
