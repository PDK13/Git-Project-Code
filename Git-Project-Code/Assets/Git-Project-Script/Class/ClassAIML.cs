using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Working on AI m_achine Learning Primary
/// </summary>
public class ClassAIML
//AI m_achine Learning Primary
{
    public ClassAIML()
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
    private List<List<float>> m_Activation;

    /// <summary>
    /// Number of Neural in Layer (gồm Input - Hidden - Output) (0)
    /// </summary>
    private List<int> m_NeuralCount;

    /// <summary>
    /// List of Weight[L][L-1] Layer L (gồm Hidden - Output) (-1)
    /// </summary>
    private List<List<List<float>>> m_Weight;

    /// <summary>
    /// List of Bias Layer (gồm Input - Hidden) (-1)
    /// </summary>
    private List<float> m_Bias;

    /// <summary>
    /// List of Sum Layer (gồm Hidden - Output) (-1)
    /// </summary>
    private List<List<float>> m_Sum;

    /// <summary>
    /// List of Error Layer (gồm Hidden - Output) (-1)
    /// </summary>
    private List<List<float>> m_Error;

    /// <summary>
    /// List of Neural Output wanted
    /// </summary>
    private List<float> m_Desired;

    ////Error before BackPropagation
    //private float m_ErrorTotal = 0;

    ////Count on run FeedForward và BackPropagation
    //private int m_LoopLearning = 0;

    /// <summary>
    /// Save Imformation
    /// </summary>
    private readonly List<float> m_Comment_dou = new List<float>();

    /// <summary>
    /// Save Imformation
    /// </summary>
    private readonly List<string> m_Comment_str = new List<string>();

    /// <summary>
    /// Reset Neural Network
    /// </summary>
    public void SetReset()
    {
        m_NeuralCount = new List<int>();
        m_Activation = new List<List<float>>();
        m_Weight = new List<List<List<float>>>();
        m_Bias = new List<float>();
        m_Sum = new List<List<float>>();
        m_Error = new List<List<float>>();
        m_Desired = new List<float>();
    }

    //---------------------------------------------------------------------------- Set / Get

    /// <summary>
    /// Set Number of Layer
    /// </summary>
    /// <param name="LayerCount"></param>
    public void SetInputLayerCount(int LayerCount)
    {
        ClassScene.SetPlayerPrefs("LC", (LayerCount < 0) ? 2 : LayerCount);
    }

    /// <summary>
    /// Get Number of Layer
    /// </summary>
    /// <returns></returns>
    public int GetIntLayerCount()
    {
        return m_LayerCount;
    }

    /// <summary>
    /// Set new Number of Neural Layer
    /// </summary>
    /// <param name="Layer"></param>
    /// <param name="NeuralCount"></param>
    public void SetInputNeuralCount(int Layer, int NeuralCount)
    {
        if (Layer >= 0)
        {
            ClassScene.SetPlayerPrefs("NC_" + Layer.ToString(), (NeuralCount > 0) ? NeuralCount : 0);
        }
    }

    /// <summary>
    /// Get Number of Neural of Layer
    /// </summary>
    /// <param name="Layer"></param>
    /// <returns></returns>
    public int GetNeuralCount(int Layer)
    {
        return m_NeuralCount[Layer];
    }

    /// <summary>
    /// Start create new Neural Network
    /// </summary>
    /// <param name="RandomNumber">If "True", Weight & Bias will gain random value</param>
    public void SetNeuralNetworkCreate(bool RandomNumber)
    {
        //LayerCount
        m_LayerCount = ClassScene.GetPlayerPrefsInt("LC");

        //NeuralCount
        m_NeuralCount = new List<int>();
        for (int lay = 0; lay < m_LayerCount; lay++)
        {
            m_NeuralCount.Add(ClassScene.GetPlayerPrefsInt("NC_" + lay.ToString()));
        }

        //Activation
        m_Activation = new List<List<float>>();
        for (int lay = 0; lay < m_LayerCount; lay++)
        {
            m_Activation.Add(new List<float> { });
            for (int neu = 0; neu < m_NeuralCount[lay]; neu++)
            {
                m_Activation[lay].Add(0);
            }
        }

        //int m_o = 0;
        //Weight
        m_Weight = new List<List<List<float>>>();
        for (int lay = 0; lay < m_LayerCount - 1; lay++)
        {
            m_Weight.Add(new List<List<float>> { });
            for (int neuY = 0; neuY < m_NeuralCount[lay + 1]; neuY++)
            {
                m_Weight[lay].Add(new List<float> { });
                for (int neuX = 0; neuX < m_NeuralCount[lay]; neuX++)
                {
                    //m_o++;
                    if (RandomNumber)
                    {
                        System.Random Rand = new System.Random();
                        float Value =
                            (float)(
                            (m_LayerCount * Rand.Next(1, 500) + m_NeuralCount[lay] *
                            Rand.Next(500, 1000) + neuX * Rand.Next(100, 200) + neuY *
                            Rand.Next(200, 300) + lay * Rand.Next(300, 400)) * Rand.Next(1, 50) / 100000.0) / 100.0f;
                        m_Weight[lay][neuY].Add(Value);
                    }
                    else
                    {
                        m_Weight[lay][neuY].Add(0.0f);
                        //Debug.Log("SetNeuralNetworkCreate: " lay + " " + neuY + " " + neuX + " : " + m_Weight[lay][neuY][neuX]);
                    }

                }
            }
        }

        //Bias
        m_Bias = new List<float>();
        for (int lay = 0; lay < m_LayerCount - 1; lay++)
        {
            if (RandomNumber)
            {
                System.Random Rand = new System.Random();
                float Value = 0;
                m_Bias.Add(Value);
            }
            else
            {
                m_Bias.Add(0.0f);
            }
        }

        //Sum
        //Debug.Log("Sum");
        m_Sum = new List<List<float>>();
        for (int lay = 1; lay < m_LayerCount; lay++)
        {
            m_Sum.Add(new List<float> { });
            for (int neu = 0; neu < m_NeuralCount[lay]; neu++)
            {
                //Debug.Log(lay + " " + neu);
                m_Sum[lay - 1].Add(0.0f);
            }
        }

        //Error
        m_Error = new List<List<float>>();
        for (int lay = 0; lay < m_LayerCount - 1; lay++)
        {
            m_Error.Add(new List<float> { });
            for (int neu = 0; neu < m_NeuralCount[lay + 1]; neu++)
            {
                m_Error[lay].Add(0.0f);
            }
        }

        //Desired
        m_Desired = new List<float>();
        for (int neu = 0; neu < m_NeuralCount[m_LayerCount - 1]; neu++)
        {
            m_Desired.Add(0.0f);
        }
    }

    /// <summary>
    /// Set new Bias in Layer
    /// </summary>
    /// <param name="m_Layer"></param>
    /// <param name="m_Bias"></param>
    public void SetInputBias(int m_Layer, float m_Bias)
    {
        if (m_Layer < m_LayerCount && m_Layer >= 0)
        {
            this.m_Bias[m_Layer] = m_Bias;
        }
    }

    /// <summary>
    /// Set new Weight with X (L-1) << Y (L)
    /// </summary>
    /// <param name="m_Layer">L</param>
    /// <param name="m_NeuralY">Y (L)</param>
    /// <param name="m_NeuralX">X (L-1)</param>
    /// <param name="m_Weight"></param>
    public void SetInputWeight(int m_Layer, int m_NeuralY, int m_NeuralX, float m_Weight)
    {
        if (m_Layer < m_LayerCount - 1 && m_Layer >= 0)
        {
            this.m_Weight[m_Layer][m_NeuralY][m_NeuralX] = m_Weight;
        }
    }

    /// <summary>
    /// Set new Input
    /// </summary>
    /// <param name="m_Neu_Input"></param>
    /// <param name="m_ValueInput"></param>
    public void SetInputLayerInput(int m_Neu_Input, float m_ValueInput)
    {
        if (m_Neu_Input >= 0 && m_Neu_Input < m_NeuralCount[0])
        {
            m_Activation[0][m_Neu_Input] = m_ValueInput;
        }
    }

    /// <summary>
    /// Set new Input
    /// </summary>
    /// <param name="mListInput"></param>
    public void SetInputLayerInput(List<float> m_ListInput)
    {
        if (m_ListInput == null)
        {
            return;
        }

        if (m_Activation[0].Count == m_ListInput.Count)
        {
            //Gán List of nếu độ dài List of bằng nhau
            m_Activation[0] = m_ListInput;
        }
        else
        {
            //Nếu độ dài không List of không bằng nhau >> Gán from đầu đến cuối from vị trí 0
            for (int i = 0; i < m_ListInput.Count; i++)
            {
                m_Activation[0][i] = m_ListInput[i];
            }
        }
    }

    /// <summary>
    /// Set Input
    /// </summary>
    /// <param name="ListInput"></param>
    /// <param name="m_SetFrom"></param>
    public void SetInputLayerInput(List<float> ListInput, int m_SetFrom)
    {
        if (ListInput == null)
        {
            return;
        }

        for (int i = 0; i < ListInput.Count; i++)
        {
            //Gán List of bắt đầu from SetFrom
            m_Activation[0][i + m_SetFrom] = ListInput[i];
        }
    }

    /// <summary>
    /// Get List of Neural of Input
    /// </summary>
    /// <returns></returns>
    public List<float> GetListLayerInput()
    {
        return m_Activation[0];
    }

    /// <summary>
    /// Get Neural from Input
    /// </summary>
    /// <param name="m_Neural"></param>
    /// <returns></returns>
    public float GetLayerInput(int m_Neural)
    {
        return m_Activation[0][m_Neural];
    }

    /// <summary>
    /// Set new Output Desired
    /// </summary>
    /// <param name="m_NeuDesired"></param>
    /// <param name="m_ValueDesired"></param>
    public void SetInputDesired(int m_NeuDesired, float m_ValueDesired)
    {
        if (m_NeuDesired >= 0 && m_NeuDesired < m_NeuralCount[m_LayerCount - 1])
        {
            m_Desired[m_NeuDesired] = m_ValueDesired;
        }
    }

    /// <summary>
    /// Get Output Desired
    /// </summary>
    /// <returns></returns>
    public List<float> GetListDesired()
    {
        return m_Desired;
    }

    /// <summary>
    /// Get List of Neural of Output
    /// </summary>
    /// <returns></returns>
    public List<float> GetListLayerOutput()
    {
        return m_Activation[m_LayerCount - 1];
    }

    /// <summary>
    /// Get Neural of Output
    /// </summary>
    /// <param name="m_Neural"></param>
    /// <returns></returns>
    public float GetLayerOutput(int m_Neural)
    {
        return m_Activation[m_LayerCount - 1][m_Neural];
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
    /// <param name="m_Path"></param>
    /// <returns></returns>
    public bool GetCheckFileExist(string m_Path)
    {
        return ClassFileIO.GetCheckFileExist(m_Path);
    }

    /// <summary>
    /// Save Current AIML Data to File work on this Script
    /// </summary>
    /// <param name="m_Path"></param>
    public void SetFileSave(string m_Path)
    {
        ClassFileIO m_yFile = new ClassFileIO();

        m_yFile.SetWriteDataAdd("LayerCount:");
        m_yFile.SetWriteDataAdd(m_LayerCount);

        m_yFile.SetWriteDataAdd("NeuralCount:");
        for (int lay = 0; lay < m_LayerCount; lay++)
        {
            m_yFile.SetWriteDataAdd(m_NeuralCount[lay]);
            //Lưu Number of Neural of  Layer
        }

        m_yFile.SetWriteDataAdd("Bias:");
        for (int lay = 0; lay < m_LayerCount - 1; lay++)
        {
            m_yFile.SetWriteDataAdd(m_Bias[lay]);
            //Lưu Bias of  Layer
        }

        m_yFile.SetWriteDataAdd("Weight:");
        for (int lay = 0; lay < m_LayerCount - 1; lay++)
        {
            //Xét fromng Layer (L-1)
            for (int neuY = 0; neuY < m_NeuralCount[lay + 1]; neuY++)
            {
                //Xét fromng Neural Y of Layer (L)
                for (int neuX = 0; neuX < m_NeuralCount[lay]; neuX++)
                {
                    //Xét fromng Neural X of Layer (L-1)
                    m_yFile.SetWriteDataAdd(m_Weight[lay][neuY][neuX]);
                    //Lưu Weight of  Layer
                }
            }
        }

        //for (int neu = 0; neu < m_NeuralCount[0]; neu++)
        //{
        //	m_yFile.SetWrite_Add(BrainGetInput()[neu]);
        //	//Lưu Input
        //}

        //for (int neu = 0; neu < m_NeuralCount[m_LayerCount - 1]; neu++)
        //{
        //	m_yFile.SetWrite_Add(BrainGetDesired()[neu]);
        //	//Lưu Desired
        //}

        //m_yFile.SetWrite_Add(BrainGetErrorTotal());
        ////Lưu Error Total

        //m_yFile.SetWrite_Add(BrainGetLoopLearned());
        ////Lưu Loop Learning

        m_yFile.SetWriteDataAdd("Comment:");
        m_yFile.SetWriteDataAdd(m_Comment_dou.Count);
        //m_yFile.FileWrite(m_Comment_str.Count);
        //two List of này có cùng Number of phần tử
        for (int i = 0; i < m_Comment_dou.Count; i++)
        {
            m_yFile.SetWriteDataAdd(m_Comment_str[i]);
            m_yFile.SetWriteDataAdd(m_Comment_dou[i]);
            //Lưu Imformation
        }

        //Kích hoạt lưu File
        m_yFile.SetWriteDataStart(m_Path);
    }

    /// <summary>
    /// Read AIML Data from File work on this Script
    /// </summary>
    /// <param name="m_Path"></param>
    public void SetFileOpen(string m_Path)
    {
        ClassFileIO m_FileIO = new ClassFileIO();

        //Kích hoạt đọc File
        m_FileIO.SetReadDataStart(m_Path);

        string t;

        t = m_FileIO.GetReadDataAutoString();
        int LayerCount = m_FileIO.GetReadDataAutoInt();
        SetInputLayerCount(LayerCount);

        t = m_FileIO.GetReadDataAutoString();
        for (int lay = 0; lay < LayerCount; lay++)
        {
            SetInputNeuralCount(lay, m_FileIO.GetReadDataAutoInt());
            //Ghi Number of Neural of  Layer
        }

        //Debug.Log("Create");
        SetNeuralNetworkCreate(false);
        //Debug.Log("Weight");

        t = m_FileIO.GetReadDataAutoString();
        for (int lay = 0; lay < m_LayerCount - 1; lay++)
        {
            SetInputBias(lay, m_FileIO.GetReadDataAutoFloat());
            //Ghi Bias of  Layer
        }

        t = m_FileIO.GetReadDataAutoString();
        for (int lay = 0; lay < m_LayerCount - 1; lay++)
        {
            //Xét fromng Layer (L-1)
            for (int neuY = 0; neuY < m_NeuralCount[lay + 1]; neuY++)
            {
                //Xét fromng Neural Y of Layer (L)
                for (int neuX = 0; neuX < m_NeuralCount[lay]; neuX++)
                {
                    //Xét fromng Neural X of Layer (L-1)
                    //Debug.Log(lay + " " + neuY + " " + neuX + " : " + m_Weight[lay][neuY][neuX]);
                    //m_Weight[lay][neuY][neuX] = m_yFile.GetRead_AutoFloat();
                    SetInputWeight(lay, neuY, neuX, m_FileIO.GetReadDataAutoFloat());
                    //Ghi Weight of  Layer
                }
            }
        }

        //for (int neu = 0; neu < m_NeuralCount[0]; neu++)
        //{
        //	BrainSetInput(neu, m_yFile.GetRead_AutoFloat());
        //	//Ghi Input
        //}

        //for (int neu = 0; neu < m_NeuralCount[m_LayerCount - 1]; neu++)
        //{
        //	BrainSetDesired(neu, m_yFile.GetRead_AutoFloat());
        //	//Ghi Desired
        //}

        //m_ErrorTotal = m_yFile.GetRead_AutoFloat();
        ////Ghi Error Total

        //mLoopLearning = m_yFile.GetRead_AutoInt();
        ////Ghi Loop Learning

        t = m_FileIO.GetReadDataAutoString();
        int CommentCount = m_FileIO.GetReadDataAutoInt();
        //m_yFile.FileReader(m_Comment_str.Count);
        //two List of này có cùng Number of phần tử
        for (int i = 0; i < CommentCount; i++)
        {
            m_Comment_str.Add(m_FileIO.GetReadDataAutoString());
            m_Comment_dou.Add(m_FileIO.GetReadDataAutoFloat());
            //Ghi Imformation
        }
    }

    //---------------------------------------------------------------------------- FeedForward

    /// <summary>
    /// Caculate Sum of between two Layer X (L-1) >> Y (L)
    /// </summary>
    /// <param name="m_Layer"></param>
    private void SetFeedForward_Sum(int m_Layer)
    {
        //Debug.Log("After");
        for (int neuY = 0; neuY < m_NeuralCount[m_Layer]; neuY++)
        {
            //Xét Layer Y (L)

            //Sum = Weight * m_Activation(L-1) + Bias
            //Debug.Log(Layer + " " + neuY + " ");
            m_Sum[m_Layer - 1][neuY] = m_Bias[m_Layer - 1];

            for (int neuX = 0; neuX < m_NeuralCount[m_Layer - 1]; neuX++)
            {
                //Xét Layer X (L-1)
                m_Sum[m_Layer - 1][neuY] +=
                    m_Weight[m_Layer - 1][neuY][neuX] * m_Activation[m_Layer - 1][neuX];
            }
        }
    }

    /// <summary>
    /// Caculate Sigmoid
    /// </summary>
    /// <param name="Value"></param>
    /// <returns></returns>
    public float GetFeedForward_Sigmoid_Single(float Value)
    {
        return (float)1.0 / ((float)1.0 + (float)Math.Exp(-Value));
    }

    /// <summary>
    /// Caculate Sigmoid from Sum of Layer Y (L)
    /// </summary>
    /// <param name="Layer"></param>
    private void SetFeedForward_Sigmoid(int Layer)
    {
        for (int neuY = 0; neuY < m_NeuralCount[Layer]; neuY++)
        {
            //Xét Layer Y (L)

            //m_Activation(L) = Sigmoid(Sum)
            m_Activation[Layer][neuY] =
                GetFeedForward_Sigmoid_Single(m_Sum[Layer - 1][neuY]);
        }
    }

    //Caculate Error after FeedForward và before BackPropagation
    //private void SetError()
    //{
    //	m_ErrorTotal = 0;
    //	for (int neuY = 0; neuY < m_NeuralCount[m_LayerCount - 1]; neuY++)
    //	{
    //		float Delta = m_Desired[neuY] - m_Activation[m_LayerCount - 1][neuY];
    //		m_ErrorTotal += (float)0.5 * Delta * Delta;
    //	}
    //}

    /// <summary>
    /// Active FeedForward
    /// </summary>
    public void SetFeedForward()
    {
        for (int lay = 1; lay < m_LayerCount; lay++)
        {
            SetFeedForward_Sum(lay);
            SetFeedForward_Sigmoid(lay);
        }
        //SetError();
    }

    //---------------------------------------------------------------------------- BackPropagation

    /// <summary>
    /// Caculate Error between Layer Output >> Desired
    /// </summary>
    private void SetBackPropagationErrorOuput()
    {
        int layerY = m_LayerCount - 1;
        for (int neuY = 0; neuY < m_NeuralCount[layerY]; neuY++)
        {
            //Xét Layer Y (L) với Desired
            m_Error[layerY - 1][neuY] =
                -(m_Desired[neuY] - m_Activation[layerY][neuY]);
        }
    }

    /// <summary>
    /// Caculate Sigmoid
    /// </summary>
    /// <param name="m_Value"></param>
    /// <returns></returns>
    public float GetBackPropagationSigmoid_Single(float m_Value)
    {
        float Sigmoid = GetFeedForward_Sigmoid_Single(m_Value);
        return Sigmoid * ((float)1.0 - Sigmoid);
    }

    /// <summary>
    /// Caculate Error between Layer Y (L) >> Layer Z (L+1)
    /// </summary>
    /// <param name="m_Layer"></param>
    private void SetBackPropagationErrorHidden(int m_Layer)
    {
        int layerZ = m_Layer + 1;
        int layerY = m_Layer;
        for (int neuY = 0; neuY < m_NeuralCount[layerY]; neuY++)
        {
            //Xét Layer Y (L) với Layer Z (L+1)
            //Debug.Log(Layer + " " + neuY);
            m_Error[layerY - 1][neuY] = 0;

            for (int neuZ = 0; neuZ < m_NeuralCount[layerZ]; neuZ++)
            {
                //Xét Layer Y (L) với Layer Z (L+1)
                m_Error[layerY - 1][neuY] +=
                    m_Error[layerZ - 1][neuZ] *
                    GetBackPropagationSigmoid_Single(m_Sum[layerZ - 1][neuZ]) *
                    m_Weight[layerZ - 1][neuZ][neuY];
            }
        }
    }

    /// <summary>
    /// Set Weight Layer
    /// </summary>
    /// <param name="Layer"></param>
    private void SetBackPropagationUpdate(int Layer)
    {
        //Layer Output
        int layerX = Layer - 1;
        int layerY = Layer;
        for (int neuX = 0; neuX < m_NeuralCount[layerX]; neuX++)
        {
            //Xét Layer X (L-1) >> Layer Y (L)
            for (int neuY = 0; neuY < m_NeuralCount[layerY]; neuY++)
            {
                //Xét Layer X (L-1) >> Layer Y (L)
                m_Weight[layerY - 1][neuY][neuX] -=
                    (float)0.5 * (
                        m_Error[layerY - 1][neuY] *
                        GetBackPropagationSigmoid_Single(m_Sum[layerY - 1][neuY]) *
                        m_Activation[layerX][neuX]);
            }
        }
    }

    /// <summary>
    /// Active BackPropagation
    /// </summary>
    public void SetBackPropagation()
    {
        for (int lay = m_LayerCount - 1; lay > 0; lay--)
        {
            //Xét Layer X (L-1) >> Layer Y (L) >> Layer Z (L+1)

            //Caculate Error
            if (lay == m_LayerCount - 1)
            {
                SetBackPropagationErrorOuput();
            }
            else
            {
                SetBackPropagationErrorHidden(lay);
            }
        }

        for (int lay = m_LayerCount - 1; lay > 0; lay--)
        {
            //Xét Layer X (L-1) >> Layer Y (L) >> Layer Z (L+1)

            //Set
            SetBackPropagationUpdate(lay);
        }

        //mLoopLearning++;
    }

    //---------------------------------------------------------------------------- Ghi chú

    /// <summary>
    /// Get Note Index if Exist
    /// </summary>
    /// <param name="CommentString"></param>
    /// <returns></returns>
    public int GetIndexComment(string CommentString)
    {
        for (int i = 0; i < m_Comment_str.Count; i++)
        {
            if (m_Comment_str[i] == CommentString)
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
    public void SetInputComment(string CommentString, float CommentNumber)
    {
        int Index = GetIndexComment(CommentString);
        if (Index != -1)
        {
            m_Comment_dou[Index] = CommentNumber;
        }
        else
        {
            m_Comment_str.Add(CommentString);
            m_Comment_dou.Add(CommentNumber);
        }
    }

    /// <summary>
    /// Get Note
    /// </summary>
    /// <param name="CommentString"></param>
    /// <returns>If not found, it will return "int.MaxValue"</returns>
    public float GetComment(string CommentString)
    {
        int Index = GetIndexComment(CommentString);
        if (Index != -1)
        {
            return m_Comment_dou[Index];
        }
        return int.MaxValue;
    }

    //---------------------------------------------------------------------------- Delay

    /// <summary>
    /// Delay Time
    /// </summary>
    private int m_BrainDelayTime = 3;
    private int m_BrainDelayTimeCurrent = 0;

    /// <summary>
    /// Set Delay Time
    /// </summary>
    /// <param name="Value"></param>
    public void SetInputmyBrainDelayTime(int Value)
    {
        m_BrainDelayTime = Value;
    }

    /// <summary>
    /// Check Delay Time
    /// </summary>
    /// <returns>Will return "True" if Delay Time = 0</returns>
    public bool GetCheckBoom_myBrainDelayTimeValue()
    {
        if (m_BrainDelayTimeCurrent > 0)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// Check Delay Time & Continue work on Delay Time
    /// </summary>
    /// <returns>Will return "True" if Delay Time = 0</returns>
    public bool GetCheckBoom_myBrainDelayTimeOver()
    {
        if (m_BrainDelayTimeCurrent > 0)
        {
            m_BrainDelayTimeCurrent -= 1;
            return false;
        }
        m_BrainDelayTimeCurrent = m_BrainDelayTime;
        return true;
    }

    //---------------------------------------------------------------------------- Debug
}