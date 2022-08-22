namespace Agenda
{
    public class Contato
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public Contato(string nome,string email,string telefone)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} - {2}", Nome, Email, Telefone);
        }
    }
}
