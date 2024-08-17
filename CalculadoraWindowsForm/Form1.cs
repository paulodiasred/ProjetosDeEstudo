using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora
{
    public partial class Form1 : Form
    {
        public decimal Resultado { get; set; }
        public decimal Valor { get; set; }
        private Operacao OperacaoSelecionada { get; set; }
        private enum Operacao
        {
            Adicao,
            Subtracao,
            Multiplicacao,
            Divisao
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            btnTexto.Text += "0";
        }

        private void btnUm_Click(object sender, EventArgs e)
        {
            btnTexto.Text += "1";
        }

        private void btnDois_Click(object sender, EventArgs e)
        {
            btnTexto.Text += "2";
        }

        private void btnTres_Click(object sender, EventArgs e)
        {
            btnTexto.Text += "3";
        }

        private void btnQuatro_Click(object sender, EventArgs e)
        {
            btnTexto.Text += "4";
        }

        private void btnCinco_Click(object sender, EventArgs e)
        {
            btnTexto.Text += "5";
        }

        private void btnSeis_Click(object sender, EventArgs e)
        {
            btnTexto.Text += "6";
        }

        private void btnSete_Click(object sender, EventArgs e)
        {
            btnTexto.Text += "7";
        }

        private void btnOito_Click(object sender, EventArgs e)
        {
            btnTexto.Text += "8";
        }

        private void btnNove_Click(object sender, EventArgs e)
        {
            btnTexto.Text += "9";
        }

        private void btnAdicao_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(btnTexto.Text)) return;
            OperacaoSelecionada = Operacao.Adicao;
            Valor = Convert.ToDecimal(btnTexto.Text);
            btnTexto.Text = "";
            lblOperacao.Text = "+";
        }

        private void btnSubtracao_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(btnTexto.Text)) return;
            OperacaoSelecionada = Operacao.Subtracao;
            Valor = Convert.ToDecimal(btnTexto.Text);
            btnTexto.Text = "";
            lblOperacao.Text = "-";
        }

        private void btnMultiplicacao_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(btnTexto.Text)) return;
            OperacaoSelecionada = Operacao.Multiplicacao;
            Valor = Convert.ToDecimal(btnTexto.Text);
            btnTexto.Text = "";
            lblOperacao.Text = "X";
        }

        private void btnDivisao_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(btnTexto.Text)) return;
            OperacaoSelecionada = Operacao.Divisao;
            Valor = Convert.ToDecimal(btnTexto.Text);
            btnTexto.Text = "";
            lblOperacao.Text = "/";
        }

        private void btnIgual_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(btnTexto.Text)) return;
                switch (OperacaoSelecionada)
                {
                    case Operacao.Adicao:
                        Resultado = Valor + Convert.ToDecimal(btnTexto.Text);
                        break;
                    case Operacao.Subtracao:
                        Resultado = Valor - Convert.ToDecimal(btnTexto.Text);
                        break;
                    case Operacao.Multiplicacao:
                        Resultado = Valor * Convert.ToDecimal(btnTexto.Text);
                        break;
                    case Operacao.Divisao:
                        if (Convert.ToDecimal(btnTexto.Text) == 0)
                        {
                            MessageBox.Show("Divisão por zero não é permitida.");
                            return;
                        }
                        Resultado = Valor / Convert.ToDecimal(btnTexto.Text);
                        break;
                }
                btnTexto.Text = Convert.ToString(Resultado);
                lblOperacao.Text = "=";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message);
            }
        }

            private void btnVirgula_Click(object sender, EventArgs e)
            {
                if (!btnTexto.Text.Contains(","))
                    btnTexto.Text += ",";
            }

            private void btnLimpar_Click(object sender, EventArgs e)
            {
                btnTexto.Text = "";
                lblOperacao.Text = "";
            }
        }
}
