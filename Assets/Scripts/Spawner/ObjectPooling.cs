using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] GameObject flyingEyePrefab;
    [SerializeField] GameObject goblinPrefab;
    [SerializeField] GameObject mushroomPrefab;
    [SerializeField] GameObject skeletonPrefab;

    [SerializeField] GameObject whipPrefab;
    [SerializeField] GameObject biblePrefab;
    [SerializeField] GameObject axePrefab;
    [SerializeField] GameObject pigeonPrefab;
    [SerializeField] GameObject lightningPrefab;
    [SerializeField] GameObject magicWandPrefab;

    static ObjectPooling instance;
    Dictionary<string, Queue<GameObject>> poolingDict = new Dictionary<string, Queue<GameObject>>();
    const int initialNumber = 200;

    void Awake()
    {
        instance = this;
        Initialize();
    }

    void Initialize()
    {
        foreach(CharacterData.CharacterType characterType in Enum.GetValues(typeof(CharacterData.CharacterType)))
        {
            Queue<GameObject> newQue = new Queue<GameObject>();

            for (int j = 0; j < initialNumber; j++)
            {
                newQue.Enqueue(CreateObject(characterType));
            }

            poolingDict.Add(characterType.ToString(), newQue);
        }

        foreach (WeaponData.WeaponType weaponType in Enum.GetValues(typeof(WeaponData.WeaponType)))
        {
            Queue<GameObject> newQue = new Queue<GameObject>();

            for (int j = 0; j < initialNumber; j++)
            {
                newQue.Enqueue(CreateObject(weaponType));
            }

            poolingDict.Add(weaponType.ToString(), newQue);
        }
    }

    static GameObject CreateObject<T>(T type)
    {
        GameObject newEnemy;
        switch (type)
        {
            default:
            case CharacterData.CharacterType.FlyingEye:
                newEnemy = Instantiate(instance.flyingEyePrefab);
                break;
            case CharacterData.CharacterType.Goblin:
                newEnemy = Instantiate(instance.goblinPrefab);
                break;
            case CharacterData.CharacterType.Mushroom:
                newEnemy = Instantiate(instance.mushroomPrefab);
                break;
            case CharacterData.CharacterType.Skeleton:
                newEnemy = Instantiate(instance.skeletonPrefab);
                break;

            case WeaponData.WeaponType.Whip:
                newEnemy = Instantiate(instance.whipPrefab);
                break;
            case WeaponData.WeaponType.Bible:
                newEnemy = Instantiate(instance.biblePrefab);
                break;
            case WeaponData.WeaponType.Axe:
                newEnemy = Instantiate(instance.axePrefab);
                break;
            case WeaponData.WeaponType.Pigeon:
                newEnemy = Instantiate(instance.pigeonPrefab);
                break;
            case WeaponData.WeaponType.Lightning:
                newEnemy = Instantiate(instance.lightningPrefab);
                break;
            case WeaponData.WeaponType.MagicWand:
                newEnemy = Instantiate(instance.magicWandPrefab);
                break;
        }

        newEnemy.transform.parent = instance.transform;
        newEnemy.SetActive(false);

        return newEnemy;
    }

    public static GameObject GetObject<T>(T type)
    {
        if (instance.poolingDict[type.ToString()].Count > 0)
        {
            return instance.poolingDict[type.ToString()].Dequeue();
        }
        else
        {
            return CreateObject(type);
        }
    }

    public static void ReturnObject<T>(GameObject deadEnemy, T type)
    {
        instance.poolingDict[type.ToString()].Enqueue(deadEnemy);
    }
}
