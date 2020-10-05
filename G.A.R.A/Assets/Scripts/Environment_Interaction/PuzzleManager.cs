using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : Singleton<PuzzleManager>
{
    private Queue<GameObject> deletedObjects;
    public bool puzzleCompleted;
    public GameObject[] puzzle;
    private Vector3[] puzzleStartPos;
    private Quaternion[] puzzleStartRot;
    private Vector3[] puzzleStartScale;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        deletedObjects = new Queue<GameObject>();
        puzzleCompleted = false;
        FillStartTransforms();
    }
    public void FillStartTransforms()
    {
        
        puzzleStartPos = new Vector3[puzzle.Length];
        puzzleStartRot = new Quaternion[puzzle.Length];
        puzzleStartScale = new Vector3[puzzle.Length];
        for (int i = 0; i < puzzle.Length; i++)
        {
            puzzleStartPos[i] = puzzle[i].transform.position;
            puzzleStartRot[i] = puzzle[i].transform.rotation;
            puzzleStartScale[i] = puzzle[i].transform.localScale;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            for (int i = 0; i < puzzle.Length; i++)
            {
                puzzle[i].transform.position = puzzleStartPos[i];
                puzzle[i].transform.rotation = puzzleStartRot[i];
                puzzle[i].transform.localScale = puzzleStartScale[i];
                if (puzzle[i].TryGetComponent<MovementInteraction>(out MovementInteraction mi))
                {
                    mi.stopCoroutine = false;
                }
            }
        }
    }

    public void AddDeletedObjectToQueue(GameObject gameObject)
    {
        deletedObjects.Enqueue(gameObject);
        Debug.Log(deletedObjects.Count);
    }
}
