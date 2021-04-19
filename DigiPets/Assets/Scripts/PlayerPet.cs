using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPet : MonoBehaviour
{
    public int petID = 1;
    public PetClass.Pet pet;
    public GameObject petCursor;
    public GameObject stateIcon;
    public Animator animator;
    public Text playerInteractText;
    private Rigidbody2D rb;
    private GameObject currentInteractable = null;

    void Awake()
    {
        pet = new PetClass.Pet(7f, 5f, false, false, false, "", false, petID);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pet.currentState = "neutral";
    }

    // Update is called once per frame
    void Update()
    {
        if (pet.isStopMovement == false)
        { 
            pet.Move(gameObject, rb, pet.SetTravelPosition(gameObject, petCursor), animator);
            if (pet.isInteracting == false)
            {
                pet.Roam(gameObject, rb, pet.PickRandomRoamPoint(), animator);
            }
            pet.Interact(gameObject, petCursor, currentInteractable, animator);
        }
        if (pet.isPlayingTOW == true)
        {
            pet.InteractTOW(gameObject, petCursor, currentInteractable, animator);
        }
        
        pet.ShowCurrentState(gameObject, stateIcon);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            pet.EndemicBehavior(gameObject, collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(pet.isInteracting == false)
        {
            if (!collider.gameObject.CompareTag("Untagged"))
            {
                currentInteractable = collider.gameObject;
            }
            pet.petText(collider.gameObject, playerInteractText);
            //pet.Interact(gameObject, petCursor, collider.gameObject, animator);
        }

    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (pet.isInteracting == false)
        {
            playerInteractText.gameObject.SetActive(false);
            currentInteractable = null;
            //pet.isInteracting = false;
        }
    }
}
