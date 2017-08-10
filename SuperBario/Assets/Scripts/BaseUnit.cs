using UnityEngine;
using System.Collections;

public class BaseUnit : MonoBehaviour {

    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Animator anim;
    public float speed = 4.0f;
    private float raycastDistance = 0.1f;
    public float groundedOffset = 0.4f;
    private MovingPlatform currentPlatform;

    // Use this for initialization
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected void Move(float input)
    {
        //Use physics instead of transform because transform causes a jitter when colliding
        //Dont reset velocity on Y or you wont fall correctly. Always keep the Y
        rb.velocity = new Vector2(input * speed, rb.velocity.y);

        if (input < 0)
        {
            sr.flipX = true;
        }
        else if (input > 0)
        {
            sr.flipX = false;
        }
        anim.SetFloat("Speed", Mathf.Abs(input));
    }

    protected bool IsGrounded(float offset) {
        //Physics.Raycast
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position + new Vector3(offset, 0.0f, 0.0f), Vector2.down, raycastDistance);
        //Debug turn gizmos on in game view
        Debug.DrawRay(transform.position + new Vector3(offset, 0.0f, 0.0f), Vector2.down * raycastDistance, Color.red);
        //return !(hitInfo.collider == null);
        //if (hitInfo.collider == null)
        //    return false;
        //return true;
        if (hitInfo.collider != null) {
            MovingPlatform platform = hitInfo.collider.GetComponent<MovingPlatform>();
            //if (platform != null) {
            if (platform != null && platform != currentPlatform) {
                currentPlatform = platform;
                transform.SetParent(currentPlatform.transform, true);
            } else if (platform == null) {
                currentPlatform = null;
                transform.SetParent(null);
            }
            return true;
        }
        if (currentPlatform != null) {
            currentPlatform = null;
            transform.SetParent(null);
        }
        return false;
    }

    public virtual void Die() {

    }

}
