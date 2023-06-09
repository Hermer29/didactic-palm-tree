using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvas;
    [SerializeField] private Slider _slider;

    private const float SliderSpeed = .5f;
    
    private float _targetSliderTime;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _slider.value = 0;
        _canvas.interactable = true;
        _canvas.blocksRaycasts = true;
    }

    private void Update()
    {
        _slider.value = Mathf.MoveTowards(_slider.value, _targetSliderTime, Time.deltaTime * SliderSpeed);
    }

    public void FadeIn()
    {
        StartCoroutine(ProcessCoroutine(0, (process) =>
        {
            _canvas.alpha = process;
        }));
        _canvas.interactable = true;
        _canvas.blocksRaycasts = true;
    }

    public void SetSliderProcess(float t)
    {
        _targetSliderTime = t;
    }

    private IEnumerator ProcessCoroutine(float time, Action<float> process)
    {
        float startTime = Time.time;
        float endTime = startTime + time;
        while (startTime <= endTime)
        {
            process.Invoke(Mathf.InverseLerp(startTime, endTime, Time.time));
            yield return null;
        }
        process.Invoke(1);
    }

    public void FadeOut()
    {
        StartCoroutine(ProcessCoroutine(1, (process) =>
        {
            _canvas.alpha = 1 - process;
        }));
        _canvas.interactable = false;
        _canvas.blocksRaycasts = false;
    }

    public void FadeInImmediately()
    {
        _canvas.alpha = 1;
    }
}
