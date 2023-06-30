using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        const string CAMINHO_ARQUIVO_NOMES = "nomes.txt";
        const string CAMINHO_ARQUIVO_SORTEADOS = "sorteados.txt";

        if (!File.Exists(CAMINHO_ARQUIVO_NOMES))
        {
            Console.WriteLine($"Crie o arquivo {CAMINHO_ARQUIVO_NOMES} com a lista de nomes, um por linha.");
            Environment.Exit(1);
        }

        // Ler os nomes do arquivo TXT
        List<string> nomes = ListaListaNomesDoArquivo(CAMINHO_ARQUIVO_NOMES);

        // Ler os nomes já sorteados do arquivo "sorteados.txt"
        HashSet<string> nomesSorteados = LerNomesSorteados(CAMINHO_ARQUIVO_SORTEADOS);

        // Inicializar o gerador de números aleatórios
        Random random = new Random();

        // Sortear um nome de cada vez
        while (nomes.Count > 0)
        {
            // Sortear um índice aleatório dentro do intervalo dos nomes restantes
            int indiceSorteado = random.Next(nomes.Count);

            // Obter o nome sorteado
            string nomeSorteado = nomes[indiceSorteado];

            // Remover o nome sorteado da lista de nomes
            nomes.RemoveAt(indiceSorteado);

            // Verificar se o nome já foi sorteado antes
            if (!nomesSorteados.Contains(nomeSorteado))
            {
                // Adicionar o nome sorteado ao arquivo "sorteados.txt"
                AdicionarNomeSorteado(nomeSorteado, CAMINHO_ARQUIVO_SORTEADOS);

                // Exibir o nome sorteado
                Console.WriteLine("Nome sorteado: ");
                ImprimirNomeComoBanner(nomeSorteado);

                break;
            }
        }

        if (nomes.Count == 0)
        {
            Console.WriteLine("Todos os nomes foram sorteados.");
        }

    }

    static void ImprimirNomeComoBanner(string nome)
    {
        string[] letras = {
        "  A  ", " A A ", "AAAAA", "A   A", "A   A", // A
        "BBBB ", "B   B", "BBBB ", "B   B", "BBBB ", // B
        " CCC ", "C   C", "C    ", "C   C", " CCC ", // C
        "DDDD ", "D   D", "D   D", "D   D", "DDDD ", // D
        "EEEEE", "E    ", "EEE  ", "E    ", "EEEEE", // E
        "FFFFF", "F    ", "FFF  ", "F    ", "F    ", // F
        " GGG ", "G    ", "G  GG", "G   G", " GGG ", // G
        "H   H", "H   H", "HHHHH", "H   H", "H   H", // H
        " III ", "  I  ", "  I  ", "  I  ", " III ", // I
        " JJJ ", "   J ", "   J ", "J  J ", " JJ  ", // J
        "K   K", "K  K ", "KK   ", "K  K ", "K   K", // K
        "L    ", "L    ", "L    ", "L    ", "LLLLL", // L
        "M   M", "MM MM", "M M M", "M   M", "M   M", // M
        "NN  N", "N N N", "N  NN", "N   N", "N   N", // N
        " OOO ", "O   O", "O   O", "O   O", " OOO ", // O
        "PPPP ", "P   P", "PPPP ", "P    ", "P    ", // P
        " QQQ ", "Q   Q", "Q Q Q", "Q  QQ", " QQ Q", // Q
        "RRRR ", "R   R", "RRRR ", "R  R ", "R   R", // R
        " SSS ", "S    ", " SSS ", "    S", " SSS ", // S
        "TTTTT", "  T  ", "  T  ", "  T  ", "  T  ", // T
        "U   U", "U   U", "U   U", "U   U", " UUU ", // U
        "V   V", "V   V", "V   V", " V V ", "  V  ", // V
        "W   W", "W   W", "W W W", "WW WW", "W   W", // W
        "X   X", " X X ", "  X  ", " X X ", "X   X", // X
        "Y   Y", " Y Y ", "  Y  ", "  Y  ", "  Y  ", // Y
        "ZZZZZ", "   Z ", "  Z  ", " Z   ", "ZZZZZ"  // Z
    };
        // Converter o nome para maiúsculas
        nome = nome.ToUpper();

        // Imprimir cada linha do banner para cada letra do nome
        for (int linha = 0; linha < 5; linha++)
        {
            foreach (char c in nome)
            {
                string letra = "";

                if (c == ' ')
                {
                    letra = "   ";
                }
                else
                {
                    int indiceLetra = (c - 'A') * 5 + linha;

                    // Verificar se o caractere é uma letra de A a Z
                    if (indiceLetra >= 0 && indiceLetra < letras.Length)
                    {
                        letra = letras[indiceLetra];
                    }
                }
                letra = Regex.Replace(letra, "[A-za-z]", "#");
                Console.Write(letra + " ");
            }
            Console.WriteLine();
        }
    }

    static List<string> ListaListaNomesDoArquivo(string caminhoArquivo)
    {
        List<string> nomes = new List<string>();

        try
        {
            using (StreamReader sr = new StreamReader(caminhoArquivo))
            {
                while (!sr.EndOfStream)
                {
                    string? linha = sr.ReadLine();
                    nomes.Add(linha);
                }
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Ocorreu um erro ao ler o arquivo TXT: " + e.Message);
        }

        return nomes;
    }

    static HashSet<string> LerNomesSorteados(string caminhoArquivo)
    {
        HashSet<string> nomesSorteados = new HashSet<string>();

        try
        {
            if (File.Exists(caminhoArquivo))
            {
                using (StreamReader sr = new StreamReader(caminhoArquivo))
                {
                    while (!sr.EndOfStream)
                    {
                        string linha = sr.ReadLine();
                        nomesSorteados.Add(linha);
                    }
                }
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Ocorreu um erro ao ler o arquivo de nomes sorteados: " + e.Message);
        }

        return nomesSorteados;
    }

    static void AdicionarNomeSorteado(string nomeSorteado, string caminhoArquivo)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(caminhoArquivo, true))
            {
                sw.WriteLine(nomeSorteado);
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Ocorreu um erro ao gravar o nome sorteado: " + e.Message);
        }
    }
}
