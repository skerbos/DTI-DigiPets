using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{

    public CursorClass.Cursor cursor = new CursorClass.Cursor(KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None, 7f, new Vector2(0,0), false);
    public KeyCode up = KeyCode.UpArrow;
    public KeyCode down = KeyCode.DownArrow;
    public KeyCode left = KeyCode.LeftArrow;
    public KeyCode right = KeyCode.RightArrow;
    public KeyCode clickKey = KeyCode.A;
    public int cursorID = 1;
    private Vector2 moveDir;
    private Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        cursor.up = up;
        cursor.down = down;
        cursor.left = left;
        cursor.right = right;
        cursor.click = clickKey;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        cursor.Move(rb);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Player") && cursorID == collider.transform.GetComponent<PlayerPet>().petID)
        {
            cursor.isOnPet = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cursor.isOnPet = false;
    }

}
