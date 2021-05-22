using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grannyManager : MonoBehaviour
{
    public List<grandma> grannys = new List<grandma>();
    public List<grandma> startingGrannys;
    public List<GameObject> StartingGrannys;

    public GameObject granny;
    public grandma newGranny;
    public GameObject NewGranny;

    public gameManager game;
    public GameObject p1;
    public GameObject p2;

    public GameObject G1;
    public GameObject G2;
    public GameObject G3;
    public GameObject G4;
    public GameObject G5;
    public GameObject G6;
    public GameObject G7;
    public GameObject G8;
    public GameObject G9;

    public grandma g1;
    public grandma g2;
    public grandma g3;
    public grandma g4;
    public grandma g5;
    public grandma g6;
    public grandma g7;
    public grandma g8;
    public grandma g9;


    Vector3 spawn1 = new Vector3 (0, 4.25f,0);
    Vector3 spawn2 = new Vector3 (0, -4.25f,0);
    Vector3 spawn3 = new Vector3 (-8f, 0.32f,0);
    Vector3 spawn4 = new Vector3 (8f, 0.32f,0);


    bool lostAlready;

    public IEnumerator activateStartingGrannys()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i<3; i++)
        {
            StartingGrannys[i].SetActive(true);
        }

        yield return new WaitForSeconds(1);
        for (int i = 3; i < 6; i++)
        {
            StartingGrannys[i].SetActive(true);
        }
        yield return new WaitForSeconds(1);
        for (int i = 6; i < 9; i++)
        {
            StartingGrannys[i].SetActive(true);
        }
        yield return new WaitForSeconds(1);
    }

    private void Start()
    {
        startingGrannys = new List<grandma>() { g1, g2, g3, g4, g5, g6, g7, g8, g9 };
        StartingGrannys = new List<GameObject>() { G1, G2, G3, G4, G5, G6, G7, G8, G9 };
        grannys.AddRange(startingGrannys);
    }
    public void spawnGranny()
    {
        int randomInt = Random.Range(0, 4);
        switch (randomInt)
        {
            case 0: spawn(spawn1); break;
            case 1: spawn(spawn2); break;
            case 2: spawn(spawn3); break;
            case 3: spawn(spawn4); break;
        }
    }

    void spawn(Vector3 spawnPoint)
    {
        NewGranny = Instantiate(granny, spawnPoint, Quaternion.identity);
        newGranny = NewGranny.GetComponent<grandma>();

        newGranny.grannyManager = this;
        newGranny.gameManager = game;
        newGranny.playerOne = p1;
        newGranny.playerTwo = p2;

        grannys.Add(newGranny);
    }

    public void loose(bool p1Loose)
    {
        if (!lostAlready)
        {
            lostAlready = true;
            StartCoroutine(game.gameOver(p1Loose));
        }
    }
}
