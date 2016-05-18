using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Entity;
using System.IO;
using System.Net;
using DesafioFCamara.Models;

namespace DesafioFCamaraWCF
{
    public class DesafioFCamaraWCF : IDesafioFCamaraWCF
    {
        public MyToken GenerateToken()
        {
            return new MyToken(Guid.NewGuid().ToString(), DateTime.Now); // cria novo token com data de agora
        }

        public HttpStatusCode ValidateToken(string date)
        {
            DateTime result;
            if (!DateTime.TryParse(date, out result)) // verifica se é uma data
            {
                return HttpStatusCode.BadRequest;
            }

            try
            {
                double totalSeconds = DateTime.Now.Subtract(result).TotalSeconds;

                if (0 <= totalSeconds && totalSeconds <= 60) // menos de 1 minuto
                {
                    return HttpStatusCode.Accepted; // token valido
                }
            }
            catch (Exception ex)
            {
                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.Unauthorized; // se chegou ate aqui, algo de errado aconteceu
        }
    }
}
