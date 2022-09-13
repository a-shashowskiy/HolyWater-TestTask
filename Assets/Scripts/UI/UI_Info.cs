using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Info : MonoBehaviour, IWindow
{
    public Action openInfo;
    public Action closeInfo; 
    CanvasGroup _canGr;
    [SerializeField] Button _closeBtn;
    bool _isOpen;

    public void Init(UI_Manager m)
    {
        _canGr = GetComponent<CanvasGroup>();
        _closeBtn.onClick.AddListener(Close); 
    }

    void Update()
    {
        if (_isOpen && _canGr.alpha <= 1)
        {
            _canGr.alpha += 0.01f;
        }
        if(!_isOpen && _canGr.alpha > 0)
        {
            _canGr.alpha -= 0.01f;
        }
    }

    // Update is called once per frame
    public void OpenURL(string url)
    {
        Application.OpenURL(url);           
    }

    public void Open()
    {
        if(!_isOpen)openInfo.Invoke();
        _isOpen = true;
        _canGr.interactable = true;
        _canGr.blocksRaycasts = true;
    }
    void OpenWindow()
    {
        _isOpen = true;
        _canGr.interactable = true;
        _canGr.blocksRaycasts = true;
    }
    void CloseWindow()
    {
        _isOpen = false;
        _canGr.interactable = false;
        _canGr.blocksRaycasts = false;
    }
    public void Close()
    {
        if(_isOpen)closeInfo.Invoke();
        _isOpen = false;
        _canGr.interactable = false;
        _canGr.blocksRaycasts = false;
    }
}
