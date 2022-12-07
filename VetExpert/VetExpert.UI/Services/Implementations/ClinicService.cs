using System.Text.Json;
using System;
using VetExpert.UI.Services.Interfaces;
using VetExpert.Domain;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace VetExpert.UI.Services.Implementations
{
    public class ClinicService : IClinicService
    {
        private const string ApiURL = "https://localhost:7231/api/Clinics";
        private readonly HttpClient httpClient;

        public ClinicService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Clinic>> GetAllClinics()
        {
            var result = await httpClient.GetStringAsync(ApiURL);

            return JsonConvert.DeserializeObject<IEnumerable<Clinic>>(result);
        }
    }
}
