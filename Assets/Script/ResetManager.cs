using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResetManager
{
	public static void ResetGame()
	{
		FailCounter.Count = 0;
		TimeIndicator.TotalTime = 0;
	} 
}
