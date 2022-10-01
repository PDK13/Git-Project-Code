using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Working on AI Machine Learning Primary
/// </summary>
public class Class_AIML
//AI Machine Learning Primary
{
    public Class_AIML()
    {

    }

    //---------------------------------------------------------------------------

    /// <summary>
    /// Number of Layer in Neural (Input - Hidden - Output) (*)
    /// </summary>
    private int m_LayerCount = 2;

    /// <summary>
    /// List of Neural in Layer (gồm Input - Hidden - Output) (*)
    /// </summary>
    private List<List<float>> li2_Activation;

    /// <summary>
    /// Number of Neural in Layer (gồm Input - Hidden - Output) (0)
    /// </summary>
    private List<int> lm_NeuralCount;

    /// <summary>
    /// List of Weight[L][L-1] Layer L (gồm Hidden - Output) (-1)
    /// </summary>
    private List<List<List<float>>> li3_Weight;

    /// <summary>
    /// List of Bias Layer (gồm Input - Hidden) (-1)
    /// </summary>
    private List<float> lm_Bias;

    /// <summary>
    /// List of Sum Layer (gồm Hidden - Output) (-1)
    /// </summary>
    private List<List<float>> li2_Sum;

    /// <summary>
    /// List of Error Layer (gồm Hidden - Output) (-1)
    /// </summary>
    private List<List<float>> li2_Error;

    /// <summary>
    /// List of Neural Output wanted
    /// </summary>
    private List<float> lm_Desired;

    ////Error before BackPropagation
    //private float m_ErrorTotal = 0;

    ////Count on run FeedForward và BackPropagation
    //private int m_LoopLearning = 0;

    /// <summary>
    /// Save Imformation
    /// </summary>
    private readonly List<float> lm_Comment_dou = new List<float>();
    /// <summary>
    /// Save Imformation
    /// </summary>
    private readonly List<string> lm_Comment_str = new List<string>();

    /// <summary>
    /// Reset Neural Network
    /// </summary>
    public void Set_Reset()
    {
        lm_NeuralCount = new List<int>();
        li2_Activation = new List<List<float>>();
        li3_Weight = new List<List<List<float>>>();
        lm_Bias = new List<float>();
        li2_Sum = new List<List<float>>();
        li2_Error = new List<List<float>>();
        lm_Desired = new List<float>();
    }

    //---------------------------------------------------------------------------- Set / Get

    /// <summary>
    /// Set Number of Layer
    /// </summary>
    /// <param name="LayerCount"></param>
    public void Set_Input_LayerCount(int LayerCount)
    {
        PlayerPrefs.SetInt("LC", (LayerCount < 0) ? 2 : LayerCount);
    }

    /// <summary>
    /// Get Number of Layer
    /// </summary>
    /// <returns></returns>
    public int GetInt_LayerCount()
    {
        return m_LayerCount;
    }

    /// <summary>
    /// Set new Number of Neural Layer
    /// </summary>
    /// <param name="Layer"></param>
    /// <param name="NeuralCount"></param>
    public void Set_Input_NeuralCount(int Layer, int NeuralCount)
    {
        if (Layer >= 0)
        {
            PlayerPrefs.SetInt("NC_" + Layer.ToString(), (NeuralCount > 0) ? NeuralCount : 0);
        }
    }

    /// <summary>
    /// Get Number of Neural of Layer
    /// </summary>
    /// <param name="Layer"></param>
    /// <returns></returns>
    public int GetInt_NeuralCount(int Layer)
    {
        return lm_NeuralCount[Layer];
    }

    /// <summary>
    /// Start create new Neural Network
    /// </summary>
    /// <param name="RandomNumber">If "True", Weight & Bias will gain random value</param>
    public void Set_NeuralNetworkCreate(bool RandomNumber)
    {
        //LayerCount
        m_LayerCount = PlayerPrefs.GetInt("LC");

        //NeuralCount
        lm_NeuralCount = new List<int>();
        for (int lay = 0; lay < m_LayerCount; lay++)
        {
            lm_NeuralCount.Add(PlayerPrefs.GetInt("NC_" + lay.ToString()));
        }

        //Activation
        li2_Activation = new List<List<float>>();
        for (int lay = 0; lay < m_LayerCount; lay++)
        {
            li2_Activation.Add(new List<float> { });
            for (int neu = 0; neu < lm_NeuralCount[lay]; neu++)
            {
                li2_Activation[lay].Add(0);
            }
        }

        //int m_o = 0;
        //Weight
        li3_Weight = new List<List<List<float>>>();
        for (int lay = 0; lay < m_LayerCount - 1; lay++)
        {
            li3_Weight.Add(new List<List<float>> { });
            for (int neuY = 0; neuY < lm_NeuralCount[lay + 1]; neuY++)
            {
                li3_Weight[lay].Add(new List<float> { });
                for (int neuX = 0; neuX < lm_NeuralCount[lay]; neuX++)
                {
                    //m_o++;
                    if (RandomNumber)
                    {
                        System.Random Rand = new System.Random();
                        float Value =
                            (float)(
                            (m_LayerCount * Rand.Next(1, 500) + lm_NeuralCount[lay] *
                            Rand.Next(500, 1000) + neuX * Rand.Next(100, 200) + neuY *
                            Rand.Next(200, 300) + lay * Rand.Next(300, 400)) * Rand.Next(1, 50) / 100000.0) / 100.0f;
                        li3_Weight[lay][neuY].Add(Value);
                    }
                    else
                    {
                        li3_Weight[lay][neuY].Add(0.0f);
                        //Debug.Log("Set_NeuralNetworkCreate: " lay + " " + neuY + " " + neuX + " : " + this.li3_Weight[lay][neuY][neuX]);
                    }

                }
            }
        }

        //Bias
        lm_Bias = new List<float>();
        for (int lay = 0; lay < m_LayerCount - 1; lay++)
        {
            if (RandomNumber)
            {
                System.Random Rand = new System.Random();
                float Value = 0;
                lm_Bias.Add(Value);
            }
            else
            {
                lm_Bias.Add(0.0f);
            }
        }

        //Sum
        //Debug.Log("Sum");
        li2_Sum = new List<List<float>>();
        for (int lay = 1; lay < m_LayerCount; lay++)
        {
            li2_Sum.Add(new List<float> { });
            for (int neu = 0; neu < lm_NeuralCount[lay]; neu++)
            {
                //Debug.Log(lay + " " + neu);
                li2_Sum[lay - 1].Add(0.0f);
            }
        }

        //Error
        li2_Error = new List<List<float>>();
        for (int lay = 0; lay < m_LayerCount - 1; lay++)
        {
            li2_Error.Add(new List<float> { });
            for (int neu = 0; neu < lm_NeuralCount[lay + 1]; neu++)
            {
                li2_Error[lay].Add(0.0f);
            }
        }

        //Desired
        lm_Desired = new List<float>();
        for (int neu = 0; neu < lm_NeuralCount[m_LayerCount - 1]; neu++)
        {
            lm_Desired.Add(0.0f);
        }
    }

    /// <summary>
    /// Set new Bias in Layer
    /// </summary>
    /// <param name="m_Layer"></param>
    /// <param name="m_Bias"></param>
    public void Set_Input_Bias(int m_Layer, float m_Bias)
    {
        if (m_Layer < m_LayerCount && m_Layer >= 0)
        {
            lm_Bias[m_Layer] = m_Bias;
        }
    }

    /// <summary>
    /// Set new Weight with X (L-1) << Y (L)
    /// </summary>
    /// <param name="m_Layer">L</param>
    /// <param name="m_NeuralY">Y (L)</param>
    /// <param name="m_NeuralX">X (L-1)</param>
    /// <param name="m_Weight"></param>
    public void Set_Input_Weight(int m_Layer, int m_NeuralY, int m_NeuralX, float m_Weight)
    {
        if (m_Layer < m_LayerCount - 1 && m_Layer >= 0)
        {
            li3_Weight[m_Layer][m_NeuralY][m_NeuralX] = m_Weight;
        }
    }

    /// <summary>
    /// Set new Input
    /// </summary>
    /// <param name="m_Neu_Input"></param>
    /// <param name="m_ValueInput"></param>
    public void Set_Input_LayerInput(int m_Neu_Input, float m_ValueInput)
    {
        if (m_Neu_Input >= 0 && m_Neu_Input < lm_NeuralCount[0])
        {
            li2_Activation[0][m_Neu_Input] = m_ValueInput;
        }
    }

    /// <summary>
    /// Set new Input
    /// </summary>
    /// <param name="l_ListInput"></param>
    public void Set_Input_LayerInput(List<float> l_ListInput)
    {
        if (l_ListInput == null)
        {
            return;
        }

        if (li2_Activation[0].Count == l_ListInput.Count)
        {
            //Gán List of nếu độ dài List of bằng nhau
            li2_Activation[0] = l_ListInput;
        }
        else
        {
            //Nếu độ dài không List of không bằng nhau >> Gán from đầu đến cuối from vị trí 0
            for (int i = 0; i < l_ListInput.Count; i++)
            {
                li2_Activation[0][i] = l_ListInput[i];
            }
        }
    }

    /// <summary>
    /// Set Input
    /// </summary>
    /// <param name="ListInput"></param>
    /// <param name="m_SetFrom"></param>
    public void Set_Input_LayerInput(List<float> ListInput, int m_SetFrom)
    {
        if (ListInput == null)
        {
            return;
        }

        for (int i = 0; i < ListInput.Count; i++)
        {
            //Gán List of bắt đầu from SetFrom
            li2_Activation[0][i + m_SetFrom] = ListInput[i];
        }
    }

    /// <summary>
    /// Get List of Neural of Input
    /// </summary>
    /// <returns></returns>
    public List<float> GetListFloat_LayerInput()
    {
        return li2_Activation[0];
    }

    /// <summary>
    /// Get Neural from Input
    /// </summary>
    /// <param name="m_Neural"></param>
    /// <returns></returns>
    public float GetFloat_LayerInput(int m_Neural)
    {
        return li2_Activation[0][m_Neural];
    }

    /// <summary>
    /// Set new Output Desired
    /// </summary>
    /// <param name="m_Neu_Desired"></param>
    /// <param name="m_ValueDesired"></param>
    public void Set_Input_Desired(int m_Neu_Desired, float m_ValueDesired)
    {
        if (m_Neu_Desired >= 0 && m_Neu_Desired < lm_NeuralCount[m_LayerCount - 1])
        {
            lm_Desired[m_Neu_Desired] = m_ValueDesired;
        }
    }

    /// <summary>
    /// Get Output Desired
    /// </summary>
    /// <returns></returns>
    public List<float> GetListFloat_Desired()
    {
        return lm_Desired;
    }

    /// <summary>
    /// Get List of Neural of Output
    /// </summary>
    /// <returns></returns>
    public List<float> GetListFloat_LayerOutput()
    {
        return li2_Activation[m_LayerCount - 1];
    }

    /// <summary>
    /// Get Neural of Output
    /// </summary>
    /// <param name="m_Neural"></param>
    /// <returns></returns>
    public float GetFloat_LayerOutput(int m_Neural)
    {
        return li2_Activation[m_LayerCount - 1][m_Neural];
    }

    //Get Error Total sau khi chạy FeedForward và trước khi chạy BackPropagation
    //public float BrainGetErrorTotal()
    //{
    //	return m_ErrorTotal;
    //}

    //Get số lần đã chạy BackPropagation
    //public int BrainGetLoopLearned()
    //{
    //	return m_LoopLearning;
    //}

    //---------------------------------------------------------------------------- File

    /// <summary>
    /// Check AIML File Exist
    /// </summary>
    /// <param name="s_Link"></param>
    /// <returns></returns>
    public bool GetBool_FileCheck(string s_Link)
    {
        return Class_FileIO.GetFileIsExist(s_Link);
    }

    /// <summary>
    /// Save Current AIML Data to File work on this Script
    /// </summary>
    /// <param name="s_Link"></param>
    public void Set_FileSave(string s_Link)
    {
        Class_FileIO myFile = new Class_FileIO();

        myFile.Set_Data_Write_Add("LayerCount:");
        myFile.Set_Data_Write_Add(m_LayerCount);

        myFile.Set_Data_Write_Add("NeuralCount:");
        for (int lay = 0; lay < m_LayerCount; lay++)
        {
            myFile.Set_Data_Write_Add(lm_NeuralCount[lay]);
            //Lưu Number of Neural of  Layer
        }

        myFile.Set_Data_Write_Add("Bias:");
        for (int lay = 0; lay < m_LayerCount - 1; lay++)
        {
            myFile.Set_Data_Write_Add(lm_Bias[lay]);
            //Lưu Bias of  Layer
        }

        myFile.Set_Data_Write_Add("Weight:");
        for (int lay = 0; lay < m_LayerCount - 1; lay++)
        {
            //Xét fromng Layer (L-1)
            for (int neuY = 0; neuY < lm_NeuralCount[lay + 1]; neuY++)
            {
                //Xét fromng Neural Y of Layer (L)
                for (int neuX = 0; neuX < lm_NeuralCount[lay]; neuX++)
                {
                    //Xét fromng Neural X of Layer (L-1)
                    myFile.Set_Data_Write_Add(li3_Weight[lay][neuY][neuX]);
                    //Lưu Weight of  Layer
                }
            }
        }

        //for (int neu = 0; neu < lm_NeuralCount[0]; neu++)
        //{
        //	myFile.Set_Write_Add(BrainGetInput()[neu]);
        //	//Lưu Input
        //}

        //for (int neu = 0; neu < lm_NeuralCount[m_LayerCount - 1]; neu++)
        //{
        //	myFile.Set_Write_Add(BrainGetDesired()[neu]);
        //	//Lưu Desired
        //}

        //myFile.Set_Write_Add(BrainGetErrorTotal());
        ////Lưu Error Total

        //myFile.Set_Write_Add(BrainGetLoopLearned());
        ////Lưu Loop Learning

        myFile.Set_Data_Write_Add("Comment:");
        myFile.Set_Data_Write_Add(lm_Comment_dou.Count);
        //myFile.FileWrite(lm_Comment_str.Count);
        //two List of này có cùng Number of phần tử
        for (int i = 0; i < lm_Comment_dou.Count; i++)
        {
            myFile.Set_Data_Write_Add(lm_Comment_str[i]);
            myFile.Set_Data_Write_Add(lm_Comment_dou[i]);
            //Lưu Imformation
        }

        //Kích hoạt lưu File
        myFile.Set_Data_Write_Start(s_Link);
    }

    /// <summary>
    /// Read AIML Data from File work on this Script
    /// </summary>
    /// <param name="s_Link"></param>
    public void Set_FileOpen(string s_Link)
    {
        Class_FileIO myFile = new Class_FileIO();

        //Kích hoạt đọc File
        myFile.Set_Data_Read_Start(s_Link);

        string t;

        t = myFile.GetData_Read_Auto_String();
        int LayerCount = myFile.GetData_Read_Auto_Int();
        Set_Input_LayerCount(LayerCount);

        t = myFile.GetData_Read_Auto_String();
        for (int lay = 0; lay < LayerCount; lay++)
        {
            Set_Input_NeuralCount(lay, myFile.GetData_Read_Auto_Int());
            //Ghi Number of Neural of  Layer
        }

        //Debug.Log("Create");
        Set_NeuralNetworkCreate(false);
        //Debug.Log("Weight");

        t = myFile.GetData_Read_Auto_String();
        for (int lay = 0; lay < m_LayerCount - 1; lay++)
        {
            Set_Input_Bias(lay, myFile.GetData_Read_Auto_Float());
            //Ghi Bias of  Layer
        }

        t = myFile.GetData_Read_Auto_String();
        for (int lay = 0; lay < m_LayerCount - 1; lay++)
        {
            //Xét fromng Layer (L-1)
            for (int neuY = 0; neuY < lm_NeuralCount[lay + 1]; neuY++)
            {
                //Xét fromng Neural Y of Layer (L)
                for (int neuX = 0; neuX < lm_NeuralCount[lay]; neuX++)
                {
                    //Xét fromng Neural X of Layer (L-1)
                    //Debug.Log(lay + " " + neuY + " " + neuX + " : " + this.li3_Weight[lay][neuY][neuX]);
                    //this.li3_Weight[lay][neuY][neuX] = myFile.GetRead_Auto_Float();
                    Set_Input_Weight(lay, neuY, neuX, myFile.GetData_Read_Auto_Float());
                    //Ghi Weight of  Layer
                }
            }
        }

        //for (int neu = 0; neu < lm_NeuralCount[0]; neu++)
        //{
        //	BrainSet_Input(neu, myFile.GetRead_Auto_Float());
        //	//Ghi Input
        //}

        //for (int neu = 0; neu < lm_NeuralCount[m_LayerCount - 1]; neu++)
        //{
        //	BrainSet_Desired(neu, myFile.GetRead_Auto_Float());
        //	//Ghi Desired
        //}

        //this.m_ErrorTotal = myFile.GetRead_Auto_Float();
        ////Ghi Error Total

        //this.m_LoopLearning = myFile.GetRead_Auto_Int();
        ////Ghi Loop Learning

        t = myFile.GetData_Read_Auto_String();
        int CommentCount = myFile.GetData_Read_Auto_Int();
        //myFile.FileReader(lm_Comment_str.Count);
        //two List of này có cùng Number of phần tử
        for (int i = 0; i < CommentCount; i++)
        {
            lm_Comment_str.Add(myFile.GetData_Read_Auto_String());
            lm_Comment_dou.Add(myFile.GetData_Read_Auto_Float());
            //Ghi Imformation
        }
    }

    //---------------------------------------------------------------------------- FeedForward

    /// <summary>
    /// Caculate Sum of between two Layer X (L-1) >> Y (L)
    /// </summary>
    /// <param name="m_Layer"></param>
    private void Set_FeedForward_Sum(int m_Layer)
    {
        //Debug.Log("After");
        for (int neuY = 0; neuY < lm_NeuralCount[m_Layer]; neuY++)
        {
            //Xét Layer Y (L)

            //Sum = Weight * lm_Activation(L-1) + Bias
            //Debug.Log(Layer + " " + neuY + " ");
            li2_Sum[m_Layer - 1][neuY] = lm_Bias[m_Layer - 1];

            for (int neuX = 0; neuX < lm_NeuralCount[m_Layer - 1]; neuX++)
            {
                //Xét Layer X (L-1)
                li2_Sum[m_Layer - 1][neuY] +=
                    li3_Weight[m_Layer - 1][neuY][neuX] * li2_Activation[m_Layer - 1][neuX];
            }
        }
    }

    /// <summary>
    /// Caculate Sigmoid
    /// </summary>
    /// <param name="Value"></param>
    /// <returns></returns>
    public float GetFloat_FeedForward_Sigmoid_Single(float Value)
    {
        return (float)1.0 / ((float)1.0 + (float)Math.Exp(-Value));
    }

    /// <summary>
    /// Caculate Sigmoid from Sum of Layer Y (L)
    /// </summary>
    /// <param name="Layer"></param>
    private void Set_FeedForward_Sigmoid(int Layer)
    {
        for (int neuY = 0; neuY < lm_NeuralCount[Layer]; neuY++)
        {
            //Xét Layer Y (L)

            //lm_Activation(L) = Sigmoid(Sum)
            li2_Activation[Layer][neuY] =
                GetFloat_FeedForward_Sigmoid_Single(li2_Sum[Layer - 1][neuY]);
        }
    }

    //Caculate Error after FeedForward và before BackPropagation
    //private void Set_Error()
    //{
    //	m_ErrorTotal = 0;
    //	for (int neuY = 0; neuY < lm_NeuralCount[m_LayerCount - 1]; neuY++)
    //	{
    //		float Delta = lm_Desired[neuY] - li2_Activation[m_LayerCount - 1][neuY];
    //		m_ErrorTotal += (float)0.5 * Delta * Delta;
    //	}
    //}

    /// <summary>
    /// Active FeedForward
    /// </summary>
    public void Set_FeedForward()
    {
        for (int lay = 1; lay < m_LayerCount; lay++)
        {
            Set_FeedForward_Sum(lay);
            Set_FeedForward_Sigmoid(lay);
        }
        //Set_Error();
    }

    //---------------------------------------------------------------------------- BackPropagation

    /// <summary>
    /// Caculate Error between Layer Output >> Desired
    /// </summary>
    private void Set_BackPropagation_ErrorOuput()
    {
        int layerY = m_LayerCount - 1;
        for (int neuY = 0; neuY < lm_NeuralCount[layerY]; neuY++)
        {
            //Xét Layer Y (L) với Desired
            li2_Error[layerY - 1][neuY] =
                -(lm_Desired[neuY] - li2_Activation[layerY][neuY]);
        }
    }

    /// <summary>
    /// Caculate Sigmoid
    /// </summary>
    /// <param name="m_Value"></param>
    /// <returns></returns>
    public float GetFloat_BackPropagation_Sigmoid_Single(float m_Value)
    {
        float Sigmoid = GetFloat_FeedForward_Sigmoid_Single(m_Value);
        return Sigmoid * ((float)1.0 - Sigmoid);
    }

    /// <summary>
    /// Caculate Error between Layer Y (L) >> Layer Z (L+1)
    /// </summary>
    /// <param name="m_Layer"></param>
    private void Set_BackPropagation_ErrorHidden(int m_Layer)
    {
        int layerZ = m_Layer + 1;
        int layerY = m_Layer;
        for (int neuY = 0; neuY < lm_NeuralCount[layerY]; neuY++)
        {
            //Xét Layer Y (L) với Layer Z (L+1)
            //Debug.Log(Layer + " " + neuY);
            li2_Error[layerY - 1][neuY] = 0;

            for (int neu_Z = 0; neu_Z < lm_NeuralCount[layerZ]; neu_Z++)
            {
                //Xét Layer Y (L) với Layer Z (L+1)
                li2_Error[layerY - 1][neuY] +=
                    li2_Error[layerZ - 1][neu_Z] *
                    GetFloat_BackPropagation_Sigmoid_Single(li2_Sum[layerZ - 1][neu_Z]) *
                    li3_Weight[layerZ - 1][neu_Z][neuY];
            }
        }
    }

    /// <summary>
    /// Set Weight Layer
    /// </summary>
    /// <param name="Layer"></param>
    private void Set_BackPropagation_Update(int Layer)
    {
        //Layer Output
        int layerX = Layer - 1;
        int layerY = Layer;
        for (int neuX = 0; neuX < lm_NeuralCount[layerX]; neuX++)
        {
            //Xét Layer X (L-1) >> Layer Y (L)
            for (int neuY = 0; neuY < lm_NeuralCount[layerY]; neuY++)
            {
                //Xét Layer X (L-1) >> Layer Y (L)
                li3_Weight[layerY - 1][neuY][neuX] -=
                    (float)0.5 * (
                        li2_Error[layerY - 1][neuY] *
                        GetFloat_BackPropagation_Sigmoid_Single(li2_Sum[layerY - 1][neuY]) *
                        li2_Activation[layerX][neuX]);
            }
        }
    }

    /// <summary>
    /// Active BackPropagation
    /// </summary>
    public void Set_BackPropagation()
    {
        for (int lay = m_LayerCount - 1; lay > 0; lay--)
        {
            //Xét Layer X (L-1) >> Layer Y (L) >> Layer Z (L+1)

            //Caculate Error
            if (lay == m_LayerCount - 1)
            {
                Set_BackPropagation_ErrorOuput();
            }
            else
            {
                Set_BackPropagation_ErrorHidden(lay);
            }
        }

        for (int lay = m_LayerCount - 1; lay > 0; lay--)
        {
            //Xét Layer X (L-1) >> Layer Y (L) >> Layer Z (L+1)

            //Set
            Set_BackPropagation_Update(lay);
        }

        //m_LoopLearning++;
    }

    //---------------------------------------------------------------------------- Ghi chú

    /// <summary>
    /// Get Note Index if Exist
    /// </summary>
    /// <param name="CommentString"></param>
    /// <returns></returns>
    public int GetIndex_Comment(string CommentString)
    {
        for (int i = 0; i < lm_Comment_str.Count; i++)
        {
            if (lm_Comment_str[i] == CommentString)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// Set Note
    /// </summary>
    /// <param name="CommentString"></param>
    /// <param name="CommentNumber"></param>
    public void Set_Input_Comment(string CommentString, float CommentNumber)
    {
        int Index = GetIndex_Comment(CommentString);
        if (Index != -1)
        {
            lm_Comment_dou[Index] = CommentNumber;
        }
        else
        {
            lm_Comment_str.Add(CommentString);
            lm_Comment_dou.Add(CommentNumber);
        }
    }

    /// <summary>
    /// Get Note
    /// </summary>
    /// <param name="CommentString"></param>
    /// <returns>If not found, it will return "int.MaxValue"</returns>
    public float GetFloat_Comment(string CommentString)
    {
        int Index = GetIndex_Comment(CommentString);
        if (Index != -1)
        {
            return lm_Comment_dou[Index];
        }
        return int.MaxValue;
    }

    //---------------------------------------------------------------------------- Delay

    /// <summary>
    /// Delay Time
    /// </summary>
    private int m_BrainDelayTime = 3;
    private int m_BrainDelayTime_Cur = 0;

    /// <summary>
    /// Set Delay Time
    /// </summary>
    /// <param name="Value"></param>
    public void Set_Input_myBrainDelayTime(int Value)
    {
        m_BrainDelayTime = Value;
    }

    /// <summary>
    /// Check Delay Time
    /// </summary>
    /// <returns>Will return "True" if Delay Time = 0</returns>
    public bool GetBool_myBrainDelayTime_Value()
    {
        if (m_BrainDelayTime_Cur > 0)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Check Delay Time & Continue work on Delay Time
    /// </summary>
    /// <returns>Will return "True" if Delay Time = 0</returns>
    public bool GetBool_myBrainDelayTime_Over()
    {
        if (m_BrainDelayTime_Cur > 0)
        {
            m_BrainDelayTime_Cur -= 1;
            return false;
        }
        m_BrainDelayTime_Cur = m_BrainDelayTime;
        return true;
    }

    //---------------------------------------------------------------------------- Debug
}