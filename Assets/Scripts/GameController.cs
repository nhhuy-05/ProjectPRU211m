using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    Tilemap mountainTilemap; // reference to the "Mountain" tilemap

    //---
    public GameObject archerPrefab;
    public GameObject cowboyPrefab;
    public GameObject wizardPrefab;
    public GameObject tankPrefab;

    //--
    Vector3 RandomPosition()    // on Mountain
    {
        // Generate a random X and Y position within the bounds of the tilemap
        int randomX = Random.Range(mountainTilemap.cellBounds.xMin, mountainTilemap.cellBounds.xMax);
        int randomY = Random.Range(mountainTilemap.cellBounds.yMin, mountainTilemap.cellBounds.yMax);

        // Convert the random X and Y positions to a cell position on the tilemap
        Vector3Int cellPosition = new Vector3Int(randomX, randomY, 0);

        // Convert the cell position to a world position
        Vector3 worldPosition = mountainTilemap.CellToWorld(cellPosition);

        return worldPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        int numArchers = 9;
        int numCowboys = 8;
        int numWizards = 4;
        int numTanks = 4;


        //--
        GameObject mountainGameObject = GameObject.Find("Mountain");
        mountainTilemap = mountainGameObject.GetComponent<Tilemap>();


        for (int i = 0; i < numArchers; i++)
        {

            GameObject archer = Instantiate<GameObject>(archerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            archer.transform.position = RandomPosition();


        }

        for (int i = 0; i < numCowboys; i++)
        {

            GameObject cowboy = Instantiate(cowboyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            cowboy.transform.position = RandomPosition();
        }

        for (int i = 0; i < numWizards; i++)
        {
            
            GameObject wizard = Instantiate(wizardPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            wizard.transform.position = RandomPosition();
        }

        for (int i = 0; i < numTanks; i++)
        {
            
            GameObject tank = Instantiate(tankPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            tank.transform.position = RandomPosition();
        }

    }

}
