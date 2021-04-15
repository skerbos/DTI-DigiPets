using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPet : MonoBehaviour
{

    public PetClass.Pet npc = new PetClass.Pet(7f, 5f, false, false, false);
    private Rigidbody2D rb;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (npc.isInteracting == false)
        {
            npc.Roam(gameObject, rb, npc.PickRandomRoamPoint(), animator);
        }
    }
}
