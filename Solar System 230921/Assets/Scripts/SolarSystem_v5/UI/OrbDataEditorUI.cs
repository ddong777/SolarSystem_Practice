using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class OrbDataEditorUI : AbstractUI
{
    public Dropdown orbTypeDrd;

    public InputField posXInF;
    public InputField rotZInF;
    public InputField sizeInF;

    public Dropdown spinDirDrd;
    public InputField spinSpeedInF;

    public Dropdown orbitDirDrd;
    public InputField OrbitSpeedInF;

    public Button applyBtn;

    public override void Init()
    {
        orbTypeDrd.ClearOptions();
        List<Dropdown.OptionData> optionDatas = new List<Dropdown.OptionData>();
        foreach (string type in Enum.GetNames(typeof(OrbType)))
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData();
            optionData.text = type;
            optionDatas.Add(optionData);
        }
        orbTypeDrd.AddOptions(optionDatas);

        applyBtn.onClick.RemoveAllListeners();

        applyBtn.onClick.AddListener(OnUpdateData.Invoke);
    }

    public void SetAccess(bool value)
    {
        orbTypeDrd.interactable = value;

        posXInF.interactable = value;
        rotZInF.interactable = value;
        sizeInF.interactable = value;

        spinDirDrd.interactable = value;
        spinSpeedInF.interactable = value;

        orbitDirDrd.interactable = value;
        OrbitSpeedInF.interactable = value;

        applyBtn.gameObject.SetActive(value);
    }

    // 천체데이터관리창 세팅
    public void SetEditor(Dictionary<string, float> orbData)
    {
        orbTypeDrd.value = (int)orbData["orbType"];

        posXInF.text = orbData["orbPosX"].ToString();
        rotZInF.text = orbData["orbRotZ"].ToString();
        sizeInF.text = orbData["orbSize"].ToString();

        spinDirDrd.value = (int)orbData["spinDir"];
        spinSpeedInF.text = orbData["spinSpeed"].ToString();

        orbitDirDrd.value = (int)orbData["orbitDir"];
        OrbitSpeedInF.text = orbData["orbitSpeed"].ToString();
    }
}
