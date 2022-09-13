using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using System.IO;
using System;
using UnityEngine.Networking;
using System.Threading;
using System.Threading.Tasks;
namespace Assets.Scripts.GameScene
{
    
    public class Loader : MonoBehaviour
    {
        [SerializeField] SO_Assets soLoad;
        [SerializeField] private Image _loadBarImage;
        [SerializeField] private TMPro.TextMeshProUGUI _loadProgressText;
        public static AssetBundle bundle = null; 
        public string AssetName; 
        string urlBundle = "https://drive.google.com/uc?export=download&id=1-ILyvZa4Mz-BixIttv9AI55VKyW6fkxV";
        string urlManifest = "https://drive.google.com/uc?export=download&id=1-EMO8Orudt6pMD90xvoJTZHp1Ezu2t8N";
        Hash128 hashBundle =   Hash128.Parse("628c24b67e1048e56f5c9e21eb27781e")  ;
        Action<float> progress;
        float progressValue;
        System.Threading.CancellationTokenSource cancellation;
        bool isBunddleLoaded = false;

        // Start is called before the first frame update
        async void Start()
        {
            progress += SetProgresValue;
            //_bLoader = BoundlessLoader.GetLoader();
            //LOCAL BUNDLE WORKING
           
           await WebRequestData(); 
        }
        void SetProgresValue(float value)
        {
            progressValue = value;
        }
        private void Update()
        {
            if (bundle == null)
            {
                _loadProgressText.text = (progressValue * 100).ToString();
                _loadBarImage.fillAmount = progressValue;
            }else if(bundle != null && progressValue != 1)
            {
                progressValue = Mathf.MoveTowards(progressValue, 1, 0.01f);
                _loadProgressText.text = ((int)(progressValue * 100)).ToString();
                _loadBarImage.fillAmount = progressValue;
            }

            if(progressValue == 1 && isBunddleLoaded)
            {
                LoadScene();
            }
            
        }
       
        async Task WebRequestData()
        {
            Thread.Sleep(3000);
            
            bundle = await Network.GetAssetBundle(urlBundle, urlManifest, cancellation, progress, true) ;
            
            
            if(bundle != null)
            {
                string[]  names;
                names = bundle.GetAllAssetNames();
                foreach(string name in names) Debug.Log(name);
                Debug.Log("Download finished");
                isBunddleLoaded = true;
                soLoad.loadedAsset = bundle;
            }
        }

        void LoadScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}