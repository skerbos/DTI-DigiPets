using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{

    public CursorClass.Cursor cursor = new CursorClass.Cursor(5f, new Vector2(0,0), false);
    public KeyCode clickKey = KeyCode.A;
    public int cursorID = 1;
    private Vector2 moveDir;
    private Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        cursor.Move(rb);
        cursor.ClickOnPet(gameObject, clickKey);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player") && cursorID == collision.transform.GetComponent<PlayerPet>().petID)
        {
            cursor.isOnPet = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cursor.isOnPet = false;
    }

}
