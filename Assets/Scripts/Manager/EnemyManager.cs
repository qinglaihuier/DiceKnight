using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 敌人管理器（刷新敌人）
/// </summary>
public class EnemyManager : SingletonMono<EnemyManager>
{
    [SerializeField] private TextAsset waveInfo, waveEnemy;//波次信息

    private string[] waveLine, enemyLine; 

    private int waveTime = 0;//当前波次持续的时长

    public int timer = 0;//计时器

    public int weight;//当前波次的总权值

    public int wave = 0;//波次

    public GameObject[] enemyPrefabs;//敌人预制体

    public int spawnInterval;//平时刷新时间间隔

    private float leftBound;

    private float rightBound;

    private float topBound;

    private float bottomBound;

    public List<int> enemyType = new List<int>();

    private void Start() 
    {
        leftBound = transform.position.x - transform.localScale.x / 2;
        rightBound = transform.position.x + transform.localScale.x / 2;
        topBound = transform.position.y + transform.localScale.y / 2;
        bottomBound = transform.position.y - transform.localScale.y / 2;

        waveLine = waveInfo.text.Split('\n');
        enemyLine = waveEnemy.text.Split('\n');

        StartCoroutine(Timer());    
         
    }

    private void Update() 
    {
        if(timer == waveTime)
        {
            UpdateWave();
        }    
    }

    IEnumerator Timer()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            timer++;
        }
    }

    //更新波次
    private void UpdateWave()
    {
        wave++;
        if(wave <= 16)
        {
            string[] cell = waveLine[wave].Split(',');
            waveTime += int.Parse(cell[1]);
            weight = int.Parse(cell[2]);
            enemyType.Clear();
            if(cell[9] == "A")
            {
                for(int i = 0;i < int.Parse(cell[3]);i++)
                {
                    enemyType.Add(int.Parse(cell[i + 4]));
                }
            }
            else if(cell[9] == "R")
            {
                for(int i = 0;i < int.Parse(cell[3]);i++)
                {
                    int x = Random.Range(0, 5);
                    while(enemyType.Contains(x))
                    {
                        x = Random.Range(0, 5);
                    }
                    enemyType.Add(x);
                }
            }
            spawnInterval = int.Parse(cell[1]) / weight;
        }
        else
        {
            waveTime += 60;
            enemyType.Clear();
            if(wave % 3 == 0)
            {
                weight = 40;
            }
            else
            {
                weight = 45;
            }
            for(int i = 0;i < 3;i++)
            {
                int x = Random.Range(0, 5);
                while(enemyType.Contains(x))
                {
                    x = Random.Range(0, 5);
                }
                enemyType.Add(x);
            }
            spawnInterval = 60 / weight;
        }
        SpawnWaveEnemy();
        StartCoroutine(SpawnNormalEnemy());
    }

    //生成当前波次的通常敌人
    IEnumerator SpawnNormalEnemy()
    {
        int curWeight = 0;
        int min = int.MaxValue;
        List<int> n = new List<int>();
        for(int i = 0;i < enemyType.Count;i++)
        {
            n.Add(0);
            if(enemyPrefabs[enemyType[i]].GetComponent<EnemyBase>().weight < min)
            {
                min = enemyPrefabs[enemyType[i]].GetComponent<EnemyBase>().weight;
            }
        }
        int j = 0;
        int max = enemyType.Count;
        while(curWeight < weight && (weight - curWeight) > min)
        {
            n[j]++;
            for(int i = 0;i < enemyType.Count;i++)
            {
                if(n[i] == enemyPrefabs[enemyType[i]].GetComponent<EnemyBase>().weight)
                {
                    SpawnEnemy(enemyType[i]);
                    curWeight += enemyPrefabs[enemyType[i]].GetComponent<EnemyBase>().weight;
                    n[i] = 0;
                }
            }
            if(j < max - 1)
            {
                j++;
            }
            else
            {
                j = 0; 
            }
            yield return new WaitForSeconds(spawnInterval);
        }
        yield break;
    }

    //生成当前波次的敌人
    private void SpawnWaveEnemy()
    {
        string[] cell = enemyLine[wave].Split(','); 

        if(wave >= 16 && wave % 3 != 0)
        {
            List<int> type = new List<int>();
            for(int i = 0;i < 5;i++)
            {
                type.Add(i);
            }
            int min = 1;
            int curWeight = 15;
            int a = 0;
            while(a < curWeight && (curWeight - a) > min)
            {
                int b = Random.Range(0, type.Count);
                SpawnEnemy(type[b]);
                a += enemyPrefabs[type[b]].GetComponent<EnemyBase>().weight;
            }
        }
        else if(wave >= 16 && wave % 3 == 0)
        {
            SpawnEnemy(5);
        }
        else if(cell[0] == "A")
        {
            for(int i = 0;i < int.Parse(cell[1]);i++)
            {
                for(int j = 0;j < int.Parse(cell[3 + i * 2]);j++)
                {
                    SpawnEnemy(int.Parse(cell[2 + i * 2]));
                }
            }
        }
        else if(cell[0] == "R")
        {
            List<int> type = new List<int>();
            for(int i = 0;i < int.Parse(cell[1]);i++)
            {
                int x = Random.Range(0, 5);
                while(type.Contains(x))
                {
                    x = Random.Range(0, 5);
                }
                type.Add(x);
            }
            int min = int.MaxValue;
            for(int i = 0;i < type.Count;i++)
            {
                if(enemyPrefabs[type[i]].GetComponent<EnemyBase>().weight < min)
                {
                    min = enemyPrefabs[type[i]].GetComponent<EnemyBase>().weight;
                }
            }
            int curWeight = int.Parse(cell[2]);
            int a = 0;
            while(a < curWeight && (curWeight - a) > min)
            {
                int b = Random.Range(0, type.Count);
                SpawnEnemy(type[b]);
                a += enemyPrefabs[type[b]].GetComponent<EnemyBase>().weight;
            }
        }
    }

    //生成敌人
    private void SpawnEnemy(int id)
    {
        Vector2 pos = GetRandomPosition();
        GameObject enemy = ObjectPool.GetInstance().GetGameObject(enemyPrefabs[id]);
        enemy.transform.position = pos;
    }

    //获取随机位置
    private Vector2 GetRandomPosition()
    {
        Vector2 pos;
        float randomX = Random.Range(leftBound, rightBound);
        float randomY = Random.Range(bottomBound, topBound);
        pos = new Vector2(randomX, randomY);
        while(IsPositionInsideCamera(pos))
        {
            randomX = Random.Range(leftBound, rightBound);
            randomY = Random.Range(bottomBound, topBound);
            pos = new Vector2(randomX, randomY);
        }
        return pos;
    }

    //判断是否在摄像机范围内
    private bool IsPositionInsideCamera(Vector2 position)
    {
        Vector2 pos = Camera.main.WorldToViewportPoint(position);

        return pos.x >= 0 && pos.x <= 1 && pos.y >= 0 && pos.y <= 1; 
    }
}
