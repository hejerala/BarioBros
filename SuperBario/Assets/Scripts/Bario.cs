using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Bario : BaseUnit {

    public float jumpHeight = 6.0f;
    public float deathPosY = -10.0f;
    public float jumpKillHeight = 0.2f;
    public float particleDurationOnCoin = 1.0f;
    public float gravityMultiplier = 10.0f;
    public Fireball fireballPrefab;
    public Star starPrefab;
    public Flag flagPrefab;
    //public float bulletSpeed = 1.5f;
    //private Vector3 bulletTarget;
    private float particleDurationLeft = 0.0f;
    public Texture2D cursorTexture;
    public AudioClip jumpSound;
    public AudioClip deathSound;

    public GameObject fireballIcon;
    public GameObject starIcon;

    public GameObject victoryCanvas;

    private ParticleSystem flameTrail;
    private AudioSource audioPlayer;
    private static Vector3 savedPosition;
    private Flag checkpointFlag;
    private bool teleporterAvailable;
    private Star teleporter;
    private Fireball shootBullet;
    private bool inTeleportMode;
    private float defaultGravity;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        defaultGravity = rb.gravityScale;
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        audioPlayer = GetComponent<AudioSource>();
        flameTrail = GetComponentInChildren<ParticleSystem>();
        teleporterAvailable = false;
        inTeleportMode = false;
        fireballIcon.SetActive(true);
        victoryCanvas.SetActive(false);
        //savedPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update() {
        if (particleDurationLeft > 0) {
            particleDurationLeft -= Time.deltaTime;
            if (particleDurationLeft <= 0)
                flameTrail.Stop();
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float sprintMod = 1.0f;
        if (Input.GetKey(KeyCode.LeftShift))
            sprintMod = 2.0f;
        Move(horizontalInput * sprintMod);
        if (IsGrounded(groundedOffset) || IsGrounded(-groundedOffset))
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                Jump();
        if (transform.position.y < deathPosY)
            Die();

        CheckForJumpKill();

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            rb.gravityScale = defaultGravity * gravityMultiplier;
        }

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)){
            rb.gravityScale = defaultGravity;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("MenuScene");
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            if (checkpointFlag != null)
                checkpointFlag.DestroyFlag();
            Vector3 placeFlagFrom = transform.position;
            placeFlagFrom.y += GetComponent<BoxCollider2D>().bounds.size.y / 2;
            savedPosition = transform.position;
            checkpointFlag = (Flag)Instantiate(flagPrefab, placeFlagFrom, transform.rotation);
            Physics2D.IgnoreCollision(checkpointFlag.transform.GetComponent<Collider2D>(), transform.GetComponent<Collider2D>());
            //checkpointFlag = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //checkpointFlag.transform.position = savedPosition;
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            if (checkpointFlag != null) {
                transform.position = savedPosition;
                checkpointFlag.DestroyFlag();
            }
        }

        //Toggles the edition mode on and off
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (inTeleportMode) {
                fireballIcon.SetActive(true);
                starIcon.SetActive(false);
                inTeleportMode = false;
            } else {
                fireballIcon.SetActive(false);
                starIcon.SetActive(true);
                inTeleportMode = true;
            }
        }

        if (Input.GetMouseButtonDown(0)) {
            Vector3 shootBulletFrom = transform.position;
            shootBulletFrom.y += GetComponent<BoxCollider2D>().bounds.size.y / 2;
            if (inTeleportMode) {
                if (teleporter != null)
                    teleporter.DestroyBullet();
                teleporter = (Star)Instantiate(starPrefab, shootBulletFrom, transform.rotation);
                Physics2D.IgnoreCollision(teleporter.transform.GetComponent<Collider2D>(), transform.GetComponent<Collider2D>());
                teleporterAvailable = true;
                //bulletTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //bulletTarget.z = transform.position.z;
            }
            else {
                shootBullet = (Fireball)Instantiate(fireballPrefab, shootBulletFrom, transform.rotation);
                Physics2D.IgnoreCollision(shootBullet.transform.GetComponent<Collider2D>(), transform.GetComponent<Collider2D>());
            }

        }

        if (teleporterAvailable) {
            if (Input.GetMouseButtonDown(1)) {
                transform.position = teleporter.transform.position;
                teleporter.DestroyBullet();
                teleporterAvailable = false;
            }
        }

    }

    //public void OnCoinCollected() {
    //    particleDurationLeft += particleDurationOnCoin;
    //    if (!flameTrail.isPlaying) {
    //        flameTrail.Play();
    //    }
    //}

    public void OnCoinCollected() {
        if (particleDurationLeft <= 0) {
            flameTrail.Play();
        }
        particleDurationLeft += particleDurationOnCoin;
    }

    void CheckForJumpKill() {
        Vector3 topLeft = transform.position - new Vector3(groundedOffset, 0.0f, 0.0f);
        Vector3 bottomRight = transform.position + new Vector3(groundedOffset, -jumpKillHeight, 0.0f);
        //Top line of the killbox
        Debug.DrawLine(topLeft, transform.position + new Vector3(groundedOffset, 0.0f, 0.0f), Color.red);
        //Left Line of the killbox
        Debug.DrawLine(topLeft, topLeft - new Vector3(0.0f, jumpKillHeight, 0.0f), Color.red);
        //Right line of the killbox
        Debug.DrawLine(bottomRight, bottomRight + new Vector3(0.0f, jumpKillHeight, 0.0f), Color.red);
        //Bottom Line of the killbox
        //Debug.DrawLine(bottomRight, transform.position - new Vector3(groundedOffset, jumpKillHeight, 0.0f), Color.red);
        Debug.DrawLine(bottomRight, bottomRight + new Vector3(-groundedOffset*2.0f, 0.0f, 0.0f), Color.red);
        foreach (Collider2D coll in Physics2D.OverlapAreaAll(topLeft, bottomRight)) {
            Trowser enemy = coll.GetComponent<Trowser>();
            if (enemy != null) {
                enemy.Die();
                Jump();
            }
        }
    }

    void Jump() {
        //We dont add(+=) velocity in case the player is able to jump twice in two frames (we reset it =)
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        audioPlayer.PlayOneShot(jumpSound);
    }

    void SceneReload() {
        //Add scene to the build settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public override void Die() {
        enabled = false;
        //Jump();
        rb.velocity = new Vector2(0.0f, jumpHeight);
        GetComponent<Collider2D>().enabled = false;
        audioPlayer.PlayOneShot(deathSound);
        Invoke("SceneReload", deathSound.length);
        //Invoke("SceneReload", 2.0f);
        Camera.main.transform.SetParent(null);
    }

    public static void ResetSavedLocation(Vector3 newPosition) {
        savedPosition = newPosition;
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.GetComponent<Trowser>() != null || coll.gameObject.GetComponent<Spike>() != null) {
            Die();
            return;
        }
        if (coll.gameObject.GetComponent<Door>() != null) {
            victoryCanvas.SetActive(true);
        }
    }

}
