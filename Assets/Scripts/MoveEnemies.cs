using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemies : MonoBehaviour
{
    [SerializeField]
    Transform[] paths;

    // move support
    int randomPath;
    int pathIndex = 0;
    public float moveUnitsPerSecond = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        randomPath = Random.Range(0, paths.Length);
        transform.position = paths[randomPath].GetChild(0).position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    // move enemies follow the path
    private void Move()
    {
        // move enemies to the next waypoint
        transform.position = Vector3.MoveTowards(transform.position, paths[randomPath].GetChild(pathIndex).position, moveUnitsPerSecond * Time.deltaTime);
        // rotate enemies to the next waypoint
        // transform.rotation = Quaternion.LookRotation(paths[randomPath].GetChild(pathIndex + 1).position - paths[randomPath].GetChild(pathIndex).position);
        // if enemies reaches the waypoint
        if (transform.position == paths[randomPath].GetChild(pathIndex).position)
        {
            // if enemies reaches the last waypoint
            if (pathIndex == paths[randomPath].childCount - 1)
            {
                // destroy enemies
                Destroy(gameObject);
                return;
            }
            // move to the next waypoint
            pathIndex++;
        }
    }
}
