using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

	List<Transform> Path0Waypoints;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] WaveConfig waveConfig;
    int waypointIndex = 0;



    // Use this for initialization
    void Start () {

        Path0Waypoints = waveConfig.GetWayPoints();
        transform.position = Path0Waypoints[waypointIndex].transform.position;

	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();

    }

    private void Move()
    {
        if (waypointIndex <= Path0Waypoints.Count - 1)

        {
            var targetPosition = Path0Waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)

            {
                waypointIndex++;

            }


        }

        else
        {
            Destroy(gameObject);

        }
    }
}
