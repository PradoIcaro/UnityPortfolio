using System;
using System.Collections;
using System.Collections.Generic;
//using Boo.Lang.Environments;
using UnityEngine;
//using UnityEngine.Timeline;
using Random = UnityEngine.Random;
public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        private int m_minimum;
        private int m_maximum;

        public int Minimum
        {
            get => m_minimum;
            private set => m_minimum = value;
        }
        public int Maximum
        {
            get => m_maximum;
            private set => m_maximum = value;
        }

        public Count(int min, int max)
        {
            Minimum = min;
            Maximum = max;
        }
    }

    private int m_columns = 8;
    private int m_rows = 8;
    private Count m_wallCount = new Count(5,9);
    private Count m_foodCount = new Count(1,5);
    private GameObject m_exit;
    private GameObject[] m_floorTiles;
    private GameObject[] m_wallTiles;
    private GameObject[] m_foodTiles;
    private GameObject[] m_enemyTiles;
    private GameObject[] m_outerWallTiles;

    private Transform m_boardHolder;
    private List<Vector3> m_gridPositions =  new List<Vector3>();

    void InitializeList()
    {
        m_gridPositions.Clear();

        for (int x = 1; x < m_columns - 1; x++)
        {
            for (int y = 1; y < m_rows - 1; y++)
            {
                m_gridPositions.Add(new Vector3(x, y, 0f));
            }

        }
    }

    void BoardSetup()
    {
        m_boardHolder = new GameObject("Board").transform;
        for (int x = -1; x < m_columns + 1; x++)
        {
            for (int y = -1; y < m_rows + 1; y++)
            {
                GameObject toInstantiate = m_floorTiles[Random.Range(0,m_floorTiles.Length)];
                if (x == -1 || (x == m_columns) || y == -1 || y == m_rows)
                {
                    toInstantiate = m_outerWallTiles[Random.Range(0, m_outerWallTiles.Length)];                    
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x,y,0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(m_boardHolder);
            }

        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0,m_gridPositions.Count);
        Vector3 randomPosition = m_gridPositions[randomIndex];
        m_gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3  randomPosition =  RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0,tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupScene(int level)
    {
        BoardSetup();
        InitializeList();
        LayoutObjectAtRandom(m_wallTiles, m_wallCount.Minimum, m_wallCount.Maximum);
        LayoutObjectAtRandom(m_foodTiles, m_foodCount.Minimum, m_foodCount.Maximum);
        int enemyCount = (int)Mathf.Log(level, 2f);
        LayoutObjectAtRandom(m_enemyTiles, enemyCount, enemyCount);
        Instantiate(m_exit, new Vector3(m_columns - 1, m_rows - 1, 0f), Quaternion.identity);
    }
}
