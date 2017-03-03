using UnityEngine;

public class BikeController : MonoBehaviour {

    public float speed;
    public float rotSpeed;

    private float horizontal;

	private void Update ()
    {
        horizontal = Input.GetAxisRaw (gameObject.transform.parent.name);
    }

    private void FixedUpdate ()
    {
        transform.Translate (Vector2.up * speed * Time.fixedDeltaTime, Space.Self);
        transform.Rotate (Vector3.forward * rotSpeed * -horizontal * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.transform.parent.gameObject.tag == "Player")
        {
            GameManager.Instance.GameOver (transform.parent.gameObject);
            speed = 0f;
            rotSpeed = 0f;
        }
    }

}
