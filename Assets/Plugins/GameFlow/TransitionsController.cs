using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionsController : MonoBehaviour
{

    [SerializeField]
    Image _fadeImage;

    [SerializeField] private RectTransform _circleTransform;
    [SerializeField] private Image[] _circleImage;
    // [SerializeField] private Transform _lookAt;
    // [SerializeField] private Camera _renderCamera;

    float _fadeTime = 1f;

    public void Start()
    {
        FadeFromBlack();
    }

    public IEnumerator coFadeToBlack()
    {
        circleSetEnabled(true);
        for (float i = 0; i < _fadeTime; i += Time.deltaTime)
        {
            _circleTransform.localScale = Vector3.Lerp(Vector3.one * 15f, Vector3.one * 0.1f, i/_fadeTime);
            yield return 0;
        }
        _circleTransform.localScale = Vector3.one * 0.1f;
    }
    public void FadeToBlack(float time)
    {
        StartCoroutine(coFadeToBlack(time));
    }
    public IEnumerator coFadeToBlack(float t)
    {
        circleSetEnabled(true);
        for (float i = 0; i < t; i += Time.deltaTime)
        {
            _circleTransform.localScale = Vector3.Lerp(Vector3.one * 15f, Vector3.one * 0.1f, i/t);
            yield return 0;
        }
        _circleTransform.localScale = Vector3.one * 0.1f;
    }

    public void FadeFromBlack()
    {
        StartCoroutine(coFadeFromBlack());
    }

    public IEnumerator coFadeFromBlack()
    {
        circleSetEnabled(true);
        for (float i = 0; i < _fadeTime; i += Time.deltaTime)
        {
            _circleTransform.localScale = Vector3.Lerp(Vector3.one * 0.1f, Vector3.one * 15f, i/_fadeTime);
            yield return 0;
        }
        _circleTransform.localScale = Vector3.one * 15f;
        circleSetEnabled(false);

    }
    public void FadeFromBlack(float time)
    {
        StartCoroutine(coFadeFromBlack(time));
    }

    public IEnumerator coFadeFromBlack(float t)
    {
        circleSetEnabled(true);
        for (float i = 0; i < t; i += Time.deltaTime)
        {
            // Escalar
            _circleTransform.localScale = Vector3.Lerp(Vector3.one * 0.1f, Vector3.one * 15f, i/t);
            yield return 0;
        }
        _circleTransform.localScale = Vector3.one * 15f;
        circleSetEnabled(false);

    }

    private void circleSetEnabled(bool value)
    {
        // _circleTransform.anchoredPosition = _renderCamera.WorldToScreenPoint(_lookAt.position);
        foreach (var image in _circleImage)
        {
            image.enabled = value;
        }
    }


    #region Default Fade

    // public IEnumerator coFadeToBlack()
    // {
    //     _fadeImage.enabled = true;
    //     for (float i = 0; i < _fadeTime; i += Time.deltaTime)
    //     {
    //         _fadeImage.color = Color.Lerp(Color.clear, Color.black, i / _fadeTime);
    //         yield return 0;
    //     }
    //     _fadeImage.color = Color.black;
    // }
    // public void FadeToBlack(float time)
    // {
    //     StartCoroutine(coFadeToBlack(time));
    // }
    // public IEnumerator coFadeToBlack(float t)
    // {
    //     _fadeImage.enabled = true;

    //     for (float i = 0; i < t; i += Time.deltaTime)
    //     {
    //         _fadeImage.color = Color.Lerp(Color.clear, Color.black, i / t);
    //         yield return 0;
    //     }
    //     _fadeImage.color = Color.black;
    // }

    // public void FadeFromBlack()
    // {
    //     StartCoroutine(coFadeFromBlack());
    // }

    // public IEnumerator coFadeFromBlack()
    // {
    //     _fadeImage.enabled = true;
    //     for (float i = 0; i < _fadeTime; i += Time.deltaTime)
    //     {
    //         _fadeImage.color = Color.Lerp(Color.black, Color.clear, i / _fadeTime);
    //         yield return 0;
    //     }
    //     _fadeImage.color = Color.clear;
    //     _fadeImage.enabled = false;

    // }
    // public void FadeFromBlack(float time)
    // {
    //     StartCoroutine(coFadeFromBlack(time));
    // }

    // public IEnumerator coFadeFromBlack(float t)
    // {
    //     _fadeImage.enabled = true;
    //     for (float i = 0; i < t; i += Time.deltaTime)
    //     {
    //         _fadeImage.color = Color.Lerp(Color.black, Color.clear, i / t);
    //         yield return 0;
    //     }
    //     _fadeImage.color = Color.clear;
    //     _fadeImage.enabled = false;

    // }

    #endregion

}

