using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Process
{
        Add,
        Multiply,
        Remove,
        Divide,
    }
public class GateController : MonoBehaviour
{
    public Process _processs;
    public int pointValue;
    
    [SerializeField] private  TextMeshPro pointText;

    
    // Start is called before the first frame update
    void Start()
    {
        switch (_processs)
        {
            case Process.Add:
                pointText.text = $"+{pointValue}";
                break;
            case Process.Multiply:
                pointText.text = $"x{pointValue}";
                break;
            case Process.Remove:  
                pointText.text = $"-{pointValue}";
                break;  
            case Process.Divide:  
                pointText.text = $"÷{pointValue}";
                break;  
            default:  
                Console.WriteLine("Value didn’t match earlier.");  
                break;  
        }
    }
   
}
