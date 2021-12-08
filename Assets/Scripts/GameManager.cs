using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text distext;
    public Text fpstext;
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    GameObject timeline;
    bool debagactive = false;

    private float disatance;
    private string data;
    private SerialPort serialPort;

    // Start is called before the first frame update
    void Start()
    {
        serialPort = new SerialPort("COM3", 9600);  //ポート指定
        serialPort.Open();                          //ポートを開く
    }

    // Update is called once per frame
    void Update()
    {
        //fps計算
        float fps = 1f / Time.deltaTime;
        fpstext.text = fps.ToString();

        if (serialPort.IsOpen)
        {
            data = serialPort.ReadLine();
            disatance = float.Parse(data);
            distext.text = (data + "cm");
            if (disatance < 10)
            {
                timeline.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            if (debagactive)
            {
                canvas.SetActive(false);
                debagactive = false;
            }
            else
            {
                canvas.SetActive(true);
                debagactive = true;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnApplicationQuit()
    {
        serialPort.Close(); //アプリを閉じるときポートを閉める
    }
}
