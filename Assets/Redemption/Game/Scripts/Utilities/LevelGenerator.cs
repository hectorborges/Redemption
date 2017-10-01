﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject[] walls;

    public List<Vector3> createdTileLocations;
    public List<GameObject> createdTiles;
    public List<Vector3> wallLocations;

    public int tileAmount;
    public float tileSize;
    public float waitTime;

    public float chanceUp;
    public float chanceDown;
    public float chanceLeft;

    public float minX = 999999999f;
    public float maxX;
    public float minZ = 999999999f;
    public float maxZ;

    public float xAmount;
    public float zAmount;
    public float extraWallX;
    public float extraWallZ;

    public delegate void GenerationEvents(List<GameObject> objects);
    public static event GenerationEvents OnFloorCompleted;
    public static event GenerationEvents OnWallsCompleted;

    IEnumerator Start()
    {
        // Random.seed = 10;
        for (int i = 0; i < tileAmount; i++)
        {
            float direction = Random.Range(0f, 1f);
            int randomTile = Random.Range(0, tiles.Length);
            CreateTile(randomTile);
            CallMoveGenerator(direction);
            yield return new WaitForSeconds(waitTime);

            if (i == tileAmount - 1)
            {
                Finish();
            }
        }
    }

    void MoveGenerator(int direction)
    {
        switch (direction)
        {
            case 0:
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + tileSize);
                break;
            case 1:
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - tileSize);
                break;
            case 2:
                transform.position = new Vector3(transform.position.x + tileSize, transform.position.y, transform.position.z);
                break;
            case 3:
                transform.position = new Vector3(transform.position.x - tileSize, transform.position.y, transform.position.z);
                break;
        }
    }

    void CallMoveGenerator(float randomDirection)
    {
        if (randomDirection < chanceUp)
            MoveGenerator(0);
        else if (randomDirection < chanceDown)
            MoveGenerator(1);
        else if (randomDirection < chanceLeft)
            MoveGenerator(2);
        else
            MoveGenerator(3);
    }

    void CreateTile(int randomTile)
    {
        if (!createdTileLocations.Contains(transform.position))
        {
            GameObject tile = Instantiate(tiles[randomTile], transform.position, transform.rotation) as GameObject;
            createdTileLocations.Add(tile.transform.position);
            createdTiles.Add(tile);
        }
        else
            tileAmount++;
    }

    void Finish()
    {
        OnFloorCompleted(createdTiles);
        CreateWallValues();
        StartCoroutine(CreateWalls());
    }

    void CreateWallValues()
    {
        for (int i = 0; i < createdTileLocations.Count; i++)
        {
            if (createdTileLocations[i].x < minX)
            {
                minX = createdTileLocations[i].x;
            }

            if (createdTileLocations[i].x > maxX)
            {
                maxX = createdTileLocations[i].x;
            }

            if (createdTileLocations[i].z < minZ)
            {
                minZ = createdTileLocations[i].z;
            }

            if (createdTileLocations[i].z > maxZ)
            {
                maxZ = createdTileLocations[i].z;
            }

            xAmount = ((maxX - minX) / tileSize) + extraWallX;
            zAmount = ((maxZ - minZ) / tileSize) + extraWallZ;
        }
    }

    IEnumerator CreateWalls()
    {
        for(int i = 0; i < createdTileLocations.Count; i++)
        {
            //Right Side
            if (!createdTileLocations.Contains(createdTileLocations[i] + new Vector3(tileSize, 0, 0)))
                if (!wallLocations.Contains(createdTileLocations[i] + new Vector3(tileSize, 0, 0)))
                    wallLocations.Add(createdTileLocations[i] + new Vector3(tileSize, 0, 0));

            //Left Side
            if (!createdTileLocations.Contains(createdTileLocations[i] + new Vector3(-tileSize, 0, 0)))
                if (!wallLocations.Contains(createdTileLocations[i] + new Vector3(-tileSize, 0, 0)))
                    wallLocations.Add(createdTileLocations[i] + new Vector3(-tileSize, 0, 0));


            //Forward Side
            if (!createdTileLocations.Contains(createdTileLocations[i] + new Vector3(0, 0, tileSize)))
                if (!wallLocations.Contains(createdTileLocations[i] + new Vector3(0, 0, tileSize)))
                    wallLocations.Add(createdTileLocations[i] + new Vector3(0, 0, tileSize));

            //Backward Side
            if (!createdTileLocations.Contains(createdTileLocations[i] + new Vector3(0, 0, -tileSize)))
                if (!wallLocations.Contains(createdTileLocations[i] + new Vector3(0, 0, -tileSize)))
                    wallLocations.Add(createdTileLocations[i] + new Vector3(0, 0, -tileSize));

            //Forward Right Side
            if (!createdTileLocations.Contains(createdTileLocations[i] + new Vector3(tileSize, 0, tileSize)))
                if (!wallLocations.Contains(createdTileLocations[i] + new Vector3(tileSize, 0, tileSize)))
                    wallLocations.Add(createdTileLocations[i] + new Vector3(tileSize, 0, tileSize));

            //Forward Left Side
            if (!createdTileLocations.Contains(createdTileLocations[i] + new Vector3(-tileSize, 0, tileSize)))
                if (!wallLocations.Contains(createdTileLocations[i] + new Vector3(-tileSize, 0, tileSize)))
                    wallLocations.Add(createdTileLocations[i] + new Vector3(-tileSize, 0, tileSize));


            //Backward Right Side
            if (!createdTileLocations.Contains(createdTileLocations[i] + new Vector3(tileSize, 0, -tileSize)))
                if (!wallLocations.Contains(createdTileLocations[i] + new Vector3(tileSize, 0, -tileSize)))
                    wallLocations.Add(createdTileLocations[i] + new Vector3(tileSize, 0, -tileSize));

            //Backward Left Side
            if (!createdTileLocations.Contains(createdTileLocations[i] + new Vector3(-tileSize, 0, -tileSize)))
                if (!wallLocations.Contains(createdTileLocations[i] + new Vector3(-tileSize, 0, -tileSize)))
                    wallLocations.Add(createdTileLocations[i] + new Vector3(-tileSize, 0, -tileSize));
        }

        for(int j = 0; j < wallLocations.Count; j++)
        {
            Instantiate(walls[Random.Range(0, walls.Length)], wallLocations[j], Quaternion.identity);
            yield return new WaitForSeconds(waitTime);
        }

        List<GameObject> empty = new List<GameObject>();
       // OnWallsCompleted(empty);
    }
}
