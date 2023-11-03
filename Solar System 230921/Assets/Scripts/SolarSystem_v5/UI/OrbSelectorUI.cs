using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbSelectorUI : AbstractUI
{
    public string[] orbNameData;

    private GameObject starUIPrefab;
    private GameObject planetUIPrefab;

    public Transform solarSystemOrbsTrn;
    private List<Button> orbBtnList = new List<Button>();
    public Button addBtn;

    public override void Init()
    {
        starUIPrefab = Resources.Load<GameObject>("Prefabs/Templete/V4/Star_UI");
        planetUIPrefab = Resources.Load<GameObject>("Prefabs/Templete/V4/Planet_UI");
    }

    public void SetSelector(Dictionary<string, float> _data)
    {
        for (int i = 0; i < solarSystemOrbsTrn.childCount; i++)
        {
            Destroy(solarSystemOrbsTrn.GetChild(i).gameObject);
        }

        for (int i = 0; i < _data.Count; i++)
        {
            GameObject orbNameUI;
            if (_data["isCenterOrb"] > 0.5)
            {
                orbNameUI = Instantiate(starUIPrefab, solarSystemOrbsTrn);
            }
            else
            {
                orbNameUI = Instantiate(planetUIPrefab, solarSystemOrbsTrn);
            }

            orbNameUI.transform.GetComponentsInChildren<Text>()[0].text = _data["orbType"].ToString();
            orbNameUI.transform.GetComponentsInChildren<Text>()[1].text = _data["id"].ToString();

            Button btn = orbNameUI.transform.GetComponent<Button>();
            btn.onClick.AddListener(() => { GetSelector(btn); });
            btn.onClick.AddListener(OnChangeData.Invoke);
            orbBtnList.Add(btn);
        }
    }

    public void GetSelector(Button btn)
    {
        int nowOrbID = int.Parse(btn.transform.GetComponentsInChildren<Text>()[1].text);
    }
}
