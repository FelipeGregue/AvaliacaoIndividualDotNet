using System;
using System.Collections.Generic;

class Pessoa
{
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string CPF { get; set; }

    public Pessoa(string nome, DateTime dataNascimento, string cpf)
    {
        Nome = nome;
        DataNascimento = dataNascimento;
        CPF = cpf;
    }
}

class Treinador : Pessoa
{
    public string CREF { get; set; }

    public Treinador(string nome, DateTime dataNascimento, string cpf, string cref) : base(nome, dataNascimento, cpf)
    {
        CREF = cref;
    }
}

class Cliente : Pessoa
{
    public double Altura { get; set; }
    public double Peso { get; set; }

    public Cliente(string nome, DateTime dataNascimento, string cpf, double altura, double peso) : base(nome, dataNascimento, cpf)
    {
        Altura = altura;
        Peso = peso;
    }

    public double CalcularIMC()
    {
        return Peso / (Altura * Altura);
    }
}

class Program
{
    static void Main(string[] args)
    {
        int mes = 11;
        var treinadores = new List<Treinador>();
        var clientes = new List<Cliente>();

        // Exemplo de uso
        var treinador1 = new Treinador("João", new DateTime(1980, 1, 1), "12345678901", "ABC123");
        var treinador2 = new Treinador("Maria", new DateTime(1990, 1, 1), "23456789012", "DEF456");

        var cliente1 = new Cliente("José", new DateTime(2000, 1, 1), "34567890123", 1.8, 80);
        var cliente2 = new Cliente("Ana", new DateTime(1995, 1, 1), "45678901234", 1.6, 60);
        var cliente3 = new Cliente("Pedro", new DateTime(1985, 1, 1), "56789012345", 1.7, 70);

        treinadores.Add(treinador1);
        treinadores.Add(treinador2);

        clientes.Add(cliente1);
        clientes.Add(cliente2);
        clientes.Add(cliente3);

        // Verificar CPFs únicos
        var cpfs = new HashSet<string>();
        foreach (var treinador in treinadores)
        {
            if (!cpfs.Add(treinador.CPF))
            {
                Console.WriteLine($"Erro: CPF duplicado para treinador {treinador.Nome}.");
                return;
            }
        }
        foreach (var cliente in clientes)
        {
            if (!cpfs.Add(cliente.CPF))
            {
                Console.WriteLine($"Erro: CPF duplicado para cliente {cliente.Nome}.");
                return;
            }
        }

        // Relatório: Treinadores com idade entre dois valores
        Console.WriteLine("Treinadores com idade entre 30 e 40 anos:");
        var treinadoresEntre30e40 = treinadores.FindAll(t => t.DataNascimento >= DateTime.Now.AddYears(-40) && t.DataNascimento < DateTime.Now.AddYears(-30));
        foreach (var treinador in treinadoresEntre30e40)
        {
            Console.WriteLine($"- {treinador.Nome} ({treinador.DataNascimento:dd/MM/yyyy})");
        }

        // Relatório: Clientes com idade entre dois valores
        Console.WriteLine("\nClientes com idade entre 20 e 30 anos:");
        var clientesEntre20e30 = clientes.FindAll(c => c.DataNascimento >= DateTime.Now.AddYears(-30) && c.DataNascimento < DateTime.Now.AddYears(-20));
        foreach (var cliente in clientesEntre20e30)
        {
            Console.WriteLine($"- {cliente.Nome} ({cliente.DataNascimento:dd/MM/yyyy})");
        }

        // Relatório: Clientes com IMC maior que um valor informado, em ordem crescente
        Console.WriteLine("\nClientes com IMC maior que 25, em ordem crescente:");
        var clientesComIMCMaiorQue25 = clientes.FindAll(c => c.CalcularIMC() > 25);
        clientesComIMCMaiorQue25.Sort((c1, c2) => c1.CalcularIMC().CompareTo(c2.CalcularIMC()));
        foreach (var cliente in clientesComIMCMaiorQue25)
        {
            Console.WriteLine($"- {cliente.Nome} ({cliente.CalcularIMC():F2})");
        }

        // Relatório: Clientes em ordem alfabética
        Console.WriteLine("\nClientes em ordem alfabética:");
        clientes.Sort((c1, c2) => c1.Nome.CompareTo(c2.Nome));
        foreach (var cliente in clientes)
        {
            Console.WriteLine($"- {cliente.Nome}");
        }

        // Relatório: Clientes do mais velho para o mais novo
        Console.WriteLine("\nClientes do mais velho para o mais novo:");
        clientes.Sort((c1, c2) => c1.DataNascimento.CompareTo(c2.DataNascimento));
        foreach (var cliente in clientes)
        {
            Console.WriteLine($"- {cliente.Nome} ({cliente.DataNascimento:dd/MM/yyyy})");
        }

        // Relatório: Treinadores e clientes aniversariantes do mês informado
        var aniversariantes = new List<Pessoa>();
        aniversariantes.AddRange(treinadores.FindAll(t => t.DataNascimento.Month == mes));
        aniversariantes.AddRange(clientes.FindAll(c => c.DataNascimento.Month == mes));
        aniversariantes.Sort((p1, p2) => p1.DataNascimento.CompareTo(p2.DataNascimento));
        Console.WriteLine($"\nAniversariantes do mês {mes} ({aniversariantes.Count}):");
        foreach (var pessoa in aniversariantes)
        {
            Console.WriteLine($"- {pessoa.Nome} ({pessoa.DataNascimento:dd/MM/yyyy})");
        }
    }
}