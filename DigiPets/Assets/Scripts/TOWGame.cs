using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TOWGame : MonoBehaviour
{

    public GameObject TOWBar;
    public Vector3 TOWBarSize;
    public GameObject TOWBarCountdown;
    public GameObject TOWBarFillLeft;
    public GameObject TOWBarFillRight;
    public TOWBarFill fill;
    public MeshRenderer TOWBarRender;

    PlayerCursor LeftPlayerCursor;
    PlayerCursor RightPlayerCursor;
    PlayerPet LeftPlayer;
    PlayerPet RightPlayer;
    bool isGameLocked;

    public int countdownTime;
    public Text countdownDisplay;

    int? winnerId;

    public TOWGame()
    {

    }



    void Start()
    {
        // Setup - Position Meter
        TOWBar = GameObject.Find("TOWCanvas/TOWBar");
        TOWBarCountdown = GameObject.Find("TOWCanvas/TOWBar/TOWBarBorder/TOWCountdown");
        TOWBarFillLeft = GameObject.Find("TOWCanvas/TOWBar/BarFillLeft/TOWBarFillLeft");
        TOWBarFillRight = GameObject.Find("TOWCanvas/TOWBar/BarFillRight/TOWBarFillRight");
        TOWBarSize = TOWBar.transform.localScale;
        //Hide Bar
        TOWBar.transform.localScale = new Vector3(0, 0, 0);
    }

    public void StartNewGame(PlayerPet LeftPlayer, PlayerPet RightPlayer, float x, float y)
    {
        //Display
        TOWBar.transform.localScale = TOWBarSize;

        TOWBar.SetActive(true);
        TOWBar.transform.position = new Vector3(x, y);
        fill = TOWBar.GetComponent<TOWBarFill>();
        this.LeftPlayer = LeftPlayer;
        this.RightPlayer = RightPlayer;
        isGameLocked = true;
        SetBarFillColors(LeftPlayer, RightPlayer);

        // Unlock game
        StartCoroutine(CountdownToStart(3));
    }

    private void SetBarFillColors(PlayerPet LeftPlayer, PlayerPet RightPlayer)
    {
        Color LeftPlayerCursorColor = new Color();
        foreach (Transform child in LeftPlayer.petCursor.transform)
        {
            SpriteRenderer sprite = child.gameObject.GetComponent<SpriteRenderer>();
            LeftPlayerCursorColor = sprite.color;
        }
        Color RightPlayerCursorColor = new Color();
        foreach (Transform child in RightPlayer.petCursor.transform)
        {
            SpriteRenderer sprite = child.gameObject.GetComponent<SpriteRenderer>();
            RightPlayerCursorColor = sprite.color;
        }

        Image leftBar = TOWBarFillLeft.GetComponent<Image>();
        leftBar.color = LeftPlayerCursorColor;
        Image rightBar = TOWBarFillRight.GetComponent<Image>();
        rightBar.color = RightPlayerCursorColor;
    }

    public enum TOW
    {
        LeftPlayerId = -1,
        RightPlayerId = 1
    }

    IEnumerator CountdownToStart(int countdownTime = 3)
    {
        countdownDisplay = TOWBarCountdown.GetComponent<Text>();
        
        countdownDisplay.text = countdownTime.ToString();
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        isGameLocked = false;

        yield return new WaitForSeconds(0.5f);
        countdownDisplay.text = "";

    }

    IEnumerator DisplayVictoryStatus(PlayerPet winner, string winnerMsg, PlayerPet loser, string loserMsg, float countdownTime = 1)
    {
        //hide bar
        TOWBar.transform.localScale = new Vector3(0, 0, 0);

        winner.playerInteractText.text = winnerMsg;
        winner.playerInteractText.gameObject.SetActive(true);
        loser.playerInteractText.text = loserMsg;
        loser.playerInteractText.gameObject.SetActive(true);
        yield return new WaitForSeconds(countdownTime);
        winner.playerInteractText.text = (winner.playerInteractText.text == winnerMsg) ?  "" : winner.playerInteractText.text;
        loser.playerInteractText.text = (loser.playerInteractText.text == loserMsg) ?  "" : loser.playerInteractText.text;
        EndGame();

    }

    public void UpdateBar(GameObject self, GameObject cursor, GameObject interactable, Animator animator)
    {
        if (isGameLocked == false && Input.GetKeyDown(cursor.GetComponent<PlayerCursor>().cursor.click))
        {
            // Delta Updates
            if (LeftPlayer == self.gameObject.GetComponent<PlayerPet>())
            {
                winnerId = fill.UpdateBar((int)TOW.LeftPlayerId);
            }
            else if (RightPlayer == self.gameObject.GetComponent<PlayerPet>())
            {
                winnerId = fill.UpdateBar((int)TOW.RightPlayerId);
            }
        }
        
        // End Game
        if(winnerId != null)
        {
            if(winnerId > 0)
            {
                StartCoroutine(DisplayVictoryStatus(RightPlayer, "Winner!", LeftPlayer, "Loser!", 3));
            }
            else
            {
                StartCoroutine(DisplayVictoryStatus(LeftPlayer, "Winner!", RightPlayer, "Loser!", 3));
            }

            
        }

        

    }

    void EndGame()
    {

        // Destroy & Reset
        LeftPlayer.pet.isPlayingTOW = false;
        LeftPlayer.pet.isInteracting = false;
        LeftPlayer.pet.isStopMovement = false;
        RightPlayer.pet.isPlayingTOW = false;
        RightPlayer.pet.isInteracting = false;
        RightPlayer.pet.isStopMovement = false;
        

        fill.ResetBar();

        winnerId = null;
    }



    // Update is called once per frame
    void Update()
    {



    }
}
