/*
Script to dynamically create walls in Unity.
By Marmik Mundada
*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWalls : MonoBehaviour{
    bool creating;
    public GameObject start;
    public GameObject end;
    public GameObject wallPrefab;
    GameObject wall;

    // Start is called before the first frame update
    void Start(){
    }
    // Update is called once per frame
    void Update(){
        getInput();
    }

    private void getInput(){
        /*
        Input.GetMouseButtonDown(int button)
        button = 0 -> left buttom
        button = 1 -> right button
        button = 2 -> middle button
        */
        if (Input.GetMouseButtonDown(0)){
            setStart();
        }
        else if (Input.GetMouseButtonUp(0)){
            setEnd();
        }
        else{
            if (creating){
                adjust();
            }
        }
    }
    //To get the position of the mouse
    Vector3 getWorldPoint(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){
            return hit.point;
        }
        return Vector3.zero;
    }
    //To start the process of wall creation,setting the position of the start pole and instantiating a wall.
    void setStart(){
        creating = true;
        start.transform.position = getWorldPoint();
        wall = (GameObject)Instantiate(wallPrefab, start.transform.position, Quaternion.identity);
    }
    //To end the process of wall creation, setting the position of the end pole.
    void setEnd(){
        creating = false;
        end.transform.position = getWorldPoint();
    }
    //To dynamically adjust the position, size and rotation of the wall when creating
    void adjust(){
        end.transform.position = getWorldPoint();
        start.transform.LookAt(end.transform.position);
        end.transform.LookAt(start.transform.position);
        float distance = Vector3.Distance(start.transform.position, end.transform.position);
        wall.transform.position = start.transform.position + distance / 2 * start.transform.forward;
        wall.transform.rotation = start.transform.rotation;
        wall.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, distance);
    }   
}
