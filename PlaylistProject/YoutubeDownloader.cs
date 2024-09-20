using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistProject
{
    public class YoutubeDownloader : IPlataformaDownload
    {
        public async Task ProcessarDownload(string urlOuNome, string diretorioSalvar)
        {
            var youtubeService = new YoutubeService();
            if (urlOuNome.Contains("playlist"))
            {
                await youtubeService.BaixarPlaylist(urlOuNome, diretorioSalvar);
            }
            else
            {
                await youtubeService.PesquisarEbaixarMusica(urlOuNome, diretorioSalvar);
            }
        }
    }
}
