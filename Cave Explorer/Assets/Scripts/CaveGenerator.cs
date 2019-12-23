using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveGenerator : MonoBehaviour
{
    public GameObject cube;
    public GameObject start;
    public GameObject end;
    public int size;
    public float toughness;
    private int[,] bitMap;
    public (int, int) startState;
    public (int, int) goalState;


    public GameObject sphere;
    // Start is called before the first frame update
    void Start()
    {
        bitMap = new int[size, size];
        //createFloor();
        createMaze();

        while (!SolutionCheck())
        {
            Debug.Log("No Path");
        
   
            Debug.Log("Start: " + startState);
            Debug.Log("End: " + goalState);
            bitMap = new int[size, size];
            createMaze();
        }
        Debug.Log("Path exists");
        renderMaze();
        Debug.Log("Moving sphere to: " + startState);
        sphere.GetComponent<Transform>().position = new Vector3(startState.Item1, 0.05f, startState.Item2);
        //sphere.transform.position = 
    }

    void createFloor()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Vector3 position = new Vector3(i, 0, j);
                Quaternion rotation = new Quaternion();
                Instantiate(cube, position, rotation);
            }
        }
    }

    void createMaze()
    {
        int randomStartX = Random.Range(1, 3);
        int randomStartZ = Random.Range(1, 3);

        startState = (randomStartX, randomStartZ);

        int randomEndX = Random.Range(size - 3, size - 1);
        int randomEndZ = Random.Range(size - 3, size - 1);

        goalState = (randomEndX, randomEndZ);

        createPath();
        bitMap[startState.Item1, startState.Item2] = 1;
        bitMap[goalState.Item1, goalState.Item2] = 1;

        // Create a heuristic that tells you how far you are from the goal
        // Create a random path starting from start state to goal state and then randomly populate the rest of the map with walls
    }
    void renderMaze()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (bitMap[i, j] == 0)
                {
                    Vector3 position = new Vector3(i, 0.5f, j);
                    Quaternion rotation = new Quaternion();
                    Instantiate(cube, position, rotation);
                }
                else
                {
                    if (i == startState.Item1 && j == startState.Item2)
                    {
                        Debug.Log("Start: " + startState);

                    }
                    if (i == goalState.Item1 && j == goalState.Item2)
                    {
                        Vector3 position = new Vector3(i, 0.5f, j);
                        Quaternion rotation = new Quaternion();
                        Instantiate(end, position, rotation);
                        Debug.Log("End: " + goalState);
                    }
                }

            }
        }
    }
    void createPath()
    {
        // wall or open space
        (int, int) currentState = startState;

        for (int i = 1; i < size - 1; i++)
        {
            for (int j = 1; j < size - 1; j++)
            {
                float choice = Random.Range(0.0f, 1.0f);
                if (choice < toughness)
                {
                    bitMap[i, j] = 0;
                }
                else
                {
                    bitMap[i, j] = 1;
                }
            }
        }
    }

    bool SolutionCheck()
    {
        Queue<(int, int)> q = new Queue<(int, int)>();
        q.Enqueue(startState);

        HashSet<(int, int)> visited = new HashSet<(int, int)>();
        visited.Add(startState);
        while(q.Count != 0)
        {
            (int, int) current = q.Dequeue();
            List<(int, int)> neighbors = getNeighbors(current);
            for(int i = 0; i < neighbors.Count; i++)
            {
                if(!visited.Contains(neighbors[i]))
                {
                    q.Enqueue(neighbors[i]);
                    visited.Add(neighbors[i]);
                }
                if(neighbors[i] == goalState)
                {
                    return true;
                }
            }
        }
        return false;
    }


    List<(int, int)> getNeighbors((int, int) currentState)
    {
        int currentX = currentState.Item1;
        int currentZ = currentState.Item2;
        List<(int, int)> neighbors = new List<(int, int)>();

        if(currentX != 0)
        {
            if(bitMap[currentX - 1, currentZ] == 1)            
                neighbors.Add((currentX - 1, currentZ)); // UP            

        }

        if(currentX != size - 1)
        {
            if(bitMap[currentX + 1, currentZ] == 1)
                neighbors.Add((currentX + 1, currentZ)); // DOWN
        }
        if (currentZ != 0)
        {
            if(bitMap[currentX, currentZ - 1] == 1)
                neighbors.Add((currentX, currentZ - 1)); // LEFT
        }

        if (currentZ != size - 1)
        {
            if(bitMap[currentX, currentZ + 1] == 1)
                neighbors.Add((currentX, currentZ + 1)); // RIGHT
        }
        return neighbors;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
