using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public float timeBetweenDecisionsAtInference;
    float m_TimeSinceDecision;
    public GameObject ground;
    public GameObject goal;
    public GameObject enemy;

    public int positionX;
    public int positionZ;
    public const int k_Extents = 20;

    Agent m_Agent;

    public void OnEnable()
    {
        m_Agent = GetComponent<Agent>();
    }

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
       if (Input.GetKey("right"))
        {
            xDirection = 1;
        }else if (Input.GetKey("left"))
        {
            xDirection = -1;
        }
        if (Input.GetKey("up"))
        {
            zDirection = -1;
        }else if (Input.GetKey("down"))
        {
            zDirection = 1;
        } 
    }

    int xDirection = 0;
    int zDirection = 0;

    void FixedUpdate()
    {
        if(xDirection != 0 || zDirection != 0){
            try
            {
                MoveDirection(xDirection, zDirection); 
            }
            catch (System.Exception)
            {
                throw;
            }
            xDirection = 0;
            zDirection = 0; 
        }
    }

    public void MoveDirection(int leftRight = 0, int ForwardBackward = 0)
    {
        Vector3 position = transform.position;
        position.x += ForwardBackward * 0.1f;
        position.z += leftRight * 0.1f;
        transform.position = position;
         m_Agent.AddReward(-0.01f);
    }

    

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "goal")
        {
            print("Reward +1");
            m_Agent.AddReward(+1f);
            m_Agent.EndEpisode();
            ResetAgent();
        }
        else if(other.gameObject.tag == "enemy")
        {
            print("Reward -1");
            m_Agent.AddReward(-1f);
            m_Agent.EndEpisode();
            ResetAgent();
        }
    } 

    void WaitTimeInference()
    {
        if (m_Agent == null)
        {
            return;
        }
        if (Academy.Instance.IsCommunicatorOn)
        {
            m_Agent?.RequestDecision();
        }
        else
        {
            if (m_TimeSinceDecision >= timeBetweenDecisionsAtInference)
            {
                m_TimeSinceDecision = 0f;
                m_Agent?.RequestDecision();
            }
            else
            {
                m_TimeSinceDecision += Time.fixedDeltaTime;
            }
        }
    }

    public void ResetAgent()
    {
        // This is a very inefficient way to reset the scene. Used here for testing.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        m_Agent = null; // LoadScene only takes effect at the next Update.
        // We set the Agent to null to avoid using the Agent before the reload
    }



}
