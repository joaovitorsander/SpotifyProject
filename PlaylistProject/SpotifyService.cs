using System;
using System.IO;
using System.Threading.Tasks;
using SpotifyAPI.Web;

public class SpotifyService
{
    public async Task BaixarPlaylistSpotify(string linkPlaylist, string diretorioSalvar)
    {
        try
        {
            var config = SpotifyClientConfig.CreateDefault();
            var request = new ClientCredentialsRequest("d68a06ff14914e8996fe9c2d978724c4", "c4a309de56624139ba2eeda4cea19251");
            var response = await new OAuthClient(config).RequestToken(request);
            var spotify = new SpotifyClient(config.WithToken(response.AccessToken));

            string playlistId = ExtrairSpotifyPlaylistId(linkPlaylist);
            var playlist = await spotify.Playlists.Get(playlistId);

            Console.WriteLine($"Baixando playlist: {playlist.Name}");

            foreach (var item in playlist.Tracks.Items)
            {
                if (item.Track is FullTrack track)
                {
                    var nomeMusica = track.Name;
                    var artista = track.Artists[0].Name;
                    var query = $"{nomeMusica} {artista}";

                    Console.WriteLine($"Procurando e baixando: {nomeMusica} de {artista}");
                    var youtubeService = new YoutubeService();
                    await youtubeService.PesquisarEbaixarMusica(query, diretorioSalvar);
                }
            }

            Console.WriteLine("Todas as músicas da playlist foram processadas.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao baixar playlist do Spotify: {ex.Message}");
        }
    }

    public async Task BaixarMusicaUnicaSpotify(string nomeMusica, string diretorioSalvar)
    {
        try
        {
            var config = SpotifyClientConfig.CreateDefault();
            var request = new ClientCredentialsRequest("d68a06ff14914e8996fe9c2d978724c4", "c4a309de56624139ba2eeda4cea19251");
            var response = await new OAuthClient(config).RequestToken(request);
            var spotify = new SpotifyClient(config.WithToken(response.AccessToken));

            var searchRequest = new SearchRequest(SearchRequest.Types.Track, nomeMusica);
            var searchResult = await spotify.Search.Item(searchRequest);

            if (searchResult.Tracks.Items.Count > 0)
            {
                var track = searchResult.Tracks.Items[0];
                var query = $"{track.Name} {track.Artists[0].Name}";

                Console.WriteLine($"Procurando e baixando: {track.Name} de {track.Artists[0].Name}");
                var youtubeService = new YoutubeService();
                await youtubeService.PesquisarEbaixarMusica(query, diretorioSalvar);
            }
            else
            {
                Console.WriteLine($"Nenhum resultado encontrado para {nomeMusica}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao procurar e baixar música do Spotify: {ex.Message}");
        }
    }

    private string ExtrairSpotifyPlaylistId(string url)
    {
        var uri = new Uri(url);
        var segments = uri.Segments;
        if (segments.Length >= 3 && segments[1] == "playlist/")
        {
            return segments[2].Split('?')[0];
        }

        throw new ArgumentException("URL inválida de playlist do Spotify.");
    }
}
