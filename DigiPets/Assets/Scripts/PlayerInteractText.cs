using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractText : MonoBehaviour
{
    public GameObject player;
    private Vector2 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = Camera.main.WorldToScreenPoint(player.transform.position);
        transform.position = new Vector2(playerPos.x, playerPos.y + 30);
    }
}
