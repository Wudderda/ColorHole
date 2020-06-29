using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnet : MonoBehaviour
{
    private List<GameObject> cubes;
    public int forceMagnitude = 8;
    public GameObject blackHole;
    public GameObject platform;
    int maxCube= 47 ;
    int theDroppedCubes = 0;
    public GameManager theManagerObject;
    GameManager theManager;
    // Start is called before the first frame update
    void Start()
    {
        cubes = new List<GameObject>();
        theManager= theManagerObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject cube in cubes) { 
            Vector3 forceDir = (this.transform.position+0.4f*Vector3.down - cube.transform.position).normalized;
            if (cube.layer!=8 )//it is not floor
            {
                cube.GetComponent<Rigidbody>().AddForce(forceDir * forceMagnitude, 0);
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        cubes.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        cubes.Remove(other.gameObject);
        if (other.gameObject.transform.position.y < 1.99)//falling
        {
            other.gameObject.SetActive(false);
            if (other.gameObject.layer==14)//colored block is dropped game over
            {
                Debug.Log("Game Over!");
                theDroppedCubes = 0;
                theManager.repeatLevel();
            }
            theDroppedCubes++;
            if (theDroppedCubes==maxCube)
            {
                theManager.levelUp();
                theDroppedCubes = 0;
            }
            
        }
        else if (other.gameObject.layer != 8)// if it is not the floor , make its layer default
        {
            if (other.gameObject.layer == 14 || other.gameObject.layer ==13 ) //colored block was near
            {
                other.gameObject.layer = 13;
            }
            else
            {
                other.gameObject.layer = 0;
            }
        }
    }
}
