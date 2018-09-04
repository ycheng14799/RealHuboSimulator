using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.IO;

public class motorLocsEditor : MonoBehaviour {

    private InputField fieldIn;
    private string textIn;
    private int tagInd;
    private TextAsset motFile;
    private string path = "Assets/Resources/motorLocsSpeeds.txt";
    private string input;

    void Start()
    {
        fieldIn = gameObject.GetComponent<InputField>();
    }


    void Update()
    {
        //fieldIn.onEndEdit.AddListener (AcceptStringInput);
        fieldIn.onEndEdit.AddListener(sub);
        
    }

    void sub(string userIn)
    {
        //input = fieldIn.text;
        input = userIn;
        TextAsset motFile = (TextAsset)Resources.Load("motorLocsSpeeds");
        textIn = motFile.ToString();
        tagInd = textIn.IndexOf(input.Substring(0, 3));
        textIn = textIn.Remove(tagInd,12);
        textIn = textIn.Insert(tagInd, input);
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.Write(textIn);
            writer.Close();
        }
        fieldIn.ActivateInputField();
        fieldIn.text = null;
    }
}
