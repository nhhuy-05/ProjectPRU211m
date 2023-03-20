using Mono.Cecil;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController SelectedPlayer; // static reference to the currently selected player
    Tilemap mountainTilemap; // reference to the "Mountain" tilemap
    public float moveSpeed = 5f; // player's movement speed

    private Transform sleepingEffect;// reference to the "SleepingEffect" particle system
    private Transform Canvas;
    private Button yesOption, noOption;
    private bool moneyDeducted = false;
    private bool isSleeping = true; // flag to indicate if the player is sleeping
    private Vector3 targetPosition; // position where the player is moving to


    void Start()
    {
        // find the Mountain GameObject and Sleeping Effect GameObject by name
        GameObject mountainGameObject = GameObject.Find("Mountain");
        // Get Body
        GameObject body = gameObject.transform.Find("Body").gameObject;
        // get the ParticleSystem from the Sleeping Effect GameObject
        sleepingEffect = body.transform.Find("SleepingEffect");
        // get canvas from Player gameObject
        Canvas = body.transform.Find("Canvas");
        // get Button component from Canvas
        yesOption = Canvas.gameObject.transform.Find("Yes").GetComponent<Button>();
        noOption = Canvas.gameObject.transform.Find("No").GetComponent<Button>();
        // get the Tilemap component from the Mountain GameObject
        mountainTilemap = mountainGameObject.GetComponent<Tilemap>();
    }

    public void clickYes()
    {

        Time.timeScale = 1;
        isSleeping = false;
        Canvas.gameObject.SetActive(false);
        sleepingEffect.gameObject.GetComponent<ParticleSystem>().Stop();

        if (!moneyDeducted) // check if the money has not been deducted yet
        {
            if (gameObject.tag == "Archer")
            {
                CommonPropeties.coin -= CommonPropeties.coinOfArcher;
                moneyDeducted = true; // set the flag to true to prevent multiple deductions
            }
            else if (gameObject.tag == "Cowboy")
            {
                CommonPropeties.coin -= CommonPropeties.coinOfCowboy;
                moneyDeducted = true; // set the flag to true to prevent multiple deductions
            }
            else if (gameObject.tag == "Wizard")
            {
                CommonPropeties.coin -= CommonPropeties.coinOfWizard;
                moneyDeducted = true; // set the flag to true to prevent multiple deductions
            }
            else if (gameObject.tag == "Tank")
            {
                CommonPropeties.coin -= CommonPropeties.coinOfTank;
                moneyDeducted = true; // set the flag to true to prevent multiple deductions
            }
        }
    }
    public void clickNo()
    {
        Time.timeScale = 1;
        Canvas.gameObject.SetActive(false);
    }

    void Update()
    {
        // check if the player is sleeping
        if (isSleeping)
        {
            // check if the player is clicked on
            if (Input.GetMouseButton(0))
            {
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // check if the click is on the player
                if (GetComponent<Collider2D>().OverlapPoint(mouseWorldPosition))
                {
                    //Pause Game
                    Time.timeScale = 0;
                    // wake up the player if have enough money
                    if (gameObject.tag == "Archer")
                    {
                        if (CommonPropeties.coin >= CommonPropeties.coinOfArcher)
                        {
                            Canvas.gameObject.SetActive(true);
                        }
                        else
                        {
                            Time.timeScale = 1;
                        }
                    }
                    else if (gameObject.tag == "Cowboy")
                    {
                        if (CommonPropeties.coin >= CommonPropeties.coinOfCowboy)
                        {
                            Canvas.gameObject.SetActive(true);
                        }
                        else
                        {
                            Time.timeScale = 1;
                        }
                    }
                    else if (gameObject.tag == "Wizard")
                    {
                        if (CommonPropeties.coin >= CommonPropeties.coinOfWizard)
                        {
                            Canvas.gameObject.SetActive(true);
                        }
                        else
                        {
                            Time.timeScale = 1;
                        }
                    }
                    else if (gameObject.tag == "Tank")
                    {
                        if (CommonPropeties.coin >= CommonPropeties.coinOfTank)
                        {
                            Canvas.gameObject.SetActive(true);
                        }
                        else
                        {
                            Time.timeScale = 1;
                        }
                    }
                    yesOption.onClick.AddListener(clickYes);
                    noOption.onClick.AddListener(clickNo);
                }
            }
        }
        else // the player is awake and can move
        {
            // check if the player is trying to move
            if (Input.GetMouseButton(0))
            {
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // check if the click is on the player
                if (GetComponent<Collider2D>().OverlapPoint(mouseWorldPosition))
                {
                    SelectedPlayer = this;
                }
                // round the position to the nearest tile
                Vector3Int tilePosition = mountainTilemap.WorldToCell(mouseWorldPosition);
                Vector3 targetWorldPosition = mountainTilemap.CellToWorld(tilePosition);

                // check if the target tile is walkable

                if (mountainTilemap.GetTile(tilePosition) != null)
                {
                    // set the target position to the center of the tile
                    targetPosition = new Vector3(targetWorldPosition.x + 0.5f, targetWorldPosition.y + 0.5f, 0f);
                }

            }
            // move the player towards the target position
            if (SelectedPlayer == this)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        }
    }
}
