using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class calculator : EditorWindow
{
    [MenuItem("CustomTool/Windows/calculator")]
    public static void ShowWindow()
    {
        GetWindow<calculator>("calculator");
    }
    public class ExpressionClass
    {
        public string number = "";
        public string sign = "";
    }
    public List<ExpressionClass> ExpressionList = new List<ExpressionClass>();
    public class ExpressionClassCalculate
    {
        public float number;
        public string sign;
    }
    public List<ExpressionClassCalculate> ExpressionListCalculate = new List<ExpressionClassCalculate>();

    public string expression = "0";
    public string result = "0";
    void OnGUI()
    {
        var style = new GUIStyle(GUI.skin.button);
        style.fixedWidth = position.width / 4.1f;

        GUILayout.BeginVertical();

        GUILayout.Label(expression);
        GUILayout.Label(result);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("1", style))
        {
            add("1", true);
        }

        if (GUILayout.Button("2", style))
        {
            add("2", true);
        }

        if (GUILayout.Button("3", style))
        {
            add("3", true);
        }

        if (GUILayout.Button("^", style))
        {
            add("^", false);
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("4", style))
        {
            add("4", true);
        }

        if (GUILayout.Button("5", style))
        {
            add("5", true);
        }

        if (GUILayout.Button("6", style))
        {
            add("6", true);
        }

        if (GUILayout.Button(".", style))
        {
            add(",", true);
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("7", style))
        {
            add("7", true);
        }

        if (GUILayout.Button("8", style))
        {
            add("8", true);
        }

        if (GUILayout.Button("9", style))
        {
            add("9", true);
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("/", style))
        {
            add("/", false);
        }

        if (GUILayout.Button("*", style))
        {
            add("*", false);
        }

        if (GUILayout.Button("-", style))
        {
            add("-", false);
        }
        if (GUILayout.Button("+", style))
        {
            add("+", false);
        }

        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("0", style))
        {
            add("0", true);
        }

        if (GUILayout.Button("C", style))
        {
            clear();
        }

        if (GUILayout.Button("<-", style))
        {
            erase();
        }

        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
    }
    public void add(string sign, bool isNumber)
    {
        if (ExpressionList.Count == 0)
        {
            ExpressionList.Add(new ExpressionClass());
            if (isNumber)
            {
                ExpressionList[0].number += sign;
            }
            else
            {
                ExpressionList[0].number = "0";
                ExpressionList[0].sign = sign;
            }
        }
        else
        {
            int i = ExpressionList.Count - 1;
            if (ExpressionList[i].sign == string.Empty || ExpressionList[i].sign == "")
            {
                if (isNumber)
                {
                    ExpressionList[i].number += sign;
                }
                else
                {
                    ExpressionList[i].sign = sign;
                }
            }
            else
            {
                if (isNumber)
                {
                    ExpressionList.Add(new ExpressionClass());
                    ExpressionList[i+1].number += sign;
                }
                else
                {
                    ExpressionList[i].sign = sign;
                }
            }
        }
        calculate();
    }
    public void calculate()
    {
        if (ExpressionList[0].number == "")
        {
            ExpressionList[0].number = "0";
        }
        else if(ExpressionList[0].number[0].ToString() == "0" && ExpressionList[0].number.Length > 1)
        {
            ExpressionList[0].number = ExpressionList[0].number.Remove(0, 1);
        }
        expression = "";
        for (int i = 0; i < ExpressionList.Count; i++)
        {
            expression += ExpressionList[i].number;
            expression += ExpressionList[i].sign;
        }
        ExpressionListCalculate = new List<ExpressionClassCalculate>();
        for (int i = 0; i < ExpressionList.Count; i++)
        {
            ExpressionListCalculate.Add(new ExpressionClassCalculate());
            ExpressionListCalculate[i].sign = ExpressionList[i].sign;
            ExpressionListCalculate[i].number = (float)double.Parse(ExpressionList[i].number);
        }
        while (ExpressionListCalculate.Count > 1)
        {
            for (int i = 0; i < ExpressionListCalculate.Count - 1; i++)
            {
                if (ExpressionListCalculate[i].sign == "^")
                {
                    ExpressionListCalculate[i].number = Mathf.Pow(ExpressionListCalculate[i].number, ExpressionListCalculate[i + 1].number);
                    ExpressionListCalculate[i].sign = ExpressionListCalculate[i + 1].sign;
                    ExpressionListCalculate.RemoveAt(i + 1);
                    result = ExpressionListCalculate[i].number.ToString();
                    break;
                }
                if (ExpressionListCalculate[i].sign == "*")
                {
                    ExpressionListCalculate[i].number = ExpressionListCalculate[i].number * ExpressionListCalculate[i + 1].number;
                    ExpressionListCalculate[i].sign = ExpressionListCalculate[i + 1].sign;
                    ExpressionListCalculate.RemoveAt(i + 1);
                    result = ExpressionListCalculate[i].number.ToString();
                    break;
                }
                if (ExpressionListCalculate[i].sign == "/")
                {
                    ExpressionListCalculate[i].number = ExpressionListCalculate[i].number / ExpressionListCalculate[i + 1].number;
                    ExpressionListCalculate[i].sign = ExpressionListCalculate[i + 1].sign;
                    ExpressionListCalculate.RemoveAt(i + 1);
                    result = ExpressionListCalculate[i].number.ToString();
                    break;
                }
            }
            for (int i = 0; i < ExpressionListCalculate.Count - 1; i++)
            {
                if (ExpressionListCalculate[i].sign == "+")
                {
                    ExpressionListCalculate[i].number = ExpressionListCalculate[i].number + ExpressionListCalculate[i + 1].number;
                    ExpressionListCalculate[i].sign = ExpressionListCalculate[i + 1].sign;
                    ExpressionListCalculate.RemoveAt(i + 1);
                    result = ExpressionListCalculate[i].number.ToString();
                    break;
                }
                if (ExpressionListCalculate[i].sign == "-")
                {
                    ExpressionListCalculate[i].number = ExpressionListCalculate[i].number - ExpressionListCalculate[i + 1].number;
                    ExpressionListCalculate[i].sign = ExpressionListCalculate[i + 1].sign;
                    ExpressionListCalculate.RemoveAt(i + 1);
                    result = ExpressionListCalculate[i].number.ToString();
                    break;
                }
                ExpressionListCalculate[i].sign = ExpressionListCalculate[i + 1].sign;
                ExpressionListCalculate.RemoveAt(i + 1);
                result = ExpressionListCalculate[i].number.ToString();
            }
        }
    }
    public void clear()
    {
        ExpressionList = new List<ExpressionClass>();
        expression = "0";
        result = "0";
    }
    public void erase()
    {
        if (ExpressionList.Count != 0)
        {
            int i = ExpressionList.Count - 1;
            if (ExpressionList[i].sign != "")
            {
                ExpressionList[i].sign = "";
            }
            else if (ExpressionList[i].number != "")
            {
                if (ExpressionList[i].number.Length == 1)
                {
                    ExpressionList[i].number = "";
                }
                else
                {
                    ExpressionList[i].number = ExpressionList[i].number.Remove(ExpressionList[i].number.Length - 1);
                }
            }
            else
            {
                ExpressionList.RemoveAt(i);
                erase();
            } 
        }
        calculate();
    }
}