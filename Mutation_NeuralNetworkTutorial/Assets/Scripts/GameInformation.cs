using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Grid;

public class GameInformation : MonoBehaviour
{
    public Grid grid;
    public Cell[,] board;
    void Start()
    {
        grid = FindObjectOfType<Grid>();
        board = grid.board;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
