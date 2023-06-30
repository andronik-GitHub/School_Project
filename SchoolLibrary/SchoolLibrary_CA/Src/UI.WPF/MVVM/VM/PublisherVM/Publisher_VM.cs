﻿using Application.Features.BookFeatures.Commands.CreateBook;
using Application.Features.BookFeatures.Commands.UpdateBook;
using Application.Features.PublisherFeatures.Commands.CreatePublisher;
using Application.Features.PublisherFeatures.Commands.UpdatePublisher;
using Application.Features.PublisherFeatures.Queries.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace UI.WPF.MVVM.VM.PublisherVM
{
    public class Publisher_VM : BaseVM
    {
        public Publisher_VM() : base("publisher")
        {
        }

        public async Task<IEnumerable<PublisherDTO>?> GetListAsync()
        {
            var response = await Client.GetAsync($"{HostName}");
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var temp = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<ObservableCollection<PublisherDTO>>(temp);
                return list;
            }
            throw new Exception("Problem");
        }
        public async Task<Guid> InsertAsync(CreatePublisherCommand entity)
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
        public async Task<bool> UpdateAsync(UpdatePublisherCommand entity)
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
