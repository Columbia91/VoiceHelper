using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Speech.Recognition;

namespace SpeechRecognition
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static Label label;
        static void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs spRecEvA)
        {
            if(spRecEvA.Result.Text == "блокнот" && spRecEvA.Result.Confidence > 0.5)
            {
                label.Text = "Блокнот";
                System.Diagnostics.Process.Start("notepad");
            }
            if (spRecEvA.Result.Text == "закрыть блокнот" && spRecEvA.Result.Confidence > 0.5)
            {
                label.Text = "Закрыть блокнот";
                System.Diagnostics.Process.Start("taskkill", "/im:notepad.exe");
            }
            if (spRecEvA.Result.Text == "калькулятор" && spRecEvA.Result.Confidence > 0.5)
            {
                label.Text = "Калькулятор";
                System.Diagnostics.Process.Start("calc");
            }
            if (spRecEvA.Result.Text == "закрыть калькулятор" && spRecEvA.Result.Confidence > 0.5)
            {
                label.Text = "Закрыть калькулятор";
                System.Diagnostics.Process.Start("taskkill", "/im:calc.exe");
            }
            if (spRecEvA.Result.Text == "диспетчер задач" && spRecEvA.Result.Confidence > 0.5)
            {
                label.Text = "Диспетчер задач";
                System.Diagnostics.Process.Start("taskmgr");
            }
            if (spRecEvA.Result.Text == "хром" && spRecEvA.Result.Confidence > 0.5)
            {
                label.Text = "Хром";
                System.Diagnostics.Process.Start("chrome");
            }
            if (spRecEvA.Result.Text == "уорд" && spRecEvA.Result.Confidence > 0.5)
            {
                label.Text = "Word";
                System.Diagnostics.Process.Start("winword");
            }
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            label = label1;

            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-ru");
            SpeechRecognitionEngine sre = new SpeechRecognitionEngine(ci);
            sre.SetInputToDefaultAudioDevice();

            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);


            Choices numbers = new Choices();
            numbers.Add(new string[] { "блокнот", "калькулятор", "диспетчер задач", "хром", "уорд",
                "закрыть блокнот", "закрыть калькулятор" });


            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = ci;
            gb.Append(numbers);


            Grammar g = new Grammar(gb);
            sre.LoadGrammar(g);

            sre.RecognizeAsync(RecognizeMode.Multiple);
        }
    }
}
