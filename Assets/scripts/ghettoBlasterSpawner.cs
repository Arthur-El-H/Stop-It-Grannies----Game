using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghettoBlasterSpawner : MonoBehaviour
{
    public GameObject gb;

    Vector3 pos1 = new Vector3( 4.2f,  2.2f, 0);
    Vector3 pos2 = new Vector3(-4.2f, -2.2f, 0);
    Vector3 pos3 = new Vector3(4.2f, -2.2f, 0);
    Vector3 pos4 = new Vector3(-4.2f, 2.2f, 0);
    Vector3 pos5 = new Vector3(0, 2f, 0);


    public void spawn()
    {
        int random = Random.Range(0, 5);
        switch (random)
        {
            case 0: Instantiate(gb, pos1, Quaternion.identity); break;
            case 1: Instantiate(gb, pos2, Quaternion.identity); break;
            case 2: Instantiate(gb, pos3, Quaternion.identity); break;
            case 3: Instantiate(gb, pos4, Quaternion.identity); break;
            case 4: Instantiate(gb, pos5, Quaternion.identity); break;
        }
    }
}
