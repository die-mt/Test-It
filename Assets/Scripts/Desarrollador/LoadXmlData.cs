﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using UnityEngine.UI;


public class LoadXmlData : MonoBehaviour // the Class
{
    public TextAsset GameAsset;
    public GameObject Controller;
    public Text text;
    public Image bubble;
    public Transform Deidadpos;
    public float smoothLerp=1;

    string lvlName = "";
   // string tutorial = "";

    private bool speaking = false; 
    List<Dictionary<string, string>> levels = new List<Dictionary<string, string>>();
    Dictionary<string, string> obj;

    private static int xbaseCanvas = 120;
    private static int ybaseCanvas = 40;

    Vector3 posfinal;
    private float LerpX;
    private float LerpY;
    private bool moving;

    void Awake()
    { //Timeline of the Level creator
        GetLevel();
        bubble.enabled = false;
    }

    public void GetLevel()
    {
        XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
        xmlDoc.LoadXml(GameAsset.text); // load the file.
        XmlNodeList levelsList = xmlDoc.GetElementsByTagName("level"); // array of the level nodes.

        foreach (XmlNode levelInfo in levelsList)
        {
            XmlNodeList levelcontent = levelInfo.ChildNodes;
            obj = new Dictionary<string, string>(); // Create a object(Dictionary) to colect the both nodes inside the level node and then put into levels[] array.

            foreach (XmlNode levelsItens in levelcontent) // levels itens nodes.
            {
                if (levelsItens.Name == "name")
                {
                    obj.Add("name", levelsItens.InnerText); // put this in the dictionary.
                }
                if (levelsItens.Name == "introduccion")
                {
                    obj.Add("introduccion", levelsItens.InnerText); // put this in the dictionary.
                }


                if (levelsItens.Name == "coleccionable")
                {
                    obj.Add("coleccionable", levelsItens.InnerText); // put this in the dictionary.
                }

                if (levelsItens.Name == "object")
                {
                    switch (levelsItens.Attributes["name"].Value)
                    {
                        case "Mexican": obj.Add("Mexican", levelsItens.InnerText); break; // put this in the dictionary.
                        case "Enemigo": obj.Add("Enemigo", levelsItens.InnerText); break; // put this in the dictionary.
                        case "CajaFalsa": obj.Add("CajaFalsa", levelsItens.InnerText); break; // put this in the dictionary.
                        
                        case "Puzzle": obj.Add("Puzzle", levelsItens.InnerText); break;
                        case "Bug": obj.Add("Bug", levelsItens.InnerText); break;
                        case "Explicacion1": obj.Add("Explicacion1", levelsItens.InnerText); break;
                        case "Explicacion2": obj.Add("Explicacion2", levelsItens.InnerText); break;
                        case "Explicacion3": obj.Add("Explicacion3", levelsItens.InnerText); break;
                        case "Explicacion4": obj.Add("Explicacion4", levelsItens.InnerText); break;
                        case "Explicacion5": obj.Add("Explicacion5", levelsItens.InnerText); break;
                        case "Explicacion6": obj.Add("Explicacion6", levelsItens.InnerText); break;
                        case "Explicacion7": obj.Add("Explicacion7", levelsItens.InnerText); break;
                        case "Explicacion8": obj.Add("Explicacion8", levelsItens.InnerText); break;

                        case "Explicacion0-1": obj.Add("Explicacion0-1", levelsItens.InnerText); break;
                        case "Explicacion0-2": obj.Add("Explicacion0-2", levelsItens.InnerText); break;
                        case "Explicacion0-3": obj.Add("Explicacion0-3", levelsItens.InnerText); break;
                        case "Explicacion0-4": obj.Add("Explicacion0-4", levelsItens.InnerText); break;
                        case "Explicacion0-5": obj.Add("Explicacion0-5", levelsItens.InnerText); break;
                        case "Explicacion0-6": obj.Add("Explicacion0-6", levelsItens.InnerText); break;
                        case "Explicacion0-7": obj.Add("Explicacion0-7", levelsItens.InnerText); break;
                        case "Explicacion0-8": obj.Add("Explicacion0-8", levelsItens.InnerText); break;
                        case "Explicacion0-9": obj.Add("Explicacion0-9", levelsItens.InnerText); break;

                        case "Explicacion2-1": obj.Add("Explicacion2-1", levelsItens.InnerText); break;
                        case "Explicacion2-2": obj.Add("Explicacion2-2", levelsItens.InnerText); break;
                        case "Explicacion2-3": obj.Add("Explicacion2-3", levelsItens.InnerText); break;
                        case "Explicacion2-4": obj.Add("Explicacion2-4", levelsItens.InnerText); break;
                        case "Explicacion2-5": obj.Add("Explicacion2-5", levelsItens.InnerText); break;
                        case "Explicacion2-6": obj.Add("Explicacion2-6", levelsItens.InnerText); break;
                        case "Explicacion2-7": obj.Add("Explicacion2-7", levelsItens.InnerText); break;
                        case "Explicacion2-8": obj.Add("Explicacion2-8", levelsItens.InnerText); break;
                        case "Explicacion2-9": obj.Add("Explicacion2-9", levelsItens.InnerText); break;
                        case "Explicacion2-10": obj.Add("Explicacion2-10", levelsItens.InnerText); break;
                        case "Explicacion2-11": obj.Add("Explicacion2-11", levelsItens.InnerText); break;
                        case "Explicacion2-12": obj.Add("Explicacion2-12", levelsItens.InnerText); break;

                        case "Gag1": obj.Add("Gag1", levelsItens.InnerText); break;
                        case "Gag2": obj.Add("Gag2", levelsItens.InnerText); break;
                    }
                }

                if (levelsItens.Name == "finaltext")
                {
                    obj.Add("finaltext", levelsItens.InnerText); // put this in the dictionary.
                }
            }
            levels.Add(obj); // add whole obj dictionary in the levels[].
        }
    }

    public void Escribe(int actualLevel, string key, int TiempoDelMensaje, float FadeTime)
    {
        if (speaking)
        {
            text.CrossFadeAlpha(1, 0.001f, false);
            bubble.GetComponent<Image>().CrossFadeAlpha(1,0.001f, false);
        }
        speaking = true;
        levels[actualLevel - 1].TryGetValue(key, out lvlName);
        bubble.enabled = true;
        text.text = lvlName.ToString();
        print(text.text);
        GetComponent<Temporizador>().MarcaTiempos(TiempoDelMensaje,FadeTime,1);
    }

    public void Fade(float FadeTime)
    {
        text.CrossFadeAlpha(0.005f, FadeTime + 0.01f, false);
        bubble.GetComponent<Image>().CrossFadeAlpha(0.005f, FadeTime + 0.01f, false);
    }


    public void Borra()
    {
        text.text = "";
        bubble.enabled = false;
        text.CrossFadeAlpha(1, 1, false);
        bubble.GetComponent<Image>().CrossFadeAlpha(1, 1, false);
        speaking = false;
    }

    public void MueveDeidad(float posx, float posy) //(0,0) del canvas=(-840,-500). He hecho un "cambio de base" y ahora el 0.0 mandado equivale al del canvas. CUIDADO que el pivote de los objetos está en el centro
    {
        Deidadpos.position = new Vector3(posx+xbaseCanvas, posy+ybaseCanvas, transform.position.z);
    }

    public void DeslizaDeidad(float posx, float posy)
    {
        LerpX = posx;
        LerpY = posy;
        posfinal = new Vector3(posx + xbaseCanvas, posy + ybaseCanvas, transform.position.z);
        moving = true;
        
        Deidadpos.position = Vector3.Lerp(Deidadpos.position, posfinal,Time.deltaTime);
    }

    void Update()
    {
        if (moving)
        {
            Deidadpos.position = Vector3.Lerp(Deidadpos.position, posfinal, Time.deltaTime*smoothLerp);
            if (Deidadpos.position == posfinal)
            {
                moving = false;
            }
        }

    }
}


