using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to store common properties for Game 
/// </summary>
public class CommonPropeties
{
    // Common properties
    #region Common Properties
    public static int coin { get; set; } = 500; // Coin
    public static int healthOfVillage { get; set; } = 100; // Health of Village
    public static int highestScore { get; set; } = 0; // Highest score
    public static int score { get; set; } = 0; // Score
    public static int currentScore { get; set; } = 0; // Current score
    public static int currentWave { get; set; } = 1; // Current wave
    public static int currentRound { get; set; } = 1; // Current round
    public static float timeToSpawn { get; set; } = 0.5f; //Time to spawn each enemy 
    #endregion
    // Score of each enemy
    #region Score of enemy
    public static int scoreOfBoss { get; set; } = 50;
    public static int scoreOfEyes { get; set; } = 20;
    public static int scoreOfGoblin { get; set; } = 30;
    public static int scoreOfMushroom { get; set; } = 20;
    public static int scoreOfSkeleton { get; set; } = 20;
    #endregion

    // Speed of the enemies
    #region Speed Of enemies
    public static float speedOfBoss { get; set; } = 5f; // Speed of Boss
    public static float speedOfEyes { get; set; } = 4f; // Speed of Eyes
    public static float speedOfMushroom { get; set; } = 1f; // Speed of Mushroom
    public static float speedOfSkeleton { get; set; } = 3f; // Speed of Skeleton
    public static float speedOfGoblin { get; set; } = 2f; // Speed of Goblin
    #endregion

    // Damage of Weapons on Enemies
    #region Damage of Heroes
    public static int damageOfArrow { get; set; } = 25; // Damage of Arrow
    public static int damageOfBullet { get; set; } = 30; // Damage of Bullet
    public static int damageOfRoundShot { get; set; } = 40; // Damage of RoundShot
    public static int damageOfMagicBall { get; set; } = 5; // Damage of MagicBall
    #endregion

    // Damage of Enemies on Village
    #region Damage of Enemies
    public static int damageOfBoss { get; set; } = 4; // Damage of Boss on Village
    public static int damageOfEyes { get; set; } = 2; // Damage of Eyes on Village
    public static int damageOfMushroom { get; set; } = 2; // Damage of Mushroom on Village
    public static int damageOfSkeleton { get; set; } = 3; // Damage of Skeleton on Village
    public static int damageOfGoblin { get; set; } = 3; // Damage of Goblin on Village
    #endregion

    // Health of Enemies
    #region Health of Enemies
    public static int healthOfBoss { get; set; } = 300; // Health of Boss
    public static int healthOfEyes { get; set; } = 75; // Health of Eyes
    public static int healthOfMushroom { get; set; } = 150; // Health of Mushroom
    public static int healthOfSkeleton { get; set; } = 200; // Health of Skeleton
    public static int healthOfGoblin { get; set; } = 100; // Health of Goblin
    #endregion

    // FireRate of Heroes
    #region FireRate of Heroes
    public static float fireRateOfArcher { get; set; } = 0.5f; // FireRate of Archer
    public static float fireRateOfCowboy { get; set; } = 0.5f; // FireRate of Cowboy
    public static float fireRateOfWizard { get; set; } = 2f; // FireRate of Wizard
    public static float fireRateOfTank { get; set; } = 4f; // FireRate of Tank
    #endregion

    // Number Instantiate Enemies
    #region Number Instantiate Enemies
    public static int numberInstantiateBoss { get; set; } = 1; // Number Instantiate Boss
    public static int numberInstantiateSkeleton { get; set; } = 1; // Number Instantiate Skeleton
    public static int numberInstantiateEyes { get; set; } = 1; // Number Instantiate Eyes
    public static int numberInstantiateMushroom { get; set; } = 1; // Number Instantiate Mushroom
    public static int numberInstantiateGoblin { get; set; } = 1; // Number Instantiate Goblin
    #endregion

    // Number of Heroes
    #region Number of Heroes
    public static int numberArcher { get; set; } = 9; // Number of Archer
    public static int numberCowboy { get; set; } = 8; // Number of Cowboy
    public static int numberWizard { get; set; } = 4; // Number of Wizard
    public static int numberTank { get; set; } = 4; // Number of Tank
    #endregion

    // Coin of Enemies when they die
    #region Coin of Enemies
    public static int coinOfBoss { get; set; } = 20; // Coin of Boss when they die
    public static int coinOfEyes { get; set; } = 5; // Coin of Eyes when they die
    public static int coinOfMushroom { get; set; } = 5; // Coin of Mushroom when they die
    public static int coinOfSkeleton { get; set; } = 4; // Coin of Skeleton when they die
    public static int coinOfGoblin { get; set; } = 3; // Coin of Goblin when they die
    #endregion

    // Coin of Heroes when buy them
    #region Coin to buy Heroes
    public static int coinOfArcher { get; set; } = 125; // Coin of Archer when buy them
    public static int coinOfCowboy { get; set; } = 125; // Coin of Cowboy when buy them
    public static int coinOfWizard { get; set; } = 150; // Coin of Wizard when buy them
    public static int coinOfTank { get; set; } = 175; // Coin of Tank when buy them
    #endregion

    //--
    #region number Enemies first wave
    public static int maxNumberOfBossFirstWave { get; set; } = 2;
    public static int maxNumberOfEyesFirstWave { get; set; } = 7;
    public static int maxNumberOfGoblinFirstWave { get; set; } = 8;
    public static int maxNumberOfMushroomFirstWave { get; set; } = 9;
    public static int maxNumberOfSkeletonFirstWave { get; set; } = 10;
    #endregion
}
