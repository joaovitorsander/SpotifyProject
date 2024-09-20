using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaylistProject
{
    public interface IPlataformaDownload
    {
        Task ProcessarDownload(string urlOuNome, string diretorioSalvar);
    }
}
