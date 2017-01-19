using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using DiseaseRiskEstimate.DAO;

namespace DiseaseRiskEstimate.Business
{
    public class DiseaseRiskDenominator
    {

        /* the value of Risk Denominator*/
        private double RD = 1;

        /*the variable RRC and Pi*/
        private double[] RRC, Pi;

        /*the row of the variable*/
        private int ROW = 0;

        /*get variables from excel*/
        public DiseaseRiskDenominator(string filePath, string fileName)
        {
            ConExcel ce = new ConExcel();
            DataSet d = ce.LoadDataFromExcel(Application.StartupPath + filePath, fileName);


            DataTable dt = d.Tables[0];
            DataRow[] myRow = dt.Select();
            int cl = dt.Columns.Count;
            ROW = dt.Rows.Count;


            RRC = new double[ROW];
            Pi = new double[ROW];

            String s = "";

            for (int i = 0; i < ROW; i++)
            {
                RRC[i] = Convert.ToDouble(myRow[i][cl - 2]);
                s += RRC[i].ToString() + ",";
                Pi[i] = Convert.ToDouble(myRow[i][cl - 1]);
                s += Pi[i].ToString() + "\n";
            }
            //MessageBox.Show(s);
        }

        /*return the array RRC*/
        public double[] getRRC()
        {
            return RRC;
        }


        /*calculate one line*/
        private double oneFactor(double RRC, double P)
        {
            return P * RRC + (1 - P);
        }

        /*calculate the denominator*/
        public double calculateRiskDenominator()
        {
            for (int i = 0; i < ROW; i++)
            {
                RD = RD * oneFactor(RRC[i], Pi[i]);
            }
            return RD;
        }
    }
}

