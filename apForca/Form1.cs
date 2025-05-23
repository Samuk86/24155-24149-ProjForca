﻿//Rafael Ferreira Rigo - 24149
//Samuel Rosa Parra - 24155


using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace apListaLigada
{
  public partial class FrmAlunos : Form
  {
    ListaDupla<PalavraDica> lista1;

    public FrmAlunos()
    {
      InitializeComponent();
    }

    private void FazerLeitura(ref ListaDupla<PalavraDica> qualLista)
    {
      // instanciar a lista de palavras e dicas
      qualLista = new ListaDupla<PalavraDica>();
      // pedir ao usuário o nome do arquivo de entrada
      if (dlgAbrir.ShowDialog() == DialogResult.OK)
            {
                // abrir esse arquivo e lê-lo linha a linha
                StreamReader arquivo = new StreamReader(dlgAbrir.FileName);
                string linha = "";
                while (!arquivo.EndOfStream)
                {
                    // para cada linha, criar um objeto da classe de Palavra e Dica
                    // e inseri-lo no final da lista duplamente ligada
                    linha = arquivo.ReadLine();
                    if (linha != "")
                        qualLista.InserirAposFim(new PalavraDica(linha));
                }
                arquivo.Close();
            }
    }

    private void btnIncluir_Click(object sender, EventArgs e)
    {
            // sx''e o usuário digitou palavra e dica:
            if (txtPalavra.Text != "" && txtDica.Text != "")
            {
                // criar objeto da classe Palavra e Dica para busca
                var novaPalavra = new PalavraDica(txtPalavra.Text, txtDica.Text);
                // tentar incluir em ordem esse objeto na lista1
                if (!lista1.InserirEmOrdem(novaPalavra))
                    // se não incluiu (já existe) avisar o usuário
                    MessageBox.Show("Palavra repetida.");
                else
                    MessageBox.Show("Palavra incluída em ordem.");
                ExibirRegistroAtual();
            }
      
    }


    private void btnBuscar_Click(object sender, EventArgs e)
    {
            // se a palavra digitada não é vazia:
            if (txtPalavra.Text != "")
            {
                // criar um objeto da classe de Palavra e Dica para busca
                var palavraProcurada = new PalavraDica(txtPalavra.Text, ".");
                // se a palavra existe na lista1, posicionar o ponteiro atual nesse nó e exibir o registro atual
                if (!lista1.Existe(palavraProcurada))
                    // senão, avisar usuário que a palavra não existe
                    MessageBox.Show("Palavra não encontrada!");
                // exibir o nó atual
                else
                    ExibirRegistroAtual();
            }
    }

    private void btnExcluir_Click(object sender, EventArgs e)
    {
            if (txtPalavra. Text != "")
            {
                // para o nó atualmente visitado e exibido na tela:
                // perguntar ao usuário se realmente deseja excluir essa palavra e dica
                DialogResult resultado = MessageBox.Show(
                    "Deseja continuar?",           // Texto da mensagem
                    "Confirmação",                 // Título da janela
                    MessageBoxButtons.YesNo,       // Botões: Sim e Não
                    MessageBoxIcon.Question        // Ícone de interrogação
                    );

                if (resultado == DialogResult.Yes)
                {
                    var palavraARemover = new PalavraDica(txtPalavra.Text, ".");
                    lista1.Remover(palavraARemover);
                }
                else
                {
                    // Usuário clicou em Não
                    MessageBox.Show("Palavra não removida!");
                }
                // se sim, remover o nó atual da lista duplamente ligada e exibir o próximo nó

                // se não, manter como está
                ExibirRegistroAtual();
            }
      
    }

    private void FrmAlunos_FormClosing(object sender, FormClosingEventArgs e)
    {
      // solicitar ao usuário que escolha o arquivo de saída
      if (dlgAbrir.ShowDialog() == DialogResult.OK){
                // percorrer a lista ligada e gravar seus dados no arquivo de saída
                lista1.GravarDados(dlgAbrir.FileName);
      }
    }

    private void ExibirDados(ListaDupla<PalavraDica> aLista, ListBox lsb, Direcao qualDirecao)
    {
      lsb.Items.Clear();
      var dadosDaLista = aLista.Listagem(qualDirecao);
      foreach (PalavraDica palavra in dadosDaLista)
        lsb.Items.Add(palavra);
    }

    private void tabControl1_Enter(object sender, EventArgs e)
    {
      rbFrente.PerformClick();
    }

    private void rbFrente_Click(object sender, EventArgs e)
    {
      ExibirDados(lista1, lsbDados, Direcao.paraFrente);
    }

    private void rbTras_Click(object sender, EventArgs e)
    {
      ExibirDados(lista1, lsbDados, Direcao.paraTras);
    }

    private void FrmAlunos_Load(object sender, EventArgs e)
    {
            // fazer a leitura do arquivo escolhido pelo usuário e armazená-lo na lista1
            FazerLeitura(ref lista1);
            // posicionar o ponteiro atual no início da lista duplamente ligada
            lista1.PosicionarNoInicio();
            // Exibir o Registro Atual;
            ExibirRegistroAtual();
    }

    private void btnInicio_Click(object sender, EventArgs e)
    {
            // posicionar o ponteiro atual no início da lista duplamente ligada
            lista1.PosicionarNoInicio();
            // Exibir o Registro Atual;
            ExibirRegistroAtual();
    }

    private void btnAnterior_Click(object sender, EventArgs e)
    {
            // Retroceder o ponteiro atual para o nó imediatamente anterior 
            lista1.Retroceder();
            // Exibir o Registro Atual;
            ExibirRegistroAtual();
    }

    private void btnProximo_Click(object sender, EventArgs e)
    {
            // Avançar o ponteiro atual para o nó seguinte 
            lista1.Avancar();
            // Exibir o Registro Atual;
            ExibirRegistroAtual();
    }

    private void btnFim_Click(object sender, EventArgs e)
    {
            // posicionar o ponteiro atual no último nó da lista 
            lista1.PosicionarNoFinal();
            // Exibir o Registro Atual;
            ExibirRegistroAtual();
    }

    private void ExibirRegistroAtual()
    {
            // se a lista não está vazia:
            if (!lista1.EstaVazia)
            {
                // acessar o nó atual e exibir seus campos em txtDica e txtPalavra
                var palavraAtual = lista1[lista1.NumeroDoNoAtual];
                txtPalavra.Text = palavraAtual.Palavra;
                txtDica.Text = palavraAtual.Dica;
                // atualizar no status bar o número do registro atual / quantos nós na lista
                slRegistro.Text = $"Registro: {lista1.NumeroDoNoAtual+1}/{lista1.QuantosNos}";
            }
            
    }

    private void btnEditar_Click(object sender, EventArgs e)
    {
            // alterar a dica e guardar seu novo valor no nó exibido
            if (txtDica.Text != "")
            {
                string novaDica = txtDica.Text;
                lista1[lista1.NumeroDoNoAtual].Dica = novaDica;
                MessageBox.Show("Dica alterada.");
            }
    }

    private void btnSair_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void btnCancelar_Click(object sender, EventArgs e)
    {
            txtPalavra.Text = lista1[lista1.NumeroDoNoAtual].Palavra;
            txtDica.Text = lista1[lista1.NumeroDoNoAtual].Dica;
    }

    }
}
