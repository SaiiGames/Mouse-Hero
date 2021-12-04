using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RecordSprite : SpriteSequence
{
	public Sprite stop;
	private bool stopRecord = false;
	public override void Start()
	{
		if (maxIndex < 0)
		{
			Debug.Log("Sprite missing");
			return;
		}
		InvokeRepeating(nameof(ChangeSprite),frequency,frequency);
	}

	public override void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			if (!stopRecord)
			{
				CancelInvoke(nameof(ChangeSprite));
				StartCoroutine(StopSequence());
				stopRecord = true;
			}
			else
			{
				StopCoroutine(StopSequence());
				stopRecord = false;
				_image.sprite = Sprites[0];
				index = 0;
				InvokeRepeating(nameof(ChangeSprite),frequency,frequency);
			}
			
		}
		
	}

	IEnumerator StopSequence()
	{
		_image.DOFade(0, 0.2f);
		yield return new WaitForSeconds(0.2f);
		_image.sprite = stop;
		_image.DOFade(1f, 0.2f);
	}
}
