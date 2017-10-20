using System.Collections;
using UnityEngine;

public class Level1 : MonoBehaviour, LevelInterface
{
    private GameController gameController;
    private SpawnController spawnController;
    private GameObject[] hazards;
    private GameObject boss;
    private int curWave;
    private bool isBoss;

    public float startWait;
    public float spawnWait;
    public float waveWait;
    public int hazardCount;

    private void Awake()
    {
        spawnController = gameObject.AddComponent<SpawnController>();
        gameController = GetComponent<GameController>();
        hazards = gameController.hazards;

        foreach(GameObject hazard in hazards)
        {
            if (!hazard.name.StartsWith("Asteroid"))
            {
                boss = hazard;
            }
        }

        isBoss = false;
        curWave = 0;
        startWait = 2;
        spawnWait = 0.5f;
        waveWait = 4;
        hazardCount = 20;
    }

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    public void PrepareWave()
    {
        gameController.restartText.text = "Wave " + curWave;

        switch (curWave)
        {
            case 1:
                spawnController.SetPoints("N");
                break;
            case 2:
                spawnController.SetPoints("E");
                break;
            case 3:
                spawnController.SetPoints("W");
                break;
            case 4:
                spawnController.SetPoints("E,W");
                break;
            case 5:
                spawnController.SetPoints("S");
                break;
            case 6:
                spawnController.SetPoints("N,S");
                break;
            case 7:
                spawnController.SetPoints("N,S,NE");
                break;
            case 8:
                spawnController.SetPoints("N,S,NE,NW");
                break;
            case 9:
                spawnController.SetPoints("N,S,NE,NW,SE,SW");
                break;
            case 10:
                spawnController.SetPoints("N,S,E,W,NE,NW,SE,SW");
                break;
            case 11:
                gameController.restartText.text = "Boss Wave";
                isBoss = true;
                spawnController.SetPoints("N,S");
                break;
            default:
                spawnController.SetPoints("N");
                break;
        }
    }

    public IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (!gameController.gameOver)
        {
            curWave++;
            PrepareWave();

            if (isBoss)
            {
                SpawnPoint point = spawnController.GetRandomPoint();
                Mover mover = boss.GetComponent<Mover>();
                mover.rotation = point.GetVelocityRotation();
                Instantiate(boss, point.GetRandomPosition(), point.GetQuaternion());
                break;
            }
            else
            {
                for (int i = 0; i < hazardCount; i++)
                {
                    GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                    SpawnPoint point = spawnController.GetRandomPoint();
                    Mover mover = hazard.GetComponent<Mover>();
                    mover.rotation = point.GetVelocityRotation();
                    Instantiate(hazard, point.GetRandomPosition(), point.GetQuaternion());

                    yield return new WaitForSeconds(spawnWait);
                }

                yield return new WaitForSeconds(waveWait);
            }

            if (gameController.gameOver)
            {
                gameController.restart = true;
                gameController.restartText.text = "Press 'R' to restart the game";
                break;
            }
        }
    }
}