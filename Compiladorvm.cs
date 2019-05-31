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
using System.Text.RegularExpressions;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader oblector = new StreamReader(@"C:\Users\Felipe\Desktop\Pruebavm.txt");
            string sLine = "";
            ArrayList arrText = new ArrayList();
            ArrayList tlimpio = new ArrayList();
            ArrayList listafuncion = new ArrayList();

            while (sLine != null)
            {
                sLine = oblector.ReadLine();
                if ((sLine != null) && (sLine.Length > 0))
                {
                    arrText.Add(sLine);
                }
            }
            oblector.Close();

            string dato = "";
            string inst = "";
            string seg = "";
            string num = "";
            string instru = "";
            int index = 1;
            string segtem = "";
            string nm = "";
            string inst2 = "";
            string label = "";
            string funcion = "";
            string fname = "";
            string arg = "";
            



            foreach (string sOutput in arrText)
            {
                dato = sOutput;
                inst = "";
                seg = "";
                num = "";
                instru = "";
                inst2 = "";
                label = "";
                funcion = "";
                fname = "";
                arg = "";

                if (dato.Contains("function")| dato.Contains("call"))
                {
                    var parts2 = dato.Split(new[] { ' ' }, StringSplitOptions.None);
                    funcion = parts2[0];
                    fname = parts2[1];
                    arg = parts2[2];
                }

                if (funcion == "function")
                {
                    for (int i = 0; i < arg.Length; i++)
                    {
                        tlimpio.Add("(" + fname + ")");
                        tlimpio.Add("@0");
                        tlimpio.Add("D=A");
                        tlimpio.Add("@SP");
                        tlimpio.Add("A=M");
                        tlimpio.Add("M=D");
                        tlimpio.Add("@SP");
                        tlimpio.Add("M=M+1");
                    }
                }

                if (funcion == "call")
                {
                    tlimpio.Add("@" + fname + "funcionlabelindex");
                    tlimpio.Add("D=A");
                    tlimpio.Add("@SP");
                    tlimpio.Add("A=M");
                    tlimpio.Add("M=D");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M+1");

                    tlimpio.Add("@LCL");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@SP");
                    tlimpio.Add("A=M");
                    tlimpio.Add("M=D");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M+1");

                    tlimpio.Add("@ARG");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@SP");
                    tlimpio.Add("A=M");
                    tlimpio.Add("M=D");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M+1");

                    tlimpio.Add("@THIS");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@SP");
                    tlimpio.Add("A=M");
                    tlimpio.Add("M=D");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M+1");

                    tlimpio.Add("THAT");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@SP");
                    tlimpio.Add("A=M");
                    tlimpio.Add("M=D");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M+1");

                    tlimpio.Add("D=M");
                    tlimpio.Add("@"+(5+arg));
                    tlimpio.Add("D=D-A");
                    tlimpio.Add("@ARG");
                    tlimpio.Add("M=D");

                    tlimpio.Add("@"+fname);
                    tlimpio.Add("0;JMP");
                    tlimpio.Add("(" + fname + "funcionlabelindex"+")");

                }

                if (dato.Contains("return"))
                {
                    tlimpio.Add("@LCL");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@frame");
                    tlimpio.Add("M=D");

                    tlimpio.Add("@5");
                    tlimpio.Add("D=D-A");
                    tlimpio.Add("A=D");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@return");
                    tlimpio.Add("M=D");
                    tlimpio.Add("@LCL");

                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M-1");
                    tlimpio.Add("A=M");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@ARG");
                    tlimpio.Add("A=M");
                    tlimpio.Add("M=D");

                    tlimpio.Add("@ARG");
                    tlimpio.Add("D=M+1");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=D");

                    tlimpio.Add("@frame");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@1");
                    tlimpio.Add("D=D-A");
                    tlimpio.Add("A=D");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@THAT");
                    tlimpio.Add("M=D");

                    tlimpio.Add("@frame");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@2");
                    tlimpio.Add("D=D-A");
                    tlimpio.Add("A=D");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@THIS");
                    tlimpio.Add("M=D");

                    tlimpio.Add("@frame");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@3");
                    tlimpio.Add("D=D-A");
                    tlimpio.Add("A=D");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@ARG");
                    tlimpio.Add("M=D");

                    tlimpio.Add("@frame");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@4");
                    tlimpio.Add("D=D-A");
                    tlimpio.Add("A=D");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@LCL");
                    tlimpio.Add("M=D");
                    tlimpio.Add("@return");
                    tlimpio.Add("A=M");
                    tlimpio.Add("0;JMP");
                }

                if (dato.Contains("label") | dato.Contains("goto") | dato.Contains("if-goto"))
                {
                    var parts1 = dato.Split(new[] { ' ' }, StringSplitOptions.None);
                    inst2 = parts1[0];
                    label = parts1[1];
                }

                if (inst2=="label") {

                    tlimpio.Add("("+label+")");
                }
                if (inst2 == "if-goto")
                {
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M-1");
                    tlimpio.Add("A=M");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@"+label);
                    tlimpio.Add("D;JNE");
                }
                if (inst2 == "goto")
                {
                    tlimpio.Add("@"+label);
                    tlimpio.Add("0;JMP");
                }

                if (dato.Contains("push") | dato.Contains("pop"))
                    {
                    var parts = dato.Split(new[] { ' ' }, StringSplitOptions.None);

                    inst = parts[0];
                    seg = parts[1];
                    num = parts[2];
                    Console.WriteLine(inst);
                    Console.WriteLine(seg);
                    Console.WriteLine(num);

                    if (seg == "local") { segtem = "LCL"; }
                    if (seg == "argument") { segtem = "ARG"; }
                    if (seg == "this") { segtem = "THIS"; }
                    if (seg == "that") { segtem = "THAT"; }



                    if (inst == "push")
                    {
                        if (seg == "constant")
                        {
                            tlimpio.Add("@" + num);
                            tlimpio.Add("D=A");
                            tlimpio.Add("@SP");
                            tlimpio.Add("A=M");
                            tlimpio.Add("M=D");
                            tlimpio.Add("@SP");
                            tlimpio.Add("M=M+1");
                        }
                        else if (seg == "local" | seg == "argument" | seg == "this" | seg == "that")
                        {
                            tlimpio.Add("@" + num);
                            tlimpio.Add("D=A");
                            tlimpio.Add("@" + segtem);
                            tlimpio.Add("A=D+M");
                            tlimpio.Add("D=M");
                            tlimpio.Add("@SP");
                            tlimpio.Add("A=M");
                            tlimpio.Add("M=D");
                            tlimpio.Add("@SP");
                            tlimpio.Add("M=M+1");
                        }
                        else if (seg == "temp")
                        {
                            tlimpio.Add("@" + num);
                            tlimpio.Add("D=A");
                            tlimpio.Add("@5");
                            tlimpio.Add("A=D+A");
                            tlimpio.Add("D=M");
                            tlimpio.Add("@SP");
                            tlimpio.Add("A=M");
                            tlimpio.Add("M=D");
                            tlimpio.Add("@SP");
                            tlimpio.Add("M=M+1");
                        }
                        else if (seg == "pointer")
                        {
                            if (int.Parse(num) == 0) { nm = "THIS"; }
                            else { nm = "THAT"; }
                            tlimpio.Add("@" + nm);
                            tlimpio.Add("D=M");
                            tlimpio.Add("@SP"); 
                            tlimpio.Add("A=M");
                            tlimpio.Add("M=D");
                            tlimpio.Add("@SP");
                            tlimpio.Add("M=M+1");
                        }
                        else if (seg == "static")
                        {
                            tlimpio.Add("@" + "filename" + "." + num);
                            tlimpio.Add("D=M");
                            tlimpio.Add("@SP");
                            tlimpio.Add("A=M");
                            tlimpio.Add("M=D");
                            tlimpio.Add("@SP");
                            tlimpio.Add("M=M+1");

                        }

                    }

                    if (inst == "pop")
                    {
                        if (seg == "local" | seg == "argument" | seg == "this" | seg == "that")
                        {
                            tlimpio.Add("@" + num);
                            tlimpio.Add("D=A");
                            tlimpio.Add("@" + segtem);
                            tlimpio.Add("D=D+M");
                            tlimpio.Add("@frame");
                            tlimpio.Add("M=D");
                            tlimpio.Add("@SP");
                            tlimpio.Add("M=M-1");
                            tlimpio.Add("A=M");
                            tlimpio.Add("D=M");
                            tlimpio.Add("@frame");
                            tlimpio.Add("A=M");
                            tlimpio.Add("M=D");
                        }
                        else if (seg == "temp")
                        {
                            tlimpio.Add("@" + num);
                            tlimpio.Add("D=A");
                            tlimpio.Add("@5");
                            tlimpio.Add("D=D+A");
                            tlimpio.Add("@frame");
                            tlimpio.Add("M=D");
                            tlimpio.Add("@SP");
                            tlimpio.Add("M=M-1");
                            tlimpio.Add("A=M");
                            tlimpio.Add("D=M");
                            tlimpio.Add("@frame");
                            tlimpio.Add("A=M");
                            tlimpio.Add("M=D");
                        }
                        else if (seg == "pointer")
                        {
                            if (int.Parse(num) == 0) { nm = "THIS"; }
                            else { nm = "THAT"; }
                            tlimpio.Add("@SP");
                            tlimpio.Add("M=M-1");
                            tlimpio.Add("A=M");
                            tlimpio.Add("D=M");
                            tlimpio.Add("@" + nm);
                            tlimpio.Add("M=D");
                        }

                        else if (seg == "static")
                        {
                            tlimpio.Add("@SP");
                            tlimpio.Add("M=M-1");
                            tlimpio.Add("A=M");
                            tlimpio.Add("D=M");
                            tlimpio.Add("@" + "filename" + "." + num);
                            tlimpio.Add("M=D");
                        }
                    }
                }

                else { instru = dato; }
                if (instru == "add")
                {
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M-1");
                    tlimpio.Add("A=M");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M-1");
                    tlimpio.Add("A=M");
                    tlimpio.Add("M=D+M");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M+1");
                }
                if (instru == "sub")
                {
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M-1");
                    tlimpio.Add("A=M");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M-1");
                    tlimpio.Add("A=M");
                    tlimpio.Add("M=M-D");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M+1");
                }
                if (instru == "neg")
                {
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M-1");
                    tlimpio.Add("A=M");
                    tlimpio.Add("D=-M");
                    tlimpio.Add("M=D");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M+1");
                }
                if (instru == "eq")
                {
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M-1");
                    tlimpio.Add("A=M");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M-1");
                    tlimpio.Add("A=M");
                    tlimpio.Add("D=D-M");
                    tlimpio.Add("@IGUAL" + index.ToString());
                    tlimpio.Add("D;JEQ");
                    tlimpio.Add("D=0");
                    tlimpio.Add("@FIN" + index.ToString());
                    tlimpio.Add("0;JEQ");
                    tlimpio.Add("(IGUAL" + index.ToString() + ")");
                    tlimpio.Add("D=-1");
                    tlimpio.Add("(FIN" + index.ToString() + ")");
                    tlimpio.Add("@SP");
                    tlimpio.Add("A=M");
                    tlimpio.Add("M=D");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M+1");

                    index++;
                }
                if (instru == "gt")
                {
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M-1");
                    tlimpio.Add("A=M");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M-1");
                    tlimpio.Add("A=M");
                    tlimpio.Add("D=M-D");
                    tlimpio.Add("@G_T" + index.ToString());
                    tlimpio.Add("D;JGT");
                    tlimpio.Add("D=0");
                    tlimpio.Add("@FIN" + index.ToString());
                    tlimpio.Add("0;JEQ");
                    tlimpio.Add("(G_T" + index.ToString() + ")");
                    tlimpio.Add("D=-1");
                    tlimpio.Add("(FIN" + index.ToString() + ")");
                    tlimpio.Add("@SP");
                    tlimpio.Add("A=M");
                    tlimpio.Add("M=D");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M+1");

                    index++;
                }
                if (instru == "lt")
                {
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M-1");
                    tlimpio.Add("A=M");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M-1");
                    tlimpio.Add("A=M");
                    tlimpio.Add("D=M-D");
                    tlimpio.Add("@L_T" + index.ToString());
                    tlimpio.Add("D;JLT");
                    tlimpio.Add("D=0");
                    tlimpio.Add("@FIN" + index.ToString());
                    tlimpio.Add("0;JEQ");
                    tlimpio.Add("(L_T" + index.ToString() + ")");
                    tlimpio.Add("D=-1");
                    tlimpio.Add("(FIN" + index.ToString() + ")");
                    tlimpio.Add("@SP");
                    tlimpio.Add("A=M");
                    tlimpio.Add("M=D");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M+1");

                    index++;
                }

                if (instru == "and")
                {
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M-1");
                    tlimpio.Add("A=M");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M-1");
                    tlimpio.Add("A=M");
                    tlimpio.Add("D=D&M");
                    tlimpio.Add("M=D");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M+1");

                    index++;
                }

                if (instru == "or")
                {
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M-1");
                    tlimpio.Add("A=M");
                    tlimpio.Add("D=M");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M-1");
                    tlimpio.Add("A=M");
                    tlimpio.Add("D=D|M");
                    tlimpio.Add("M=D");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M+1");

                    index++;
                }
                if (instru == "not")
                {
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M-1");
                    tlimpio.Add("A=M");
                    tlimpio.Add("D=!M");
                    tlimpio.Add("M=D");
                    tlimpio.Add("@SP");
                    tlimpio.Add("M=M+1");

                    index++;
                }
            }
            foreach (string sOutput in tlimpio)
            {
                Console.WriteLine(sOutput);
            }

            using (StreamWriter outputfile = new StreamWriter("C:\\Users\\Felipe\\Desktop\\Resultado.asm")) {
                foreach (string linea in tlimpio) {
                    outputfile.WriteLine(linea);
                }

            }

                Console.Read();


        }
    }
}
