using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public int width = 20;
    public int height = 20;
    public Cell[,] board;
    [SerializeField]
    private GameObject[] prefabs;
    // Start is called before the first frame update
    public Grid()
    {
        
        board = new Cell[width,height];
        //Canvas canvas = FindObjectOfType<Canvas>();
        for(int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Cell cell;
                if (y == 0 || x == 0 || y == height - 1 || x == width - 1)
                {
                    cell = new Cell(new Vector2(x, y), Cell.Space.wall, prefabs);
                }
                else
                {
                    cell = new Cell(new Vector2(x, y), Cell.Space.empty, prefabs);
                }
                board[x,y] = cell;
                Debug.Log(board[x, y].space);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class Cell
    {
        public enum Space {empty, apple, wall, snake };
        public Space space;
        public Vector2 position;
        GameObject[] prefabs;
        
        public Cell(Vector2 position, Space space, GameObject[] prefabs)
        {
            this.position = position;
            this.space = space;
            this.prefabs = prefabs;
            VisualizeCell();
        }

        private void VisualizeCell()
        {
            if(space == Space.empty)
            {
                //Instantiate(prefabs[0], new Vector3(position.x*10, position.y*10, 0), Quaternion.identity);
            }
            if (space == Space.wall)
            {
                
            }
        }

    }
}
