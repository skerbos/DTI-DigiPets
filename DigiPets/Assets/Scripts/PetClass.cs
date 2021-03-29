using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetClass : MonoBehaviour
{
    public abstract class PetBehavior
    {
        public abstract void Idle();
        public abstract void Roam(GameObject self, Rigidbody2D rb, Vector2 randomPos);
        public abstract void Move(GameObject self, Rigidbody2D rb, Vector2 cursorPos);
        public abstract void InteractWithOtherPet(GameObject self, GameObject other, Rigidbody2D rbself, Rigidbody2D rbother);
    }

    public class Pet : PetBehavior
    {

        public float moveSpeed;
        public float roamTime;
        public bool isMoving;
        public bool isRoaming;

        public Pet(float moveSpeed, float roamTime, bool isMoving, bool isRoaming)
        {
            this.moveSpeed = moveSpeed;
            this.roamTime = roamTime;
            this.isMoving = isMoving;
            this.isRoaming = isRoaming;
        }
        public override void Idle()
        {

        }

        public override void Roam(GameObject self, Rigidbody2D rb, Vector2 randomPos)
        {
            if (isRoaming == true)
            {
                Vector2 moveDir = randomPos - (Vector2)self.transform.position;
                if (moveDir.magnitude > 1f)
                {
                    rb.velocity = moveSpeed * moveDir.normalized;
                }
                else
                {
                    isRoaming = false;
                }
            }
        }

        public override void Move(GameObject self, Rigidbody2D rb, Vector2 cursorPos)
        {
            if (isMoving == true)
            {
                Vector2 moveDir = (cursorPos - (Vector2)self.transform.position);
                if (moveDir.magnitude > 1f)
                {
                    rb.velocity = moveSpeed * moveDir.normalized;
                }
                else
                {
                    isMoving = false;
                }
            }
        }

        public override void InteractWithOtherPet(GameObject self, GameObject other, Rigidbody2D rbself, Rigidbody2D rbother)
        {
            throw new System.NotImplementedException();
        }
    }
}
