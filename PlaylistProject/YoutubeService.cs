using System;
using System.IO;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Search;
using YoutubeExplode.Videos.Streams;

public class YoutubeService
{
    private YoutubeClient youtube;

    public YoutubeService()
    {
        youtube = new YoutubeClient();
    }

    public async Task PesquisarEbaixarMusica(string query, string diretorioSalvar)
    {
        try
        {
            var videos = youtube.Search.GetVideosAsync(query);
            VideoSearchResult? video = null;
            await foreach (var result in videos)
            {
                video = result;
                break;
            }

            if (video != null)
            {
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);
                var audioStreamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                var outputFilePath = Path.Combine(diretorioSalvar, $"{SanitizeFileName(video.Title)}.mp3");

                await youtube.Videos.Streams.DownloadAsync(audioStreamInfo, outputFilePath);
                Console.WriteLine($"Baixado: {video.Title}");
            }
            else
            {
                Console.WriteLine($"Nenhum resultado encontrado para {query}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao procurar e baixar do YouTube: {ex.Message}");
        }
    }

    public async Task BaixarPlaylist(string playlistUrl, string diretorioSalvar)
    {
        try
        {
            var videos = youtube.Playlists.GetVideosAsync(playlistUrl);
            Console.WriteLine($"Baixando playlist: {playlistUrl}");

            await foreach (var video in videos)
            {
                var streamManifest = await youtube.Videos.Streams.GetManifestAsync(video.Id);
                var audioStreamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                var outputFilePath = Path.Combine(diretorioSalvar, $"{SanitizeFileName(video.Title)}.mp3");

                await youtube.Videos.Streams.DownloadAsync(audioStreamInfo, outputFilePath);
                Console.WriteLine($"Baixado: {video.Title}");
            }

            Console.WriteLine("Todos os vídeos da playlist foram baixados.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao baixar playlist do YouTube: {ex.Message}");
        }
    }

    private string SanitizeFileName(string name)
    {
        foreach (char invalidChar in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(invalidChar, '_');
        }
        return name;
    }
}
