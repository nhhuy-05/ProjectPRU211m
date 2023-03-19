using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used to store common properties for Game 
/// </summary>
public class CommonPropeties
{
    
    public static int coin { get; set; } = 200; // Coin
    public static int healthOfVillage { get; set; } = 100; // Health of Village

    // Speed of the enemies
    public static float speedOfBoss { get; set; } = 1f; // Speed of Boss
    public static float speedOfEyes { get; set; } = 1f; // Speed of Eyes
    public static float speedOfMushroom { get; set; } = 1f; // Speed of Mushroom
    public static float speedOfSkeleton { get; set; } = 1f; // Speed of Skeleton
    public static float speedOfGoblin { get; set; } = 1f; // Speed of Goblin

    // Damage of Weapons on Enemies
    public static int damageOfArrow { get; set; } = 20; // Damage of Arrow
    public static int damageOfBullet { get; set; } = 20; // Damage of Bullet
    public static int damageOfRoundShot { get; set; } = 50; // Damage of RoundShot
    public static int damageOfMagicBall { get; set; } = 5; // Damage of MagicBall

    // Damage of Enemies on Village
    public static int damageOfBoss { get; set; } = 5; // Damage of Boss on Village
    public static int damageOfEyes { get; set; } = 5; // Damage of Eyes on Village
    public static int damageOfMushroom { get; set; } = 5; // Damage of Mushroom on Village
    public static int damageOfSkeleton { get; set; } = 5; // Damage of Skeleton on Village
    public static int damageOfGoblin { get; set; } = 5; // Damage of Goblin on Village

    // Health of Enemies
    public static int healthOfBoss { get; set; } = 100; // Health of Boss
    public static int healthOfEyes { get; set; } = 100; // Health of Eyes
    public static int healthOfMushroom { get; set; } = 100; // Health of Mushroom
    public static int healthOfSkeleton { get; set; } = 100; // Health of Skeleton
    public static int healthOfGoblin { get; set; } = 100; // Health of Goblin

    // FireRate of Heroes
    public static float fireRateOfArcher { get; set; } = 0.5f; // FireRate of Archer
    public static float fireRateOfCowboy { get; set; } = 0.5f; // FireRate of Cowboy
    public static float fireRateOfWizard { get; set; } = 0.5f; // FireRate of Wizard
    public static float fireRateOfTank { get; set; } = 0.5f; // FireRate of Tank

    // Number Instantiate Enemies
    public static int numberInstantiateBoss { get; set; } = 1; // Number Instantiate Boss
    public static int numberInstantiateSkeleton { get; set; } = 1; // Number Instantiate Skeleton
    public static int numberInstantiateEyes { get; set; } = 1; // Number Instantiate Eyes
    public static int numberInstantiateMushroom { get; set; } = 1; // Number Instantiate Mushroom
    public static int numberInstantiateGoblin { get; set; } = 1; // Number Instantiate Goblin

    // Number of Heroes
    public static int numberArcher { get; set; } = 0; // Number of Archer
    public static int numberCowboy { get; set; } = 0; // Number of Cowboy
    public static int numberWizard { get; set; } = 0; // Number of Wizard
    public static int numberTank { get; set; } = 0; // Number of Tank
    
    
}
