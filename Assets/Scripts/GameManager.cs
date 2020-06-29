using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State
{
    Level1,
    Level2,
    Level3
}
public class GameManager : MonoBehaviour
{
    State currLevel;
    public GameObject cubes;
    public GameObject blackHole;
    public GameObject coloredCube;
    List<GameObject> allFallable=null;
    List<Vector3> positions;
    List<Quaternion> rotations;
    Vector3 newPosition;
    Vector3 initialPosition;
    Vector3 initialColoredPosition;
    Quaternion initialColoredRotation;
    Vector3 mouseBegin;
    void Start()
    {
        positions = new List<Vector3>(); // storing the initial positions
        rotations = new List<Quaternion>();
        foreach (Transform child in cubes.transform)
        {
            positions.Add(child.position);
            rotations.Add(child.rotation);

        }
        currLevel = State.Level1;
        initialPosition = blackHole.transform.position;
        initialColoredPosition = coloredCube.transform.position;
        initialColoredRotation = coloredCube.transform.rotation;
    }
    void Update()
    {
        //x +0.4 , -10 arasi
        //z -1.9 , +1.9 arası
        newPosition = blackHole.transform.position;
        if (Input.GetMouseButtonDown(0)) 
        {
            mouseBegin = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            float horizontalDiff = (Input.mousePosition.y - mouseBegin.y)*0.05f; //x
            float verticalDiff =( Input.mousePosition.x - mouseBegin.x)*0.05f; //z
            newPosition +=   (horizontalDiff* Vector3.left  +verticalDiff* Vector3.forward)*Time.deltaTime*5f;
  
            mouseBegin = Input.mousePosition;
        }
       /* if (Input.GetKey(KeyCode.W))
        {
            newPosition += Vector3.left * Time.deltaTime * 5f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            newPosition += Vector3.back * Time.deltaTime * 5f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newPosition += Vector3.right * Time.deltaTime * 5f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newPosition += Vector3.forward * Time.deltaTime * 5f;
        }*/
        if (newPosition.x - initialPosition.x < 0.2f && newPosition.x - initialPosition.x > -8f && newPosition.z - initialPosition.z > -2.7f && newPosition.z - initialPosition.z < 2.7f)
        {
            blackHole.transform.position = newPosition;
        }
    }
    public void repeatLevel()
    {
        switch (currLevel)
        {
            case State.Level1:
                transitionToLevel1();
                break;
            case State.Level2:
                transitionToLevel2();
                break;
            case State.Level3:
                transitionToLevel3();
                break;
        }
    }
    public void levelUp()
    {
        switch (currLevel) {
            case  State.Level1:
                transitionToLevel2();
                break;
            case State.Level2:
                transitionToLevel3();
                break;
            case State.Level3:
                Debug.Log("Conguratulations , you have won the game");
                break;
        }
    }
    public List<GameObject> GetFallables()
    {
        if (allFallable == null)
        {
            allFallable = new List<GameObject>();
            foreach (Transform child in cubes.transform)
            {
                allFallable.Add(child.gameObject);
            }
        }
        return allFallable;
    }
    private void restartTheHole()
    {
        blackHole.transform.position = initialPosition;
        coloredCube.layer = 13;
        restartCube(coloredCube,initialColoredRotation,initialColoredPosition);
    }
    private void restartCube(GameObject cube , Quaternion rotation,Vector3 position)
    {
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cube.SetActive(true);
        cube.transform.position = position;
        cube.transform.rotation = rotation;
    }
    public void transitionToLevel1()
    {

        restartTheHole();
        List<GameObject> fallables = GetFallables();
        for (int i=0; i<fallables.Count; i++)
        {
            fallables[i].layer = 0;
            restartCube(fallables[i],rotations[i], positions[i]);

        }
    }
    public void transitionToLevel2()
    {
        currLevel = State.Level2;
        transitionToLevel1();
        List<GameObject> fallables = GetFallables();
        foreach(GameObject fallable in fallables)
        {
            fallable.transform.position += Vector3.back * Random.Range(-1,1)+ Vector3.left*Random.Range(-1,1) ; 
        }

    }
    public void transitionToLevel3()
    {
        currLevel = State.Level3;
        transitionToLevel1();
        List<GameObject> fallables = GetFallables();
        foreach (GameObject fallable in fallables)
        {
            fallable.transform.position += Vector3.back * Random.Range(-1, 1) + Vector3.right * Random.Range(-3, 3);
        }

    }
}
