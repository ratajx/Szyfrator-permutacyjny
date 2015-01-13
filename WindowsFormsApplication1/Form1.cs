using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public double key_lenght = 0.0;
        char[] key_tab=new char[1];
        

        public Form1()
        {
            InitializeComponent();
            key_tab[0] = '~';
            
        }

     

        private void button1_Click(object sender, EventArgs e)
        {
            if (key_tab[0] != '~')
            {

                int txt_lenght = richTextBox1.TextLength;
                double z = (double)txt_lenght / key_lenght;
                int x = (int)Math.Ceiling(z);


                char[, ,] txt_tab = new char[x, (int)key_lenght, 2];

                char[] text_tab = new char[txt_lenght];
                text_tab = richTextBox1.Text.ToArray();

                int k = 0;
                for (int i = 0; i < x; i++)
                    for (int j = 0; j < key_lenght; j++)
                    {
                        if (k < txt_lenght)
                        {
                            txt_tab[i, j, 0] = text_tab[k];
                            txt_tab[i, j, 1] = '0';
                            k++;
                        }
                        else
                        {
                            for (int a = j++; a < key_lenght; a++)
                            {
                                txt_tab[i, a, 0] = '^';
                                txt_tab[i, a, 1] = '0';
                            }
                            break;
                        }
                    }

                char letter;
                char[] tmp_tab = new char[(int)key_lenght];

                for (int i = 0; i < x; i++)
                    for (int j = 0; j < key_lenght; j++)
                    {
                        letter = txt_tab[i, key_tab[j] - 49, 0];
                        tmp_tab[key_tab[j] - 49] = letter;


                        if (txt_tab[i, j, 1] == '0')
                            txt_tab[i, key_tab[j] - 49, 0] = txt_tab[i, j, 0];
                        else
                            txt_tab[i, key_tab[j] - 49, 0] = tmp_tab[j];

                        txt_tab[i, key_tab[j] - 49, 1] = '1';


                    }
                richTextBox2.Clear();
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("Zaszyfrowano tekst.");
                for (int i = 0; i < x; i++)
                    for (int j = 0; j < key_lenght; j++)
                    {
                        
                        richTextBox2.Text = richTextBox2.Text+txt_tab[i, j, 0].ToString();
                    }
            }
            else
            {
                MessageBox.Show("Podaj poprawny klucz");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                int licz = 0,dl=textBox1.TextLength;
                                
                char [] key = new char[dl];
                key = textBox1.Text.ToCharArray();
                for (int i = 0; i < dl; i++)
                {
                    if (key[i] == ',')
                        licz++;
                }


                key_lenght = licz+1;
                key_tab = new char[(int)key_lenght];

                Console.ForegroundColor = System.ConsoleColor.Cyan;
                Console.WriteLine("Klucz:");
                Console.ForegroundColor = System.ConsoleColor.DarkCyan;
                String str="";
                int index=0;
                for (int i = 0; i < dl; i++)
                {
                    if (i != dl - 1)
                    {
                        if (key[i] != ',')
                        {
                            str = str + key[i];
                            Console.Write(str+",");
                        }
                        else
                        {
                            try
                            {
                                key_tab[index] = (char)(Int32.Parse(str) + 48);
                                index++;
                                str = "";
                            }catch(FormatException){
                                MessageBox.Show("Błędny klucz");
                                break;
                            }
                        }
                    }
                    else
                    {
                        str = str + key[i];
                        key_tab[index] = (char)(Int32.Parse(str) + 48);
                        Console.WriteLine(str);
                    }
                    

                }
                

                int max = key_tab[0]-48;
                for(int i=1;i<key_lenght;i++)
                {
                    if (key_tab[i]-48 > max)
                        max = key_tab[i]-48;
                }

                int min = 1;
                bool wieksze = true;
                for (int i = 0; i < key_lenght; i++)
                {
                    if (key_tab[i]-48 < min)
                    {
                        wieksze = false;
                        break;
                    }
                }

                bool[] tab = new bool[(int)key_lenght+1];
                tab[0] = true;
                for (int i = 1; i < key_lenght+1; i++)
                {
                    for (int j = 0; j < key_lenght; j++)
                    {
                        if (key_tab[j]-48 == i)
                        {
                            tab[i] = true;
                            break;
                        }
                        else tab[i] = false;
                    }
                }

                bool ok = true;
                for (int i = 1; i < key_lenght+1; i++)
                {
                    if (tab[i] == false)
                        ok = false;
                }

              //  Console.WriteLine(max + " " + key_lenght + " " + min + " " + wieksze+" "+ok+"\n");
                if (max != key_lenght)
                {
                    MessageBox.Show("Klucz posiada niepoprawny format. Brakuje cyfr.");
                    key_tab[0] = '~';
                }
                if (wieksze == false)
                {
                    MessageBox.Show("Klucz posiada niepoprawny format. Cyfry musza byc > lub = 1.");
                    key_tab[0] = '~';
                }
                if (ok == false && wieksze == true && max == key_lenght)
                {
                    MessageBox.Show("Klucz posiada niepoprawny format. Cyfry powtarzają się.");
                    key_tab[0] = '~';
                }

            }
            else
            {
                MessageBox.Show("Podaj klucz");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int txt_lenght = richTextBox2.TextLength;
            int z = txt_lenght / (int)key_lenght;
  
            char[, ,] txt_tab = new char[z, (int)key_lenght, 2];

            char[] text_tab = new char[txt_lenght];
            text_tab = richTextBox2.Text.ToArray();

            int k = 0;
            for (int i = 0; i < z; i++)
                for (int j = 0; j < key_lenght; j++)
                {
                        txt_tab[i, j, 0] = text_tab[k];
                        txt_tab[i, j, 1] = '0';
                        k++;
                }
            char letter;
            char[] tmp_tab=new char[txt_lenght];
             for (int i = 0; i < z; i++)
                for (int j = 0; j < key_lenght; j++)
                {
                    letter = txt_tab[i, j, 0];
                    tmp_tab[j] = letter;


                    if (txt_tab[i, key_tab[j] - 49, 1] == '0')
                        txt_tab[i, j, 0] = txt_tab[i, key_tab[j] - 49, 0];
                    else
                        txt_tab[i, j, 0] = tmp_tab[key_tab[j] - 49];

                    txt_tab[i, j, 1] = '1';
                    
                    
                }
             richTextBox1.Clear();
             Console.ForegroundColor = System.ConsoleColor.Green;
                Console.WriteLine("Zdeszyfrowano tekst.");
             for (int i = 0; i < z; i++)
                for (int j = 0; j < key_lenght; j++)
                {
                    if(txt_tab[i, j, 0]!='^')
                    richTextBox1.Text = richTextBox1.Text + txt_tab[i, j, 0].ToString();
                }

           }

        private void button6_Click(object sender, EventArgs e)
        {

            if (textBox2.Text!="")
            {
                try
                {
                    key_lenght=Int32.Parse(textBox2.Text);
                    key_tab = new char[(int)key_lenght];
                    Random r=new Random();
                    textBox1.Clear();

                    int num;
                    bool eq;
                    Console.ForegroundColor = System.ConsoleColor.Cyan;
                    Console.WriteLine("Klucz:");
                    Console.ForegroundColor = System.ConsoleColor.DarkCyan;
                    for (int i = 0; i < key_lenght; i++)
                    {
                        
                        do
                        {   eq = false;
                            num = r.Next(1, (int)key_lenght + 1);
                            for (int j = 0; j < key_lenght; j++)
                            {
                                if (key_tab[j]-48 == num)
                                {
                                    eq = true;
                                    break;
                                }
                            }
                            if (eq == false)
                            {
                                if (i == key_lenght - 1)
                                {
                                    textBox1.Text = textBox1.Text + num;
                                    Console.WriteLine(num);
                                }
                                else
                                {
                                    textBox1.Text = textBox1.Text + num + ",";
                                    Console.Write(num + ",");
                                }
                                key_tab[i] = (char)(num+48);
                                
                                
                            }
                        } while (eq);
                    }
                    }
                catch(FormatException)
                {
                MessageBox.Show("Niepoprawny format długości klucza");
                }
            }
            else
            {
                MessageBox.Show("Podaj długość klucza");
            }
           

        }

        private void button4_Click(object sender, EventArgs e)
        {

            OpenFileDialog okienko = new OpenFileDialog();
            okienko.Filter = "Pliki textowe (txt)|*.txt";
            if (okienko.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                string path = okienko.FileName;
                StreamReader sr = new StreamReader(path, Encoding.Default);
                string s;
                richTextBox1.Clear();
                Console.ForegroundColor = System.ConsoleColor.Yellow;
                Console.Write("Wczytano tekst z pliku ");
                Console.ForegroundColor = System.ConsoleColor.Green;
                Console.WriteLine("jawnego.");
                do
                {
                    s = sr.ReadLine();
                    richTextBox1.Text = richTextBox1.Text + s;
                    sb.AppendLine(s);
                } while (s != null);
                sr.Close();
            }
            else MessageBox.Show("Nie wczytano  pliku.");

            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog okienko = new OpenFileDialog();
            okienko.Filter = "Pliki textowe (txt)|*.txt";
            if (okienko.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                string path = okienko.FileName;
                StreamReader sr = new StreamReader(path, Encoding.Default);
                string s;
                richTextBox2.Clear();
                Console.ForegroundColor = System.ConsoleColor.Yellow;
                Console.Write("Wczytano tekst z pliku ");
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("zaszyfrowanego.");
                do
                {
                    s = sr.ReadLine();
                    richTextBox2.Text = richTextBox2.Text + s;
                    sb.AppendLine(s);
                } while (s != null);
                sr.Close();
            }
            else MessageBox.Show("Nie wczytywano pliku.");
        }

        private void button7_Click(object sender, EventArgs e)
        {
             SaveFileDialog okienko = new SaveFileDialog();
            okienko.Filter = "Pliki textowe (txt)|*.txt";
            if (okienko.ShowDialog() == DialogResult.OK)
            {
                Console.ForegroundColor = System.ConsoleColor.Yellow;
                Console.Write("Zapisano tekst do pliku z tekstem ");
                Console.ForegroundColor = System.ConsoleColor.Green;
                Console.WriteLine("jawnym.");
                string path = okienko.FileName;
                StreamWriter sw = new StreamWriter(path,true,Encoding.Default);
                sw.WriteLine(richTextBox1.Text);
                sw.Close();
            }
            else MessageBox.Show("Nie zapisano pliku");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SaveFileDialog okienko = new SaveFileDialog();
            okienko.Filter = "Pliki textowe (txt)|*.txt";
            if (okienko.ShowDialog() == DialogResult.OK)
            {
                Console.ForegroundColor = System.ConsoleColor.Yellow;
                Console.Write("Zapisano tekst do pliku z tekstem ");
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("zaszyfrowanym.");
                string path = okienko.FileName;
                StreamWriter sw = new StreamWriter(path,true,Encoding.Default);
                sw.WriteLine(richTextBox2.Text);
                sw.Close();
            }
            else MessageBox.Show("Nie zapisano pliku");
        }

        private void informacjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        }
 }
