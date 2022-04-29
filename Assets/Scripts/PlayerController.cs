using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject ground;
    public GameObject goal;
    public GameObject enemy;


    // Start is called before the first frame update
    void Start()
    {
        // Set the goal position
        float maxX = (float)ground.transform.localScale.x * 5.0f - 0.5f;
        float maxZ = (float)ground.transform.localScale.z * 5.0f - 0.5f;
        float randomX = 0;
        float randomZ = 0;
        while(randomX < 2 && randomX > -2){
            randomX = Random.Range(-maxX, maxX);
        }
        while(randomZ < 2 && randomZ > -2){
            randomZ = Random.Range(-maxZ, maxZ);
        }
        goal.transform.position = new Vector3(randomX, goal.transform.position.y, randomZ);   
        // Set the enemy position
        randomX = 0;
        randomZ = 0;
        while(randomX < 2 && randomX > -2){
            randomX = Random.Range(-maxX, maxX);
        }
        while(randomZ < 2 && randomZ > -2){
            randomZ = Random.Range(-maxZ, maxZ);
        }
        enemy.transform.position = new Vector3(randomX, enemy.transform.position.y, randomZ);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (Input.GetKey("right"))
        {
            MoveDirection(1,0);
        }else if (Input.GetKey("left"))
        {
            MoveDirection(-1,0);
        }
        if (Input.GetKey("up"))
        {
            MoveDirection(0,-1);
        }else if (Input.GetKey("down"))
        {
            MoveDirection(0,1);
        }
    }

    public void MoveDirection(int leftRight = 0, int ForwardBackward = 0)
    {
        Vector3 position = transform.position;
        position.x += ForwardBackward * 0.1f;
        position.z += leftRight * 0.1f;
        transform.position = position;
    }


}
