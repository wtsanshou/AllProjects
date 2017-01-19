using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiseaseRiskEstimate.DAO;
using System.Data;
using System.Windows.Forms;

namespace DiseaseRiskEstimate.Business
{
    class CSData
    {
        /*define the score array*/
        private int[] maleScore, femaleScore;

        /*define the sum score array*/
        private int[] maleSum, femaleSum;

        /*define the mobidity of coronary stroke */
        private string[] maleRisk, femaleRisk;

        /*the row of the variable*/
        private int ROW = 0;

        public int[] getMaleScore()
        {
            return maleScore;
        }

        public int[] getFemaleScore()
        {
            return femaleScore;
        }

        public int[] getMaleSum()
        {
            return maleSum;
        }

        public int[] getFemaleSum()
        {
            return femaleSum;
        }

        public string[] getMaleRisk()
        {
            return maleRisk;
        }

        public string[] getFemaleRisk()
        {
            return femaleRisk;
        }

        public CSData(string filePath, string fileName)
        {
            /*get variables from excel*/
            ConExcel ce = new ConExcel();
            DataSet d = ce.LoadDataFromExcel(Application.StartupPath + filePath, fileName);

            DataTable dt = d.Tables[0];
            DataRow[] myRow = dt.Select();
            int cl = dt.Columns.Count;
            ROW = dt.Rows.Count;

            maleScore = new int[ROW];
            femaleScore = new int[ROW];

            maleSum = new int[ROW];
            femaleSum = new int[ROW - 3];

            maleRisk = new string[ROW];
            femaleRisk = new string[ROW];

            string s = "";

            int i = 0;

            for (i = 0; i < ROW - 1; i++)
            {
                maleScore[i] = Convert.ToInt32(myRow[i][2]);
                s += maleScore[i].ToString() + ",";
                femaleScore[i] = Convert.ToInt32(myRow[i][cl - 3]);
                s += femaleScore[i].ToString() + "\t";
            }
            s += "\n";
            for (i = 0; i < ROW; i++)
            {
                maleSum[i] = Convert.ToInt32(myRow[i][3]);
                maleRisk[i] = Convert.ToString(myRow[i][4]);
                s += maleSum[i].ToString() + "\t";

            }
            s += "\n";
            for (i = 0; i < ROW - 3; i++)
            {
                femaleSum[i] = Convert.ToInt32(myRow[i][cl - 2]);
                femaleRisk[i] = Convert.ToString(myRow[i][cl - 1]);
                s += femaleSum[i].ToString() + "\t";
            }


            //MessageBox.Show(s);

        }
    }
}
