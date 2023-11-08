using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbSelectorUI : MonoBehaviour, IReceiver
{
    private GameObject starUIPrefab;
    private GameObject planetUIPrefab;

    public Transform solarSystemOrbsTrn;
    private List<Button> orbBtnList = new List<Button>();
    public Button addBtn;

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
            btn.onClick.AddListener(() => { GetSelector(btn); });
            orbBtnList.Add(btn);

            OrbType _type = (OrbType)datas[i]["orbType"];
            orbBtnList[i].transform.GetComponentsInChildren<Text>()[0].text = _type.ToString();
            orbBtnList[i].transform.GetComponentsInChildren<Text>()[1].text = datas[i]["id"].ToString();
        }
    }

    private void GetSelector(Button btn)
    {
       // int nowOrbID = int.Parse(btn.transform.GetComponentsInChildren<Text>()[1].text);
    }

    private void SetSelector(Dictionary<string, float> data)
    {
        OrbType _type = (OrbType)data["orbType"];
        orbBtnList[(int)data["id"]].transform.GetComponentsInChildren<Text>()[0].text = _type.ToString();
    }

    //=======================================================================


    public void ReceiveData<T>(T _data)
    {
        if (_data.GetType() == typeof(Dictionary<string, float>)) 
        {
            SetSelector(_data as Dictionary<string, float>);
        }
    }
}
