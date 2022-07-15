using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Localizator : MonoBehaviour
{
    public TextAsset DataSheet;
    public static Localizator Instance;
    private Dictionary<string, LanguageData> Data;
    private Language currentLanguage;
    public Language DefaultLenguage;

    public delegate void LanguageChangeDelegate();
    public static LanguageChangeDelegate OnLanguageChangeDelegate;


    
    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            Instance = this;
            currentLanguage = DefaultLenguage;
            ReadSheet();
        }
        
    }
   

    private void ReadSheet()
    {
        string[] line = DataSheet.text.Split(new char[] { '\n' });
        for (int i = 0; i < line.Length; i++)
        {
            if (line.Length > 0)
            {
                AddNewEntry(line[i]);
            }
        }
    }

    private void AddNewEntry(String linea)
    {
        string[] texto = linea.Split(new char[] { ';' });
        var languageData = new LanguageData(texto);
        if(Data == null)
        {
            Data = new Dictionary<string, LanguageData>();
        }
        Data.Add(texto[0], languageData);
    }

    public static void SetLanguage(Language language)
    {
        Instance.currentLanguage = language;
        OnLanguageChangeDelegate?.Invoke();
    }

    public static string GetText(string key)
    {
       return Instance.Data[key].GetText(Instance.currentLanguage);
    }
}
public enum Language
{
    English = 1,
    Catalan = 2,
    Spanish = 3
}

public class LanguageData
{
    private Dictionary<Language, string> Data;
    public LanguageData(string[] rawData)
    {
        Data = new Dictionary<Language, string>();
        for (int i = 1; i < rawData.Length; i++)//Empezamos en uno para saltarnos la primera linea del csv
        {
            Data.Add((Language)i, rawData[i]);
        }
    }
    internal string GetText(Language language)
    {
        return Data[language];
    }
}