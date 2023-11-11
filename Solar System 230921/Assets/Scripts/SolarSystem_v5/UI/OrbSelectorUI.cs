using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OrbSelectorUI : MonoBehaviour, ISender, IReceiver
{
    private GameObject starUIPrefab;
    private GameObject planetUIPrefab;

    public Transform solarSystemOrbsTrn;
    private List<Button> orbBtnList = new List<Button>();
    private UnityAction<int> OnOrbSelected;
    public Button addBtn;

    private int nowID;

    public void Init()
    {
        starUIPrefab = Resources.Load<GameObject>("Prefabs/Templete/V4/Star_UI");
        planetUIPrefab = Resources.Load<GameObject>("Prefabs/Templete/V4/Planet_UI");

        solarSystemOrbsTrn = GameObject.Find("SolarSystemOrbsTrn").transform;
    }

    public void Set(List<Dictionary<string, float>> datas)
    {
        for (int i = 0; i < solarSystemOrbsTrn.childCount; i++)
        {
            Destroy(solarSystemOrbsTrn.GetChild(i).gameObject);
        }

        for (int i = 0; i < datas.Count; i++)
        {
            GameObject orbNameUI;
            if (datas[i]["isCenter"] > .5)
            {
                orbNameUI = Instantiate(starUIPrefab, solarSystemOrbsTrn);
            }
            else
            {
                orbNameUI = Instantiate(planetUIPrefab, solarSystemOrbsTrn);
            }

            Button btn = orbNameUI.transform.GetComponent<Button>();
            
            OrbType _type = (OrbType)datas[i]["orbType"];
            btn.transform.GetComponentsInChildren<Text>()[0].text = _type.ToString();
            btn.transform.GetComponentsInChildren<Text>()[1].text = datas[i]["id"].ToString();
            btn.onClick.AddListener(() => GetNowOrbID(btn));

            orbBtnList.Add(btn);
        }
    }

    private void GetNowOrbID(Button btn)
    {
        int nowOrbID = int.Parse(btn.transform.GetComponentsInChildren<Text>()[1].text);
        nowID = nowOrbID;
        SendData(nowOrbID);
    }

    private void SetSelector(Dictionary<string, float> data)
    {
        OrbType _type = (OrbType)data["orbType"];
        orbBtnList[nowID].transform.GetComponentsInChildren<Text>()[0].text = _type.ToString();
    }

    //=======================================================================
    private List<IReceiver> receivers = new List<IReceiver>();

    public void Attach(IReceiver receiver)
    {
        receivers.Add(receiver);
    }

    public void Detach(IReceiver receiver)
    {
        receivers.Remove(receiver);
    }

    public void SendData<T>(T data)
    {
        foreach (IReceiver r in receivers)
        {
            r.ReceiveData(data);
        }
    }
    public void ReceiveData<T>(T _data)
    {
        if (_data is int)
        {
            nowID = (int)(object)_data;
        }
        else if (_data is Dictionary<string, float>) 
        {
            SetSelector(_data as Dictionary<string, float>);
        }
        else if (_data is List<Dictionary<string, float>>)
        {
            List<Dictionary<string, float>> temp = _data as List<Dictionary<string, float>>;
            for (int i = 0; i < orbBtnList.Count; i++)
            {
                nowID = i;
                SetSelector(temp[i]);
            }
        }
    }
}
