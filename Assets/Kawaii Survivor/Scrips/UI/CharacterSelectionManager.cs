using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;
using Tabsil.Sijil;

public class CharacterSelectionManager : MonoBehaviour, IWantToBeSaved
{
    [Header("Elements")]
    [SerializeField] private Transform characterButtonsParent;
    [SerializeField] private CharacterButton characterButtonsPrefab;
    [SerializeField] private Image centerCharacterImage;
    [SerializeField] private Image menuCharacterImage;

    [SerializeField] private CharacterInfoPanel characterInfo;

    [Header("Data")]
    private CharacterDataSO[] characterDatas;
    private List<bool> unlockedStates = new List<bool>();
    private const string unlockedStatesKey = "unlockedStatesKey";
    private const string lastSelectedCharacterKey = "lastSelectedCharacterKey";

    [Header("Settings")]
    private int selectedCharacterIndex;
    private int lastSelectedCharacterIndex;

    [Header("Action")]
    public static Action<CharacterDataSO> onCharacterSelected;

    private void Awake()
    {


    }
    // Start is called before the first frame update
    void Start()
    {
        characterInfo.Button.onClick.RemoveAllListeners();
        characterInfo.Button.onClick.AddListener(PurchaseSelectedCharater);

        CharacterSelectedCallback(lastSelectedCharacterIndex);


    }

    private void Initialize()
    {

        for (int i = 0; i < characterDatas.Length; i++)
        {
            CreateCharacterButton(i);
        }
    }

    private void CreateCharacterButton(int index)
    {
        CharacterDataSO characterData = characterDatas[index];

        CharacterButton characterButtonInstance = Instantiate(characterButtonsPrefab, characterButtonsParent);
        characterButtonInstance.Configure(characterData.Sprite, unlockedStates[index]);

        characterButtonInstance.Button.onClick.RemoveAllListeners();
        characterButtonInstance.Button.onClick.AddListener(() => CharacterSelectedCallback(index));
    }

    private void CharacterSelectedCallback(int index)
    {
        selectedCharacterIndex = index;

        CharacterDataSO characterData = characterDatas[index];

        if (unlockedStates[index])
        {
            lastSelectedCharacterIndex = index;
            characterInfo.Button.interactable = false;
            Save();

            onCharacterSelected?.Invoke(characterData);
        }
        else
        {
            characterInfo.Button.interactable =
                CurrencyManager.instance.HasEnoughPremiumCurrency(characterData.PurchasePrice);

        }


        centerCharacterImage.sprite = characterDatas[index].Sprite;
        menuCharacterImage.sprite = characterDatas[index].Sprite;
        characterInfo.Configure(characterDatas[index], unlockedStates[index]);
    }

    private void PurchaseSelectedCharater()
    {
        int price = characterDatas[selectedCharacterIndex].PurchasePrice;
        CurrencyManager.instance.UsePremiumCurrency(price);

        unlockedStates[selectedCharacterIndex] = true;

        characterButtonsParent.GetChild(selectedCharacterIndex).GetComponent<CharacterButton>().UnLock();

        CharacterSelectedCallback(selectedCharacterIndex);

        Save();
    }

    public void Load()
    {
        characterDatas = ResourcesManager.Characters;

        for (int i = 0; i < characterDatas.Length; i++)
            unlockedStates.Add(i == 0);

        if (Sijil.TryLoad(this, unlockedStatesKey, out object unlockedStatesObject))
            unlockedStates = (List<bool>)unlockedStatesObject;

        if (Sijil.TryLoad(this, lastSelectedCharacterKey, out object lastSelectedCharacterObject))
            lastSelectedCharacterIndex = (int)lastSelectedCharacterObject;

        Initialize();
    }

    public void Save()
    {
        Sijil.Save(this, unlockedStatesKey, unlockedStates);
        Sijil.Save(this, lastSelectedCharacterKey, lastSelectedCharacterIndex);
    }
}
