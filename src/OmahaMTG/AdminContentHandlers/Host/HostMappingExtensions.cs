using System.Collections.Generic;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Host
{
    internal static class HostMappingExtensions
    {
        internal static HostData ToHostData(this Create.Command createHostRequest)
        {
            return new HostData()
            {
                Name = createHostRequest.Name,
                Blurb = createHostRequest.Blurb,
                ContactInfo = createHostRequest.ContactInfo,
                Address = createHostRequest.Address
            };
        }

        internal static Model ToHost(this HostData hostData)
        {
            return new Model()
            {
                Id = hostData.Id,
                Name = hostData.Name,
                Blurb = hostData.Blurb,
                ContactInfo = hostData.ContactInfo,
                Address = hostData.Address,
                IsDeleted = hostData.IsDeleted
            };
        }

        internal static void ApplyUpdateHostRequestToHostData(this HostData hostDataToUpdate, Update.Command updateHostRequest)
        {
            hostDataToUpdate.Name = updateHostRequest.Name;
            hostDataToUpdate.Blurb = updateHostRequest.Blurb;
            hostDataToUpdate.ContactInfo = updateHostRequest.ContactInfo;
            hostDataToUpdate.Address = updateHostRequest.Address;

        }

        //internal static IEnumerable<HostModel> ToHosts(this IEnumerable<HostData> hostDatas)
        //{
        //    return hostDatas.Select(u => u.ToHost());
        //}

    }
}