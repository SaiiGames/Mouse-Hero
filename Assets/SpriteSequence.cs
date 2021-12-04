using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSequence : MonoBehaviour
{
	public Sprite[] Sprites;
	public Image _image;
	public int index = 0;
	public int maxIndex;
	public float frequency = 1f;
	private float aniamtionTime;
	public bool overrideTimeScale = false;
	public bool useTransition;

	private void Awake()
	{
		maxIndex = Sprites.Length - 1;
		_image = GetComponent<Image>();
	}
	public virtual void Start()
	{
		if (maxIndex < 0)
		{
			Debug.Log("Sprite missing");
			return;
		}
		if(!overrideTimeScale) InvokeRepeating(nameof(ChangeSprite),frequency,frequency);
		else
		{
			aniamtionTime = 0;
		}
	}

	public virtual void Update()
	{
		
		if (overrideTimeScale)
		{
			OverrideChangeSprite();
		}
	}

	void OverrideChangeSprite()
	{
		if (aniamtionTime <= frequency)
		{
			aniamtionTime += Time.unscaledDeltaTime;
		}
		else
		{
			aniamtionTime = 0;
			ChangeSprite();
		}
	}

	public void ChangeSprite()
	{
		if (index < maxIndex)
		{
			index++;
		}
		else
		{
			index = 0;
		}
		
		if (useTransition)
		{
			StartCoroutine(Transition());
		}
		else
		{
			_image.sprite = Sprites[index];
		}
	}
	IEnumerator Transition()
	{
		_image.DOFade(0, 0.2f);
		yield return new WaitForSeconds(0.2f);
		_image.sprite = Sprites[index];
		_image.DOFade(1f, 0.2f);

	}
}
