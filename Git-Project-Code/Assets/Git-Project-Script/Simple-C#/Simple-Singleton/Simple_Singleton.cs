public class Simple_Singleton
{
    //Xác nhận chỉ 1 lớp này tồn tại trong hệ thống
    private static readonly Simple_Singleton instance = new Simple_Singleton();

    private int m_Number = 2;

    public int m_Number_Public = 5;

    public static Simple_Singleton GetInstance()
    {
        return instance;
    }

    public static void Set_Number(int m_Number)
    {
        GetInstance().m_Number = m_Number;
    }

    public static int GetNumber()
    {
        return GetInstance().m_Number;
    }
}