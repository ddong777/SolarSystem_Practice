using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OrbDataEditorUI : MonoBehaviour, IReceiver
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

    public void Init()
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
    }

    public void Set(bool _access, Dictionary<string, float> _datas)
    {
        SetAccess(_access);
        SetEditor(_datas);
    }

    private void SetAccess(bool value)
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

    public void SetEvent(UnityAction<Dictionary<string, float>> _syncFunc1)
    {
        SetApplyBtn(_syncFunc1);
    }
    private void SetApplyBtn(UnityAction<Dictionary<string, float>> func)
    {
        applyBtn.onClick.RemoveAllListeners();
        Debug.Log("Editor ApplyBtn pressed");
        applyBtn.onClick.AddListener(() => func(GetOrbData()));
    }

    // 천체데이터관리창 세팅
    private void SetEditor(Dictionary<string, float> orbData)
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

    private Dictionary<string, float> GetOrbData()
    {
        Dictionary<string, float> data = new Dictionary<string, float>();
        data["orbType"] = orbTypeDrd.value;
        data["orbPosX"] = float.Parse(posXInF.text);
        data["orbRotZ"] = float.Parse(rotZInF.text);
        data["orbSize"] = float.Parse(sizeInF.text);
        data["spinDir"] = spinDirDrd.value;
        data["spinSpeed"] = float.Parse(spinSpeedInF.text);
        data["orbitDir"] = orbitDirDrd.value;
        data["orbitSpeed"] = float.Parse(OrbitSpeedInF.text);

        return data;
    }

    //=======================================================================

    void IReceiver.ReceiveData<T>(T _data)
    {
        if (_data is bool)
        {
            // isMaster
            SetAccess((bool)(object)_data);
        }
        else if (_data is Dictionary<string, float>)
        {
            SetEditor(_data as Dictionary<string, float>);
        }
    }
}
