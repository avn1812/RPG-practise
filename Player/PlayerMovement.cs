using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] float walkMoveStopRadius = 0.2f, walkAttackStopRadius = 3f;

    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination, clickPoint;

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        ProcessMouseMovement();
    }

	void WalkToDestination ()
	{
		var playerToClickPoint = currentDestination - transform.position;
		if (playerToClickPoint.magnitude >= 0) {
			thirdPersonCharacter.Move (playerToClickPoint, false, false);
		}
		else {
			thirdPersonCharacter.Move (Vector3.zero, false, false);
		}
	}

	public Vector3 ShortenDestination (Vector3 destination, float shortenDistance)
	{
		Vector3 shortDistance = (destination - transform.position).normalized * shortenDistance;
		return destination - shortDistance;
	}

    private void ProcessMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
			clickPoint = cameraRaycaster.hit.point;
            switch (cameraRaycaster.currentLayerHit)
            {
                case Layer.Walkable:
					currentDestination = ShortenDestination(clickPoint, walkMoveStopRadius);
                    break;
                case Layer.Enemy:
					currentDestination = ShortenDestination(clickPoint, walkAttackStopRadius);
                    break;
                default:
                    print("Unexpected layer found");
                    return;
            }
        }
        WalkToDestination ();
    }

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.white;
		Gizmos.DrawLine (clickPoint, transform.position);
		Gizmos.DrawSphere (clickPoint, 0.1f);

		Gizmos.DrawSphere (currentDestination, 0.1f);
		Gizmos.DrawWireSphere (transform.position, walkAttackStopRadius);
	}
}

