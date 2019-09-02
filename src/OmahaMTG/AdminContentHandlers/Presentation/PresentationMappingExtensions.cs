using System.Collections.Generic;
using System.Linq;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Presentation
{
    internal static class PresentationMappingExtensions
    {
        internal static PresentationData ToPresentationData(this Create.Command createPresentationRequest)
        {
            return new PresentationData()
            {

                Title = createPresentationRequest.Title,
                Details = createPresentationRequest.Details,
                PresentationPresenters = createPresentationRequest.PresenterIds.Select(s => new PresentationPresenterData() { PresenterId = s }).ToList(),
            };
        }

        internal static Model ToPresentation(this PresentationData presentationData)
        {
            return new Model()
            {
                Id = presentationData.Id,
                Details = presentationData.Details,
                Title = presentationData.Title,
                PresenterIds = presentationData.PresentationPresenters?.Select(pp => pp.PresenterId)

            };
        }

        //internal static IEnumerable<Model> ToPresentations(this IEnumerable<PresentationData> presentationDatas)
        //{
        //    return presentationDatas.Select(u => u.ToPresentation());
        //}

        //internal static IEnumerable<PresentationData> ToPresentationsDatas(this IEnumerable<CreateUpdatePresentationRequest> presentationDatas)
        //{
        //    return presentationDatas.Select(u => u.ToPresentationData());
        //}

        internal static void ApplyUpdatePresentationRequestToPresentationData(this PresentationData presentationDataToUpdate, Update.Command updatePresentationRequest)
        {
            presentationDataToUpdate.Details = updatePresentationRequest.Details;
            presentationDataToUpdate.Title = updatePresentationRequest.Title;
            presentationDataToUpdate.PresentationPresenters = updatePresentationRequest.PresenterIds.Select(s => new PresentationPresenterData() { PresenterId = s }).ToList();

        }
    }
}