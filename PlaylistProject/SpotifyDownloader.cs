using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistProject
{
    public class SpotifyDownloader : IPlataformaDownload
    {
        public async Task ProcessarDownload(string urlOuNome, string diretorioSalvar)
        {
            var spotifyService = new SpotifyService();
            if (urlOuNome.Contains("playlist"))
            {
                await spotifyService.BaixarPlaylistSpotify(urlOuNome, diretorioSalvar);
            }
            else
            {
                await spotifyService.BaixarMusicaUnicaSpotify(urlOuNome, diretorioSalvar);
            }
        }
    }
}
