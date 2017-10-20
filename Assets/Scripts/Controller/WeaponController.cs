using UnityEngine;

public class WeaponController : MonoBehaviour {

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;

    private AudioSource audioSource;

	private void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", delay, fireRate);
	}

    private void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
    }
}
