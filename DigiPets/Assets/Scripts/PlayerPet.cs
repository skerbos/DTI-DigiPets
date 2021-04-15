using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPet : MonoBehaviour
{
    public PetClass.Pet pet = new PetClass.Pet(7f, 5f, false, false, false);
    public int petID = 1;
    public GameObject petCursor;
    public Animator animator;
    public Text playerInteractText;
    private Rigidbody2D rb;
    private GameObject currentInteractable;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        pet.Move(gameObject, rb, pet.SetTravelPosition(gameObject, petCursor), animator);
        if(pet.isInteracting == false)
        {
            pet.Roam(gameObject, rb, pet.PickRandomRoamPoint(), animator);
        }
        pet.Interact(gameObject, petCursor, currentInteractable, animator);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        
        if (!collider.gameObject.CompareTag("Untagged"))
        {
            currentInteractable = collider.gameObject;
        }
        pet.petText(collider.gameObject, playerInteractText);
        //pet.Interact(gameObject, petCursor, collider.gameObject, animator);
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        playerInteractText.gameObject.SetActive(false);
        //currentInteractable = null;
        pet.isInteracting = false;
    }
}
