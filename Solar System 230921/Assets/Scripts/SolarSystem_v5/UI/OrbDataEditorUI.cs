using System;
using System.Collections;
using System.Collections.Generic;
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

        applyBtn.onClick.AddListener(OnChangeData.Invoke);
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
    public void SetEditor(Dictionary<string, float> _data)
    {
        orbTypeDrd.value = (int)_data["orbType"];

        posXInF.text = _data["orbPosX"].ToString();
        rotZInF.text = _data["orbRotZ"].ToString();
        sizeInF.text = _data["orbSize"].ToString();

        spinDirDrd.value = (int)_data["spinDir"];
        spinSpeedInF.text = _data["spinSpeed"].ToString();

        orbitDirDrd.value = (int)_data["orbitDir"];
        OrbitSpeedInF.text = _data["orbitSpeed"].ToString();
    }

    public OrbData GetEditor()
    {
        OrbData data = new OrbData();
        data.orbType = (OrbType)Enum.ToObject(typeof(OrbType), orbTypeDrd.value);
        data.orbPrefab = Resources.Load<GameObject>($"Prefabs/OrbFeature/{orbTypeDrd.value}_{orbTypeDrd.value}Feature");
        data.orbPosX = float.Parse(posXInF.text);
        data.orbRotZ = float.Parse(rotZInF.text);
        data.orbSize = float.Parse(sizeInF.text);
        data.spinDir = (MoveDir)Enum.ToObject(typeof(MoveDir), spinDirDrd.value);
        data.spinSpeed = float.Parse(spinSpeedInF.text);
        data.orbitDir = (MoveDir)Enum.ToObject(typeof(MoveDir), orbitDirDrd.value);
        data.orbitSpeed = float.Parse(OrbitSpeedInF.text);

        return data;
    }
}
