using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    private bool movingRight = true;
    public float speed = 1f;

    private float xMax;
    private float xMin;

	// Use this for initialization
	void Start ()
	{
	    var distanceToCamera = transform.position.z - Camera.main.transform.position.z;
	    var leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
	    var rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
	    xMax = rightEdge.x;
	    xMin = leftEdge.x;

        foreach (Transform child in transform)
        {
            var enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity);
            enemy.transform.parent = child;
        }
	}

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
	
	void Update () {
	    if (movingRight)
	    {
	        transform.position += Vector3.right * speed * Time.deltaTime;
	    }
	    else
	    {
	        transform.position += Vector3.left * speed * Time.deltaTime;
	    }

	    var rightEdgeOfFormation = transform.position.x + (0.5f * width);
	    var leftEdgeOfFormation = transform.position.x - (0.5f * width);

        if (leftEdgeOfFormation < xMin)
	    {
	        movingRight = true;
	    }
	    else if (rightEdgeOfFormation > xMax)
	    {
	        movingRight = false;
	    }
	}
}
