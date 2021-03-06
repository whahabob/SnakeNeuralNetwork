﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Grid;

public class AppleBehaviour : MonoBehaviour
{
    Cell[,] board;
    public GameObject applePrefab;
    GameObject apple;
    void Start()
    {
        apple = Instantiate(applePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        StartCoroutine("SpawnApple");
    }

    // Update is called once per frame
    void Update()
    {
        if(board == null)
        {
            GameInformation gameInformation = FindObjectOfType<GameInformation>();
            board = gameInformation.board;
        }
    }

    IEnumerator SpawnApple()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(2f);
            Debug.Log("apple!");
            //CollisionWithSnake();
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Snake")
        {
            CollisionWithSnake(collision.gameObject);
        }
    }

    private void CollisionWithSnake(GameObject snake)
    {
        Debug.Log(apple.transform.position.x + " : " + apple.transform.position.y);
        board[(int)apple.transform.position.x, (int)apple.transform.position.y].space = Cell.Space.empty;
        Vector2 position = new Vector2(UnityEngine.Random.Range(0, board.GetLength(0)), UnityEngine.Random.Range(0, board.GetLength(1)));
        while(board[(int)position.x,(int)position.y].space != Cell.Space.empty)
        {
           position = new Vector2(UnityEngine.Random.Range(0, board.GetLength(0)), UnityEngine.Random.Range(0, board.GetLength(1)));
        }
        apple.transform.position = new Vector3(position.x, position.y, 0);
        board[(int)position.x, (int)position.y].space = Cell.Space.apple;
    }
}
