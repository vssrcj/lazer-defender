using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 15f;
    private float padding = 1f;
    private float projectileSpeed = 5f;
    public float fireRate = 1f;

    public GameObject projectile;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float Padding
    {
        get { return padding; }
        set { padding = value; }
    }

    private float xMin;
    private float xMax;

    private void Start()
    {
        var distance = transform.position.z - Camera.main.transform.position.z;
        var leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        var rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = leftMost.x + padding;
        xMax = rightMost.x - padding;
    }

    private void Fire()
    {
        var beam = Instantiate(projectile, transform.position, Quaternion.identity);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed);
    }

    // Update is called once per frame
    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, fireRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }

		if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        // restrict ship to gamespace
        var newX = Mathf.Clamp(transform.position.x, xMin, xMax);

        transform.position = new Vector3(newX, transform.position.y);
    }
}
