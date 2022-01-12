using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(test("011"));
    }

   private string test(string str)
   {
       var _char = str.ToCharArray();
       var _pow = 0;
       double totalValue = 0;

       
       for (int i = _char.Length-1; i>=0 ; i--)
       {
           Debug.Log(_char[i]);
           if (_char[i].ToString()=="1")
           {
               totalValue+= Math.Pow(2, _pow);
           }
           _pow++;
       }

       str = totalValue.ToString();
        return str;
    }
}
