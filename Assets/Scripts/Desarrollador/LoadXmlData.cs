using UnityEngine;
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

    static string Cube_Character = "";
    static string Cylinder_Character = "";
    static string Capsule_Character = "";
    static string Sphere_Character = "";

    string lvlName = "";
    string tutorial = "";


    List<Dictionary<string, string>> levels = new List<Dictionary<string, string>>();
    Dictionary<string, string> obj;

    void Awake()
    { //Timeline of the Level creator
        GetLevel();
       
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
                        case "Salto": obj.Add("Salto", levelsItens.InnerText); break; // put this in the dictionary.
                        case "Enemigo": obj.Add("Enemigo", levelsItens.InnerText); break; // put this in the dictionary.
                        case "Capsule": obj.Add("Capsule", levelsItens.InnerText); break; // put this in the dictionary.
                        case "Sphere": obj.Add("Sphere", levelsItens.InnerText); break; // put this in the dictionary.
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

    /*public void Escribe(int actualLevel, string key)
    {
        levels[actualLevel - 1].TryGetValue(key, out lvlName);
            
        text.text = lvlName.ToString();
        print(text.text);

    }*/
    public void Escribe(int actualLevel, string key, int TiempoDelMensaje)
    {
    levels[actualLevel - 1].TryGetValue(key, out lvlName);
        bubble.enabled = true;
    text.text = lvlName.ToString();
    print(text.text);
    GetComponent<Temporizador>().MarcaTiempos(TiempoDelMensaje);


    }

    public void Borra()
    {
        text.text = "";
        bubble.enabled = false;
    }
}


