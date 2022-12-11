namespace CO3015_Assignment3
{
    class Program
    {
        public static void Main(string[] args)
        {
            A01AddMenu driverA01 = new A01AddMenu();
            driverA01.RunTest();

            A02OrderAction driverA02 = new A02OrderAction();
            driverA02.RunTest();
        }
    }
}