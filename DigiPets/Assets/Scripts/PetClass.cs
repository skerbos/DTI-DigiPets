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
        public abstract void InteractTOW(GameObject self, GameObject cursor, GameObject interactable, Animator animator);
        public abstract void InteractWithOtherPet(GameObject self, GameObject other, Rigidbody2D rbself, Rigidbody2D rbother);
        public abstract void EndemicBehavior(GameObject self, GameObject other);
        public abstract void ShowCurrentState(GameObject self, GameObject stateIcon);
    }

    public class Pet : PetBehavior
    {

        public int petId;
        public GameObject currentInteractableObject = null;
        public float moveSpeed;
        public float roamTime;
        public bool isMoving;
        public bool isStopMovement;
        public bool isPlayingTOW;
        public bool isRoaming;
        public bool isInteracting;
        public string currentState;
        private float endRoamTime;
        private Vector2 randomPos;
        private Vector2 travelPos;
        TOWGame TOW;
        GameObject TOWBar;


		public Pet(float moveSpeed, float roamTime, bool isMoving, bool isRoaming, bool isInteracting, string currentState, bool isStopMovement = false, int petId = 0)
        {
            this.petId = petId;
            this.moveSpeed = moveSpeed;
            this.roamTime = roamTime;
            this.isMoving = isMoving;
            this.isRoaming = isRoaming;
            this.isInteracting = isInteracting;
			this.isStopMovement = isStopMovement;
			this.currentState = currentState;
        }

        public override Vector2 SetTravelPosition(GameObject self, GameObject cursor)
        {
            if (cursor.GetComponent<PlayerCursor>().cursorID == self.GetComponent<PlayerPet>().petID && cursor.GetComponent<PlayerCursor>().cursor.isOnPet == false && Input.GetButtonDown("JFire1" + cursor.GetComponent<PlayerCursor>().cursorID))
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
                text.text = "Play Tug-Of-War!";
            }
            //else if (interactable.CompareTag("NPC"))
            //{
            //    text.gameObject.SetActive(true);
            //    text.text = "Interact!";
            //}
        }

        public override void Idle()
        {

        }

        public override Vector2 PickRandomRoamPoint()
        {
            if (Time.time > endRoamTime + roamTime && isMoving == false && isRoaming == false && isInteracting == false)
            {
                isRoaming = true;
                randomPos = new Vector2(Random.Range(-13f, 13f), Random.Range(-13f, 13f));
                endRoamTime = Time.time;
                roamTime = Random.Range(7f, 10f);
            }
            else if (isMoving == true || isInteracting == true)
            {
                endRoamTime = Time.time;
                roamTime = Random.Range(7f, 10f);
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
                isInteracting = false;
                animator.SetBool("isFeeding", false);
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

        public override void InteractTOW(GameObject self, GameObject cursor, GameObject interactable, Animator animator)
        {
            TOW.UpdateBar(self, cursor, interactable, animator);
        }

        public override void Interact(GameObject self, GameObject cursor, GameObject interactable, Animator animator)
        {
            if (interactable == null)
            {

            }
            else if (interactable.CompareTag("Feed"))
            {
				if (cursor.GetComponent<PlayerCursor>().cursor.isOnPet == true && Input.GetButtonDown("JFire1" + cursor.GetComponent<PlayerCursor>().cursorID) && isInteracting == false)                {
                    currentState = "happy";
                    isInteracting = true;
                    animator.SetBool("isFeeding", true);
                }
                else if (cursor.GetComponent<PlayerCursor>().cursor.isOnPet == true && Input.GetButtonDown("JFire1" + cursor.GetComponent<PlayerCursor>().cursorID) && isInteracting == true || isMoving == true)
                {
                    isInteracting = false;
                    animator.SetBool("isFeeding", false);
                }
            }
            else if (interactable.CompareTag("Play"))
            {
                if (cursor.GetComponent<PlayerCursor>().cursor.isOnPet == true && Input.GetButtonDown("JFire1" + cursor.GetComponent<PlayerCursor>().cursorID) && isInteracting == false)
                {
                    isInteracting = true;
                    animator.SetBool("isFeeding", true);
                }
                else if (cursor.GetComponent<PlayerCursor>().cursor.isOnPet == true && Input.GetButtonDown("JFire1" + cursor.GetComponent<PlayerCursor>().cursorID) && isInteracting == true || isMoving == true)
                {
                    isInteracting = false;
                    animator.SetBool("isFeeding", false);
                }
            }
            else if (interactable.CompareTag("Player"))
            {
                if (cursor.GetComponent<PlayerCursor>().cursor.isOnPet == true && Input.GetButtonDown("JFire1" + cursor.GetComponent<PlayerCursor>().cursorID) && isInteracting == false)
                {
                    //Start Game
                    PlayerPet HostPlayer = self.GetComponent<PlayerPet>();
                    PlayerPet OpponentPlayer = interactable.GetComponent<PlayerPet>();

                    //Stop movement for pets
                    HostPlayer.pet.isPlayingTOW = true;
                    HostPlayer.pet.isInteracting = true;
                    HostPlayer.pet.isStopMovement = true;
                    HostPlayer.pet.isMoving = false;
                    HostPlayer.pet.isRoaming = false;
                    OpponentPlayer.pet.isPlayingTOW = true;
                    OpponentPlayer.pet.isInteracting = true;
                    OpponentPlayer.pet.isStopMovement = true;
                    OpponentPlayer.pet.isMoving = false;
                    OpponentPlayer.pet.isRoaming = false;

                    // Get Position each Player
                    var HostPosition = self.transform.position;
                    var OpponentPosition = interactable.transform.position;

                    // Find Mid X Distance between Player to setup bar position
                    float xDist = HostPosition.x - ((HostPosition.x - OpponentPosition.x) / 2);

                    // Find Bigger Y distance between player to setup bar position
                    // todo: Add check if out of bounds. If -5 is out of bounds, put +5 units above biggest Y between players
                    float yDist = HostPosition.y <= OpponentPosition.y ? HostPosition.y : OpponentPosition.y;
                    yDist = yDist - 5;

                    // Decide Who is Left & Right Player
                    PlayerPet LeftPlayer = (HostPosition.x <= OpponentPosition.x) ? HostPlayer : OpponentPlayer;
                    PlayerPet RightPlayer = (LeftPlayer == OpponentPlayer) ? HostPlayer : OpponentPlayer;


                    //self.GetComponent<PlayerPet>();
                    //interactable.AddComponent<TOWGame> ();

                    // Instantiate Game between both players
                    //tow = new TOWGame(LeftPlayer, RightPlayer, xDist, yDist);
                    TOWBar = GameObject.Find("TOWCanvas/TOWBar");
                    TOW = TOWBar.GetComponent<TOWGame>();
                    TOW.StartNewGame(LeftPlayer, RightPlayer, xDist, yDist);
                    OpponentPlayer.pet.TOW = TOW;


                    //animator.SetBool("isFeeding", true);
                }
                else if (cursor.GetComponent<PlayerCursor>().cursor.isOnPet == true && Input.GetButtonDown("JFire1" + cursor.GetComponent<PlayerCursor>().cursorID) && isInteracting == true || isMoving == true)
                {
                    isInteracting = false;
                    isStopMovement = false;
                    //animator.SetBool("isPlayingTugOfWar", false);
                    //animator.SetBool("isFeeding", false);

                }
            }
        }

        public override void EndemicBehavior(GameObject self, GameObject other)
        {
            if (currentState != "neutral" && other.GetComponent<NPCPet>().npc.currentState == "neutral")
            {
                if (Random.Range(0, 50) == 1)
                {
                    other.GetComponent<NPCPet>().npc.currentState = currentState;
                }
            }
            else if ((currentState == "happy" && other.GetComponent<NPCPet>().npc.currentState == "sad") || (currentState == "sad" && other.GetComponent<NPCPet>().npc.currentState == "happy"))
            {
                if (Random.Range(0, 50) == 1)
                {
                    currentState = "neutral";
                    other.GetComponent<NPCPet>().npc.currentState = "neutral";
                }
            }

        }

        public override void ShowCurrentState(GameObject self, GameObject stateIcon)
        {
            if (currentState == "happy")
            {
                stateIcon.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else if (currentState == "sad")
            {
                stateIcon.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if (currentState == "neutral")
            {
                stateIcon.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            //stateIcon.transform.position = new Vector2(self.transform.position.x, self.transform.position.y + 50);
        }

        public override void InteractWithOtherPet(GameObject self, GameObject other, Rigidbody2D rbself, Rigidbody2D rbother)
        {
            throw new System.NotImplementedException();
        }
    }
}
