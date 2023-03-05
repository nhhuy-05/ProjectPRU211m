using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : MonoBehaviour
{

    public GameObject grid; // Reference to the Grid game object

    //---
    public GameObject archerPrefab;
    public GameObject cowboyPrefab;
    public GameObject wizardPrefab;
    public GameObject tankPrefab;

    // Start is called before the first frame update
    void Start()
    {
        int numArchers = 9;
        int numCowboys = 8;
        int numWizards = 4;
        int numTanks = 4;

        Vector2Int heroCellPosition; // The cell position of the hero on the grid
        List<Vector2Int> heroCellPositions = new List<Vector2Int>() {
            new Vector2Int(-3,1),
            new Vector2Int(-3,3),
            new Vector2Int(-3,5),
            new Vector2Int(-3,7),
            new Vector2Int(-3,9),
            new Vector2Int(-3,11),
            //--
            new Vector2Int(-3,-5),
            new Vector2Int(-3,-7),
            new Vector2Int(-5,-5),
            new Vector2Int(-5,-7),
            //--
            new Vector2Int(-12, -5),
            new Vector2Int(-12, -7),
            new Vector2Int(-12, -9),
            new Vector2Int(-12, -11),
            new Vector2Int(-12, -13),
            new Vector2Int(-12, -15),
            new Vector2Int(-12, -17),
            //--
            new Vector2Int(5,5),
            new Vector2Int(7,5),
            new Vector2Int(9,5),
            new Vector2Int(11,5),
            //--
            new Vector2Int(13,1),
            new Vector2Int(13,3),
            new Vector2Int(13,5),
            //--
            new Vector2Int(-3,-5),
            new Vector2Int(-3,-7),
        };
        Vector3 cellSize = grid.GetComponent<Grid>().cellSize;

        List<Vector2Int> temp = new List<Vector2Int>();

        for (int i = 0; i < numArchers; i++)
        {
            // Get position for hero
            heroCellPosition = heroCellPositions[i];


            GameObject archer = Instantiate<GameObject>(archerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            // Calculate the position of the hero in world space based on its cell position and the cell size
            Vector3 heroWorldPosition = grid.transform.position + new Vector3(heroCellPosition.x * cellSize.x, heroCellPosition.y * cellSize.y, 0);
            archer.transform.position = heroWorldPosition;

            temp.Add(heroCellPosition);

        }

        foreach (var item in temp)
        {
            heroCellPositions.Remove(item);
        }


        temp.Clear();
        for (int i = 0; i < numCowboys; i++)
        {
            // Get position for hero
            heroCellPosition = heroCellPositions[i];

            GameObject cowboy = Instantiate(cowboyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            // Calculate the position of the hero in world space based on its cell position and the cell size
            Vector3 heroWorldPosition = grid.transform.position + new Vector3(heroCellPosition.x * cellSize.x, heroCellPosition.y * cellSize.y, 0);
            cowboy.transform.position = heroWorldPosition;


            temp.Add(heroCellPosition);
        }

        foreach (var item in temp)
        {
            heroCellPositions.Remove(item);
        }

        temp.Clear();
        for (int i = 0; i < numWizards; i++)
        {
            // Get position for hero
            heroCellPosition = heroCellPositions[i];
            GameObject wizard = Instantiate(wizardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            // Calculate the position of the hero in world space based on its cell position and the cell size
            Vector3 heroWorldPosition = grid.transform.position + new Vector3(heroCellPosition.x * cellSize.x, heroCellPosition.y * cellSize.y, 0);
            wizard.transform.position = heroWorldPosition;

            temp.Add(heroCellPosition);
        }

        foreach (var item in temp)
        {
            heroCellPositions.Remove(item);
        }

        temp.Clear();
        for (int i = 0; i < numTanks; i++)
        {
            // Get position for hero
            heroCellPosition = heroCellPositions[i];

            GameObject tank = Instantiate(tankPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            // Calculate the position of the hero in world space based on its cell position and the cell size
            Vector3 heroWorldPosition = grid.transform.position + new Vector3(heroCellPosition.x * cellSize.x, heroCellPosition.y * cellSize.y, 0);
            tank.transform.position = heroWorldPosition;

            temp.Add(heroCellPosition);
        }

        foreach (var item in temp)
        {
            heroCellPositions.Remove(item);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
