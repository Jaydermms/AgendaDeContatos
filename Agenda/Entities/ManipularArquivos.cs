using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agenda
{
    public class ManipularArquivos
    {
        private static string DiretorioArquivo = AppDomain.CurrentDomain.BaseDirectory + "contatos.csv";

        public static List<Contato> LerArquivo()
        {
            List<Contato> contatosList = new List<Contato>();

            if (File.Exists(@DiretorioArquivo))
            {
                try
                {
                    using (StreamReader sr = File.OpenText(@DiretorioArquivo))
                    {
                        while (sr.Peek() >= 0)
                        {
                            string[] dadosContato = sr.ReadLine().Split(';');

                            if (dadosContato.Count() == 3)
                            {
                                string nome = dadosContato[0];
                                string email = dadosContato[1];
                                string telefone = dadosContato[2];

                                contatosList.Add(new Contato(nome, email, telefone));
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Erro ao ler o arquivo!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return contatosList;
        }

        public static void EscreverArquivo(List<Contato> contatos)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(@DiretorioArquivo, false))
                {
                    foreach (Contato contato in contatos)
                    {
                        if(string.IsNullOrWhiteSpace(contato.Nome) || string.IsNullOrWhiteSpace(contato.Email) || string.IsNullOrWhiteSpace(contato.Telefone))
                        {
                            MessageBox.Show("Todos os campos devem estar preenchidos", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        string dadosFormatados = string.Format("{0};{1};{2}", contato.Nome.Trim(), contato.Email.Trim(), contato.Telefone.Trim());
                        sw.WriteLine(dadosFormatados);
                    }
                    sw.Flush();
                }
            }
            catch
            {
                MessageBox.Show("Erro ao incluir dados no arquivo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
