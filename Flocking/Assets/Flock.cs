using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockingManager myManager;
    Vector3 direction;
    float speed;
    float freq;

    private Vector3 currentVelocity;
    // Start is called before the first frame update
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if((myManager.transform.position - transform.position).magnitude > myManager.swimLimit)
        {
            Debug.Log("me cago en dios");
            direction = (Cohesion() + Align() + Separation()).normalized;
        }
        else
        {
            direction = (Cohesion() + Align() + Separation()).normalized * speed;
            Debug.Log((Cohesion() + Align() + Separation()).normalized);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
        transform.Translate(Time.deltaTime * speed, 0.0f, 0.0f);*/
        
        var boundsVector = CalculateBoundsVector() * myManager.boundsWeight;

        direction = (Cohesion() + Align() + Separation() - CalculateBoundsVector()).normalized * speed;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), myManager.rotationSpeed * Time.deltaTime);
        transform.Translate(Time.deltaTime * speed,0.0f,0.0f);
        Debug.Log((myManager.transform.position - transform.position).magnitude);
    }

    private Vector3 CalculateBoundsVector()
	{
		var offsetToCenter = myManager.transform.position - transform.position;
		bool isNearCenter = (offsetToCenter.magnitude >= myManager.boundsDistance * 0.9f);
		return isNearCenter ? offsetToCenter : Vector3.zero;
	}

    Vector3 Cohesion()
    {
        Vector3 cohesion = Vector3.zero;
        int num = 0;
        foreach (GameObject go in myManager.allFish)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance <= myManager.neighbourDistance)
                {
                    cohesion += go.transform.position;
                    num++;
                }
            }
        }
        if (num > 0)
            cohesion = (cohesion / num - transform.position).normalized * speed;

        return cohesion;
    }

    Vector3 Align()
    {
        Vector3 align = Vector3.zero;
        int num = 0;
        foreach (GameObject go in myManager.allFish) {
            if (go != this.gameObject) {
                float distance = Vector3.Distance(go.transform.position, transform.position);
                if (distance <= myManager.neighbourDistance) {
                    align += go.GetComponent<Flock>().direction;
                    num++;
                }
            }
        }
        if (num > 0) {
            align /= num;
            speed = Mathf.Clamp(align.magnitude, myManager.minSpeed, myManager.maxSpeed);
        }
        return align;
    }
    Vector3 Separation()
    {
       Vector3 separation = Vector3.zero;
        foreach (GameObject go in myManager.allFish) {
            if (go != this.gameObject) {
                float distance = Vector3.Distance(go.transform.position, 
                                                transform.position);
                if (distance <= myManager.neighbourDistance)
                    separation -= (transform.position - go.transform.position) / 
                                (distance * distance);
            }
        }
        return separation;
    }
}
