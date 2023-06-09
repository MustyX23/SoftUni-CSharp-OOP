namespace PlayersAndMonsters
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Wizard wizard = new Wizard("SashoMagiosnika", 17);
            System.Console.WriteLine(wizard.Level);
        }
    }
}