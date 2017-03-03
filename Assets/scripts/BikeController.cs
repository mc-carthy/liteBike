using UnityEngine;

public class BikeController : MonoBehaviour {

    public float speed;
    public float rotSpeed;

    private float horizontal;

	private void Update ()
    {
        horizontal = Input.GetAxisRaw ("Horizontal");
    }

    private void FixedUpdate ()
    {
        transform.Translate (Vector2.up * speed * Time.fixedDeltaTime, Space.Self);
        transform.Rotate (Vector3.forward * rotSpeed * -horizontal * Time.fixedDeltaTime);
    }

}
