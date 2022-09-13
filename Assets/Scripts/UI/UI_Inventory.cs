using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

using System.IO;

public class UI_Inventory : MonoBehaviour, IWindow
{
    [SerializeField] private CanvasGroup layoutGroupTop;
    [SerializeField] private CanvasGroup layoutGroupBottom;
    [SerializeField] private Scrollbar sliderVert;
    [SerializeField] private Transform contentSpawnPlace;
    [SerializeField] private Assets.Scripts.GameScene.SO_Assets soLoad;
    [SerializeField] private int cardItemSpawnCount = 25;
    [SerializeField] private Button RefillButton;
    [SerializeField] private ContentSizeFitter filterSize;
    [SerializeField] private RectTransform contentSize;
    public int leftCardFromSave = 0;
   public static Action desroyRearange;
   GameObject[] spawnableCards;
    void LoadAsset()
    {
        spawnableCards = new GameObject[12];
        for (int i = 0; i < spawnableCards.Length; i++)
        {
            GameObject cardBoundle = soLoad.loadedAsset.LoadAsset<GameObject>("Card_"+i);
            spawnableCards[i] = cardBoundle;
        }

        if (leftCardFromSave > 0)
        {
            for (int i = 0; i < leftCardFromSave; i++)
            {
                int r = UnityEngine.Random.Range(0, spawnableCards.Length - 1);

                if (spawnableCards[r] != null)
                {
                    Instantiate(spawnableCards[r], contentSpawnPlace);
                }
            }
        }
        else
        {
            for (int i = 0; i < cardItemSpawnCount; i++)
            {
                int r = UnityEngine.Random.Range(0, spawnableCards.Length - 1);

                if (spawnableCards[r] != null)
                {
                    Instantiate(spawnableCards[r], contentSpawnPlace);
                }
            }
        }
        
        RearangeWindowContent();
        RearangeDelay();
    }

    void RefillCardToMax()
    {
        UI_Card[] leftCard = GetComponentsInChildren<UI_Card>();

        for (int i = leftCard.Length; i < cardItemSpawnCount; i++)
        {
            int r = UnityEngine.Random.Range(0, spawnableCards.Length - 1);

            if (spawnableCards[r] != null)
            {
                Instantiate(spawnableCards[r], contentSpawnPlace);
            }
        }
       RearangeWindowContent();
    }
    public int ReturnLeftCard()
    {
        UI_Card[] leftCard = GetComponentsInChildren<UI_Card>();
        return leftCard.Length;
    }
     void RearangeWindowContent()
    {
        StartCoroutine(RearangeDelay());
        contentSize.sizeDelta = new Vector2(contentSize.sizeDelta.x, contentSpawnPlace.GetComponent<RectTransform>().sizeDelta.y + 440);
    }
    IEnumerator RearangeDelay()
    {
        filterSize.enabled=false;
        yield return new WaitForSeconds(0.25f);
        filterSize.enabled = true;
        yield return new WaitForSeconds(0.25f);
        filterSize.enabled = false;
    }
   public void Close()
    {
        layoutGroupTop.interactable = false;
        layoutGroupBottom.interactable = false;
        sliderVert.interactable = false;
    }

    public void Init(UI_Manager m)
    {
        desroyRearange += RearangeWindowContent;
        if(m.save != null) leftCardFromSave =  m.save.cardsLeft;
        LoadAsset();
        RefillButton.onClick.AddListener(RefillCardToMax); 
    }

    public void Open()
    {
        layoutGroupTop.interactable = true;
        layoutGroupBottom.interactable = true;
        sliderVert.interactable = true;
    }

    
}
