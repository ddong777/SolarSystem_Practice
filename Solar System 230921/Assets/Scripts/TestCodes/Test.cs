using UnityEngine;

public class Test : MonoBehaviour
{

    private void Start()
    {
        test();
    }
    void test()
    {
        Data_Network data_Network = FindObjectOfType<Data_Network>();
        Data_SolarSystem data_SolarSystem = FindObjectOfType<Data_SolarSystem>();
        Data_UI data_UI = FindObjectOfType<Data_UI>();
        Data_Cam data_Cam = FindObjectOfType<Data_Cam>();

        data_Network.Init(true, null);
        data_SolarSystem.Init();
    }
}
