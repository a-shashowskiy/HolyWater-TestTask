using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public UI_Settings setingWind;
    public UI_Info infoWind;
    public UI_Inventory inventoryWind;
    public GameSave save;
    [SerializeField] private Button closeAplicationBtn;
    [SerializeField] private Button settingBth;
    Hash128 hashBundle = Hash128.Parse("628c24b67e1048e56f5c9e21eb27781e");
    // Start is called before the first frame update
    void Start()
    {
        save = SaveToJSON.LoadData();
        IWindow[] listOb = GetComponentsInChildren<IWindow>(); 

        foreach (IWindow window in listOb)
        {
            window.Init(this);
        }

        closeAplicationBtn.onClick.AddListener(CloseAplication);
        settingBth.onClick.AddListener(setingWind.Open);

        setingWind.openSettigs += inventoryWind.Close;
        setingWind.closeSettigs += inventoryWind.Open;
        infoWind.openInfo += setingWind.Close;
        infoWind.closeInfo  += inventoryWind.Open;


        infoWind.Open(); // EVRY START OPEN INFO WINDOWS, can be updatet to show one time to checking save exist 

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CloseAplication()
    {
        save = new GameSave();
        save.vibro = setingWind.vibroActive;
        save.sound = setingWind.soundActive;
        save.cardsLeft = inventoryWind.ReturnLeftCard();

        SaveToJSON.SaveData(save);
        //TODO SAVE;
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
    }
}
