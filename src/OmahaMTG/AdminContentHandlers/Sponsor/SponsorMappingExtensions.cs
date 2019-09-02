using System.Collections.Generic;
using System.Linq;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Sponsor
{
    internal static class SqlSponsorMappingExtensions
    {
        internal static SponsorData ToSponsorData(this Create.Command createSponsorRequest)
        {
            return new SponsorData()
            {
                Name = createSponsorRequest.Name,
                Blurb = createSponsorRequest.Blurb,
                ContactInfo = createSponsorRequest.ContactInfo,
                Url = createSponsorRequest.Url,
                ShortBlurb = createSponsorRequest.ShortBlurb
            };
        }

        internal static Model ToSponsor(this SponsorData sponsorData)
        {
            return new Model()
            {
                Id = sponsorData.Id,
                Name = sponsorData.Name,
                Blurb = sponsorData.Blurb,
                ContactInfo = sponsorData.ContactInfo,
                Url = sponsorData.Url,
                ShortBlurb = sponsorData.ShortBlurb,


                IsDeleted = sponsorData.IsDeleted
            };
        }

        internal static void ApplyUpdateSponsorRequestToSponsorData(this SponsorData sponsorDataToUpdate, Update.Command updateSponsorRequest)
        {
            sponsorDataToUpdate.Name = updateSponsorRequest.Name;
            sponsorDataToUpdate.Blurb = updateSponsorRequest.Blurb;
            sponsorDataToUpdate.ContactInfo = updateSponsorRequest.ContactInfo;
            sponsorDataToUpdate.Url = updateSponsorRequest.Url;
            sponsorDataToUpdate.ShortBlurb = updateSponsorRequest.ShortBlurb;
        }

        //internal static IEnumerable<SponsorModel> ToSponsors(this IEnumerable<SponsorData> sponsorDatas)
        //{
        //    return sponsorDatas.Select(u => u.ToSponsor());
        //}

    }
}