//Rafael Ferreira Rigo - 24149
//Samuel Rosa Parra - 24155

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Dicionario : IComparable<Dicionario>, IRegistro
{
  // mapeamento dos campos da linha de dados do arquivo
  const int tamanhoPalavra = 30;
  const int inicioPalavra = 0;
  const int inicioDica = inicioPalavra + tamanhoPalavra;

  // atributos da classe PalavraDica:
  string palavra, dica;

    public bool[] acertou = new bool[tamanhoPalavra];

  public string Palavra
  {
    get => palavra;
    set
    {
      if (value != "")
         palavra = value;
      else
        throw new Exception("Palavra vazia é inválida.");
    }
  }

  public string Dica 
  { 
    get => dica;
    set
    {
      if (value != "")
        dica = value;
      else
        throw new Exception("Dica vazia é inválida.");
    }
  }

  public Dicionario(string linhaDeDados)
  {
        Palavra = linhaDeDados.Substring(inicioPalavra, tamanhoPalavra).Trim();
        Dica = linhaDeDados.Substring(inicioDica);
  }
  public Dicionario(string palavra, string dica)
    {
        Palavra = palavra;
        Dica = dica;
    }

  public int CompareTo(Dicionario outroAluno)
  {
    return this.palavra.CompareTo(outroAluno.palavra);
  }
  public override string ToString()
  {
    return palavra + " " + dica;
  }

  public string FormatoDeArquivo()
  {
    return $"{palavra.PadRight(tamanhoPalavra)}{dica}";
  }

}