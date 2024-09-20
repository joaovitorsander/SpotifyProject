using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistProject
{
    public static class PlataformaDownloadFactory
    {
        public static IPlataformaDownload Criar(string plataforma)
        {
            switch (plataforma)
            {
                case "youtube":
                    return new YoutubeDownloader();
                case "spotify":
                    return new SpotifyDownloader();
                default:
                    return null;
            }
        }
    }

}
