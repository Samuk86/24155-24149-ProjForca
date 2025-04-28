using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PalavraDica : IComparable<PalavraDica>, IRegistro
{
  // mapeamento dos campos da linha de dados do arquivo
  const int tamanhoPalavra = 30;
  const int inicioPalavra = 0;
  const int inicioDica = inicioPalavra + tamanhoPalavra;

  // atributos da classe PalavraDica:
  string palavra, dica;

  public string Palavra
  {
    get => palavra;
    set
    {
      if (value != "")
         palavra = value.PadLeft(tamanhoPalavra, '0').Substring(0, tamanhoPalavra);
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

  public PalavraDica(string linhaDeDados)
  {
        Palavra = linhaDeDados.Substring(inicioPalavra, tamanhoPalavra);
        Dica = linhaDeDados.Substring(inicioDica);
  }
  public PalavraDica(string palavra, string dica)
    {
        Palavra = palavra;
        Dica = dica;
    }

  public int CompareTo(PalavraDica outroAluno)
  {
    return this.palavra.CompareTo(outroAluno.palavra);
  }
  public override string ToString()
  {
    return palavra + " " + dica;
  }

  public string FormatoDeArquivo()
  {
    return $"{palavra}{dica}";
  }

}