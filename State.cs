using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace starkartu
{
    class State
    {
        public List<Kartu> lkartu = new List<Kartu>();
        public float gx;
        public float fx;
        public float hx;
        public bool finish = false;
        public List<List<Kartu>> step=new List<List<Kartu>>();
        public State()
        {

        }
        

        public bool cekState(State stc)
        {
            for (int i=0;i<stc.lkartu.Count-1;i++)
            {
                if (stc.lkartu[i]!=lkartu[i])
                {
                    return false;
                }
            }
            return true;
        }
        public void addStep(List<Kartu> tstep)
        {
            step.Add(tstep);

        }
        public void copystep(State x)
        {
            for (int i = 0; i < x.step.Count;i++ )
            {
                step.Add(x.step[i]);
            }
            
        }

        bool checkprimenumber(int n)
        {
            int m = n / 2;
            for (int i = 2; i <= m; i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }


        public void calculateallvalue()
        {
            gx = step.Count;


            finish = true;
            int totaltidaksama = 0;
            int totalprima = 0;
            for (int i=0;i<lkartu.Count-1;i++)
            {
                if (lkartu[i].warna!=(lkartu[i+1].warna))
                {
                    totaltidaksama++;
                }
                else
                {
                    finish = false;
                }
                int totalA = lkartu[i].isi + lkartu[i+1].isi;
                if (checkprimenumber(totalA))
                {
                    totalprima++;
                }
                else
                {
                    finish = false;
                }
            }


            hx = (lkartu.Count * 2) - totaltidaksama - totalprima;
            fx = hx + gx;
            int a = -1;
            
        }

        

    }
}

