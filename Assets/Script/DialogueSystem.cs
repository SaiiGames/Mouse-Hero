using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class DialogueSystem : MonoBehaviour
{
	public DialogueSO OnFallDialogueSo,OnElevateStageSo;
	private TextMeshPro _textMeshPro;
	private int lastID;
	public float waitBeforeShow = 1f, waitBeforeHide = 4f;

	private bool isPlayingDialoge = false;
	private Coroutine PlayingElevateCoroutine;

	private bool inElevateStageSequence;
	private void Awake()
	{
		_textMeshPro = GetComponent<TextMeshPro>();
	}


	private void KillDialogueCoroutine(Coroutine coroutine)
	{
		if(coroutine != null) StopCoroutine(coroutine);
		isPlayingDialoge = false;
		_textMeshPro.text = "";
		

	}
	private void Update()
	{
		if (!isPlayingDialoge && !inElevateStageSequence)
		{
			var guess = Random.Range(0, 1000);
			if (guess > 999)
			{
				Debug.Log("chitchat");
			}

		}
	}

	public void OnElevateStage()
	{
		if (StageSystem.currentStage > 1)
		{
			inElevateStageSequence = true;
			StartCoroutine(CheckElevateStageSequence());
		}

	}
	IEnumerator ConfirmElevateStage()
	{
		var targetID = Random.Range(0, OnElevateStageSo.dialogues.Length );
		if (!isPlayingDialoge)
		{
			PlayingElevateCoroutine = StartCoroutine(PutText(OnElevateStageSo,targetID));
		}
		yield return new WaitUntil(() => isPlayingDialoge == false);
		inElevateStageSequence = false;
	}
	
	private void FailedElevateStage()
	{
		
	}
	
	IEnumerator CheckElevateStageSequence()
	{
		var currentStage = StageSystem.currentStage;
		yield return new WaitForSeconds(2f);
		if (StageSystem.currentStage >= currentStage)
		{
			StartCoroutine(ConfirmElevateStage());
		}
		else
		{
			FailedElevateStage();
		}
	}
	

	public void OnFall()
	{
		if (inElevateStageSequence)
		{
			StopCoroutine(CheckElevateStageSequence());
			if (isPlayingDialoge)
			{
				KillDialogueCoroutine(PlayingElevateCoroutine);
			}
		}
		
		var targetID = Random.Range(0, OnFallDialogueSo.dialogues.Length );
		// Debug.Log(targetID);
		if (!isPlayingDialoge)
		{
			StartCoroutine(PutText(OnFallDialogueSo,targetID));
		}
	}

	private IEnumerator PutText(DialogueSO _dialogue,int _targetID)
	{
		isPlayingDialoge = true;
		// Debug.Log(targetID);
		yield return new WaitForSeconds(waitBeforeShow);
		_textMeshPro.text = _dialogue.dialogues[_targetID];
		yield return new WaitForSeconds(waitBeforeHide);
		_textMeshPro.text = "";
		isPlayingDialoge = false;

	}
	
}
