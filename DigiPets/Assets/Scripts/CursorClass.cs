using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorClass : MonoBehaviour
{
    public abstract class CursorBehavior
    {
        public abstract void Move(Rigidbody2D self);
        public abstract void ClickOnPet(GameObject cursor, KeyCode key);

    }

    public class Cursor : CursorBehavior
    {
        public float moveSpeed;
        public Vector2 clickPos;
        public bool isOnPet;
        public Cursor(float moveSpeed, Vector2 clickPos, bool isOnPet)
        {
            this.moveSpeed = moveSpeed;
            this.clickPos = clickPos;
            this.isOnPet = isOnPet;
        }

        public override void Move(Rigidbody2D self)
        {
            Vector2 moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            self.velocity = moveDir * moveSpeed;
        }

        public override void ClickOnPet(GameObject cursor, KeyCode key)
        {
            if (Input.GetKeyDown(key) && isOnPet == true)
            {
                Debug.Log("Clicked on pet");
            }
        }
    }
}
