using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimeIndicator : MonoBehaviour
{

    public GameObject[] StageIndicators;
    public static float TotalTime;
    public static float[] stageTime;
    public TextMeshProUGUI totalTimeIndicator;

    private TextMeshProUGUI currentStageIndicator;

    private float playerPos;

    public float fadeAlpha = 0.5f;

    [SerializeField]public static bool EnableAntiBurn = true;

    private void Awake()
    {
        stageTime = new float[StageSystem.StageCeiling.Length];

    }
    private void Start()
    {
        
        PutTimeOntoText(totalTimeIndicator, TotalTime);
        currentStageIndicator = StageIndicators[StageSystem.currentStage].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        StageIndicators[StageSystem.currentStage].GetComponent<CanvasGroup>().DOFade(1f, 1f);
        
    }

    private void Update()
    {
        TotalTime += Time.deltaTime;

        if (StageSystem.lastStage != StageSystem.currentStage)
        {
            StageIndicators[StageSystem.lastStage].GetComponent<CanvasGroup>().DOFade(fadeAlpha, 1f);
            StageIndicators[StageSystem.currentStage].GetComponent<CanvasGroup>().DOFade(1f, 1f);
            currentStageIndicator = StageIndicators[StageSystem.currentStage].transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        }
        stageTime[StageSystem.currentStage] += Time.deltaTime;
        PutTimeOntoText(totalTimeIndicator, TotalTime);
        PutTimeOntoText(currentStageIndicator, stageTime[StageSystem.currentStage]);

        

    }

    private void PutTimeOntoText(TextMeshProUGUI textMeshPro, float time)
    {
        if (EnableAntiBurn)
        {
            textMeshPro.text = time.ToString("F0"); 
        }
        else
        {
            textMeshPro.text = time.ToString("F2");
        }
    }


}
