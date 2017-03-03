using UnityEngine;

public class BikeController : MonoBehaviour {

    public float speed;
    public float rotSpeed;

    private ParticleSystem partSys;
    private SpriteRenderer sprRen;
    private float horizontal;

    private void Awake ()
    {
        partSys = GetComponentInChildren<ParticleSystem> ();
        sprRen = GetComponent<SpriteRenderer> ();
    }

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
        if (other.gameObject.tag == "Player")
        {
            speed = 0f;
            rotSpeed = 0f;
            partSys.Play ();
            sprRen.enabled = false;

            GameManager.Instance.GameOver (transform.parent.gameObject);
        }
    }

}
