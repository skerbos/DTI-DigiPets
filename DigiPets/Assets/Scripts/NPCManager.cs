using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{

    public GameObject npc;
    public int spawnLimit = 10;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnControl();
    }

    void Spawn()
    {
        for (int i = 0; i < spawnLimit; i++)
        {
            GameObject npcClone = Instantiate(npc);
            npcClone.transform.position = new Vector2(Random.Range(-13f, 13f), Random.Range(-13f, 13f));
            AssignState(npcClone);
        }
    }

    void SpawnControl()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameObject npcClone = Instantiate(npc);
            //AssignSprite(npcClone);
            npcClone.transform.position = new Vector2(Random.Range(-13f, 13f), Random.Range(-13f, 13f));
            AssignState(npcClone);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            for (int i = 0; i < 10; i++)
            {
                GameObject npcClone = Instantiate(npc);
                npcClone.transform.position = new Vector2(Random.Range(-13f, 13f), Random.Range(-13f, 13f));
                //AssignSprite(npcClone);
                AssignState(npcClone);
            }
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            Destroy(GameObject.FindGameObjectWithTag("NPC"));
        }

    }

    void AssignState(GameObject npc)
    {
        int randomState = Random.Range(0, 10);

        if (randomState == 0)
        {
            npc.GetComponent<NPCPet>().npc.currentState = "happy";
        }
        else if (randomState == 1)
        {
            npc.GetComponent<NPCPet>().npc.currentState = "sad";
        }
        else
        {
            npc.GetComponent<NPCPet>().npc.currentState = "neutral";
        }
    }

    void AssignSprite(GameObject npc)
    {
        //int randomSprite = Random.Range(0, 2);

        //if (randomSprite == 0)
        //{
        npc.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Assets/Animations/Player.controller");
        //}
        //else 
        //{
        //    npc.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Assets/Animations/Other Player") as RuntimeAnimatorController;
        //}
    }
}
