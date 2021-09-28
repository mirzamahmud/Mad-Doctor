// this script will handle our camera, so that it will follow our whole gameplay

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTarget;

    [SerializeField]
    private float smoothSpeed = 2f;

    [SerializeField]
    private float playerBoundMin_Y = -1, playerBoundMin_X = -65f, playerBoundMax_X = 65f; // this is up position where camera will follow our player

    [SerializeField]
    private float Y_Gap = 2f; // this is the gap between the player and the camera

    private Vector3 tempPosition;

    private void Start()
    {
        playerTarget = GameObject.FindWithTag(TagManager.Player_Tag).transform;
    }

    private void Update()
    {
        if (!playerTarget)
        {
            return;
        }

        tempPosition = transform.position;

        if (playerTarget.position.y <= playerBoundMin_Y)
        {
            tempPosition = Vector3.Lerp(transform.position, new Vector3(playerTarget.position.x, playerTarget.position.y, -10f), Time.deltaTime * smoothSpeed);
        }
        else
        {
            tempPosition = Vector3.Lerp(transform.position, new Vector3(playerTarget.position.x, playerTarget.position.y + Y_Gap, -10f), Time.deltaTime * smoothSpeed);
        }

        if(tempPosition.x > playerBoundMax_X)
        {
            tempPosition.x = playerBoundMax_X;
        }

        if(tempPosition.x < playerBoundMin_X)
        {
            tempPosition.x = playerBoundMin_X;
        }

        transform.position = tempPosition;
    }
}
