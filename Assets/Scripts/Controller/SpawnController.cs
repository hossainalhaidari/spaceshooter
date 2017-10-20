using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private Dictionary<string, SpawnPoint> spawnPoints;
    private string[] selectedPoints;
    private string defPoints = "N";

    private void Awake()
    {
        LoadPoints();
        SetPoints(defPoints);
    }

    public void SetPoints(string points)
    {
        selectedPoints = points.Split(',');
    }

    public SpawnPoint GetRandomPoint()
    {
        int key = Random.Range(0, selectedPoints.Length);
        return spawnPoints[selectedPoints[key]];
    }

    private void LoadPoints()
    {
        spawnPoints = new Dictionary<string, SpawnPoint>();

        SpawnPoint N = new SpawnPoint(new Vector2(-5.8f, 5.8f), new Vector2(16f, 16f), 0);
        spawnPoints.Add("N", N);

        SpawnPoint S = new SpawnPoint(new Vector2(-5.8f, 5.8f), new Vector2(-6f, -6f), 180);
        spawnPoints.Add("S", S);

        SpawnPoint E = new SpawnPoint(new Vector2(-10f, -10f), new Vector2(-3f, 15f), 90);
        spawnPoints.Add("E", E);

        SpawnPoint W = new SpawnPoint(new Vector2(10f, 10f), new Vector2(-3f, 15f), 270);
        spawnPoints.Add("W", W);

        SpawnPoint NE = new SpawnPoint(new Vector2(10f, 10f), new Vector2(-3f, 15f), 225);
        spawnPoints.Add("NE", NE);

        SpawnPoint NW = new SpawnPoint(new Vector2(-10f, -10f), new Vector2(-3f, 15f), 135);
        spawnPoints.Add("NW", NW);

        SpawnPoint SE = new SpawnPoint(new Vector2(10f, 10f), new Vector2(-5f, 15f), 315);
        spawnPoints.Add("SE", SE);

        SpawnPoint SW = new SpawnPoint(new Vector2(-10f, -10f), new Vector2(-5f, 15f), 45);
        spawnPoints.Add("SW", SW);
    }
}

public class SpawnPoint
{
    public Vector2 xBound {get; set;}
    public Vector2 zBound {get; set;}
    public float rotation {get; set;}

    public SpawnPoint(Vector2 xBound, Vector2 zBound, float rotation)
    {
        this.xBound = xBound;
        this.zBound = zBound;
        this.rotation = rotation;
    }

    public Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(xBound.x, xBound.y), 0, Random.Range(zBound.x, zBound.y));
    }

    public Quaternion GetQuaternion()
    {
        return new Quaternion(0, rotation, 0, 0);
    }

    public Vector3 GetVelocityRotation()
    {
        if (rotation == 90)
            return new Vector3(-1, 0, 0);
        else if (rotation == 180)
            return new Vector3(0, 0, -1);
        else if (rotation == 270)
            return new Vector3(1, 0, 0);
        else if (rotation == 225)
            return new Vector3(0.5f, 0, 0.5f);
        else if (rotation == 135)
            return new Vector3(-0.5f, 0, 0.5f);
        else if (rotation == 315)
            return new Vector3(0.5f, 0, -0.5f);
        else if (rotation == 45)
            return new Vector3(-0.5f, 0, -0.5f);

        return new Vector3(0, 0, 1);
    }
}
