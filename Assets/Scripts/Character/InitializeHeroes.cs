using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    // List of all the heroes and sleeping state of them
    List<Vector3> listHeroesPositon;
    List<bool> listHeroesSleeping;

    // Start is called before the first frame update
    void Start()
    {
        // Get list from json
        listHeroesPositon = SaveLoad.LoadHeroesPosition();
        Debug.Log("listHeroesPositon.Count: " + listHeroesPositon.Count);
        listHeroesSleeping = SaveLoad.LoadHeroesIsSleeping();
        Debug.Log("listHeroesSleeping.Count: " + listHeroesSleeping.Count);

        // Get the tilemap that represents the mountain area.
        mountainTilemap = GameObject.Find("Mountain").GetComponent<Tilemap>();

        // Get the valid positions inside the mountain area.
        List<Vector3Int> validTilePositions = GetValidPositions(mountainTilemap);

        // check if the list is empty, if it is, then initialize the heroes
        if (listHeroesPositon.Count == 0 || listHeroesSleeping.Count == 0)
        {
            // Create the archers.
            createHeroes(archerPrefab, CommonPropeties.numberArcher, validTilePositions);

            // Create the cowboys.
            createHeroes(cowboyPrefab, CommonPropeties.numberCowboy, validTilePositions);

            // Create the wizards.
            createHeroes(wizardPrefab, CommonPropeties.numberWizard, validTilePositions);

            // Create the tanks.
            createHeroes(tankPrefab, CommonPropeties.numberTank, validTilePositions);
        }
        else
        {
            // Create the cowboys.
            createHeroes(cowboyPrefab, 0, CommonPropeties.numberCowboy, listHeroesPositon, listHeroesSleeping);

            // Create the archers.
            createHeroes(archerPrefab, CommonPropeties.numberCowboy, CommonPropeties.numberCowboy + CommonPropeties.numberArcher, listHeroesPositon, listHeroesSleeping);

            // Create the tanks.
            createHeroes(tankPrefab, CommonPropeties.numberCowboy + CommonPropeties.numberArcher, CommonPropeties.numberCowboy + CommonPropeties.numberArcher + CommonPropeties.numberTank, listHeroesPositon, listHeroesSleeping);

            // Create the wizard.
            createHeroes(wizardPrefab, CommonPropeties.numberCowboy + CommonPropeties.numberArcher + CommonPropeties.numberTank, CommonPropeties.numberCowboy + CommonPropeties.numberArcher + CommonPropeties.numberTank + CommonPropeties.numberWizard, listHeroesPositon, listHeroesSleeping);
        }

    }

    public void createHeroes(GameObject prefab, int from, int to, List<Vector3> listHeroesPositon, List<bool> listHeroesSleeping)
    {
        for (int i = from; i < to; i++)
        {
            // Instantiate the hero.
            GameObject hero = Instantiate(prefab, listHeroesPositon.ElementAt(i), Quaternion.identity);

            // Get the sleeping effect of the hero.
            ParticleSystem sleepingEffect = hero.transform.Find("Body").gameObject.transform.Find("SleepingEffect").GetComponent<ParticleSystem>();

            // Check if the hero is sleeping.
            if (listHeroesSleeping.ElementAt(i))
            {
                sleepingEffect.Play();
            }
            else
            {
                sleepingEffect.Stop();
            }
            Debug.Log("listHeroesSleeping.ElementAt(i): " + listHeroesSleeping.ElementAt(i));
        }
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
}

