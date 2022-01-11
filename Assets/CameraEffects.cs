using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
	public void ShakeCamera()
	{
		Debug.Log("try to shake camera");
		DOTween.To(()=> GetComponent<CinemachineFramingTransposer>().m_ScreenX, x=> GetComponent<CinemachineFramingTransposer>().m_ScreenX = x, 0.4f, 0.2f).SetLoops(1,LoopType.Yoyo);
		
	}
}
