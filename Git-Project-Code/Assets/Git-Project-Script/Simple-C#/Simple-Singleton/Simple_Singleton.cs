public class Simple_Singleton
{
    //Xác nhận chỉ 1 lớp này tồn tại trong hệ thống
    private static readonly Simple_Singleton instance = new Simple_Singleton();

    private int i_Number = 2;

    public int i_Number_Public = 5;

    public static Simple_Singleton Get_Instance()
    {
        return instance;
    }

    public static void Set_Number(int i_Number)
    {
        Get_Instance().i_Number = i_Number;
    }

    public static int Get_Number()
    {
        return Get_Instance().i_Number;
    }
}