using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IconSwitchScene : MonoBehaviour
{
	public Sprite alterSprite;
	public float waitDuration;
	private Image image;
	public Image fadeoutImage;
	private bool readyForSwitch = false;
	public bool fadeOutMusic = false;
	public int switchToSceneIndex;
	[Range(1.5f,6f)]
	public float cutsceneDuration = 4f;
	public AudioSource backgroundMusic;

	private GameObject iconWhileWait;
	private void Awake()
	{
		image = GetComponent<Image>();
		iconWhileWait = transform.GetChild(0).gameObject;
	}

	private void Start()
	{
		StartCoroutine(WaitBeforeProceed());
		try
		{
			backgroundMusic = GameObject.FindWithTag("Music").GetComponent<AudioSource>();
		}
		catch (Exception e)
		{
			Debug.Log("Missing Music Object!");
		}
	}

	private void FixedUpdate()
	{
		if (image.fillAmount <= 1)
		{
			image.fillAmount += Time.deltaTime / waitDuration;
		}
		if (readyForSwitch)
		{
			if (Input.GetKey(KeyCode.Mouse2))
			{
				StartCoroutine(WaitAndSwitch());
				CancelInvoke(nameof(EmphasisIcon));
				transform.DOScale(new Vector3(0f, 0f, 0f), 0.5f);
			}
		}
	}
	IEnumerator WaitBeforeProceed()
	{
		yield return new WaitForSeconds(waitDuration);
		iconWhileWait.SetActive(false);
		readyForSwitch = true;
		GetComponent<Image>().sprite = alterSprite;
		EmphasisIcon();
		InvokeRepeating(nameof(EmphasisIcon),3f,3f);

	}

	public void EmphasisIcon()
	{
		transform.DOPunchScale(new Vector3(0.5f, 0.5f), 0.5f, 0, 1);

	}

	IEnumerator WaitAndSwitch()
	{
		GetComponent<AudioSource>().Play();
		if (fadeOutMusic)
		{
			backgroundMusic.DOFade(0f, 1f);
			Destroy(backgroundMusic,1.1f);
		}
		fadeoutImage.DOFade(1f, cutsceneDuration);
		yield return new WaitForSeconds(cutsceneDuration);
		SceneManager.LoadScene(switchToSceneIndex);
	}
}
