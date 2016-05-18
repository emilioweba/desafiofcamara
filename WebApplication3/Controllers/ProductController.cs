using DesafioFCamara.Models;
using DesafioFCamaraWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Web.Http;
//using DesafioFCamara.DesafioFCamara;

namespace DesafioFCamara.Controllers
{
    public class ProductController : ApiController
    {
        [AcceptVerbs("POST")]
        public IEnumerable<Product> GetAll(MyToken token)
        {
            var myChannelFactory = new ChannelFactory<IDesafioFCamaraWCF>(new WebHttpBinding(),
                "http://www.emilioweba.com/DesafioFCamaraWCF.svc");

            myChannelFactory.Endpoint.Behaviors.Add(new WebHttpBehavior());

            IDesafioFCamaraWCF client = null;

            try
            {
                client = myChannelFactory.CreateChannel();
                if (client.ValidateToken(token.TimeCreated.ToString()) == HttpStatusCode.Accepted)
                {
                    using (var context = new ApplicationContext())
                    {
                        return context.Products.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                if (client != null)
                {
                    ((ICommunicationObject)client).Abort();
                }
            }

            ((ICommunicationObject)client).Close();

            return null; // se chegou ate aqui, algo errado aconteceu
        }
    }
}
