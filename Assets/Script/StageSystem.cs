using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StageSystem : MonoBehaviour
{
	[SerializeField] private GameObject[] StageCheckpoints;
	public static float[] StageCeiling;
	public static int currentStage = 1, lastStage = 1;

	[SerializeField] private UnityEvent stageChangeEvent,stageElevatedEvent,stageEscalatedEvent;
	private void Awake()
	{
		// StageCheckpoints = GameObject.FindGameObjectsWithTag("Checkpoints");
		StageCeiling = new float[StageCheckpoints.Length];
		for (int i = 0; i < StageCheckpoints.Length; i++)
		{
			StageCeiling[i] = StageCheckpoints[i].transform.position.y;
			// Debug.Log(StageCheckpoints[i].name + StageCeiling[i]);
		}
		
	}

	private void Start()
	{
		CalculatePlayerStage();
		lastStage = currentStage;
	}
	void CalculatePlayerStage()
	{
		var playerPos = Movement.instance.transform.position.y;
		for (int i = 0; i < StageCeiling.Length; i++)
		{
			if (playerPos < StageCeiling[i])
			{
				currentStage = i;
				// Debug.Log("Current Stage " + StageCheckpoints[i].name);
				break;
			}
		}
	}
	
	
	private void Update()
	{
		CheckPlayerPosition();
		
	}
	
	void CheckPlayerPosition()
	{
		var playerPos = Movement.instance.transform.position.y;

        
        
		// if (playerPos < StageCeiling[currentStage - 1])
		// {
		// 	Debug.Log(currentStage + "<" + StageCheckpoints[currentStage - 1].name);
		// 	for (int i = currentStage; i >= 0; i--)
		// 	{
		// 		if (playerPos < StageCeiling[i])
		// 		{
		// 			currentStage = i;
		// 		}
		// 	}
		// 	stageChangeEvent.Invoke();
		// }
		// else if (playerPos > StageCeiling[currentStage])
		// {
		// 	Debug.Log( currentStage + ">" + StageCheckpoints[currentStage].name);
		//
		// 	for (int i = currentStage; i < StageCeiling.Length; i++)
		// 	{
		// 		if (playerPos < StageCeiling[i])
		// 		{
		// 			currentStage = i;
		// 		}
		// 	}
		// 	stageChangeEvent.Invoke();
		// }
		
		
		// Debug.Log( "StageCeiling " + StageCeiling.Length + "CurrentStage" + currentStage);
		
		if (playerPos < StageCeiling[currentStage - 1] || playerPos > StageCeiling[currentStage])
		{
			CalculatePlayerStage();
		}
		if (currentStage > lastStage)
		{
			stageChangeEvent.Invoke();
			stageElevatedEvent.Invoke();
		}
		else if(currentStage < lastStage)
		{
			stageChangeEvent.Invoke();
			stageEscalatedEvent.Invoke();
		}
		


	}
	private void LateUpdate()
	{
		lastStage = currentStage;
	}
}
