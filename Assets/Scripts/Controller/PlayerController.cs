using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    public float speed;
    public float tilt;
    public float fireRate;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;
    public Dictionary<string, Transform> shotSpawns;

    private float nextFire = 1f;
    private AudioSource audio;
    private Rigidbody rigidbody;
    private GameObject[] spawns;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();

        spawns = GameObject.FindGameObjectsWithTag("ShotSpawns");
        // shotSpawns.Add("N1", find);
    }

    private void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            foreach(GameObject spawn in spawns)
            {
                // Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
                Instantiate(shot, spawn.transform.position, spawn.transform.rotation);
            }
            audio.Play();
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidbody.velocity = movement * speed;
        rigidbody.position = new Vector3(Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax), 0.0f, Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax));
        rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);
    }
}
