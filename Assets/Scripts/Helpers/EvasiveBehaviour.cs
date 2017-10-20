using System.Collections;
using UnityEngine;

public class EvasiveBehaviour : MonoBehaviour {

    public float dodge;
    public float smoothing;
    public float tilt;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public Boundary boundary;

    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody rigidbody;

    private void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
        currentSpeed = rigidbody.velocity.z;
        StartCoroutine(Evade());
	}

    private void FixedUpdate ()
    {
        float newManeuver = Mathf.MoveTowards(rigidbody.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rigidbody.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        rigidbody.position = new Vector3(Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax), 0.0f, Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax));

        rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}

    private IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }
}
