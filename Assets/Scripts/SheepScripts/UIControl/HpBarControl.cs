using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIControl
{
    public class HpBarControl : MonoBehaviour
    {
        public int hpValue;
       // [SerializeField] private TextMeshProUGUI hpIndexTMP;
       // [SerializeField] private Image fillImage;
        private Slider _hpBar;
        private ShipManager _shipManager;
        public int comboCount;
        private TextMeshProUGUI comboTMP;
        

        public int ComboIndex()
        {
            return comboCount;
        }
        private void Start()
        {
           // _shipManager=GameObject.FindWithTag("ShipModel").GetComponent<ShipManager>();
            comboTMP = GameObject.FindWithTag("ComboTMP").GetComponent<TextMeshProUGUI>();
            _hpBar = gameObject.GetComponent<Slider>();
            hpValue = 0;
            comboCount = 1;
        }

        public void SetHpValue(int hpIndex)
        {
            if ((hpValue + hpIndex) > 100 || (hpValue + hpIndex) < -100)
            {
                hpValue = 100;
                _hpBar.value = hpValue;
                comboCount++;
                comboTMP.text = "x"+comboCount.ToString();
            }
            else
            {
                hpValue += hpIndex;
                _hpBar.value = hpValue;
            
                comboCount = 1;
                comboTMP.text = "x"+comboCount.ToString();
              //  var shipIndex = (int) (_hpBar.value / 25);
                if (gameObject.CompareTag("HpBar")) return;
               // _shipManager.SetShip(shipIndex);
            }
        }

        public void SliderValueChange()
        {
            _hpBar.value--;
        }
        public string MemberType(int sliderValue)
        {
            return sliderValue>0 ? "Good" : "Evil";
        }

        // public void SetColor(string memberType)
        // {
        //     switch (memberType)
        //     {
        //         case "Good":
        //             GoodColor();
        //             break;
        //         case "Evil":
        //             EvilColor();
        //             break;
        //     }
        // }
        
        // private void EvilColor()
        // {
        //     hpIndexTMP.color=Color.green;
        //     fillImage.color = Color.red;
        // }
        //
        // private void GoodColor()
        // {
        //     hpIndexTMP.color=Color.white;
        //     fillImage.color = Color.blue;
        // }
    }
}
