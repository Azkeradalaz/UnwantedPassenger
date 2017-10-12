using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum ObjectType { box01, box02 }
public abstract class SaveableObject : MonoBehaviour {

    protected string saveStats;

    [SerializeField]
    private ObjectType objectType;

	// Use this for initialization
	private void Start () {
        SaveGameManager.Instance.SaveableObjects.Add(this);
	}
	
    public virtual void Save(int id)
    {
        PlayerPrefs.SetString(Application.loadedLevel +"-" + id.ToString(), objectType + "_" + transform.position.ToString()+"_"+transform.localScale+"_"+transform.localRotation+"_"+saveStats);
    }

    public virtual void Load(string[] values)
    {
        transform.localPosition = SaveGameManager.Instance.StringToVector(values[1]);
        transform.localScale = SaveGameManager.Instance.StringToVector(values[2]);
        transform.localRotation = SaveGameManager.Instance.StringToQuaternion(values[3]);
    }

    public void DestroySaveable()
    {
        SaveGameManager.Instance.SaveableObjects.Remove(this);
        Destroy(gameObject);

    }

	// Update is called once per frame
	void Update () {
		
	}
}
