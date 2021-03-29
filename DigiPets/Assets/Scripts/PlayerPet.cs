using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPet : MonoBehaviour
{
    public PetClass.Pet pet = new PetClass.Pet(7f, 2f, false, false);
    public int petID = 1;
    public GameObject petCursor;
    private Rigidbody2D rb;
    private Vector2 travelPos;
    private Vector2 randomPos;
    private float endRoamTime;

    // Start is called before the first frame update
    void Start()
    {
        endRoamTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ListenForClick();
        pickRandomRoamPoint();
        pet.Move(gameObject, rb, travelPos);
        pet.Roam(gameObject, rb, randomPos);
        
    }

    void ListenForClick()
    {
        if (petCursor.GetComponent<PlayerCursor>().cursorID == petID && petCursor.GetComponent<PlayerCursor>().cursor.isOnPet == false && Input.GetKeyDown(petCursor.GetComponent<PlayerCursor>().clickKey))
        {
            travelPos = (Vector2)petCursor.transform.position;
            pet.isMoving = true;
            pet.isRoaming = false;
        }
    }

    void pickRandomRoamPoint()
    {
        if (Time.time > endRoamTime + pet.roamTime && pet.isMoving == false && pet.isRoaming == false)
        {
            pet.isRoaming = true;
            randomPos = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            endRoamTime = Time.time;
            pet.roamTime = Random.Range(10f, 15f);
            Debug.Log(randomPos);
        }
        else if (pet.isMoving == true)
        {
            endRoamTime = Time.time;
            pet.roamTime = Random.Range(10f, 15f);
        }
    }
}
