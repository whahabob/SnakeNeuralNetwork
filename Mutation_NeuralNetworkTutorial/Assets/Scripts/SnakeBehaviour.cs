using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Grid;

public class SnakeBehaviour : MonoBehaviour
{

    public GameObject tailPrefab;
    private Vector2 direction = Vector2.up;
    private float speed = 1f;

    private List<Transform> tail;
    Cell[,] board;
    // Start is called before the first frame update
    void Start()
    {

        tail = new List<Transform>();
       // Grid grid = FindObjectOfType<Grid>();
       // board = grid.board;
        gameObject.transform.position = board[5, 5].position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            direction = new Vector2(-1, 0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = new Vector2(0, 1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = new Vector2(1, 0);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direction = new Vector2(0, -1);
        }



        
    }

    IEnumerator MoveSnake()
    {
        
        yield return new WaitForSeconds(1f);
    }
}
