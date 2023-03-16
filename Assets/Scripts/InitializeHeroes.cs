using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

/// <summary>
/// This class is responsible for initializing the heroes inside the mountain area.
/// </summary>
public class InitializeHeroes : MonoBehaviour
{
    // Fields for the heroes
    [SerializeField]
    private GameObject archerPrefab;
    [SerializeField]
    private GameObject cowboyPrefab;
    [SerializeField]
    private GameObject wizardPrefab;
    [SerializeField]
    private GameObject tankPrefab;

    // The tilemap that represents the mountain area.
    Tilemap mountainTilemap;

    // Start is called before the first frame update
    void Start()
    {
        // Get the tilemap that represents the mountain area.
        mountainTilemap = GameObject.Find("Mountain").GetComponent<Tilemap>();

        // Get the valid positions inside the mountain area.
        List<Vector3Int> validTilePositions = GetValidPositions(mountainTilemap);

        // Create the archers.
        createHeroes(archerPrefab, 9, validTilePositions);

        // Create the cowboys.
        createHeroes(cowboyPrefab, 8, validTilePositions);

        // Create the wizards.
        createHeroes(wizardPrefab, 4, validTilePositions);

        // Create the tanks.
        createHeroes(tankPrefab, 4, validTilePositions);
    }

    // method get a valid position inside the mountain area.
    private List<Vector3Int> GetValidPositions(Tilemap tilemap)
    {
        List<Vector3Int> validPositions = new List<Vector3Int>();

        foreach (Vector3Int position in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(position))
            {
                validPositions.Add(position);
            }
        }

        return validPositions;
    }

    // method random create Heroes
    private void createHeroes(GameObject prefabs, int numberOfHeroes, List<Vector3Int> validTilePositions)
    {
        for (int i = 0; i < numberOfHeroes; i++)
        {
            // Get a random position inside the mountain area.
            int randomIndex = Random.Range(0, validTilePositions.Count);
            Vector3Int randomTilePosition = validTilePositions[randomIndex];

            // Remove that position and 8 positions around that
            validTilePositions.RemoveAt(randomIndex);
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Vector3Int tilePosition = new Vector3Int(randomTilePosition.x + x, randomTilePosition.y + y, 0);
                    validTilePositions.Remove(tilePosition);
                }
            }

            // Instantiate the tank hero prefab at the random position
            GameObject gameObject = Instantiate(prefabs, mountainTilemap.CellToWorld(randomTilePosition), Quaternion.identity);

            // Set the tank's z position to 0 to ensure it is on top of the tilemap
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y + 0.5f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}

