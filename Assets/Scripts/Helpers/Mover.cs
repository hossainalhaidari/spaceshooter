using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed;
    public Vector3 rotation;

    private void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (rotation == new Vector3()) rotation = transform.forward;
        rigidbody.velocity = rotation * speed;
    }
}
