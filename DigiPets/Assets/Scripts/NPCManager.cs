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
}
