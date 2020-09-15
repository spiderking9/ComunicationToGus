using _3333333333333.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace _3333333333333
{
    public class ComunicationToGus
    {

        public string Connect(string nips)
        {
            WSHttpBinding myBinding = new WSHttpBinding();
            myBinding.Security.Mode = SecurityMode.Transport;
            myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            myBinding.MessageEncoding = WSMessageEncoding.Mtom;


            EndpointAddress ea = new EndpointAddress("https://wyszukiwarkaregontest.stat.gov.pl/wsBIR/UslugaBIRzewnPubl.svc");
            UslugaBIRzewnPublClient service = new UslugaBIRzewnPublClient(myBinding, ea);
            service.Open();

            string sid = service.Zaloguj("abcde12345abcde12345");

            new OperationContextScope(service.InnerChannel);

            HttpRequestMessageProperty reqProps = new HttpRequestMessageProperty();
            reqProps.Headers.Add("sid", sid);

            OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = reqProps;
            ParametryWyszukiwania parametryWyszukiwania = new ParametryWyszukiwania();
            parametryWyszukiwania.Nipy = nips;
            
            return service.DaneSzukajPodmioty(parametryWyszukiwania);
        }
    }
}
