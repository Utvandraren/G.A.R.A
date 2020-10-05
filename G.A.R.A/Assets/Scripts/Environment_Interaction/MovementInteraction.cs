using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovementInteraction : MonoBehaviour
{
    public bool reverseMovementSecondInteract;
    public bool isFullyMoved;

    public float distanceToMove;

    private bool move = false;
    public bool stopCoroutine = false;

    public void SwitchBetweenUpAndPosX()
    {
        if (!move)
        {
            if(gameObject.TryGetComponent<ActivateInteraction>(out ActivateInteraction interaction))
            {
                if(interaction.active == ActivationEnum.On)
                {
                    MovePosX();
                }
                else
                {
                    MoveUp();
                }
            }
        }
    }

    public void SwitchDownNegX()
    {
        if (!move)
        {
            if (gameObject.TryGetComponent<ActivateInteraction>(out ActivateInteraction interaction))
            {
                if (interaction.active == ActivationEnum.On)
                {
                    MoveNegX();
                }
                else
                {
                    MoveDown();
                }
            }
        }
    }


    /// <summary>
    /// Moves the object along y-axis when invoked. Moves object back with second invoke or continues moving in same direction.
    /// </summary>
    public  void MoveUpAndDown()
    {
        if (!move)
        {

            if (reverseMovementSecondInteract)
            {
                //If second interact with object should reverse direction
                if (isFullyMoved)
                {
                    MoveDown();

                }
                else
                {
                    MoveUp();
                }

                isFullyMoved = !isFullyMoved;
            }
            else
            {
                //If second interact with object is same direction
                MoveUp();
            }
        }
    }

    public void MoveDown()
    {
        Vector3 target = transform.position - new Vector3(0, distanceToMove, 0);
        move = true;
        StartCoroutine(MoveToPosition(transform, target, 1));
    }

    public void MoveUp()
    {
        Vector3 target = transform.position + new Vector3(0, distanceToMove, 0);
        move = true;
        StartCoroutine(MoveToPosition(transform, target, 1));
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            if (stopCoroutine)
            {
                break;
            }
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
        move = false;
    }

    /// <summary>
    /// Moves the object along x-axis when invoked. Moves object back with second invoke or continues moving in same direction.
    /// </summary>
    public void MoveSideway()
    {
        if (!move)
        {
            if (reverseMovementSecondInteract)
            {
                if (isFullyMoved)
                {
                    MoveNegX();

                }
                else
                {
                    MovePosX();
                }

                isFullyMoved = !isFullyMoved;
            }
            else
            {
                MovePosX();
            }
        }
        
    }

    public void MoveNegX()
    {
        Vector3 target = transform.position - new Vector3(distanceToMove, 0, 0);
        move = true;
        StartCoroutine(MoveToPosition(transform, target, 1));
    }

    public void MovePosX()
    {
        Vector3 target = transform.position + new Vector3(distanceToMove, 0, 0);
        move = true;
        StartCoroutine(MoveToPosition(transform, target, 1));
    }
}
