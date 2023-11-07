using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbSelectorUI : MonoBehaviour
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
    }

    public void SetSelector(Dictionary<int, Dictionary<string, float>> datas)
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

            orbNameUI.transform.GetComponentsInChildren<Text>()[0].text = datas[i]["orbtype"].ToString();
            orbNameUI.transform.GetComponentsInChildren<Text>()[1].text = datas[i]["id"].ToString();

            Button btn = orbNameUI.transform.GetComponent<Button>();
            btn.onClick.AddListener(() => { GetSelector(btn); });
            //btn.onClick.AddListener(OnChangeData.Invoke);
            orbBtnList.Add(btn);
        }
    }

    public void GetSelector(Button btn)
    {
        int nowOrbID = int.Parse(btn.transform.GetComponentsInChildren<Text>()[1].text);
    }
}
