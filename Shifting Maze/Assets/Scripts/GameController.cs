using System;
using UnityEngine;

[RequireComponent (typeof(MazeConstructor))]

public class GameController : MonoBehaviour 
{
    /*The RequireComponent attribute ensures that a MazeConstructor component 
      will also be added when you add this script to a GameObject. */
    private MazeConstructor generator;

    void Start()
    {
        // A private variable that stores a reference returned by the GetComponent().
        generator = GetComponent<MazeConstructor>();
        generator.GenerateNewMaze(13, 15);
    }
}
