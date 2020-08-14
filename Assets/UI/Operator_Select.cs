using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Operator_Select : MonoBehaviour
{
    public static List<Gun> gunList = new List<Gun>
    {
        new Gun("AK-12", 44, 3, 0.07f),
        new Gun("L85A2", 47, 3, 0.09f),
        new Gun("LFP586", 78, 1, 1),
        new Gun("F2", 37, 3, 0.06f),
        new Gun("FMG-9", 30, 4, 0.075f)
    };

    public static List<Operator> operatorList = new List<Operator>
    {
        new Operator("Recruit", 0, "A recruit fresh out of the academy. Ready to fight for their country!"),
        new Operator("Sledge", 1, "He fights in close quarters with many of his crew around him."),
        new Operator("Montagne", 2, "He has a shield* that protects him from the front. *The shield is only cosmetic here."),
        new Operator("Twitch", 3, "Uses Shock Drones to wear down her enemy before finishing them off."),
        new Operator("Smoke", 4, "Uses poisonous Gas Grenades to weaken his enemy for an easier fight.")
    };

    private List<Operator> currentOperatorList = new List<Operator>();
     
    int Operator = 0;

    public TextMeshProUGUI OperatorNameTextUI;
    public Image OperatorImageUI;
    public Text OperatorBioTextUI;
    public Image GunImageUI;
    public Text gunNameTextUI;
    public Text bulletCountTextUI;
    public Slider gunSliderUI;

    private void Start()
    {
        currentOperatorList.Add(operatorList[0]);

        for (int i = 0; i < Global.Levels.Length; i++)
        {
            if (Global.Levels[i] == true)
            {
                currentOperatorList.Add(operatorList[i + 1]);
            }
        }
    }

    public void PreviousOperator()
    {
        if (Operator == 0)
        {
            Operator = currentOperatorList.Count - 1;
        }

        else
        {
            Operator--;
        }

        DisplayOperator();
    }

    public void NextOperator()
    {
        if (Operator == currentOperatorList.Count - 1)
        {
            Operator = 0;
        }

        else
        {
            Operator++;
        }

        DisplayOperator();
    }

    void DisplayOperator()
    {
        OperatorNameTextUI.SetText(currentOperatorList[Operator].Name.ToUpper());
        OperatorImageUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("Characters\\" + currentOperatorList[Operator].Name + "\\" + currentOperatorList[Operator].Name + "_CS");
        OperatorBioTextUI.GetComponent<Text>().text = currentOperatorList[Operator].Bio;
        GunImageUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("Guns\\" + gunList[currentOperatorList[Operator].Gun].Name);
        gunNameTextUI.GetComponent<Text>().text = gunList[currentOperatorList[Operator].Gun].Name;
        bulletCountTextUI.GetComponent<Text>().text = gunList[currentOperatorList[Operator].Gun].Burst.ToString();
        gunSliderUI.GetComponent<Slider>().value = gunList[currentOperatorList[Operator].Gun].Damage;
    }

    public void SelectOperator()
    {
        Global.Operator = currentOperatorList[Operator];
    }
}
