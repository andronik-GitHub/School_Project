using Application.Features.AuthorFeatures.Commands.CreateAuthor;
using Application.Features.AuthorFeatures.Commands.UpdateAuthor;
using Application.Features.GenreFeatures.Commands.CreateGenre;
using Application.Features.GenreFeatures.Commands.UpdateGenre;
using Application.Features.GenreFeatures.Queries.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace UI.WPF.MVVM.VM.GenreVM
{
    public class Genre_VM : BaseVM
    {
        public Genre_VM() : base("genre")
        {
        }

        public async Task<IEnumerable<GenreDTO>?> GetListAsync()
        {
            var response = await Client.GetAsync($"{HostName}");
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var temp = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<ObservableCollection<GenreDTO>>(temp);
                return list;
            }
            throw new Exception("Problem");
        }
        public async Task<Guid> InsertAsync(CreateGenreCommand entity)
        {
            var response = await Client.PostAsJsonAsync($"{HostName}", entity);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var temp = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Guid>(temp);
                return result;
            }
            return Guid.Empty;
        }
        public async Task<bool> UpdateAsync(UpdateGenreCommand entity)
        {
            var response = await Client.PutAsJsonAsync($"{HostName}", entity);
            if (response.IsSuccessStatusCode) return true;
            return false;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await Client.DeleteAsync($"{HostName}/{id}");
            if (response.IsSuccessStatusCode) return true;
            return false;
        }
    }
}
