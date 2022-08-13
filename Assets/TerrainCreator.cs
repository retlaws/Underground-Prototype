using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCreator : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float cubeSize = 1;
    [Tooltip("X = width, Y = height, Z = length")]
    [SerializeField] Vector3 numberOfCubes = new Vector3(100, 100, 50);
    [SerializeField] GameObject terrainCube;


    private void Start()
    {
        CreateTerrain();
    }

    private void CreateTerrain()
    {
        float terrainWidth = numberOfCubes.x * cubeSize;
        float terrainHeight = numberOfCubes.y * cubeSize;
        float terrainLength = numberOfCubes.z * cubeSize;


        float startingWidth = 0 - ((terrainWidth / 2) - cubeSize / 2);
        float startingLength = 0 - ((terrainLength / 2) - cubeSize / 2);
        float startingHeight = 0 - (terrainHeight - (cubeSize / 2));

        Vector3 currentLocation = new Vector3(startingWidth, startingHeight, startingLength);

        float currentVerticalRow = 0;
        float currentRow = 0f;

        for (int i = 0; i < numberOfCubes.y; i++)
        {
            float currentHeightRow = startingHeight + (cubeSize * currentVerticalRow);
            currentVerticalRow++;
            currentRow = 0; 

            for (int j = 0; j < numberOfCubes.x; j++)
            {
                float currentWidthRow = startingWidth + (cubeSize * currentRow);
                currentLocation = new Vector3(currentWidthRow, currentHeightRow, startingLength);
                currentRow++;

                for (int k = 0; k < numberOfCubes.z; k++)
                {
                    Instantiate(terrainCube, currentLocation, Quaternion.identity);
                    currentLocation = new Vector3(currentWidthRow, currentLocation.y, currentLocation.z + cubeSize); 
                }
            }
        }

    }
}
