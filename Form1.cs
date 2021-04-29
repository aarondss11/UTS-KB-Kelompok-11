using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace starkartu
{
    public partial class Form1 : Form
    {

        List<Kartu> lkartu = new List<Kartu>();
        public Form1()
        {
            InitializeComponent();
        }


        public static List<Kartu> clonelist(List<Kartu> kt)
        {
            List<Kartu> temp = new List<Kartu>();
            for (int i=0;i<kt.Count;i++)
            {
                temp.Add(kt[i]);
            }
            return temp;
        }

        bool cekList(List<State> st,State stbaru)
        {
            for (int i = 0; i < st.Count;i++ )
            {
                if (st[i].cekState(stbaru))
                {
                    if (st[i].fx<=stbaru.fx)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            lkartu.Clear();
            int batas = Int32.Parse(tbIterasi.Text);

            String[] linekatu = textBox1.Text.ToString().Split('\n');
            
            for (int i=0;i<5;i++)
            {
                String temp = linekatu[i].Replace("\r",String.Empty);

                String[] akartu = temp.Split(' ');
                if (akartu.Count()>=2)
                {
                    Kartu kt = new Kartu(Int32.Parse(akartu[0]),akartu[1]);
                    lkartu.Add(kt);
                }
                //String a = "";
                // String a = "a";
            }

            State st = new State();
            st.lkartu = lkartu;
            st.calculateallvalue();

            List<State> open = new List<State>();
            List<State> close = new List<State>();
            open.Add(st);
            bool finished = false;

            int ctr = 0;
            State solution = null;
            State last = null;
            while (!finished && ctr<batas)
            {
                ctr++;
                //get smallest value
                float smval = 10000;
                int smindex = -1;

                if (open.Count<=0)
                {
                    finished = true;
                }
                else
                {
                    for (int i = 0; i < open.Count(); i++)
                    {
                        if (open[i].fx < smval)
                        {
                            smindex = i;
                            smval = open[i].fx;
                        }
                    }

                    if (smindex != -1)
                    {

                        State stExpand = open[smindex];
                        last = stExpand;
                        open.RemoveAt(smindex);
                        close.Add(stExpand);
                        if (stExpand.finish)
                        {
                            finished = true;
                            solution = stExpand;
                        }
                        else
                        {

                            //expand

                            for (int i = 0; i < stExpand.lkartu.Count - 1; i++)
                            {
                                List<Kartu> lk = Form1.clonelist(stExpand.lkartu);
                                Kartu k2 = lk[i];
                                lk[i] = lk[i+1];
                                lk[i+1] = k2;

                                State stnew = new State();
                                stnew.lkartu = lk;
                                stnew.copystep(stExpand);
                                stnew.addStep(stExpand.lkartu);
                                stnew.calculateallvalue();
                                if (cekList(open, stnew) && cekList(close, stnew))
                                {
                                    open.Add(stnew);
                                }

                            }
                            //lk[0] = lk[1];
                            //int ax = -1;
                        }


                    }
                }
                

            }


            if (solution==null)
            {
                //tbResult.Text = "Hasil tidak ditemukan";
                solution = last;
                String temp = "Hasil tidak ditemukan setelah "+batas+" iterasi, hasil terbaik : \r\n";
                for (int i = 0; i < 5; i++)
                {
                    temp = temp + solution.lkartu[i].isi + " " + solution.lkartu[i].tipe + ","+ solution.lkartu[i].warna + "\r\n";
                }
                tbResult.Text = temp;


                String temp2 = "";
                for (int i = 0; i < 5; i++)
                {
                    temp2 = temp2 + "------------------------\r\n";
                    temp2 = temp2 + "State ke " + (i + 1) + "\r\n";
                    for (int j = 0; j < solution.step[i].Count; j++)
                    {
                        temp2 = temp2 + solution.step[i][j].isi + " " + solution.step[i][j].tipe + ","+ solution.step[i][j].warna + "\r\n";
                    }
                    temp2 = temp2 + "\r\n";
                    //temp = temp + solution.lkartu[i].isi + " " + solution.lkartu[i].warna + "\r\n";
                }
                tbStep.Text = temp2;
            }
            else
            {
                String temp = "";
                for (int i=0;i<5;i++)
                {
                    temp = temp+solution.lkartu[i].isi + " " + solution.lkartu[i].tipe+ " "+solution.lkartu[i].warna + "\r\n";
                }
                tbResult.Text = temp;


                String temp2 = "";
                for (int i = 0; i < 5; i++)
                {
                    temp2 = temp2 + "------------------------\r\n";
                    temp2 = temp2 + "State ke "+(i+1)+"\r\n";
                    for (int j = 0; j < 5; j++)
                    {
                        temp2 = temp2 + solution.step[i][j].isi + " " + solution.step[i][j].tipe + "\r\n";
                    }
                    temp2 = temp2 + "\n\n";
                    //temp = temp + solution.lkartu[i].isi + " " + solution.lkartu[i].warna + "\r\n";
                }
                tbStep.Text = temp2;
            }
            //String a = "";
            //String a = "";
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("abc");
        }

        private void tbResult_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
