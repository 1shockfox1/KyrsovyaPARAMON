using System;

namespace KyrsovyaPARAMON.Models
{
    public class Administrator : Person
    {
        public int AdminId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public Administrator() : base()
        {
            Login = "admin";
            Password = "123";
        }
    }
}