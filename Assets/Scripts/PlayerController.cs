using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 15f;
    private float padding = 1f;

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

    // Update is called once per frame
    private void Update ()
    {
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
