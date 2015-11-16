using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {
    public static Control Instance;

    public void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
    public void GetMessage(JSONObject jObj)
    {
        if(jObj["mType"].str == "connectionStatus")
        {
            if(jObj["connection"].str == "good")
            {
                System.Guid guid = new System.Guid();
                Client.Instance.SendMessage("{\"mType\":\"newPlayer\"," + 
                                              "\"guid\":\"" + guid.ToString() + "\"");
            }
        }
    }

}
