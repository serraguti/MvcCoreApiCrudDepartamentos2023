using MvcCoreApiCrudDepartamentos2023.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

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

        //LOS METODOS DE ACCION NO SUELEN TENER UN METODO GENERICO
        //DEBIDO A QUE CADA UNO RECIBE UN VALOR DISTINTO
        public async Task DeleteDepartamentoAsync(int iddepartamento)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/departamentos/" + iddepartamento;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                //COMO NO VAMOS A RECIBIR NADA (OBJETO) SIMPLEMENTE SE 
                //REALIZA LA ACCION
                await client.DeleteAsync(request);
            }
        }

        //VAMOS A UTILIZAR INSERTAR OBJETO EN EL BODY
        public async Task InsertDepartamentoAsync
            (int id, string nombre, string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/departamentos";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                //TENEMOS QUE ENVIAR UN OBJETO DEPARTAMENTO
                //POR LO QUE CREAMOS UNA CLASE DEL MODEL DEPARTAMENTO
                //CON LOS DATOS QUE NOS HAN PROPORCIONADO
                Departamento departamento = new Departamento();
                departamento.IdDepartamento = id;
                departamento.Nombre = nombre;
                departamento.Localidad = localidad;
                //CONVERTIMOS EL OBJETO DEPARTAMENTO EN UN JSON
                string departamentoJson =
                    JsonConvert.SerializeObject(departamento);
                //PARA ENVIAR EL OBJETO JSON EN EL BODY SE REALIZA 
                //MEDIANTE UNA CLASE LLAMADA StringContent
                //DONDE DEBEMOS INDICAR EL TIPO DE CONTENIDO QUE ESTAMOS
                //ENVIANDO (JSON)
                StringContent content =
                    new StringContent(departamentoJson, Encoding.UTF8, "application/json");
                //REALIZAMOS LA LLAMADA AL SERVICIO ENVIANDO EL OBJETO CONTENT
                await client.PostAsync(request, content);
            }
        }

        //METODO PUT CON OBJETO
        public async Task UpdateDepartamentoAsync(int id, string nombre, string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/departamentos";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                Departamento departamento = new Departamento();
                departamento.IdDepartamento = id;
                departamento.Nombre = nombre;
                departamento.Localidad = localidad;
                string json = JsonConvert.SerializeObject(departamento);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }
    }
}
