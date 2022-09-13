using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour, IWindow
{
    public Action openSettigs;
    public Action closeSettigs;
    private UI_Manager _manager; 
    public Button close;
    public Button infoOpen;
    public Toggle vibroToggle;
    public Toggle soundToggle; 
    public AudioSource musickAS;

    private CanvasGroup _canGr;
    bool _isOpen;
    public bool vibroActive { get; set; } 
    public bool soundActive { get; set; }


    public void Init(UI_Manager mg)
    { 
        _manager = mg;
        _canGr = GetComponent<CanvasGroup>();
        close.onClick.AddListener(Close);
        infoOpen.onClick.AddListener(_manager.infoWind.Open);
        if (_manager.save != null)
        {
            soundActive = _manager.save.sound;
            soundToggle.isOn = soundActive;
            vibroActive = _manager.save.vibro;
            vibroToggle.isOn = vibroActive;
           if(soundActive) musickAS.volume = 1;
           else musickAS.volume = 0;
        }
        else
        {
            musickAS.volume = 1;
            soundToggle.isOn = true;
            vibroToggle.isOn = true;
        }
        Close();
    }


    public void Close()
    {
       if(_isOpen) closeSettigs.Invoke();
        _isOpen = false;
        _canGr.interactable = false;
        _canGr.blocksRaycasts = false;
    }
    void CloseWindow()
    {
        _isOpen = false;
        _canGr.interactable = false;
        _canGr.blocksRaycasts = false;
    }
    public void Open()
    {
        if(!_isOpen)openSettigs.Invoke();
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
    void Update()
    {
        if(soundActive && musickAS != null && musickAS.volume <1)
        {
            musickAS.volume += 0.01f;
        }
        if(!soundActive && musickAS != null && musickAS.volume >= 0)
        {
            musickAS.volume -= 0.01f;
        }
        if (_isOpen && _canGr.alpha <= 1)
        {
            _canGr.alpha += 0.01f;
        }
        if (!_isOpen && _canGr.alpha > 0)
        {
            _canGr.alpha -= 0.01f;
        }
    } 
}
