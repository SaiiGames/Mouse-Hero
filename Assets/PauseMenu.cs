using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private GameObject pauseMenu, pauseIndicator, pauseIndicatorImage,pauseMenuConfirmText,Hint,pauseMenuRestart,pauseMenuQuit;
	
	
	[SerializeField] private bool indicatorPhase = true,menuPhase=false,isMiddleButtonPressing = false,inHintSequence=false;
	public float pressDuration = 3,pressedDuration = 0;
	private int restartConfirmNumber = 0;
	private bool readyRestart = false, readyQuit = false;
	private void Start()
	{
		StartCoroutine(HintSequence());
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !inHintSequence)
		{
			StartCoroutine(HintSequence());
		}
		if (indicatorPhase)
		{
			UpdateIndicator();
		}
		if (menuPhase)
		{
			UpdateMenu();
		}
		
	}

	IEnumerator HintSequence()
	{
		inHintSequence = true;
		Hint.SetActive(true);
		Hint.GetComponent<TextMeshProUGUI>().alpha = 1f;
		Hint.GetComponent<TextMeshProUGUI>().DOFade(0f, 3f);
		yield return new WaitForSeconds(3f);
		Hint.SetActive(false);
		inHintSequence = false;
	}
	void UpdateMenu()
	{
		//0 resume 1 quit 2 restart
		if (Input.GetKeyDown(KeyCode.Mouse2))
		{
			if (restartConfirmNumber == 0)
			{
				pauseMenuConfirmText.SetActive(true);
				restartConfirmNumber++;
				pauseMenuQuit.SetActive(false);
				readyRestart = true;
			}
			else if (restartConfirmNumber == 1 && readyRestart)
			{
				Time.timeScale = 1f;
				ResetManager.ResetGame();
				SceneManager.LoadScene(1);
			}
			
		}
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			pauseMenuConfirmText.SetActive(false);
			restartConfirmNumber = 0;
			pressedDuration = 0;
			isMiddleButtonPressing = false;
			menuPhase = false;
			indicatorPhase = true;
			readyQuit = false;
			readyRestart = false;
			pauseMenuQuit.SetActive(true);
			pauseMenuRestart.SetActive(true);

			Time.timeScale = 1f;
			pauseMenu.SetActive(false);
			
		}
		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			if (restartConfirmNumber == 0)
			{
				pauseMenuRestart.SetActive(false);
				pauseMenuConfirmText.SetActive(true);
				restartConfirmNumber++;
				readyQuit = true;

			}
			else if (restartConfirmNumber == 1 && readyQuit)
			{
				Application.Quit();
			}
		}
	}
	
	void UpdateIndicator()
	{
		if (isMiddleButtonPressing)
		{
			pressedDuration += Time.deltaTime;
			pauseIndicatorImage.GetComponent<Image>().fillAmount = pressedDuration/pressDuration;
			if (pressedDuration > pressDuration)
			{
				pauseMenu.SetActive(true);
				indicatorPhase = false;
				menuPhase = true;
				Time.timeScale = 0f;
				pauseIndicator.SetActive(false);
				
			}
		}
		else
		{
			if (pressedDuration < 0)
			{
				pauseIndicator.SetActive(false);
			}
			else
			{
				pressedDuration -= Time.deltaTime;
				pauseIndicatorImage.GetComponent<Image>().fillAmount = pressedDuration/pressDuration;
			}
			
		}
		
		if (Input.GetKeyDown(KeyCode.Mouse2))
		{
			pauseIndicator.SetActive(true);
			pressedDuration = 0.1f;
			isMiddleButtonPressing = true;

		}
		else if (Input.GetKeyUp(KeyCode.Mouse2))
		{
			isMiddleButtonPressing = false;
		}
	}
}
