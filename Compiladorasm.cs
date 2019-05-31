using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Compilador
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

        public int RemoveEmptyEntries { get; private set; }

        private void button1_Click(object sender, EventArgs e)
        {

            StreamReader oblector = new StreamReader(@"C:\Users\Felipe\Desktop\Prueba.txt");
            string sLine = "";
            ArrayList arrText = new ArrayList();
            ArrayList textclean = new ArrayList();
            ArrayList preclean = new ArrayList();
            ArrayList t1d = new ArrayList();
            ArrayList parte = new ArrayList();
            ArrayList layers = new ArrayList();
            ArrayList nlayer = new ArrayList();
            ArrayList limp = new ArrayList();
            int lineas = 0;
            int vari = 1024;
			

			while (sLine != null)
			{
				sLine = oblector.ReadLine();
                if ((sLine != null) && (sLine.Length > 0))
                {
                    sLine = sLine.Replace(" ", "");
                    arrText.Add(sLine);
                }
			}
			oblector.Close();

            //limpieza finalizada

            foreach (string sOutput in arrText)
			{
                
                int pos = sOutput.IndexOf("//");
                if (pos > 0)
                {
                    textclean.Add(sOutput.Substring(0, pos));
                }

                else if (pos == -1) {
                    textclean.Add(sOutput);
                }

			}

            //Reconocimiento de layers

            foreach (string linea in textclean)
            {
                int posi = linea.IndexOf("(");
                int posf = linea.IndexOf(")");
                if (posi == 0)
                {
                    layers.Add(linea.Substring(posi+1, posf-1));
                    nlayer.Add(lineas);
                    Console.WriteLine(lineas);
                }
                else {
                    parte.Add(linea);
                    lineas++;
                }
                   
            }
            foreach (string lin in parte)
            {
                string sOutput = lin;
                int pos = sOutput.IndexOf("@");
                string pos1 = sOutput.Substring(pos + 1);
                int result;
                bool variable = int.TryParse(pos1, out result);
                int con = 0;
                bool var = false;
                if (variable == false)
                {
                    foreach (string linea in layers)
                    {
                        if (pos1 == linea)
                        {
                            sOutput = "@" + nlayer[con];
                            var = true;
                        }
                        else { 

                        con++;
                        }

                    }


                    Console.WriteLine(sOutput);
                }
                if (con == nlayer.Count) {
                    var = true;
                }

                if(variable == false && var == true)
                {
                    int poso = sOutput.IndexOf("@");
                    string tempo = sOutput.Substring(pos + 1);
                    if (poso == 0)
                    {
                        if (tempo == "R0") { sOutput = "@0"; }
                        if (tempo == "R1") { sOutput = "@1"; }
                        if (tempo == "R2") { sOutput = "@2"; }
                        if (tempo == "R3") { sOutput = "@3"; }
                        if (tempo == "R4") { sOutput = "@4"; }
                        if (tempo == "R5") { sOutput = "@5"; }
                        if (tempo == "R6") { sOutput = "@6"; }
                        if (tempo == "R7") { sOutput = "@7"; }
                        if (tempo == "R8") { sOutput = "@8"; }
                        if (tempo == "R9") { sOutput = "@9"; }
                        if (tempo == "R10") { sOutput = "@10"; }
                        if (tempo == "R11") { sOutput = "@11"; }
                        if (tempo == "R12") { sOutput = "@12"; }
                        if (tempo == "R13") { sOutput = "@13"; }
                        sOutput = "@" + vari;
                        vari++;
                    }
                }
                Console.WriteLine("esto es"+sOutput);

                if (pos == 0)
                {
                    int num = int.Parse(sOutput.Substring(pos + 1));
                    string numc = Convert.ToString(num, 2);
                    int c = 16 - numc.Length;
                    string cero = "0000000000000000";
                    string sub = cero.Substring(0, c);
                    string aña = sub + numc;
                    t1d.Add(aña);
                }
                else if (pos == -1)
                {
                    //aqui la tipo c
                    string dest = "";
                    string jump = "";
                    if (sOutput.Contains("="))
                    {
                        var parts = sOutput.Split(new[] { '=' }, StringSplitOptions.None);

                        sOutput = parts[1];
                        dest = parts[0];
                    }

                    string comp = sOutput;
                    if (sOutput.Contains(";"))
                    {
                        var parts = sOutput.Split(new[] { ";" }, StringSplitOptions.None);
                        comp = parts[0];
                        jump = parts[1];
                    }
                    //t1d.Add(sOutput);
                    string instc = "111";
                    if (dest == "") { instc = instc + "000"; }
                    else if (dest == "null") { instc = instc + "000"; }
                    else if (dest == "M") { instc = instc + "001"; }
                    else if (dest == "D") { instc = instc + "010"; }
                    else if (dest == "MD") { instc = instc + "011"; }
                    else if (dest == "A") { instc = instc + "100"; }
                    else if (dest == "AM") { instc = instc + "101"; }
                    else if (dest == "AD") { instc = instc + "110"; }
                    else if (dest == "AMD") { instc = instc + "111"; }
                    Console.WriteLine(dest);

                    if (comp == "0") { instc = instc + "0101010"; }
                    else if (comp == "1") { instc = instc + "0111111"; }
                    else if (comp == "-1") { instc = instc + "0111010"; }
                    else if (comp == "D") { instc = instc + "0001100"; }
                    else if (comp == "A") { instc = instc + "0110000"; }
                    else if (comp == "!D") { instc = instc + "0001101"; }
                    else if (comp == "!A") { instc = instc + "00110001"; }
                    else if (comp == "-D") { instc = instc + "0001111"; }
                    else if (comp == "-A") { instc = instc + "0110011"; }
                    else if (comp == "D+1") { instc = instc + "0011111"; }
                    else if (comp == "A+1") { instc = instc + "0110111"; }
                    else if (comp == "D-1") { instc = instc + "0001110"; }
                    else if (comp == "A-1") { instc = instc + "0110010"; }
                    else if (comp == "D+A") { instc = instc + "0000010"; }
                    else if (comp == "D-A") { instc = instc + "0010011"; }
                    else if (comp == "A-D") { instc = instc + "0000111"; }
                    else if (comp == "D&A") { instc = instc + "0000000"; }
                    else if (comp == "D|A") { instc = instc + "0010101"; }
                    else if (comp == "M") { instc = instc + "1110000"; }
                    else if (comp == "!M") { instc = instc + "1011001"; }
                    else if (comp == "-M") { instc = instc + "1110011"; }
                    else if (comp == "M+1") { instc = instc + "1110111"; }
                    else if (comp == "M-1") { instc = instc + "1110010"; }
                    else if (comp == "D+M") { instc = instc + "1000010"; }
                    else if (comp == "D-M") { instc = instc + "1010011"; }
                    else if (comp == "M-D") { instc = instc + "1000111"; }
                    else if (comp == "D&M") { instc = instc + "1000000"; }
                    else if (comp == "D|M") { instc = instc + "1010101"; }
                    Console.WriteLine(comp);
                    if (jump == "") { instc = instc + "000"; }
                    else if (jump == "0") { instc = instc + "000"; }
                    else if (jump == "null") { instc = instc + "000"; }
                    else if (jump == "JGT") { instc = instc + "001"; }
                    else if (jump == "JEQ") { instc = instc + "010"; }
                    else if (jump == "JGE") { instc = instc + "011"; }
                    else if (jump == "JLT") { instc = instc + "100"; }
                    else if (jump == "JNE") { instc = instc + "101"; }
                    else if (jump == "JLE") { instc = instc + "110"; }
                    else if (jump == "JMP") { instc = instc + "111"; }
                    Console.WriteLine(jump);
                    t1d.Add(instc);
                }  
            }
            foreach (string sOutput in t1d)
            {
                Console.WriteLine(sOutput);
            }
            Console.WriteLine(lineas);

            using (StreamWriter outputfile = new StreamWriter("C:\\Users\\Felipe\\Desktop\\Resultadoasm.asm"))
            {
                foreach (string linea in t1d)
                {
                    outputfile.WriteLine(linea);
                }

            }
        }
	}
}
