using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    private struct GroupSpawnData
    {
        public EnemyBoat.EnemyType EnemyType;
        public SplineContainer Spline;
        public int GroupSize;

        public GroupSpawnData(EnemyBoat.EnemyType enemyType, SplineContainer spline, int groupSize)
        {
            EnemyType = enemyType;
            Spline = spline;
            GroupSize = groupSize;
        }
    }

    private const float ADD_POINT_EACH_SECONDS_INTERVAL = 5f;
    private const float SPAWN_PROBABILITY = 0.5f;
    private const int POINTS_ADDED_PER_TIME_INTERVAL = 100;

    [SerializeField] private EnemyDictionary enemyDictionary;
    [SerializeField] private GroupDictionary groupDictionary;

    [SerializeField] private List<Transform> spawnPoints = new();
    [SerializeField] private List<SplineContainer> splines = new();
    [SerializeField] private List<GroupSpawnData> groupsSpawnData = new();


    [SerializeField] private List<SplineContainer> earlySplines = new();
    [SerializeField] private List<SplineContainer> middleSplines = new();
    [SerializeField] private List<SplineContainer> lateSplines = new();
    [SerializeField] private GameObject map;

    [SerializeField] private int points;
    private float elapsedTime;
    private bool isSpawningGroups = false;


    private void Start()
    {
        elapsedTime = 0;
        splines.Clear();
        splines.AddRange(earlySplines);
    }

    private void Update()
    {
        ActivateCurrentSplines();
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= ADD_POINT_EACH_SECONDS_INTERVAL)
        {
            elapsedTime = 0;
            points += POINTS_ADDED_PER_TIME_INTERVAL;
        }

        if (points >= 3)
        {
            StartCoroutine(CheckGroupSpawns());
        }

        NormalSpawns();
        if (!isSpawningGroups)
            StartCoroutine(GroupSpawns());

    }

    private void ActivateCurrentSplines()
    {
        float yPos = map.transform.position.y;
        if (yPos < -20)
        {
            splines = new List<SplineContainer>();
            splines.AddRange(middleSplines);
        }

        if (yPos < -20)
        {
            splines = new List<SplineContainer>();
            splines.AddRange(middleSplines);
        }
        if (yPos < -80)
        {
            splines = new List<SplineContainer>();
            splines.AddRange(lateSplines);
        }
    }

    private IEnumerator GroupSpawns()
    {
        isSpawningGroups = true;
        for (int i = 0; i < groupsSpawnData.Count;)
        {
            GroupSpawnData groupSpawnData = groupsSpawnData[i];
            for (int j = 0; j < groupSpawnData.GroupSize; j++)
            {
                EnemyBoat enemyBoat = Instantiate(enemyDictionary.enemyCosts.First(ec =>
                {
                    return ec.enemyType == groupSpawnData.EnemyType;
                }).enemy,
                groupSpawnData.Spline.Spline.Knots.ToList()[0].Position,
                Quaternion.identity);

                Debug.Log($"{enemyBoat.name} : {enemyBoat.transform.position}");
                InitiateSplineEnemy(enemyBoat);
                yield return new WaitForSeconds(1);
            }
            groupsSpawnData.Remove(groupSpawnData);
        }
        isSpawningGroups = false;
        yield return null;
    }

    private void NormalSpawns()
    {
        List<EnemyCost> trimmedCosts = enemyDictionary.enemyCosts.Where(ec => ec.cost <= points).ToList();

        if (trimmedCosts.Count == 0)
        {
            return;
        }

        EnemyBoat.EnemyType randomType = (EnemyBoat.EnemyType)Random.Range(0, trimmedCosts.Count);


        float randomProb = Random.Range(0f, 1f);
        if (randomProb <= SPAWN_PROBABILITY)
        {
            return;
        }
        int totalCost = enemyDictionary.enemyCosts.First(kvp => kvp.enemyType == randomType).cost;

        if (totalCost <= points)
        {
            EnemyBoat enemy = Instantiate(enemyDictionary.enemyCosts.First(ec => ec.enemyType == randomType).enemy);
            InitiateSplineEnemy(enemy);
            points -= totalCost;
        }
        return;
    }

    private void InitiateSplineEnemy(EnemyBoat enemy)
    {
        int randomIndex = Random.Range(0, splines.Count);
        SplineContainer spline = splines[randomIndex];
        enemy.transform.position = spline.transform.TransformPoint(spline.Spline.Knots.ToList()[0].Position);
        enemy.SetBehaviour(EnemyBoat.BehaviourType.Path);
        enemy.SplineAnimator.Container = spline;
    }

    private IEnumerator CheckGroupSpawns()
    {
        int tries = 0;
        while (points != 0 || tries >= 10)
        {

            List<EnemyCost> trimmedCosts = enemyDictionary.enemyCosts.Where(ec => ec.cost <= points).ToList();

            if (trimmedCosts.Count == 0)
            {
                yield return null;
            }

            EnemyBoat.EnemyType randomType = (EnemyBoat.EnemyType)Random.Range(0, trimmedCosts.Count);

            GroupSizeByEnemyType groupSizeByEnemyType = groupDictionary.groupSizesByEnemyType.First(kvp =>
            {
                return kvp.EnemyType == randomType;
            });

            float randomProb = Random.Range(0f, 1f);
            if (randomProb <= groupSizeByEnemyType.Probability)
            {
                yield return null;
            }

            int randomGroupSize = Random.Range(groupSizeByEnemyType.MinEnemyCount, groupSizeByEnemyType.MaxEnemyCount);

            int totalPoints = enemyDictionary.enemyCosts.First(kvp => kvp.enemyType == (EnemyBoat.EnemyType)randomType).cost * randomGroupSize;

            if (totalPoints <= points)
            {
                int randomIndex = Random.Range(0, splines.Count);
                SplineContainer spline = splines[randomIndex];
                groupsSpawnData.Add(new GroupSpawnData(randomType, spline, randomGroupSize));
                points -= totalPoints;
            }
            else
            {
                tries++;
            }
        }
        yield return null;
    }
}
