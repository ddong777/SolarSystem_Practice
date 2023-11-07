using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OrbDataEditorUI : MonoBehaviour
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

        applyBtn.onClick.RemoveAllListeners();
        //applyBtn.onClick.AddListener(SendData);
    }

    // õü�����Ͱ���â ����
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
