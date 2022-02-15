using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using TMPro;

public class Logic : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ticketsPurchasedText;
    [SerializeField] TextMeshProUGUI pricePayedText;

    BinaryFormatter binaryFormatter;
    string path;
    float ticketPrice = 2.8f;
    int ticketsPurchased = 0;

    // Update is called once per frame
    void Start()
    {
        binaryFormatter = new BinaryFormatter();
        path = Application.persistentDataPath + "/save.data";
        Load();
        updateTexts();
    }

    void OnApplicationQuit() {
        Save();
    }

    public void OnIncreasePressed()
    {
        ticketsPurchased += 1;
        updateTexts();
    }

    public void OnDecreasedPressed()
    {
        if(ticketsPurchased > 0) {
            ticketsPurchased -= 1;
            updateTexts();
        }
    }

    void updateTexts() {
        ticketsPurchasedText.text = ticketsPurchased.ToString();
        pricePayedText.text = (ticketsPurchased * ticketPrice).ToString();
    }

    void Save() {
        FileStream stream = new FileStream(path, FileMode.Create);

        binaryFormatter.Serialize(stream, ticketsPurchased);
        stream.Close();
    }

    void Load() {
        if (File.Exists(path)) {
            FileStream stream = new FileStream(path, FileMode.Open);
            ticketsPurchased = (int) binaryFormatter.Deserialize(stream);
            stream.Close();
        } else {
            ticketsPurchased = 0;
        }
    }
}
