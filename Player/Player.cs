using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] float maxHitPoints = 100f;
	float currentHitpoints = 100f;

	public float healthAsPercentage 
	{
		get 
		{
			return currentHitpoints / maxHitPoints;
		}
	}
}
