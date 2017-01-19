using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DiseaseRiskEstimate.Business;
using System.Text.RegularExpressions;

namespace DiseaseRiskEstimate
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            /*Framingham default*/
            FraAge.SelectedIndex = 0;

            FraTC.SelectedIndex = 0;

            FraHDL.SelectedIndex = 0;

            FraSystolicPressure.SelectedIndex = 0;
        }

        /*function of restricting*/
        private void numberRestrict( TextBox t, double min, double max)
        {
            string RegStr = "^[0-9]*$";
            Regex rg = new Regex(RegStr);
            if (t.Text != "" && rg.IsMatch(t.Text, 0))
            {
                double key = Convert.ToDouble(t.Text);
                if (key <= min || key > max)
                {
                    double m = min + 1;

                    MessageBox.Show("请输入 " + m + " 到 " + max + " 的整数", "系统提示",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    t.Text = "";
                }
            }
            else if (!rg.IsMatch(t.Text, 0) && t.Text != "")
            {
                double m = min + 1;
                MessageBox.Show("请输入 " + m + " 到 " + max + " 的整数", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                t.Text = "";
            }
            
        }

        /*function of calculating BMI value*/
        private double calculateBMI(TextBox weight, TextBox height)
        {
            double bmi = 0;
            if (weight.Text != "" && height.Text != "")
            {
                double w = Convert.ToDouble(weight.Text);
                double h = Convert.ToDouble(height.Text);
                h /=100;
                try
                {
                    bmi = w / (h * h);
                }
                catch(Exception e2)
                {
                    e2.StackTrace.ToString();
                }
            }
            return bmi;
        }

        /*function of calculating BMI level*/
        private int calculateBMILevel(double bmi)
        {
            if (bmi < 24)
            {
                return 0;
            }
            else if (bmi >= 24 && bmi < 28)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        /****************************************************
         * Lung Cancer
         ****************************************************/

        /*The values of Lung Cancer Risk*/
        Double lungRisk = 1.0;

        /*The values of Lung Cancer fianl Risk */
        Double lungFinalRish = 1.0;

        /*if miss the factor function multiply by 1.0*/
        const double lungMissingData = 1.0;

        /*the rate of disease in the certain age*/
        private double lungrate = 0.00032;

        /*the default test years*/
        private int lungyears = 5;

        /*the rate of disease increasing*/
        private double lungincrease = 0.1;

        /*reset the values in the lung page*/
        private void LungReset_Click(object sender, EventArgs e)
        {
            LungName.Text = "";

            LungAge.Text = "";

            LungSmokingState.SelectedIndex = -1;

            LungSmokingYears.Text = "";

            LungSmokingNumber.Text = "";

            LungSmokingIndex.Text = "";

            lungSmoking.SelectedIndex = -1;

            LungAsbestos.Checked = false;

            LungSilica.Checked = false;

            LungSootTar.Checked = false;

            LungBadAir.Checked = false;

            LungHistory.Checked = false;

            LungPhthisis.Checked = false;

            LungBronchitis.Checked = false;

            LungPneumonia.Checked = false;

            LungFruitVegetable.Checked = false;

            LungYears.SelectedIndex = -1;

            LungRisk.Text = "";

            LungFinalRisk.Text = "";
        }

        /*restrict the format of age in the lung page*/
        private void LungAge_Leave(object sender, EventArgs e)
        {
            numberRestrict(LungAge, 0, 200);
        }

        /*Function of calculating smoking index*/
        private void smokingIndex()
        {
            /*calculate the index of smoking if the person smoking*/
            try
            {
                if (LungSmokingYears.Enabled && LungSmokingNumber.Enabled)
                {
                    if (LungSmokingYears.Text != "" && LungSmokingNumber.Text != "")
                    {
                        double year = Convert.ToDouble(LungSmokingYears.Text);
                        double number = Convert.ToDouble(LungSmokingNumber.Text);
                        double smokingIndex = year * number;

                        if (smokingIndex < 100)
                        {
                            lungSmoking.SelectedIndex = 2;
                        }
                        else if (smokingIndex >= 100 && smokingIndex < 200)
                        {
                            lungSmoking.SelectedIndex = 3;
                        }
                        else if (smokingIndex >= 200 && smokingIndex < 300)
                        {
                            lungSmoking.SelectedIndex = 4;
                        }
                        else if (smokingIndex >= 300 && smokingIndex < 400)
                        {
                            lungSmoking.SelectedIndex = 5;
                        }
                        else
                        {
                            lungSmoking.SelectedIndex = 6;
                        }
                    }
                    else
                    {
                        lungSmoking.SelectedIndex = 2;
                    }

                    LungSmokingIndex.Text = lungSmoking.Text;
                }
            }
            catch (Exception e1)
            {
                e1.StackTrace.ToString();
            }
        }

        /*restrict the format of smoking year*/
        private void LungSmokingYears_Leave(object sender, EventArgs e)
        {
            numberRestrict(LungSmokingYears, 0, 100);
            smokingIndex();
        }

        /*restrict the format of the average smoking number*/
        private void LungSmokingNumber_Leave_1(object sender, EventArgs e)
        {
            numberRestrict(LungSmokingNumber, 0, 1000);
            smokingIndex();
        }

        /*make the textboxes of smoking year and number enable
         change the combobox LungSmoking index 
         */
        private void LungSmokingState_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (LungSmokingState.SelectedIndex == 2)
            {
                LungSmokingYears.Enabled = true;
                LungSmokingNumber.Enabled = true;
                lungSmoking.SelectedIndex = 2;
            }
            else
            {
                LungSmokingYears.Text = "";
                LungSmokingNumber.Text = "";
                LungSmokingIndex.Text = "";
                LungSmokingYears.Enabled = false;
                LungSmokingNumber.Enabled = false;
            }

            switch (LungSmokingState.SelectedIndex)
            { 
                case 0:
                    lungSmoking.SelectedIndex = 0;
                    break;
                case 1:
                    lungSmoking.SelectedIndex = 1;
                    break;
                case 3:
                    lungSmoking.SelectedIndex = 7;
                    break;
                default:
                    break;
            }
        }

        /*calcuate the lung cancer risk*/
        private void LungCalculate_Click(object sender, EventArgs e)
        {
            /* get data of disease factors from Excel*/
            DiseaseRiskDenominator dre = new DiseaseRiskDenominator("\\Data\\LungCancerFactors.xls", "LungCancer");
            double[] rrc = dre.getRRC();
            lungRisk = 1;
            lungFinalRish = 1;

            /*Calculate smoking history */
            switch (lungSmoking.SelectedIndex)
            {
                case 1:
                    lungRisk = lungRisk * rrc[0];
                    break;
                case 2:
                    lungRisk = lungRisk * rrc[1];
                    break;
                case 3:
                    lungRisk = lungRisk * rrc[2];
                    break;
                case 4:
                    lungRisk = lungRisk * rrc[3];
                    break;
                case 5:
                    lungRisk = lungRisk * rrc[4];
                    break;
                case 6:
                    lungRisk = lungRisk * rrc[5];
                    break;
                case 7:
                    lungRisk = lungRisk * rrc[6];
                    break;

                default:
                    lungRisk = lungRisk * lungMissingData;
                    break;
            }

            /*Calculate career touching history */

            if (LungAsbestos.Checked)
            {
                lungRisk = lungRisk * rrc[7];
            }

            if (LungSilica.Checked)
            {
                lungRisk = lungRisk * rrc[8];
            }

            if (LungSootTar.Checked)
            {
                lungRisk = lungRisk * rrc[9];
            }

            /*Calculate air pollute */
            if (LungBadAir.Checked)
            {
                lungRisk = lungRisk * rrc[10];
            }

            /*Calculate Lung Cancer familly history */
            if (LungHistory.Checked)
            {
                lungRisk = lungRisk * rrc[11];
            }

            /*Calculate disease history */
            if (LungPhthisis.Checked)
            {
                lungRisk = lungRisk * rrc[12];
            }

            if (LungBronchitis.Checked)
            {
                lungRisk = lungRisk * rrc[13];
            }

            if (LungPneumonia.Checked)
            {
                lungRisk = lungRisk * rrc[14];
            }

            /*Calculate the amount of eating fruits and vegetables*/
            if (LungFruitVegetable.Checked)
            {
                lungRisk = lungRisk * rrc[15];
            }

            /*judge calculate years */
            switch (LungYears.SelectedIndex)
            {
                case 0:
                    lungyears = 5;
                    break;
                case 1:
                    lungyears = 10;
                    break;
                case 2:
                    lungyears = 15;
                    break;
                case 3:
                    lungyears = 20;
                    break;
                default:
                    lungyears = 5;
                    break;
            }

            /*get the risk using function */
            lungRisk = lungRisk / dre.calculateRiskDenominator();

            LungRisk.Text = lungRisk.ToString();

            /*increase risk rate in some years*/
            double icrEnd = 1.0;
            for (int i = 0; i < lungyears; i++)
            {
                icrEnd = icrEnd * (1 + lungincrease);
            }
            lungFinalRish = lungRisk * lungyears * lungrate * icrEnd;

            LungFinalRisk.Text = lungFinalRish.ToString();
        }

        

        /****************************************************
         * Glycuresis
         ****************************************************/
        /*The values of Glycuresis Risk*/
        Double glyRisk = 1.0;

        /*The values of Glycuresis fianl Risk */
        Double glyFinalRish = 1.0;

        /*if miss the factor function multiply by 1.0*/
        const double glyMissingData = 1.0;

        /*the rate of disease in the certain age*/
        private double glyrate = 0.0034;

        /*the default test years*/
        private int glyyears = 5;

        /*the rate of disease increasing*/
        private double glyincrease = 0.1;

        /*Calculate the risk of Glycuresis*/
        private void GlyCalculate_Click(object sender, EventArgs e)
        {
            /* get data of disease factors from Excel*/
            DiseaseRiskDenominator dre = new DiseaseRiskDenominator("\\Data\\GlycuresisDiseaseFactors.xls", "Glycuresis");
            double[] rrc = dre.getRRC();
            glyRisk = 1;
            glyFinalRish = 1;

            /*Calculate disease history */

            switch (GlyHistory.SelectedIndex)
            {
                case 1:
                    glyRisk = glyRisk * rrc[0];
                    break;
                case 2:
                    glyRisk = glyRisk * rrc[1];
                    break;
                default:
                    glyRisk = glyRisk * glyMissingData;
                    break;
            }

            /*Calculate Weight */
            switch (GlyWeight.SelectedIndex)
            {
                case 1:
                    glyRisk = glyRisk * rrc[2];
                    break;
                case 2:
                    glyRisk = glyRisk * rrc[3];
                    break;
                default:
                    glyRisk = glyRisk * glyMissingData;
                    break;
            }

            /*Calculate male waistline */
            switch (GlyMaleWaistline.SelectedIndex)
            {
                case 1:
                    glyRisk = glyRisk * rrc[4];
                    break;
                case 2:
                    glyRisk = glyRisk * rrc[5];
                    break;
                case 3:
                    glyRisk = glyRisk * rrc[6];
                    break;
                default:
                    glyRisk = glyRisk * glyMissingData;
                    break;
            }

            /*Calculate female waistline */
            switch (GlyFemaleWaistline.SelectedIndex)
            {
                case 1:
                    glyRisk = glyRisk * rrc[4];
                    break;
                case 2:
                    glyRisk = glyRisk * rrc[5];
                    break;
                case 3:
                    glyRisk = glyRisk * rrc[6];
                    break;
                default:
                    glyRisk = glyRisk * glyMissingData;
                    break;
            }

            /*Calculate Physical Activity*/
            if (GlyAlwaysSit.Checked == true)
            {
                glyRisk = glyRisk * rrc[7];
            }

            if (GlyInsufficientPhysicalActivity.Checked == true)
            {
                glyRisk = glyRisk * rrc[8];
            }

            if (GlyMeat300.Checked == true)
            {
                glyRisk = glyRisk * rrc[9];
            }

            if (GlyLessFruit.Checked == true)
            {
                glyRisk = glyRisk * rrc[10];
            }

            if (GlyOverDrink.Checked == true)
            {
                glyRisk = glyRisk * rrc[11];
            }

            if (GlySmoking.Checked == true)
            {
                glyRisk = glyRisk * rrc[12];
            }

            if (GlyBloodGlucose.Checked == true)
            {
                glyRisk = glyRisk * rrc[13];
            }

            if (GlyHypertensionHistory.Checked == true)
            {
                glyRisk = glyRisk * rrc[14];
            }

            /* Calculate blood fat*/
            if (GlyCholesterol52.Checked == true)
            {
                glyRisk = glyRisk * rrc[15];
            }

            if (GlyTriglyceride17.Checked == true)
            {
                glyRisk = glyRisk * rrc[16];
            }

            /*judge calculate years */
            switch (GlyYears.SelectedIndex)
            {
                case 0:
                    glyyears = 5;
                    break;
                case 1:
                    glyyears = 10;
                    break;
                case 2:
                    glyyears = 15;
                    break;
                case 3:
                    glyyears = 20;
                    break;
                default:
                    glyyears = 5;
                    break;
            }

            /*get the risk using function */
            glyRisk = glyRisk / dre.calculateRiskDenominator();

            GlyRisk.Text = glyRisk.ToString();

            /*increase risk rate in some years*/
            double icrEnd = 1.0;
            for (int i = 0; i < glyyears; i++)
            {
                icrEnd = icrEnd * (1 + glyincrease);
            }
            glyFinalRish = glyRisk * glyyears * glyrate * icrEnd;

            GlyFinalRisk.Text = glyFinalRish.ToString();
        }

        /*Reset the value of the Glycuresis page*/
        private void GlyReset_Click(object sender, EventArgs e)
        {
            GlyHistory.Text = "";
            GlyHistory.SelectedIndex = -1;

            GlyWeightValue.Text = "";
            GlyHeight.Text = "";
            GlyBMI.Text = "";

            GlyWeight.Text = "";
            GlyWeight.SelectedIndex = -1;

            GlyMaleWaistline.Text = "";
            GlyMaleWaistline.SelectedIndex = -1;

            GlyFemaleWaistline.Text = "";
            GlyFemaleWaistline.SelectedIndex = -1;

            GlyAlwaysSit.Checked = false;
            GlyInsufficientPhysicalActivity.Checked = false;
            GlyMeat300.Checked = false;
            GlyLessFruit.Checked = false;
            GlyOverDrink.Checked = false;
            GlySmoking.Checked = false;
            GlyBloodGlucose.Checked = false;
            GlyHypertensionHistory.Checked = false;

            GlyCholesterol52.Checked = false;
            GlyTriglyceride17.Checked = false;

            GlyYears.Text = "";
            GlyYears.SelectedIndex = -1;

            GlyRisk.Text = "";
            GlyFinalRisk.Text = "";
        }

        /*Restrict the age of the person*/
        private void GlyAge_Leave(object sender, EventArgs e)
        {
            numberRestrict(GlyAge, 0, 200);
        }

        /*Restrict the height of the person*/
        private void GlyHeight_Leave(object sender, EventArgs e)
        {
            numberRestrict(GlyHeight, 0, 300);
            double bmi = calculateBMI(GlyWeightValue, GlyHeight);
            GlyBMI.Text = bmi.ToString();
            GlyWeight.SelectedIndex = calculateBMILevel(bmi);
        }

        /*Restrict the weight of the person*/
        private void GlyWeightValue_Leave(object sender, EventArgs e)
        {
            numberRestrict(GlyWeightValue, 0, 300);
            double bmi = calculateBMI(GlyWeightValue, GlyHeight);
            GlyBMI.Text = bmi.ToString();
            GlyWeight.SelectedIndex = calculateBMILevel(bmi);
        }

        /*Make Glycuresis male waistline disable */
        private void GlyFemale_CheckedChanged(object sender, EventArgs e)
        {
            //GlyMaleWaistline.Text = "";
            GlyMaleWaistline.SelectedIndex = -1;

            GlyMaleWaistline.Enabled = false;
            GlyFemaleWaistline.Enabled = true;
        }

        /*Make Glycuresis female waistline disable */
        private void GlyMale_CheckedChanged(object sender, EventArgs e)
        {
            //GlyFemaleWaistline.Text = "";
            GlyFemaleWaistline.SelectedIndex = -1;
            GlyFemaleWaistline.Enabled = false;
            GlyMaleWaistline.Enabled = true;
        }


        /****************************************************
         * Coronary Stroke
         ****************************************************/
        /*the sum score*/
        private int sumScore = 0;

        /*if miss the factor function plus 0*/
        const int CSMissingData = 0;

        /*Restrict the value of height*/
        private void CSHeight_Leave(object sender, EventArgs e)
        {
            numberRestrict(CSHeight, 0, 300);
            double bmi = calculateBMI(CSWeightValue, CSHeight);
            CSBMI.Text = bmi.ToString();
            CSWeight.SelectedIndex = calculateBMILevel(bmi);
        }

        /*Restrict the value of weight*/
        private void CSWeightValue_Leave(object sender, EventArgs e)
        {
            numberRestrict(CSWeightValue, 0, 300);
            double bmi = calculateBMI(CSWeightValue, CSHeight);
            CSBMI.Text = bmi.ToString();
            CSWeight.SelectedIndex = calculateBMILevel(bmi);
        }


        private void CSCalculate_Click(object sender, EventArgs e)
        {
            CSData csd = new CSData("\\Data\\CoronaryStrokeFactors.xls", "CoronaryStroke");

            sumScore = 0;

            int[] maleScore = csd.getMaleScore();
            int[] femaleScore = csd.getFemaleScore();

            /*the sum score array*/
            int[] maleSum = csd.getMaleSum();
            int[] femaleSum = csd.getFemaleSum();

            /*the mobidity of coronary stroke */
            string[] maleRisk = csd.getMaleRisk();
            string[] femaleRisk = csd.getFemaleRisk();

            /*age*/
            switch (CSAge.SelectedIndex)
            {
                case 0:
                    if (CSMale.Checked)
                    {
                        sumScore += maleScore[0];
                    }
                    else
                    {
                        sumScore += femaleScore[0];
                    }
                    break;
                case 1:
                    if (CSMale.Checked)
                    {
                        sumScore += maleScore[1];
                    }
                    else
                    {
                        sumScore += femaleScore[1];
                    }
                    break;
                case 2:
                    if (CSMale.Checked)
                    {
                        sumScore += maleScore[2];
                    }
                    else
                    {
                        sumScore += femaleScore[2];
                    }
                    break;
                case 3:
                    if (CSMale.Checked)
                    {
                        sumScore += maleScore[3];
                    }
                    else
                    {
                        sumScore += femaleScore[3];
                    }
                    break;

                case 4:
                    if (CSMale.Checked)
                    {
                        sumScore += maleScore[4];
                    }
                    else
                    {
                        sumScore += femaleScore[4];
                    }
                    break;
                default:
                    sumScore += CSMissingData;
                    break;
            }

            /*Systolic Pressure*/
            switch (CSSystolicPressure.SelectedIndex)
            {
                case 0:
                    if (CSMale.Checked)
                    {
                        sumScore += maleScore[5];
                    }
                    else
                    {
                        sumScore += femaleScore[5];
                    }
                    break;
                case 1:
                    if (CSMale.Checked)
                    {
                        sumScore += maleScore[6];
                    }
                    else
                    {
                        sumScore += femaleScore[6];
                    }
                    break;
                case 2:
                    if (CSMale.Checked)
                    {
                        sumScore += maleScore[7];
                    }
                    else
                    {
                        sumScore += femaleScore[7];
                    }
                    break;
                case 3:
                    if (CSMale.Checked)
                    {
                        sumScore += maleScore[8];
                    }
                    else
                    {
                        sumScore += femaleScore[8];
                    }
                    break;

                case 4:
                    if (CSMale.Checked)
                    {
                        sumScore += maleScore[9];
                    }
                    else
                    {
                        sumScore += femaleScore[9];
                    }
                    break;
                case 5:
                    if (CSMale.Checked)
                    {
                        sumScore += maleScore[10];
                    }
                    else
                    {
                        sumScore += femaleScore[10];
                    }
                    break;
                default:
                    sumScore += CSMissingData;
                    break;
            }

            /*Weight index*/
            switch (CSWeight.SelectedIndex)
            {
                case 0:
                    if (CSMale.Checked)
                    {
                        sumScore += maleScore[11];
                    }
                    else
                    {
                        sumScore += femaleScore[11];
                    }
                    break;
                case 1:
                    if (CSMale.Checked)
                    {
                        sumScore += maleScore[12];
                    }
                    else
                    {
                        sumScore += femaleScore[12];
                    }
                    break;
                case 2:
                    if (CSMale.Checked)
                    {
                        sumScore += maleScore[13];
                    }
                    else
                    {
                        sumScore += femaleScore[13];
                    }
                    break;
                default:
                    sumScore += CSMissingData;
                    break;
            }

            /*Cholesterol*/
            switch (CSCholesterol.SelectedIndex)
            {
                case 0:
                    if (CSMale.Checked)
                    {
                        sumScore += maleScore[14];
                    }
                    else
                    {
                        sumScore += femaleScore[14];
                    }
                    break;
                case 1:
                    if (CSMale.Checked)
                    {
                        sumScore += maleScore[15];
                    }
                    else
                    {
                        sumScore += femaleScore[15];
                    }
                    break;
                default:
                    sumScore += CSMissingData;
                    break;
            }

            /*Smoking*/
            if (CSSmoking.Checked)
            {
                if (CSMale.Checked)
                {
                    sumScore += maleScore[16];
                }
                else
                {
                    sumScore += femaleScore[16];
                }
            }

            /*Glycuresis*/
            if (CSGlycuresis.Checked)
            {
                if (CSMale.Checked)
                {
                    sumScore += maleScore[17];
                }
                else
                {
                    sumScore += femaleScore[17];
                }
            }

            CSSum.Text = sumScore.ToString();

            /*thr row of male and female sum*/
            int maleSumRow = maleSum.Count();
            int femaleSumRow = femaleSum.Count();

            int i = 0;

            /*judge the male risk*/
            if (CSMale.Checked)
            {
                if (sumScore <= maleSum[0])
                {
                    CSRisk.Text = maleRisk[0];
                }
                else if (sumScore >= maleSum[maleSumRow - 1])
                {
                    CSRisk.Text = maleRisk[maleSumRow - 1];
                }
                else
                {
                    for (i = 1; i < maleSumRow - 1; i++)
                    {
                        if (sumScore == maleSum[i])
                        {
                            CSRisk.Text = maleRisk[i];
                            break;
                        }
                    }
                }
            }

            /*judge female risk*/
            else
            {
                if (sumScore <= femaleSum[0])
                {
                    CSRisk.Text = femaleRisk[0];
                }
                else if (sumScore >= femaleSum[femaleSumRow - 1])
                {
                    CSRisk.Text = femaleRisk[femaleSumRow - 1];
                }
                else
                {
                    for (i = 1; i < femaleSumRow - 1; i++)
                    {
                        if (sumScore == femaleSum[i])
                        {
                            CSRisk.Text = femaleRisk[i];
                            break;
                        }
                    }
                }
            }
        }

        private void CSReset_Click(object sender, EventArgs e)
        {
            CSName.Text = "";

            CSMale.Checked = true;

            CSAge.Text = "";
            CSAge.SelectedIndex = -1;

            CSSystolicPressure.Text = "";
            CSSystolicPressure.SelectedIndex = -1;

            CSHeight.Text = "";
            CSWeightValue.Text = "";
            CSBMI.Text = "";

            CSWeight.Text = "";
            CSWeight.SelectedIndex = -1;


            CSCholesterol.Text = "";
            CSCholesterol.SelectedIndex = -1;

            CSSmoking.Checked = false;

            CSGlycuresis.Checked = false;

            CSSum.Text = "";

            CSRisk.Text = "";
        }

        private void CSMale_CheckedChanged(object sender, EventArgs e)
        {
            CSRate.Image = Image.FromFile(Application.StartupPath + "\\Images\\maleCSRate.png");
        }

        private void CSFemale_CheckedChanged(object sender, EventArgs e)
        {
            CSRate.Image = Image.FromFile(Application.StartupPath + "\\Images\\femaleCSRate.jpg");
        }

        
        /****************************************************
        * Framingham
        ****************************************************/
        /*the points of age in 10 age sections*/
        private int[] maleAge = { -9, -4, 0, 3, 6, 8, 10, 11, 12, 13 };
        private int[] femaleAge = { -7, -3, 0, 3, 6, 8, 10, 12, 14, 16 };

        /*the points of TC in 10 age sections
         row 0: TC 160 - 199
         * row 1: TC 200 - 239
         * row 2: TC 240 - 279
         * row 3: TC >= 280
         */
        private int[,] maleTC = new int[,]{{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                            { 4, 4, 3, 3, 2, 2, 1, 1, 0, 0 },
                                            { 7, 7, 5, 5, 3, 3, 1, 1, 0, 0 } ,
                                            { 9, 9, 6, 6, 4, 4, 2, 2, 1, 1 }, 
                                            { 11, 11, 8, 8, 5, 5, 3, 3, 1, 1 } };
        private int[,] femaleTC = new int[,] {{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                { 4, 4, 3, 3, 2, 2, 1, 1, 1, 1 },
                                                { 8, 8, 6, 6, 4, 4, 2, 2, 1, 1 }, 
                                                { 11, 11, 8, 8, 5, 5, 3, 3, 2, 2 }, 
                                                { 13, 13, 10, 10, 7, 7, 4, 4, 2, 2 } };

        /*the points of smoking in 10 age sections*/
        private int[] maleSmoking = { 8, 8, 5, 5, 3, 3, 1, 1, 1, 1 };
        private int[] femaleSmoking = { 9, 9, 7, 7, 4, 4, 2, 2, 1, 1 };

        /*the points of HDL*/
        private int[] maleHDL = { -1, 0, 1, 2 };
        private int[] femaleHDL = { -1, 0, 1, 2 };


        /*the points of Systolic Pressure
         row 0: 120 - 129
         * row 1: 130 - 139
         * row 2: 140 - 159
         * row 3: >= 160
         */
        private int[,] maleSP = new[,] { { 0, 0 }, { 0, 1 }, { 1, 2 }, { 1, 2 }, { 2, 3 } };
        private int[,] femaleSP = new[,] { { 0, 0 }, { 1, 3 }, { 2, 4 }, { 3, 5 }, { 4, 6 } };

        /*the sum of points*/
        private int fraSum = 0;

        private void addOneDimensionPoints(int[] maleArray, int[] femaleArray, int selectedIndex)
        {
            if (FraMale.Checked)
            {
                fraSum += maleArray[selectedIndex];
            }
            else
            {
                fraSum += femaleArray[selectedIndex];
            }
        }

        private void addManyDimensionsPoints(int[,] maleArray, int[,] femaleArray, int rowIndex, int columnIndex)
        {
            if (FraMale.Checked)
            {
                fraSum += maleArray[rowIndex, columnIndex];
            }
            else
            {
                fraSum += femaleArray[rowIndex, columnIndex];
            }
        }

        private void FraCalculate_Click(object sender, EventArgs e)
        {
            fraSum = 0;
            /*age */
            addOneDimensionPoints(maleAge, femaleAge, FraAge.SelectedIndex);

            /*TC*/
            addManyDimensionsPoints(maleTC, femaleTC, FraTC.SelectedIndex, FraAge.SelectedIndex);


            /*smoking*/
            if (FraSmoking.Checked)
            {
                addOneDimensionPoints(maleSmoking, femaleSmoking, FraAge.SelectedIndex);
            }

            /*HDL*/
            addOneDimensionPoints(maleHDL, femaleHDL, FraHDL.SelectedIndex);

            /*Systolic Pressure*/
            if (FraNotCure.Checked)
            {
                addManyDimensionsPoints(maleSP, femaleSP, FraSystolicPressure.SelectedIndex, 0);
            }
            else
            {
                addManyDimensionsPoints(maleSP, femaleSP, FraSystolicPressure.SelectedIndex, 1);
            }

            FraSum.Text = fraSum.ToString();

        }

        private void FraReset_Click(object sender, EventArgs e)
        {
            FraName.Text = "";

            FraAge.SelectedIndex = 0;

            FraTC.SelectedIndex = 0;

            FraHDL.SelectedIndex = 0;

            FraNoSmoking.Checked = true;

            FraSystolicPressure.SelectedIndex = 0;

            FraNotCure.Checked = true;

            FraSum.Text = "";
        }

    }
}
