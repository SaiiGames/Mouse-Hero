using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraBackground : MonoBehaviour
{
	public SpriteRenderer ForestBackground, SnowBackground;

	public void OnStageChange()
	{
		if (StageSystem.currentStage >= 4)
		{
			SnowBackground.DOFade(1f, 1f);
			ForestBackground.DOFade(0f, .4f);
		}
		else
		{
			SnowBackground.DOFade(0f, .4f);
			ForestBackground.DOFade(1f, 1f);		}
	}
}
