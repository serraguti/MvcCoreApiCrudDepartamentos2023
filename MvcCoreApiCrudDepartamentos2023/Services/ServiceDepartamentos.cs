using MvcCoreApiCrudDepartamentos2023.Models;
using System.Net.Http.Headers;

namespace MvcCoreApiCrudDepartamentos2023.Services
{
    public class ServiceDepartamentos
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;

        public ServiceDepartamentos(string url)
        {
            this.UrlApi = url;
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        //METODO GENERICO PARA DEVOLVER CUALQUIER CLASE CON GET
        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Departamento>> GetDepartamentosAsync()
        {
            string request = "/api/departamentos";
            List<Departamento> departamentos = 
                await this.CallApiAsync<List<Departamento>>(request);
            return departamentos;
        }

        public async Task<Departamento> FindDepartamentoAsync(int iddepartamento)
        {
            string request = "/api/departamentos/" + iddepartamento;
            Departamento departamento = await this.CallApiAsync<Departamento>(request);
            return departamento;
        }

        public async Task<List<Departamento>> GetDepartamentosLocalidadAsync(string localidad)
        {
            string request = "/api/departamentos/finddepartamentoslocalidad/" + localidad;
            List<Departamento> departamentos =
                await this.CallApiAsync<List<Departamento>>(request);
            return departamentos;
        }
    }
}
