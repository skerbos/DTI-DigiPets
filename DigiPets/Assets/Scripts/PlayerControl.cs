using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

    public float moveForce = 10f;
    public float maxSpeed = 2f;
    public Text playerInteractText;
    public Sprite defaultSprite;
    public Sprite feedSprite;
    public Sprite playSprite;
    public Sprite interactSprite;
    private Rigidbody2D rb;
    private Vector2 netDir;
    private float xDir;
    private float yDir;
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode interact;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        playerInteractText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        FaceMoveDirection();
    }

    void FixedUpdate()
    {
        Move();
        LimitMaxSpeed();
    }

    void Move()
    {
        if (Input.GetKey(right))
        {
            xDir = 1;
        }
        else if (Input.GetKey(left))
        {
            xDir = -1;
        }
        else
        {
            xDir = 0;
        }
        if (Input.GetKey(up))
        {
            yDir = 1;
        }
        else if (Input.GetKey(down))
        {
            yDir = -1;
        }
        else
        {
            yDir = 0;
        }

        netDir = new Vector2(xDir, yDir);

        rb.AddForce(netDir * moveForce);
    }

    void LimitMaxSpeed()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    void FaceMoveDirection()
    {
        if (xDir > 0 && transform.rotation.y < 90)
        {
            float angle = Mathf.Lerp(0, 180, Time.time);
            transform.eulerAngles = new Vector3(0, angle, 0);
        }
        if (xDir < 0 && transform.rotation.y < 90)
        {
            float angle = Mathf.Lerp(180, 0, Time.time);
            transform.eulerAngles = new Vector3(0, angle, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Play"))
        {
            playerInteractText.gameObject.SetActive(true);
            playerInteractText.text = "Play!";

            if (Input.GetKey(interact))
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = playSprite;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
            }
        }
        else if (collision.gameObject.CompareTag("Feed"))
        {
            playerInteractText.gameObject.SetActive(true);
            playerInteractText.text = "Feed!";

            if (Input.GetKey(interact))
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = feedSprite;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
            }
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            playerInteractText.gameObject.SetActive(true);
            playerInteractText.text = "Interact!";

            if (Input.GetKey(interact))
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = interactSprite;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInteractText.gameObject.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
    }
}
