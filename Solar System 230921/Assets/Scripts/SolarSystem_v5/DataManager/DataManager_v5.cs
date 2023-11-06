using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager_v5 : MonoBehaviour, IReceiver
{
    public Converter converter;

    public SolarSystemData_v5 solarData;
    public UIData_v5 uidata;
    public NetworkData_v5 serverData;

    public void Init()
    {
        serverData = gameObject.AddComponent<NetworkData_v5>();
        solarData = gameObject.AddComponent<SolarSystemData_v5>();
        uidata = gameObject.AddComponent<UIData_v5>();
        converter = gameObject.AddComponent<Converter>();  

        serverData.Attach(this);
        solarData.Attach(this);
        uidata.Attach(this);
    }

    //==============================================

    public void ReceiveData(ISender _sender)
    {
        switch (_sender)
        {
            case NetworkData_v5 sender:
                Debug.Log(solarData.GetType().Name + "�� " + sender.GetType().Name + "�κ��� ������ ����");
                if (sender.IsMaster || sender.isTestMode)
                {
                    solarData.Set(null);
                }
                else
                {
                    // ���� �����ͷ� SolarData ����
                    solarData.Set(converter.FromJsonToOrbDatas(sender.GetCustomProperty_stringArray(PropertyKey.OrbData)));
                }

                Debug.Log(uidata.GetType().Name + "�� " + sender.GetType().Name + "�κ��� ������ ����");
                uidata.IsAccessible = sender.IsMaster;
                if (sender.isTestMode)
                {
                    uidata.IsAccessible = sender.isTestMode;
                }

                break;

            case SolarSystemData_v5 sender:

                Debug.Log(serverData.GetType().Name + "�� " + sender.GetType().Name + "�κ��� ������ ����");
                serverData.SetCustomProperty(PropertyKey.OrbData, converter.FromOrbDatasToJson(sender.OrbDatas));

                Debug.Log(uidata.GetType().Name + "�� " + sender.GetType().Name + "�κ��� ������ ����");
                //uidata.SelectorUIData_OrbNameList = converter.OrbTypeString(solarData.OrbDatas);
                uidata.EditorUIData_NowOrbData = converter.OrbDataToEditorData(solarData.OrbDatas[uidata.NowOrbID], uidata.EditorUIData_NowOrbData);
                uidata.SendData();
                break;

            default:
                break;
        }        
    }
}
