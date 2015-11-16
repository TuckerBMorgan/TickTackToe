using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Threading;
using System.Text;



public class Client : MonoBehaviour {

    public string outMessage;
    public string inMessage;
    private TcpClient tcp;
    private Thread write;
    private Thread read;
    private byte[] buffer;

    public bool threadStop = false;
    public static Client Instance;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
        tcp = new TcpClient();
        tcp.Connect("127.0.0.1", 1337);
        outMessage = "";
        inMessage = "";
        write = new Thread(Write);
        read = new Thread(Read);
        buffer = new byte[500];
        outMessage = "{\"mType\":\"newConnection\"}";
        write.Start();
        read.Start();

        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Write()
    {
        while(!threadStop)
        {
            if(!string.IsNullOrEmpty(outMessage))
            {
                byte[] array = Encoding.ASCII.GetBytes(outMessage);
                tcp.GetStream().Write(array, 0, array.Length);
                outMessage = null;
            }
        }
    }

    public void SendMessage(string str)
    {
        if(string.IsNullOrEmpty(outMessage))
        {
            outMessage = str;
        }
    }

    void Read()
    {
        while (!threadStop)
        {
            int bSize = tcp.GetStream().Read(buffer, 0, 500);
            if(bSize > 0)
            {
               inMessage = findMessage(buffer, bSize);
               JSONObject j = new JSONObject(inMessage);
               Control.Instance.GetMessage(j);
            }
        }
    }
    
    public string findMessage(byte[] arary, int length)
    {
        StringBuilder strBuild = new StringBuilder();


        for (int i = 0; i < length;i++)
        {
            if(arary[i] != '\0' || arary[i] != null)
            {
                strBuild.Append((char)arary[i]);
            }
        }
            return strBuild.ToString();
    }

    void OnApplicationQuit()
    {
        write.Abort();
        read.Abort();
        tcp.Close();
        threadStop = true;
    }



}
