using UnityEngine;

public class MazeConstructor : MonoBehaviour
{
    public bool showDebug;

    [SerializeField] private Material mazeMat1;
    [SerializeField] private Material mazeMat2;
    [SerializeField] private Material startMat;
    [SerializeField] private Material treasureMat;

    private MazeDataGenerator dataGenerator;
    private MazeMeshGenerator meshGenerator;

    // The data property. The access declarations (i.e. declaring the property as public but
    // then assigning private set) makes it read-only outside this class. Thus, maze data
    // can’t be modified from outside.
    // The maze data is simply a two-dimensional array that’s either 0 or 1 (to represent open
    // or blocked) for every space.It’s that simple! 
    public int[,] data
    {
        get; private set;
    }

    // This initializes data with a 3 by 3 array of ones surrounding zero. 1 means “wall” while
    // 0 means “empty”, so this default grid is simply a walled-in room.
    void Awake()
    {
        dataGenerator = new MazeDataGenerator();
        meshGenerator = new MazeMeshGenerator();

        // default to walls surrounding a single empty cell
        data = new int[,]
        {
            {1, 1, 1},
            {1, 0, 1},
            {1, 1, 1}
        };
    }

    // Generates the data for the maze.
    public void GenerateNewMaze(int sizeRows, int sizeCols)
    {
        if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
        {
            Debug.LogError("Odd numbers work better for maze size.");
        }

        data = dataGenerator.FromDimensions(sizeRows, sizeCols);

        DisplayMaze();
    }

    void OnGUI()
    {
        if (!showDebug)
        {
            return;
        }

        // Initialize several local variables: a local copy of the stored maze, the maximum row
        // and column, and a string to build up.
        int[,] maze = data;
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        string msg = "";

        // Iterate through the two dimensional array. For each row/column, the code checks the
        // stored value and appends either "...." or "==" depending if the value is zero. Also,
        // it appends a  newline after iterating through each row.
        for (int i = rMax; i >= 0; i--)
        {
            for (int j = 0; j <= cMax; j++)
            {
                if (maze[i, j] == 0)
                {
                    msg += "....";
                }
                else
                {
                    msg += "==";
                }
            }
            msg += "\n";
        }

        // It prints out the built-up string.
        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }

    // 
    private void DisplayMaze()
    {
        GameObject go = new GameObject();
        go.transform.position = Vector3.zero;
        go.name = "Procedural Maze";
        go.tag = "Generated";

        MeshFilter mf = go.AddComponent<MeshFilter>();
        mf.mesh = meshGenerator.FromData(data);

        MeshCollider mc = go.AddComponent<MeshCollider>();
        mc.sharedMesh = mf.mesh;

        MeshRenderer mr = go.AddComponent<MeshRenderer>();
        mr.materials = new Material[2] { mazeMat1, mazeMat2 };
    }
}