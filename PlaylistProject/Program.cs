using PlaylistProject;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Uso correto: PlaylistProject.exe <url/nome> <diretorioSalvar> <plataforma>");
            return;
        }

        string urlOuNome = args[0];
        string diretorioSalvar = args[1];
        string plataforma = args[2].ToLower();

        IPlataformaDownload downloader = PlataformaDownloadFactory.Criar(plataforma);

        if (downloader != null)
        {
            await downloader.ProcessarDownload(urlOuNome, diretorioSalvar);
        }
        else
        {
            Console.WriteLine("Plataforma não suportada.");
        }
    }
}