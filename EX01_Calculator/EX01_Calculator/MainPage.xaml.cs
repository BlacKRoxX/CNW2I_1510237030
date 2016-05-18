using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Vorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 dokumentiert.

namespace EX01_Calculator
{
    public sealed partial class MainPage : Page
    {
        private decimal memoryDecimal;
        private decimal a = 0;
        private decimal b = 0;
        private decimal calculationResult;


        private bool isPlus = false;
        private bool isMinus = false;
        private bool isMul = false;
        private bool isDiv = false;

        public MainPage()
        {
            this.InitializeComponent();
        }

        public decimal ParseToDecimal(string inputString)
        {
            var number = decimal.MinValue;

            return decimal.TryParse(inputString, out number) ? number : number;
        }

        public decimal CalcPlusDecimal(decimal dec1, decimal dec2)
        {
            var result = dec1 + dec2;
            return result;
        }

        public decimal CalcMinusDecimal(decimal dec1, decimal dec2)
        {
            var result = dec1 - dec2;
            return result;
        }

        public decimal CalcMultiplyDecimal(decimal dec1, decimal dec2)
        {
            var result = dec1 * dec2;
            return result;
        }

        public decimal CalcDivideDecimal(decimal dec1, decimal dec2)
        {
            if (dec2 == 0)
            {
                return decimal.MinValue;
            }
            var result = dec1 / dec2;
            return result;
        }

        private void ButtonPlus_OnClick(object sender, RoutedEventArgs e)
        {
            if (a != 0)
            {
                b = ParseToDecimal(TextBlockShowResult.Text);
                TextBlockShowResult.Text = CalcPlusDecimal(a, b).ToString();
                a = ParseToDecimal(TextBlockShowResult.Text);
            }
            else
            {
                a = ParseToDecimal(TextBlockShowResult.Text);
                TextBlockShowResult.Text = "";
                isPlus = true;
            }
        }
        private void ButtonMinus_OnClick(object sender, RoutedEventArgs e)
        {
            if (a != 0)
            {
                b = ParseToDecimal(TextBlockShowResult.Text);
                TextBlockShowResult.Text = CalcMinusDecimal(a, b).ToString();
            }
            else
            {
                a = ParseToDecimal(TextBlockShowResult.Text);
                TextBlockShowResult.Text = "";
                isMinus = true;
            }
        }

        private void ButtonMultiply_OnClick(object sender, RoutedEventArgs e)
        {
            if (a != 0)
            {
                b = ParseToDecimal(TextBlockShowResult.Text);
                TextBlockShowResult.Text = CalcMultiplyDecimal(a, b).ToString();
            }
            else
            {
                a = ParseToDecimal(TextBlockShowResult.Text);
                TextBlockShowResult.Text = "";
                isMul = true;
            }
        }

        private void ButtonDivide_OnClick(object sender, RoutedEventArgs e)
        {
            if (a != 0)
            {
                b = ParseToDecimal(TextBlockShowResult.Text);
                var result = CalcPlusDecimal(a, b);
                TextBlockShowResult.Text = result == decimal.MinValue ? "" : result.ToString();
            }
            else
            {
                a = ParseToDecimal(TextBlockShowResult.Text);
                TextBlockShowResult.Text = "";
                isDiv = true;
            }
        }


        private void ButtonEquals_OnClick(object sender, RoutedEventArgs e)
        {
            b = ParseToDecimal(TextBlockShowResult.Text);
            if (isPlus)
            {
                calculationResult = CalcPlusDecimal(a, b);
                isPlus = false;
            }
            else if (isMinus)
            {
                calculationResult = CalcMinusDecimal(a, b);
                isMinus = false;
            }
            else if (isMul)
            {
                calculationResult = CalcMultiplyDecimal(a, b);
                isMul = false;
            }
            else if (isDiv)
            {
                calculationResult = CalcDivideDecimal(a, b); 
                isDiv = false;
            }
            TextBlockShowResult.Text = calculationResult == decimal.MinValue ? "" : calculationResult.ToString();
            //a = calculationResult;
            a = 0;
            b = 0;
        }


        private void ButtonZero_OnClick(object sender, RoutedEventArgs e)
        {
            TextBlockShowResult.Text = TextBlockShowResult.Text + "0";
        }

        private void ButtonOne_OnClick(object sender, RoutedEventArgs e)
        {
            TextBlockShowResult.Text = TextBlockShowResult.Text + "1";
        }

        private void ButtonTwo_OnClick(object sender, RoutedEventArgs e)
        {
            TextBlockShowResult.Text = TextBlockShowResult.Text + "2";
        }

        private void ButtonThree_OnClick(object sender, RoutedEventArgs e)
        {
            TextBlockShowResult.Text = TextBlockShowResult.Text + "3";
        }

        private void ButtonFour_OnClick(object sender, RoutedEventArgs e)
        {
            TextBlockShowResult.Text = TextBlockShowResult.Text + "4";
        }

        private void ButtonFive_OnClick(object sender, RoutedEventArgs e)
        {
            TextBlockShowResult.Text = TextBlockShowResult.Text + "5";
        }

        private void ButtonSix_OnClick(object sender, RoutedEventArgs e)
        {
            TextBlockShowResult.Text = TextBlockShowResult.Text + "6";
        }

        private void ButtonSeven_OnClick(object sender, RoutedEventArgs e)
        {
            TextBlockShowResult.Text = TextBlockShowResult.Text + "7";
        }

        private void ButtonEight_OnClick(object sender, RoutedEventArgs e)
        {
            TextBlockShowResult.Text = TextBlockShowResult.Text + "8";
        }

        private void ButtonNine_OnClick(object sender, RoutedEventArgs e)
        {
            TextBlockShowResult.Text = TextBlockShowResult.Text + "9";
        }

        private void ButtonComma_OnClick(object sender, RoutedEventArgs e)
        {
            TextBlockShowResult.Text = TextBlockShowResult.Text + ",";
        }

        private void ButtonClearCalc_OnClick(object sender, RoutedEventArgs e)
        {
            a = 0;
            b = 0;
            calculationResult = 0;
            TextBlockShowResult.Text = "";
        }

        private void ButtonMemoryClear_OnClick(object sender, RoutedEventArgs e)
        {
            memoryDecimal = 0;
        }

        private void ButtonMemoryRecall_OnClick(object sender, RoutedEventArgs e)
        {
            TextBlockShowResult.Text = memoryDecimal.ToString();
        }

        private void ButtonMemoryStore_OnClick(object sender, RoutedEventArgs e)
        {
            memoryDecimal = ParseToDecimal(TextBlockShowResult.Text);
            TextBlockShowResult.Text = "";
        }
    }
}
