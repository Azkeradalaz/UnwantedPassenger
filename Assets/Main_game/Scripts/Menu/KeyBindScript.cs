using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindScript : MonoBehaviour {


    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public Text up, left, down, right, jump, interact;
    private Color32 normal = new Color32(135, 132, 242, 100);
    private Color32 selected = new Color32(0, 0, 0, 100);

    private GameObject currentKey;
	// Use this for initialization
	void Start () {


        keys.Add("UpButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("UpButton", "W")));
        keys.Add("DownButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("DownButton", "S")));
        keys.Add("LeftButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftButton", "A")));
        keys.Add("RightButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightButton", "D")));
        keys.Add("JumpButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("JumpButton", "Space")));
        keys.Add("InteractButton", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("InteractButton", "E")));



        SetKeysLabels();
        //up.text = keys["UpButton"].ToString();
        //down.text = keys["DownButton"].ToString();
        //left.text = keys["LeftButton"].ToString();
        //right.text = keys["RightButton"].ToString();
        //jump.text = keys["JumpButton"].ToString();
        //interact.text = keys["InteractButton"].ToString();


    }
	
    private void SetKeysLabels()
    {
        up.text = keys["UpButton"].ToString();
        down.text = keys["DownButton"].ToString();
        left.text = keys["LeftButton"].ToString();
        right.text = keys["RightButton"].ToString();
        jump.text = keys["JumpButton"].ToString();
        interact.text = keys["InteractButton"].ToString();
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(keys["UpButton"]))
        {
            Debug.Log("Up");
        }
        if (Input.GetKeyDown(keys["DownButton"]))
        {
            //Debug.Log("Down");
        }
        if (Input.GetKeyDown(keys["LeftButton"]))
        {
            //Debug.Log("Left");
        }
        if (Input.GetKeyDown(keys["RightButton"]))
        {
            //Debug.Log("Right");
        }
        if (Input.GetKeyDown(keys["JumpButton"]))
        {
            //Debug.Log("Jump");
        }
        if (Input.GetKeyDown(keys["InteractButton"]))
        {
            //Debug.Log("Interact");
        }

    }
    
    public void SetToDefault()
    {

        keys.Clear();

        keys.Add("UpButton", KeyCode.W);
        keys.Add("DownButton", KeyCode.S);
        keys.Add("LeftButton", KeyCode.A);
        keys.Add("RightButton", KeyCode.D);
        keys.Add("JumpButton", KeyCode.Space);
        keys.Add("InteractButton", KeyCode.E);
        SetKeysLabels();


    }
    private void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if (e.isMouse)
            {
                switch (e.button)
                {
                    case 0:
                        keys[currentKey.name] = KeyCode.Mouse0;
                        currentKey.transform.GetChild(0).GetComponent<Text>().text = "Mouse0";
                        break;
                    case 1:
                        keys[currentKey.name] = KeyCode.Mouse1;
                        currentKey.transform.GetChild(0).GetComponent<Text>().text = "Mouse1";
                        break;
                    case 2:
                        keys[currentKey.name] = KeyCode.Mouse2;
                        currentKey.transform.GetChild(0).GetComponent<Text>().text = "Mouse2";
                        break;

                }
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;


            }

            else if (e.isKey)
            {
                keys[currentKey.name] = e.keyCode;

                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
        }
    }


    public void ChangeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;

        }
        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }

    public void SaveKeys()
    {
        foreach (var key in keys)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
        }
        PlayerPrefs.Save();
    }
}
