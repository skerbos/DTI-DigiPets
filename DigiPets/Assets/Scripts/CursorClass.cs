using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorClass : MonoBehaviour
{
    public abstract class CursorBehavior
    {
        public abstract void Move(Rigidbody2D self, int playerNumber);
        //public abstract void Raycast(GameObject cursor);

    }

    public class Cursor : CursorBehavior
    {
        /*public KeyCode up;
        public KeyCode down;
        public KeyCode left;
        public KeyCode right;
        public KeyCode click;*/
        public float moveSpeed;
        public Vector2 clickPos;
        public bool isOnPet;
        private float xDir;
        private float yDir;
        private RaycastHit2D hit;
        public Cursor(/*KeyCode up, KeyCode down, KeyCode left, KeyCode right, KeyCode click,*/ float moveSpeed, Vector2 clickPos, bool isOnPet)
        {
            /*this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
            this.click = click;*/
            this.moveSpeed = moveSpeed;
            this.clickPos = clickPos;
            this.isOnPet = isOnPet;
        }

        public override void Move(Rigidbody2D self, int playerNumber)
        {

            /*if (Input.GetKey(right))
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
            }*/

            xDir = Input.GetAxisRaw("JHorizontal" + playerNumber);
            yDir = Input.GetAxisRaw("JVertical" + playerNumber);
            Vector2 moveDir = new Vector2(xDir, yDir);
            self.velocity = moveDir * moveSpeed;
        }
    }
}
