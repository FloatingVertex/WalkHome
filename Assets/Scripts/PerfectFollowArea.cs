using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PositionVelocity
{
    public Vector3 position;
    public Vector3 velocity;

    public PositionVelocity(Vector3 position,Vector3 velocity)
    {
        this.position = position;
        this.velocity = velocity;
    }
}

public class PerfectFollowArea : MonoBehaviour {

    private float snapDistance = 0.2f;

    private List<List<PositionVelocity>> playerPaths = new List<List<PositionVelocity>>();
    private List<PositionVelocity> CurrentPath
    {
        get
        {
            if (playerPaths.Count > 0)
            {
                return playerPaths[playerPaths.Count - 1];
            }
            else
            {
                return null;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            var currentPath = new List<PositionVelocity>();
            playerPaths.Add(currentPath);
            AddPosition(collision);
        }
    }

    private void DrawDebugCrossHair(Vector3 position, Color color, float size = 0.5f, float durration = 0.1f)
    {
        Debug.DrawLine(position + Vector3.down * size * .5f, position + Vector3.up * size * .5f, color, durration);
        Debug.DrawLine(position + Vector3.left * size * .5f, position + Vector3.right * size * .5f, color, durration);
    }

    private void AddPosition(Collider2D collision)
    {
        if (CurrentPath != null)
        {
            DrawDebugCrossHair(collision.transform.position, Color.cyan,0.5f,10f);
            CurrentPath.Add(new PositionVelocity(collision.transform.position, collision.GetComponent<Rigidbody2D>().velocity));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            AddPosition(collision);
        }
        if (collision.GetComponent<AiController>())
        {
            SetToNextPosition(collision, false);
        }
    }

    private void SetToNextPosition(Collider2D collision,bool setVelocity)
    {
        int pathIndex = 0;
        int positionIndex = 0;
        if (GetClosestPosition(collision.transform.position, snapDistance, out pathIndex, out positionIndex))
        {
            collision.GetComponent<Character>().tempMoveSpeedMultiple = 0f;
            collision.GetComponent<Character>().tempJumpForceMultiple = 0f;
            if (playerPaths[pathIndex].Count > positionIndex + 1)
            {
                positionIndex += 1;
            }
            var values = playerPaths[pathIndex][positionIndex];
            collision.transform.position = values.position;
            Debug.Log("AI position set, pathID: "+pathIndex+" positionID: " +positionIndex + " Path length: " + playerPaths[pathIndex].Count);
            if (setVelocity)
            {
                collision.GetComponent<Rigidbody2D>().velocity = values.velocity;
            }
            else
            {
                collision.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
        }
    }

    /// <summary>
    /// return true if a near enough position was found
    /// </summary>
    /// <returns></returns>
    private bool GetClosestPosition(Vector3 position, float maxDistance, out int pathIndex, out int positionIndex)
    {
        DrawDebugCrossHair(position, Color.blue,durration:.04f);
        var maxDistanceSqr = maxDistance * maxDistance;
        bool foundValue = false;
        pathIndex = 0;
        positionIndex = 0;
        for(int pathI = 0; pathI < playerPaths.Count; pathI++)
        {
            var path = playerPaths[playerPaths.Count - 1 - pathI];
            float smallestDiffSqr = float.MaxValue;
            for (int positionI = 0; positionI < path.Count; positionI++)
            {
                var pathPosition = path[path.Count - 1 - positionI].position;
                float diffSqr = (position - pathPosition).sqrMagnitude;
                if (smallestDiffSqr * 0.9f > diffSqr && maxDistanceSqr > diffSqr)
                {
                    pathIndex = playerPaths.Count - 1 - pathI;
                    positionIndex = path.Count - 1 - positionI;
                    smallestDiffSqr = (position - pathPosition).sqrMagnitude;
                    foundValue = true;
                }
            }
            if(foundValue)
            {
                return foundValue;
            }
        }
        return false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            AddPosition(collision);
        }
        if (collision.GetComponent<AiController>())
        {
            SetToNextPosition(collision, false);
        }
        if (collision.GetComponent<AiController>())
        {
            collision.GetComponent<Character>().tempMoveSpeedMultiple = 1f;
            collision.GetComponent<Character>().tempJumpForceMultiple = 1f;
        }
    }
}
