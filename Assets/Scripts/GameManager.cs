using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text distext;
    public Text distext2;
    public Text fpstext;
    [SerializeField]
    GameObject canvas;
    [SerializeField]
    GameObject timeline;
    bool debagactive = false;
    public GameObject[] obj;

    private float disatance;
    private string data;
    private float distance2;
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
            string[] i;
            i = data.Split(',');
            disatance = float.Parse(i[0]);
            distance2 = float.Parse(i[1]);
            distext.text = (i[0].ToString() + "cm");
            distext2.text = (i[1].ToString() + "cm");
            if (disatance < 10)
            {
                timeline.SetActive(true);
            }

            if (distance2 < 10)
            {
                timeline.SetActive(false);
                for (int x = 0; x < obj.Length; x++)
                {
                    obj[x].SetActive(false);
                }
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
