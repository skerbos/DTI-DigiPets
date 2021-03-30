using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetClass : MonoBehaviour
{
    public abstract class PetBehavior
    {
        public abstract Vector2 SetTravelPosition(GameObject self, GameObject cursor);
        public abstract void petText(GameObject interactable, Text text);
        public abstract void Idle();
        public abstract Vector2 PickRandomRoamPoint();
        public abstract void Roam(GameObject self, Rigidbody2D rb, Vector2 randomPos, Animator animator);
        public abstract void Move(GameObject self, Rigidbody2D rb, Vector2 cursorPos, Animator animator);
        public abstract void Interact(GameObject self, GameObject cursor, GameObject interactable, Animator animator);
        public abstract void InteractWithOtherPet(GameObject self, GameObject other, Rigidbody2D rbself, Rigidbody2D rbother);
    }

    public class Pet : PetBehavior
    {

        public float moveSpeed;
        public float roamTime;
        public bool isMoving;
        public bool isRoaming;
        public bool isInteracting;
        private float endRoamTime;
        private Vector2 randomPos;
        private Vector2 travelPos;

        public Pet(float moveSpeed, float roamTime, bool isMoving, bool isRoaming, bool isInteracting)
        {
            this.moveSpeed = moveSpeed;
            this.roamTime = roamTime;
            this.isMoving = isMoving;
            this.isRoaming = isRoaming;
            this.isInteracting = isInteracting;
        }

        public override Vector2 SetTravelPosition(GameObject self, GameObject cursor)
        {
            if (cursor.GetComponent<PlayerCursor>().cursorID == self.GetComponent<PlayerPet>().petID && cursor.GetComponent<PlayerCursor>().cursor.isOnPet == false && Input.GetKeyDown(cursor.GetComponent<PlayerCursor>().cursor.click))
            {
                travelPos = (Vector2)cursor.transform.position;
                isMoving = true;
                isRoaming = false;
            }
            return travelPos;
        }

        public override void petText(GameObject interactable, Text text)
        {
            if (interactable.CompareTag("Play"))
            {
                text.gameObject.SetActive(true);
                text.text = "Play!";
            }
            else if (interactable.CompareTag("Feed"))
            {
                text.gameObject.SetActive(true);
                text.text = "Feed!";
            }
            else if (interactable.CompareTag("Player"))
            {
                text.gameObject.SetActive(true);
                text.text = "Interact!";
            }
        }

        public override void Idle()
        {

        }

        public override Vector2 PickRandomRoamPoint()
        {
            if (Time.time > endRoamTime + roamTime && isMoving == false && isRoaming == false && isInteracting == false)
            {
                isRoaming = true;
                randomPos = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
                endRoamTime = Time.time;
                roamTime = Random.Range(10f, 15f);
                Debug.Log(randomPos);
            }
            else if (isMoving == true || isInteracting == true)
            {
                endRoamTime = Time.time;
                roamTime = Random.Range(10f, 15f);
            }
            return randomPos;
        }
        public override void Roam(GameObject self, Rigidbody2D rb, Vector2 randomPos, Animator animator)
        {
            if (isRoaming == true)
            {
                Vector2 moveDir = randomPos - (Vector2)self.transform.position;
                if (moveDir.magnitude > 1f)
                {
                    rb.velocity = moveSpeed * moveDir.normalized;

                    //animation
                    animator.SetFloat("Speed", 1);
                    if (rb.velocity.x > 0)
                    {
                        self.transform.rotation = Quaternion.Euler(0, 0, self.transform.rotation.z);
                    }
                    else if (rb.velocity.x < 0)
                    {
                        self.transform.rotation = Quaternion.Euler(0, 180, self.transform.rotation.z);
                    }
                }
                else
                {
                    isRoaming = false;
                    animator.SetFloat("Speed", 0);
                }
            }
        }

        public override void Move(GameObject self, Rigidbody2D rb, Vector2 cursorPos, Animator animator)
        {
            if (isMoving == true)
            {
                Vector2 moveDir = (cursorPos - (Vector2)self.transform.position);
                if (moveDir.magnitude > 1f)
                {
                    rb.velocity = moveSpeed * moveDir.normalized;

                    //animator
                    animator.SetFloat("Speed", 1);
                    if (rb.velocity.x > 0)
                    {
                        self.transform.rotation = Quaternion.Euler(0, 0, self.transform.rotation.z);
                    }
                    else if (rb.velocity.x < 0)
                    {
                        self.transform.rotation = Quaternion.Euler(0, 180, self.transform.rotation.z);
                    }
                }
                else
                {
                    isMoving = false;
                    animator.SetFloat("Speed", 0);
                }
            }
        }

        public override void Interact(GameObject self, GameObject cursor, GameObject interactable, Animator animator)
        {
            if (interactable.CompareTag("Feed"))
            {
                if (cursor.GetComponent<PlayerCursor>().cursor.isOnPet == true && Input.GetKeyDown(cursor.GetComponent<PlayerCursor>().cursor.click) && isInteracting == false)
                {
                    isInteracting = true;
                    animator.SetBool("isFeeding", true);
                }
                else if (cursor.GetComponent<PlayerCursor>().cursor.isOnPet == true && Input.GetKeyDown(cursor.GetComponent<PlayerCursor>().cursor.click) && isInteracting == true)
                {
                    isInteracting = false;
                    animator.SetBool("isFeeding", false);
                }
            }
        }

        public override void InteractWithOtherPet(GameObject self, GameObject other, Rigidbody2D rbself, Rigidbody2D rbother)
        {
            throw new System.NotImplementedException();
        }
    }
}
