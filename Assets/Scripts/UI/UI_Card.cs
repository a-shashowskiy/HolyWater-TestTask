using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Card : MonoBehaviour
{
    public ParticleSystem destroyParticle;
    private CanvasGroup _canGr;
    private bool _isDestroy;
    private bool spawnParticle;
    private Button _clikObjectButton;
    private void Start()
    {
        _clikObjectButton = GetComponent<Button>();
        _clikObjectButton.onClick.AddListener(DestroyCard);
    }
    public void DestroyCard()
    {
        _isDestroy = true;
        _canGr = GetComponent<CanvasGroup>();
    }
    void Update()
    {
        if (!_isDestroy) return;
        else
        {
            if (!spawnParticle)
            {
                spawnParticle = true;
                Instantiate(destroyParticle, transform.position, Quaternion.identity);
            }
            _canGr.alpha -= 0.1f;
            Destroy(gameObject, 1); 
        }
    }
    private void OnDestroy()
    {
        UI_Inventory.desroyRearange?.Invoke(); 
    }
}
