using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    public GameObject fishPrefab;
    public Vector3 minPosition;
    public Vector3 maxPosition;
    public GameObject[] allFish;
    public int numFish;
    public Transform acumulator;
    public float neighbourDistance;
    [Range(0f, 10f)]
    public float minSpeed;
    [Range(0f, 10f)]
    public float maxSpeed;
    [Range(0f, 10f)]
    public float rotationSpeed;

    public int swimLimit;

    // Start is called before the first frame update
    void Start()
    {
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; i++)
        {
            Vector3 random = new Vector3(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y), Random.Range(minPosition.z, maxPosition.z));
            Vector3 pos = this.transform.position + random;
            Vector3 randomize = random;
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.LookRotation(randomize), acumulator);
            allFish[i].GetComponent<Flock>().myManager = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            swimLimit++;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            swimLimit--;
        }
    }
}
