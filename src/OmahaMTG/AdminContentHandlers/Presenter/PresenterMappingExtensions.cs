using System.Collections.Generic;
using OmahaMTG.Data;

namespace OmahaMTG.AdminContentHandlers.Presenter
{
    internal static class PresenterMappingExtensions
    {
        internal static PresenterData ToPresenterData(this Create.Command createPresenterRequest)
        {
            return new PresenterData()
            {
                Bio = createPresenterRequest.Bio,
                Name = createPresenterRequest.Name,
                OmahaMtgUserId = createPresenterRequest.OmahaMtgUserId,
            };
        }

        internal static Model ToPresenter(this PresenterData presenterData)
        {
            return new Model()
            {
                Id = presenterData.Id,
                Bio = presenterData.Bio,
                Name = presenterData.Name,
                OmahaMtgUserId = presenterData.OmahaMtgUserId,

                IsDeleted = presenterData.IsDeleted
            };
        }

        internal static void ApplyUpdatePresenterRequestToPresenterData(this PresenterData presenterDataToUpdate, Update.Command updatePresenterRequest)
        {
            presenterDataToUpdate.Bio = updatePresenterRequest.Bio;
            presenterDataToUpdate.Name = updatePresenterRequest.Name;
            presenterDataToUpdate.OmahaMtgUserId = updatePresenterRequest.OmahaMtgUserId;
        }

        //internal static IEnumerable<PresenterModel> ToPresenters(this IEnumerable<PresenterData> presenterDatas)
        //{
        //    return presenterDatas.Select(u => u.ToPresenter());
        //}
    }
}