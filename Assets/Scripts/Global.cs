using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Global
{
    public static bool Loaded = false;

    public static Operator Operator = Operator_Select.operatorList[0];

    public static int currentLevel = 0;

    public static bool[] Levels = { false, false, false, false};
}
