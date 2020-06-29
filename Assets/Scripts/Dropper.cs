using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    // Start is called before the first frame update
    //public GameObject cube;
    public GameObject blackHole;

    int dontCollideLayer = 9;
    int collideLayer = 0;
    Vector3 newPosition;
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        //ren.material.renderQueue = 1000;
        //cube.layer =dontCollideLayer ;// this will not collide to the floor
        //cube.GetComponent<Rigidbody>().AddForce(Vector3.right*15f*Random.value,0);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 8 )// it is an object and it is close enough to be dropped
        {
            if (other.gameObject.layer == 0) // usual block that gives point
            {
                other.gameObject.layer = dontCollideLayer;
            }
            else if (other.gameObject.layer == 13)// colored block that will end the game
            {
                other.gameObject.layer = 14;

            }
        }
    }


    //#TODO Add a condition to remove the cubes that are under the floor 
}
