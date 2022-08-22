using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Agenda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            ExibirLista();
        }

        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            List<Contato> contatos = new List<Contato>();
            AtualizarLista(contatos);
            contatos.Add(new Contato(txtNome.Text, txtEmail.Text, txtTelefone.Text));
            ManipularArquivos.EscreverArquivo(contatos);

            ExibirLista();
            LimparCampos();
        }

        private void BtnDeletar_Click(object sender, EventArgs e)
        {
            if (Aviso())
            {
                return;
            }

            lbxContato.Items.RemoveAt(lbxContato.SelectedIndex);
            List<Contato> contatos = new List<Contato>();
            AtualizarLista(contatos);

            ExibirLista();
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (Aviso())
            {
                return;
            }

            int index = lbxContato.SelectedIndex;
            string[] dadosContato = lbxContato.Items[index].ToString().Trim().Split('-');

            if (dadosContato.Count() == 3)
            {
                txtNome.Text = dadosContato[0];
                txtEmail.Text = dadosContato[1];
                txtTelefone.Text = dadosContato[2];
            }

            lbxContato.Items.RemoveAt(lbxContato.SelectedIndex);
        }

        //------------FUNÇÕES CRIADAS PELO PROGRAMADOR------------

        private void LimparCampos()
        {
            txtEmail.Text = "";
            txtNome.Text = "";
            txtTelefone.Text = "";
        }
        private void ExibirLista()
        {
            lbxContato.Items.Clear();
            lbxContato.Items.AddRange(ManipularArquivos.LerArquivo().ToArray());
        }
        private void AtualizarLista(List<Contato> contatos)
        {
            foreach(Contato contato in lbxContato.Items)
            {
                contatos.Add(contato);
            }
            ManipularArquivos.EscreverArquivo(contatos);
        }

        private bool Aviso()
        {
            if (lbxContato.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione algum item para realizar esta ação", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                DialogResult res = MessageBox.Show("Tem certeza que deseja realizar esta ação?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.No)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
