namespace salesjs.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Common.Models;
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using salesjs.Helpers;

    public class ApiService
    {
        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller)
        {
            try

            {
                //creamos la variable que consume el servicio API
                var client = new HttpClient();
                //le asignamos la direccion base
                client.BaseAddress = new Uri(urlBase);
                //concatenamos la url
                var url = string.Format("{0}{1}", prefix, controller);
                //creamos el objeto response que es asincrono
                var response = await client.GetAsync(url);
                //leo la respuesta creando un obj answer, 
                //uso ReadAsStringAsync por que leo un json
                var answer = await response.Content.ReadAsStringAsync();
                //como no se si pudo leer bien, pregunto
                //si no es exitosa retorno una nueva respuesta
                if (!response.IsSuccessStatusCode)

                {

                    return new Response

                    {

                        IsSuccess = false,

                        Message = answer,

                    };

                }

                //Si si pudo leer bien, descerializo la lista con json
                //
                var list = JsonConvert.DeserializeObject<List<T>>(answer);
                //le mando una nueva respuesta exitosa y la lista
                return new Response

                {

                    IsSuccess = true,

                    Result = list,

                };

            }

            catch (Exception ex)

            {

                return new Response

                {

                    IsSuccess = false,

                    Message = ex.Message,

                };

            }
        }


        public async Task<Response> CheckConnection()

        {
            //Verificamos que el telefono esta encendida la internet
            if (!CrossConnectivity.Current.IsConnected)

            {

                return new Response

                {

                    IsSuccess = false,

                    Message = Languages.InternetSettings,

                };

            }


            //hacemos un ping para ver si hay conneccion a internet
            //si no responde es que no hay acceso
            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");

            if (!isReachable)

            {

                return new Response

                {

                    IsSuccess = false,

                    Message = Languages.NoInternet,

                };

            }



            return new Response

            {

                IsSuccess = true,

            };

        }
    }
}
